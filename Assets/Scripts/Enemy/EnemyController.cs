using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{   
    private EnemyState currState;

    public Transform[] targetPoints;
    public Transform enemyEye;
    public float playerCheckDistance;
    public float checkRadius = 0.8f;

    public NavMeshAgent agent;

    [HideInInspector] public Transform player;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        currState = new EnemyIdleState(this);
        currState.OnStateEnter();
    }

    // Update is called once per frame
    void Update()
    {
        currState.OnStateUpdate();
    }

    public void ChangeState(EnemyState state) {
        currState.OnStateExit();
        currState = state;
        currState.OnStateEnter();
    }

    /*
    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(enemyEye.position, checkRadius);
        Gizmos.DrawWireSphere(enemyEye.position + enemyEye.forward * playerCheckDistance, checkRadius);

        Gizmos.DrawLine(enemyEye.position, enemyEye.position + enemyEye.forward * playerCheckDistance);
    }
    */
}
