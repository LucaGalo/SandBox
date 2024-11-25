using System;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    [SerializeField] GameObject tooltip;

    public event Action OnInteract;

    private void Awake()
    {
        tooltip.SetActive(false);
    }

    public void ShowTooltip()
    {
        tooltip.SetActive(true);
    }

    public void HideTooltip()
    {
        tooltip.SetActive(false);
    }

    public void Interact()
    {
        OnInteract?.Invoke();
    }
}
