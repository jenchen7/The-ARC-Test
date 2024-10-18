using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : EnemyState
{   
    int currentTarget = 0;

    public EnemyIdleState(EnemyController enemy) : base(enemy) {

    }
    public override void OnStateEnter() {
        _enemy.agent.destination = _enemy.targetPoints[currentTarget].position;
    }

    public override void OnStateUpdate() {
        if(_enemy.agent.remainingDistance < 0.1f) {
            currentTarget++;
            if(currentTarget >= _enemy.targetPoints.Length)
                currentTarget = 0;
            _enemy.agent.destination = _enemy.targetPoints[currentTarget].position;
        }

        // check for player
        if(Physics.SphereCast(_enemy.enemyEye.position, _enemy.checkRadius, _enemy.transform.forward, out RaycastHit hit, _enemy.playerCheckDistance)) {
            if(hit.transform.CompareTag("Player")) {
                // Debug.Log("Player found");

                _enemy.player = hit.transform;
                _enemy.agent.destination = _enemy.player.position;

                // move to new state
                // move to the follow state
                _enemy.ChangeState(new EnemyFollowState(_enemy));
            }
        }
    }

    public override void OnStateExit() {
        // Debug.Log("Enemy stopped Idling");
    }
}
