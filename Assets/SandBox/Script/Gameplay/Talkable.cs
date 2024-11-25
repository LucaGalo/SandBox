using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Talkable : MonoBehaviour
{
    [SerializeField] GameObject tooltip;

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
        Destroy(transform.root.gameObject);
    }
}
