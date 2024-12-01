using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabbable : MonoBehaviour
{
    [SerializeField] Vector3 grabbedPosition;
    [SerializeField] Vector3 grabbedRotation;
    [SerializeField] Vector3 grabbedScale = Vector3.one;
    [SerializeField] protected Interactable interactable;
    [SerializeField] GameObject model;

    public Vector3 GrabbedPosition => grabbedPosition;
    public Vector3 GrabbedRotation => grabbedRotation;

    private void Awake()
    {
        interactable.OnInteract += OnInteract;
        interactable.OnShowTooltip += UpdateInteractState;
    }

    private void OnEnable()
    {
        Physics.Raycast(transform.position, -transform.up, out RaycastHit hit, 100, LayerMask.GetMask("Default"));
        if (hit.collider != null)
        {
            transform.position = hit.point;
        }
    }

    protected virtual void UpdateInteractState()
    {

    }

    protected virtual void OnInteract()
    {
        Character.Instance.Grab(this);
    }

    public void Grab(Transform hand, bool useOffset = true)
    {
        interactable.gameObject.SetActive(false);
        transform.parent = hand;
        transform.localPosition = useOffset ? grabbedPosition : Vector3.zero;
        transform.localEulerAngles = useOffset ? grabbedRotation : Vector3.zero;
        transform.localScale = useOffset ? grabbedScale : Vector3.one;

        //Set UI layer to see it always in overlay
        model.layer = 5;
        foreach (Transform child in model.transform)
        {
            child.gameObject.layer = 5;
        }
    }

    public void Release()
    {
        interactable.gameObject.SetActive(true);
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
        //Set UI layer to see it always in overlay
        model.layer = 0;
        foreach (Transform child in model.transform)
        {
            child.gameObject.layer = 0;
        }
    }
}
