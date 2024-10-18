using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretIdleState : TurretState
{
    public TurretIdleState(TurretController turret) : base(turret) {

    }

    public override void OnStateEnter() {
        // Debug.Log("Turret started Idling");
    }

    public override void OnStateUpdate() {
        // check for player
        if(Physics.Raycast(_turret.enemyEye.position, _turret.transform.forward, out RaycastHit hit, _turret.turretRange)) {
            if(hit.transform.CompareTag("Player")) {
                // Debug.Log("Player found");

                _turret.player = hit.transform;

                // move to attack state
                _turret.ChangeState(new TurretAttackState(_turret));
            }
        }
    }

    public override void OnStateExit() {
        // Debug.Log("Turret stopped Idling");
    }
}
