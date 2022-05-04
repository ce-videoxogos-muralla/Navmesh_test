using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AgentEnemy : MonoBehaviour
{
    public float playerDetectionDistance = 30;
    NavMeshAgent agent;
    Vector3 targetPosition;
    AgentState state;
    Transform player;
    Vector3 initPosition;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        state = AgentState.Idle;
        initPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(player.position,transform.position) < playerDetectionDistance) {
            RaycastHit hitInfo;
            Vector3 directionToTarget = (player.position - transform.position).normalized;
            if (Physics.Raycast(transform.position + Vector3.up, directionToTarget, playerDetectionDistance, -1, QueryTriggerInteraction.Ignore)) {
                targetPosition = initPosition;
            }
            targetPosition = player.position;
        } else {
            targetPosition = initPosition;
        }
         
        agent.destination = targetPosition;
    }
}

public enum AgentState {
    Idle, // Esperando
    Chasing, // Perseguindo

}