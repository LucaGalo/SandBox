using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : Singleton<Character>
{
    Rigidbody _rigidBody;
    Interactable _interactable;
    Interactable _talkable;
    float vRot;
    bool inputsEnabled;
    float moveSpeed;

    public Grabbable ObjectInHand { get; private set; }

    [SerializeField] float normalSpeed;
    [SerializeField] float runSpeed;
    [SerializeField] float rotSpeed;
    [SerializeField] Transform hand;
    [SerializeField] Transform head;
    [SerializeField] PauseUI pauseUI;

    private void Start()
    {
        _rigidBody = GetComponent<Rigidbody>();

        inputsEnabled = false;
        DialogUI.Instance.OnDialogStart += () => inputsEnabled = false;
        DialogUI.Instance.OnDialogEnd += () => inputsEnabled = true;
        GameManager.Instance.OnStartGame += () => SetPause(false);
    }

    private void FixedUpdate()
    {
        if (!inputsEnabled) return;

        UpdatePosition();
    }

    void UpdatePosition()
    {
        moveSpeed = Input.GetKey(KeyCode.LeftShift) ? runSpeed : normalSpeed;
        var dir = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        dir.Normalize();
        dir *= moveSpeed * Time.fixedDeltaTime;
        dir = transform.TransformVector(dir);
        _rigidBody.velocity = dir;
    }
    
    private void Update()
    {
        if (!GameManager.Instance.IsGameStarted) return;

        if (!inputsEnabled) return;

        CheckPause();
        UpdateRotation();
        CheckInputs();
    }

    void CheckPause()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SetPause(true);
        }
    }

    public void SetPause(bool isPaused)
    {
        inputsEnabled = !isPaused;
        Cursor.lockState = !isPaused ? CursorLockMode.Locked : CursorLockMode.None;
        Cursor.visible = isPaused;
        pauseUI.SetVisible(isPaused);
    }

    void UpdateRotation()
    {
        transform.Rotate(Vector3.up * Input.GetAxis("Mouse X") * rotSpeed * Time.deltaTime); 
        vRot -= Input.GetAxis("Mouse Y") * rotSpeed * Time.deltaTime;
        vRot = Mathf.Clamp(vRot, -60, 60);
        head.localRotation = Quaternion.Euler(Vector3.right * vRot);
    }

    void CheckInteractables()
    {
        Physics.SphereCast(head.position, .25f, head.forward, out RaycastHit hit, 2, LayerMask.GetMask("Interactable"));
        if (hit.collider != null)
        {
            var newInteractable = hit.collider.gameObject.GetComponentInParent<Interactable>();

            if (newInteractable == _interactable)
                return;

            if (_interactable != null)
                _interactable.HideTooltip();

            _interactable = newInteractable;
            _interactable.ShowTooltip();
        }
        else if (_interactable != null)
        {
            _interactable.HideTooltip();
            _interactable = null;
        }
    }

    void CheckTalkables()
    {
        Physics.SphereCast(head.position, .25f, head.forward, out RaycastHit hit, 2, LayerMask.GetMask("Talkable"));
        if (hit.collider != null)
        {
            var newTalkable = hit.collider.gameObject.GetComponentInParent<Interactable>();

            if (newTalkable == _talkable)
                return;

            if (_talkable != null)
                _talkable.HideTooltip();

            _talkable = newTalkable;
            _talkable.ShowTooltip();
        }
        else if (_talkable != null)
        {
            _talkable.HideTooltip();
            _talkable = null;
        }
    }

    void CheckInputs()
    {
        CheckTalk();
        CheckInteraction();
    }

    void CheckTalk()
    {
        CheckTalkables();
        //Press F to talk
        if (Input.GetKeyDown(KeyCode.F) && _talkable != null)
        {
            _talkable.Interact();
        }
    }

    void CheckInteraction()
    {
        CheckInteractables();
        //Press E to Interact
        if (Input.GetKeyDown(KeyCode.E) && _interactable != null)
        {
            _interactable.Interact();
        }

        //Press X to Release
        if (Input.GetKeyDown(KeyCode.X))
        {
            Release();
        }
    }

    public void Grab(Grabbable grabbedObject)
    {
        Release();

        ObjectInHand = grabbedObject;
        ObjectInHand.Grab(hand);
    }

    public void Release()
    {
        if (ObjectInHand != null)
        {
            ObjectInHand.Release();
            ObjectInHand = null;
        }
    }
}
