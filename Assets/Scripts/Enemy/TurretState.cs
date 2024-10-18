using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TurretState
{
    protected TurretController _turret;

    public TurretState(TurretController _turret) {
        this._turret = _turret;
    }

    public abstract void OnStateEnter();

    public abstract void OnStateUpdate();

    public abstract void OnStateExit();
    
}
