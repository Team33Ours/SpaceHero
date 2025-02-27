using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpener : MonoBehaviour
{
    public GameObject openDoor;
    public GameObject unlockDoor;
    public GameObject nextStageCollider;
    public void UnlockDoor()
    {
        unlockDoor.SetActive(true);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            openDoor.SetActive(false);
            nextStageCollider.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            openDoor.SetActive(true);
            nextStageCollider.SetActive(false);
        }
    }
}
