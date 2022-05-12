using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AgentEnemy : MonoBehaviour {
    public float playerDetectionDistance = 50;
    NavMeshAgent agent;
    Vector3 targetPosition;
    AgentState state;
    Transform player;
    Vector3 initPosition;


    void Start() {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        state = AgentState.Idle;
        initPosition = transform.position;
    }

    void Update() {
        float distanceToPlayer = Vector3.Distance(player.position, transform.position);
        if (distanceToPlayer < playerDetectionDistance) {
            RaycastHit hitInfo;
            Vector3 directionToTarget = (player.position - transform.position).normalized;
            
            Debug.DrawRay(transform.position + Vector3.up, directionToTarget, Color.green);
            if (Physics.Raycast(transform.position + Vector3.up, directionToTarget, out hitInfo, distanceToPlayer, -1, QueryTriggerInteraction.Ignore)) {
                targetPosition = initPosition;
                
            } else {
                targetPosition = player.position;
                Debug.Log("Perseguindo");
            }
        } else {
            targetPosition = initPosition;
        }
  
        agent.destination = targetPosition;
    }

}

public enum AgentState {
    Idle, // Esperando
    Chasing // Persiguiendo
}
