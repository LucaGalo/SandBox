using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Secchiello : Grabbable
{
    [System.Serializable]
    class SecchielloState
    {
        public int maxCount;
        public GameObject graphic;
    }

    public bool IsFull => count >= states[states.Length - 1].maxCount;

    [SerializeField] SecchielloState[] states;

    List<Grabbable> grabbedObjects = new();
    int count;

    private void Start()
    {
        UpdateGraphic();
    }

    public void Put(Grabbable grabbable)
    {
        if(count < states[states.Length - 1].maxCount)
        {
            count++;
            grabbedObjects.Add(grabbable);
            grabbable.Grab(transform, false);
            grabbable.gameObject.SetActive(false);

            UpdateGraphic();
        }
    }

    public void Pick(Grabbable grabbable)
    {
        count--;
        grabbedObjects.Remove(grabbable);

        UpdateGraphic();
    }

    void UpdateGraphic()
    {
        //Hide all graphics by default
        foreach (var state in states)
            state.graphic.SetActive(false);

        for (int i = 0; i < states.Length; i++)
        {
            if (count <= states[i].maxCount)
            {
                states[i].graphic.SetActive(true);
                break;
            }
        }
    }
}
