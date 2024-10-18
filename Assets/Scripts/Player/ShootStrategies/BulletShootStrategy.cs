using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletShootStrategy : IShootStrategy
{   
    ShootInteractor interactor;
    Transform shootPoint;

    public BulletShootStrategy(ShootInteractor _interactor) {
        Debug.Log("Switched to Bullet mode");
        this.interactor = _interactor;
        shootPoint = interactor.GetShootPoint();

        // change gun colour
        interactor.gunRenderer.materials[3].color = interactor.bulletColour;
    }

    public void Shoot() {
        PooledObject pooledObj = interactor.bulletPool.GetPooledObject();
        pooledObj.gameObject.SetActive(true);

        Rigidbody bullet = pooledObj.GetComponent<Rigidbody>();
        bullet.transform.position = shootPoint.position;
        bullet.transform.rotation = shootPoint.rotation;

        bullet.velocity = shootPoint.forward * interactor.GetShootVelocity();

        interactor.bulletPool.DestroyPooledObject(pooledObj, 5.0f);
    }
}
