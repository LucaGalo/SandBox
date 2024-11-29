using Febucci.UI;
using Febucci.UI.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogUI : Singleton<DialogUI>
{
    public event Action OnDialogStart;
    public event Action OnDialogEnd;

    DialogSO currentDialog;
    int index;

    [SerializeField] TypewriterCore typewriter;

    private void Start()
    {
        gameObject.SetActive(false);
    }

    public void StartDialog(DialogSO dialog)
    {
        gameObject.SetActive(true);
        currentDialog = dialog;
        index = 0;
        ShowMessage();
        OnDialogStart?.Invoke();
    }

    void ShowMessage()
    {
        string text = currentDialog.messages[index];
        typewriter.ShowText(text);
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0) && !typewriter.isShowingText)
        {
            index++;
            if(index == currentDialog.messages.Length)
            {
                OnDialogEnd?.Invoke();
                gameObject.SetActive(false);
                return;
            }
            ShowMessage();
        }
    }


}
