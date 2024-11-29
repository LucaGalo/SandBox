using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Dialog", menuName = "Sandbox/Dialogs", order = 1)]
public class DialogSO : ScriptableObject
{
    public DialogState state;
    public DialogState nextState;

    [TextArea]
    public string[] messages;
}
