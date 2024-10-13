using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    CharacterController _characterController;
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
        var dir = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        dir.Normalize();
        dir *= moveSpeed * Time.deltaTime;
        dir = transform.TransformVector(dir);
        _characterController.SimpleMove(dir);
        transform.Rotate(Vector3.up * Input.GetAxis("Mouse X") * rotSpeed);
        Camera.main.transform.Rotate(Vector3.right * -Input.GetAxis("Mouse Y") * rotSpeed);
    }
}
