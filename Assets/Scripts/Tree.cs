using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{
    public Transform teleportTarget;
    public GameObject playerObj;
    public CharacterController controller;
    private void Start()
    {
        playerObj = GameObject.FindGameObjectWithTag("Player");
        controller = playerObj.GetComponent<CharacterController>();
    }

    public void OnInteract()
    {
        Debug.Log("playerTryingToClimbTree");
        controller.enabled = false;
        playerObj.transform.position = teleportTarget.position;
        controller.enabled = true;
    }
}
