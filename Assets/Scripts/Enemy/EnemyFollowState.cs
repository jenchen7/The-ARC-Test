using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollowState : EnemyState
{   
    float distanceToPlayer;

    public EnemyFollowState(EnemyController enemy) : base(enemy) {

    }

    public override void OnStateEnter() {
        // Debug.Log("Enemy starting to follow the player");
    }

    public override void OnStateUpdate() {
        if(_enemy.player != null) {
            distanceToPlayer = Vector3.Distance(_enemy.transform.position, _enemy.player.position);

            if(distanceToPlayer > 10) {
                // go to the Idle state
                _enemy.ChangeState(new EnemyIdleState(_enemy));
            }

            // set attack
            if(distanceToPlayer < 2) {
                // go to the Attack state
                _enemy.ChangeState(new EnemyAttackState(_enemy));
            }

            _enemy.agent.destination = _enemy.player.position;
        }
        else {
            // go to the Idle state
            _enemy.ChangeState(new EnemyIdleState(_enemy));
        }
    }

    public override void OnStateExit() {
        // Debug.Log("Enemy stopped following player");
    }
}
