using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretController : MonoBehaviour
{
    private TurretState currState;

    public Transform enemyEye;
    public float turretRange;
    public float checkRadius = 0.8f;

    [HideInInspector] public Transform player;
    [HideInInspector] public LineRenderer laserLineRenderer; 

    // Start is called before the first frame update
    void Start()
    {
        currState = new TurretIdleState(this);
        currState.OnStateEnter();

        laserLineRenderer = GetComponent<LineRenderer>();
        laserLineRenderer.widthMultiplier = 0.075f;
    }

    // Update is called once per frame
    void Update()
    {
        currState.OnStateUpdate();
    }

    public void ChangeState(TurretState state) {
        currState.OnStateExit();
        currState = state;
        currState.OnStateEnter();
    }

    
}
