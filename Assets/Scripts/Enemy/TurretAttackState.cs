using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretAttackState : TurretState
{
    float distanceToPlayer;
    Health playerHealth;
    float turretDPS = 5f;
    LineRenderer lineRenderer;

    public TurretAttackState(TurretController turret) : base(turret) {
        playerHealth = turret.player.GetComponent<Health>();
    }

    public override void OnStateEnter() {
        // Debug.Log("Turret started attacking the player");

        // draw laser
        lineRenderer = _turret.laserLineRenderer;
        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, _turret.enemyEye.position);
        lineRenderer.SetPosition(1, _turret.transform.forward * _turret.turretRange + _turret.enemyEye.position);
    }

    public override void OnStateUpdate() {
        DrawLaser();
        Attack();
        
        if(_turret.player != null) {
            distanceToPlayer = Vector3.Distance(_turret.transform.position, _turret.player.position);
            if (distanceToPlayer > _turret.turretRange) { // turret turns off if the player is too far
                _turret.ChangeState(new TurretIdleState(_turret));
            }
        }
        else {
            // go to the Idle state
            _turret.ChangeState(new TurretIdleState(_turret));
        }
    }

    public override void OnStateExit() {
        // Debug.Log("Turret stopped attacking the player");
        lineRenderer.positionCount = 0; // stop drawing the laser
    }

    void Attack() {
        if(playerHealth != null) {
            if(Physics.Raycast(_turret.enemyEye.position, _turret.enemyEye.forward, out RaycastHit hit, _turret.turretRange)) {
                if(hit.transform.CompareTag("Player")) {
                    playerHealth.DeductHealth(turretDPS * Time.deltaTime);
                }
            }
        }
    }

    // draws path between enemy and target (player)
    private void DrawLaser(){
        if(Physics.Raycast(_turret.enemyEye.position, _turret.enemyEye.forward, out RaycastHit hit, _turret.turretRange)) {
            lineRenderer.SetPosition(1, hit.point);
        }
        else {
            lineRenderer.SetPosition(1, _turret.transform.forward * _turret.turretRange + _turret.enemyEye.position);
        }
    }
}
