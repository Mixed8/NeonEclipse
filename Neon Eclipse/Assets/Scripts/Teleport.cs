using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public Transform teleportTarget;
    public GameObject ThePlayer;

    void OnTriggerEnter(Collider other)
    {
        ThePlayer.transform.position = teleportTarget.transform.position;
    }

}
