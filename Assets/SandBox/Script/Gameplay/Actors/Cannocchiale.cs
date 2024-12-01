using DG.Tweening;
using UnityEngine;

public class Cannocchiale : Usable
{
    [SerializeField] GameObject mainCamera;
    [SerializeField] GameObject cannocchialeCamera;

    [SerializeField] Vector3 endPos;
    [SerializeField] Vector3 endRot;


    protected override bool CheckUsability()
    {
        return true;
    }

    protected override void StartUse()
    {
        DOTween.KillAll();
        var sequence = DOTween.Sequence();
        sequence.Append(transform.DOLocalMove(endPos, .3f))
            .Join(transform.DOLocalRotate(endRot, .3f))
            .AppendCallback(() =>
            {
                mainCamera.SetActive(false);
                cannocchialeCamera.SetActive(true);
            });
    }

    protected override void EndUse()
    {
        mainCamera.SetActive(true);
        cannocchialeCamera.SetActive(false);
        DOTween.KillAll();
        var sequence = DOTween.Sequence();
        sequence.Append(transform.DOLocalMove(grabbable.GrabbedPosition, .3f))
            .Join(transform.DOLocalRotate(grabbable.GrabbedRotation, .3f));
    }
}

public abstract class Usable : MonoBehaviour
{
    bool isUsed;

    protected Grabbable grabbable;

    private void Awake()
    {
        grabbable = GetComponent<Grabbable>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1) && CheckUsability() && grabbable.IsGrabbed)
        {
            isUsed = true;
            StartUse();
        }

        if (Input.GetMouseButtonUp(1) && isUsed)
        {
            isUsed = false;
            EndUse();
        }
    }

    protected abstract void StartUse();
    protected abstract void EndUse();
    protected abstract bool CheckUsability();
}
