using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : Singleton<Character>
{
    CharacterController _characterController;
    Interactable _interactable;
    Talkable _talkable;
    public Grabbable ObjectInHand { get; private set; }

    [SerializeField] float moveSpeed;
    [SerializeField] float rotSpeed;
    [SerializeField] Transform hand;

    private void Start()
    {
        _characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        UpdatePosition();
        UpdateRotation();
        CheckInputs();
    }

    void UpdatePosition()
    {
        var dir = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        dir.Normalize();
        dir *= moveSpeed * Time.deltaTime;
        dir = transform.TransformVector(dir);
        _characterController.SimpleMove(dir);
    }

    void UpdateRotation()
    {
        transform.Rotate(Vector3.up * Input.GetAxis("Mouse X") * rotSpeed);
        Camera.main.transform.Rotate(Vector3.right * -Input.GetAxis("Mouse Y") * rotSpeed);
    }

    void CheckInteractables()
    {
        Physics.SphereCast(Camera.main.transform.position, .25f, Camera.main.transform.forward, out RaycastHit hit, 2, LayerMask.GetMask("Interactable"));
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
        Physics.SphereCast(Camera.main.transform.position, .25f, Camera.main.transform.forward, out RaycastHit hit, 2, LayerMask.GetMask("Talkable"));
        if (hit.collider != null)
        {
            var newTalkable = hit.collider.gameObject.GetComponentInParent<Talkable>();

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
        if (Input.GetKeyDown(KeyCode.F))
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
