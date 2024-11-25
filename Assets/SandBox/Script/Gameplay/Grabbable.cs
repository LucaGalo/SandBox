using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabbable : MonoBehaviour
{
    [SerializeField] bool isCollectable;
    [SerializeField] Vector3 grabbedPosition;
    [SerializeField] Vector3 grabbedRotation;
    [SerializeField] Vector3 grabbedScale = Vector3.one;

    public bool IsCollectable => isCollectable;

    Interactable _interactable;

    private void Awake()
    {
        _interactable = GetComponentInChildren<Interactable>();
        _interactable.OnInteract += OnInteract;


        Physics.Raycast(transform.position, -transform.up, out RaycastHit hit, 100, LayerMask.GetMask("Default"));
        if (hit.collider != null)
        {
            transform.position = hit.point;
        }
    }

    void OnInteract()
    {
        Character.Instance.Grab(this);
    }

    public void Grab(Transform hand)
    {
        _interactable.gameObject.SetActive(false);
        transform.parent = hand;
        transform.localPosition = grabbedPosition;
        transform.localEulerAngles = grabbedRotation;
        transform.localScale = grabbedScale;
    }

    public void Release()
    {
        _interactable.gameObject.SetActive(true);
        Vector3 camForward = Camera.main.transform.forward;
        Vector3 dropPos = transform.position + new Vector3(camForward.x, 0, camForward.z).normalized;
        Physics.Raycast(dropPos, -transform.up, out RaycastHit hit, 100, LayerMask.GetMask("Default"));
        if(hit.collider != null)
        {
            transform.parent = null;
            transform.position = hit.point;
            transform.rotation = Quaternion.identity;
            transform.localScale = Vector3.one;
        }
    }
}
