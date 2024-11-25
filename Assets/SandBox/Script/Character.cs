using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    CharacterController _characterController;
    Interactable _interactable;

    [SerializeField] float moveSpeed;
    [SerializeField] float rotSpeed;


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
    }
}
