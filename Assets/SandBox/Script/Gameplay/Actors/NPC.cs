using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    [SerializeField] protected Interactable interactable;

    DialogState currentDialogState;

    [SerializeField] DialogSO[] dialogs;

    void Awake()
    {
        interactable.OnInteract += OnInteract;
        currentDialogState = DialogState.INTRO;
    }

    void OnInteract()
    {
        foreach(var dialog in dialogs)
        {
            if (dialog.state == currentDialogState)
                DialogUI.Instance.StartDialog(dialog);
        }
    }
}
