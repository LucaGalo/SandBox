using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Interactable : MonoBehaviour
{
    [System.Serializable]
    class InteractableGraphics
    {
        public Sprite sprite;
        public string label;
        public Color labelColor;
    }

    public enum InteractableState
    {
        DEFAULT,
        DISABLED
    }

    [SerializeField] GameObject tooltip;
    [SerializeField] Image buttonToPress;
    [SerializeField] TMP_Text label;

    [Header("states")]
    [SerializeField] InteractableGraphics defaultState;
    [SerializeField] InteractableGraphics disabledState;

    public event Action OnInteract;
    public event Action OnShowTooltip;

    InteractableState _currentState;

    private void Awake()
    {
        tooltip.SetActive(false);
        SetState(InteractableState.DEFAULT);
    }

    public void SetState(InteractableState state)
    {
        _currentState = state;
        var graphics = _currentState switch
        {
            InteractableState.DEFAULT => defaultState,
            InteractableState.DISABLED => disabledState
        };

        UpdateGraphics(graphics);
    }

    void UpdateGraphics(InteractableGraphics graphics)
    {
        buttonToPress.sprite = graphics.sprite;
        label.text = graphics.label;
        label.color = graphics.labelColor;
    }

    public void ShowTooltip()
    {
        OnShowTooltip?.Invoke();
        tooltip.SetActive(true);
    }

    public void HideTooltip()
    {
        tooltip.SetActive(false);
    }

    public void Interact()
    {
        switch (_currentState)
        {
            case InteractableState.DEFAULT:
                OnInteract?.Invoke();
                break;
            case InteractableState.DISABLED:
                break;
        }
    }
}
