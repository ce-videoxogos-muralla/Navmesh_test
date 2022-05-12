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
        bool inRangeAndVisible = IsInRangeAndVisible();

  
        switch (state) {
            case AgentState.Idle:
                if (inRangeAndVisible)
                    SetState(AgentState.Chasing);
                break;
            case AgentState.Chasing:
                if (inRangeAndVisible)
                    targetPosition = player.position;
                else
                    SetState(AgentState.Returning);
                break;
            case AgentState.Returning:
                if (inRangeAndVisible)
                    SetState(AgentState.Chasing);
                else if (agent.isStopped)
                    SetState(AgentState.Idle);
                break;
        }

        agent.destination = targetPosition;
    }

    bool IsInRangeAndVisible() {
        float distanceToPlayer = Vector3.Distance(player.position, transform.position);
        if (distanceToPlayer < playerDetectionDistance) {
            RaycastHit hitInfo;
            Vector3 directionToTarget = (player.position - transform.position).normalized;
            
            Debug.DrawRay(transform.position + Vector3.up, directionToTarget, Color.green);
            return Physics.Raycast(transform.position + Vector3.up, directionToTarget, out hitInfo, distanceToPlayer, -1, QueryTriggerInteraction.Ignore);
        } 
        return false;
    }

    void SetState(AgentState newState) {
        if (newState != state) {
            state = newState;
            switch (newState) {
                case AgentState.Idle:
                    break;
                case AgentState.Chasing:
                    break;
                case AgentState.Returning:
                    targetPosition = initPosition;
                    break;
            }
        }
    }

}

public enum AgentState {
    Idle,       // Esperando
    Chasing,    // Persiguiendo
    Returning   // Volta ao punto inicial
}
