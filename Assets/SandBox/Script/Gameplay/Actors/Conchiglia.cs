using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conchiglia : Grabbable
{
    protected override void UpdateInteractState()
    {
        var state = Interactable.InteractableState.DEFAULT;
        if (Character.Instance.ObjectInHand is Secchiello secchiello && secchiello.IsFull)
            state = Interactable.InteractableState.DISABLED;

        interactable.SetState(state);
    }

    protected override void OnInteract()
    {
        if(Character.Instance.ObjectInHand is Secchiello secchiello)
            secchiello.Put(this);
        else
            base.OnInteract();
    }
}
