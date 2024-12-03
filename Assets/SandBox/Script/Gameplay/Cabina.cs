using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cabina : MonoBehaviour
{
    [SerializeField] Animation animator;
    [SerializeField] AnimationClip openDoorClip;
    [SerializeField] AnimationClip closeDoorClip;
    [SerializeField] AnimationClip spawnNPCClip;

    bool isOpen;

    private void Start()
    {
        GameManager.Instance.OnStartGame += OpenDoor;
    }

    private void OnTriggerExit(Collider other)
    {
        if (!isOpen) return;

        //if player or NPC
        if(other.gameObject.layer == 3 || other.gameObject.layer == 9)
            CloseDoor();
    }

    void OpenDoor()
    {
        isOpen = true;
        animator.clip = openDoorClip;
        animator.Play();
    }

    void CloseDoor()
    {
        animator.clip = closeDoorClip;
        animator.Play();
        isOpen = false;
    }

    void SpawnNPC()
    {
        animator.clip = spawnNPCClip;
        animator.Play();
    }
}
