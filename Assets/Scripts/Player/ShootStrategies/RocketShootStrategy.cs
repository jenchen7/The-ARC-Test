using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketShootStrategy : IShootStrategy
{   
    ShootInteractor interactor;
    Transform shootPoint;

    public RocketShootStrategy(ShootInteractor _interactor) {
        Debug.Log("Switched to Rocket mode");
        this.interactor = _interactor;
        shootPoint = interactor.GetShootPoint();

        // change gun colour
        interactor.gunRenderer.materials[3].color = interactor.rocketColour;
    }

    public void Shoot() {
        PooledObject pooledObj = interactor.rocketPool.GetPooledObject();
        pooledObj.gameObject.SetActive(true);

        Rigidbody bullet = pooledObj.GetComponent<Rigidbody>();
        bullet.transform.position = shootPoint.position;
        bullet.transform.rotation = shootPoint.rotation;

        bullet.velocity = shootPoint.forward * interactor.GetShootVelocity();

        interactor.rocketPool.DestroyPooledObject(pooledObj, 5.0f);
    }
}
