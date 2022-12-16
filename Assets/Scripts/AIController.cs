using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI; 

public enum AIStates
{
    wander,
    scared,
    recover
}
public class AIController : MonoBehaviour
{
    NavMeshAgent agent;
    public AIStates state;

    public float wanderSpeed = 5;
    public float runawaySpeed = 10;

    public Transform[] waypoints;
    int waypointIndex;

    Transform playerTransform;
    Ray ray;
    RaycastHit hit;
    public float scareDistance = 5f;
    public LayerMask lineOfSightLayer;
    public float recoverWaitTime = 2f;
    float recoverTimer = 0f;
    bool onRecoverPath = false;
    private void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        state = AIStates.wander;
        waypointIndex = -1;
    }

    private void Update()
    {
        if (CanSeePlayer())
        {
            agent.ResetPath();
            state = AIStates.scared;
        }
        switch (state)
        {
            case AIStates.wander:
                WanderUpdate();
                break;
            case AIStates.scared:
                ScaredUpdate();
                break;
            case AIStates.recover:
                RecoverUpdate();
                break;
        }
    }

    void WanderUpdate()
    {
        if (!agent.hasPath)
        {
            agent.speed = wanderSpeed;
            waypointIndex++;
            if (waypointIndex >= waypoints.Length)
                waypointIndex = 0;
            agent.SetDestination(waypoints[waypointIndex].position);
        }
    }

    void ScaredUpdate()
    {
        agent.speed = runawaySpeed;
        if (!CanSeePlayer())
        {
            recoverTimer += Time.deltaTime;
            if (recoverTimer >= recoverWaitTime)
            {
                recoverTimer = 0;
                onRecoverPath = false;
                agent.ResetPath();
                state = AIStates.recover;
                return;
            }
        }
            
        if (!agent.hasPath)
        {
            Vector3 newPos = GetNewEscapePosition();
            agent.SetDestination(newPos);
        }
    }

    Vector3 GetNewEscapePosition()
    {
        Vector3 dirToPlayer = transform.position - playerTransform.position;
        Vector3 newPos = transform.position + dirToPlayer;
        return newPos;
    }

    bool CanSeePlayer()
    {
        if (Physics.Raycast(transform.position, playerTransform.position - transform.position,  out hit, scareDistance, lineOfSightLayer))
        {
            if (hit.collider.tag != "Player")
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        else
        {
            return false;
        }
    }

    void RecoverUpdate()
    {
        agent.speed = wanderSpeed;
        if (!onRecoverPath)
        {
            onRecoverPath = true;
            agent.SetDestination(waypoints[GetNearestWaypointIndex()].position);
        }

        if (!agent.hasPath)
        {
            state = AIStates.wander;
        }
    }

    int GetNearestWaypointIndex()
    {
        float distance = float.MaxValue;
        int index = 0;

        for (int i = 0; i < waypoints.Length; i++)
        {
            float thisDist = Vector3.Distance(transform.position, waypoints[i].position);
            if (thisDist < distance)
            {
                distance = thisDist;
                index = i;
            }
        }

        return index;
    }
}
