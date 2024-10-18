using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : EnemyState
{   
    float distanceToPlayer;
    Health playerHealth;
    float enemyDPS = 20f;

    public EnemyAttackState(EnemyController enemy) : base(enemy) {
        playerHealth = enemy.player.GetComponent<Health>();
    }
    
    public override void OnStateEnter() {
        //Debug.Log("Enemy starting to attack the player");
    }

    public override void OnStateUpdate() {
        Attack();
        
        if(_enemy.player != null) {
            distanceToPlayer = Vector3.Distance(_enemy.transform.position, _enemy.player.position);

            if(distanceToPlayer > 2) {
                // go to the Idle state
                _enemy.ChangeState(new EnemyFollowState(_enemy));
            }

            _enemy.agent.destination = _enemy.player.position;
        }
        else {
            // go to the Idle state
            _enemy.ChangeState(new EnemyIdleState(_enemy));
        }
    }

    public override void OnStateExit() {
        //Debug.Log("Enemy stopped attacking the player");
    }

    void Attack() {
        if(playerHealth != null) {
            playerHealth.DeductHealth(enemyDPS * Time.deltaTime);
        }
    }
}
