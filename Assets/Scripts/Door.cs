using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private BoxCollider boxCollider;

    public void OpenDoor()
    {
        boxCollider.isTrigger = true;
    }
    public void CloseDoor()
    {
        boxCollider.isTrigger = false;
    }
}
