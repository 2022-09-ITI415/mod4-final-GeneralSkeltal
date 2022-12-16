using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{
    public Transform teleportTarget;
    public GameObject playerObj;

    private void Start()
    {
        playerObj = GameObject.FindGameObjectWithTag("Player");
    }

    public void OnInteract()
    {
        Debug.Log("playerTryingToClimbTree");
        playerObj.transform.position = teleportTarget.position;
    }
}
