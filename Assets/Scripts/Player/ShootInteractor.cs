using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootInteractor : Interactor
{      

    [Header("Gun")]
    public MeshRenderer gunRenderer;
    public Color bulletColour;
    public Color rocketColour;

    [Header("Shoot")]
    public ObjectPool bulletPool;
    public ObjectPool rocketPool;

    [SerializeField] private float shootVelocity;
    [SerializeField] private Transform shootPoint;
    [SerializeField] private PlayerMoveBehaviour moveBehaviour;

    private float finalShootVelocity;
    private IShootStrategy currentStrategy;

    public override void Interact() {

        if(currentStrategy == null) {
            currentStrategy = new BulletShootStrategy(this);
        }

        if (input.weapon1Pressed) {
            currentStrategy = new BulletShootStrategy(this);
        }
        
        if(input.weapon2Pressed) {
            currentStrategy = new RocketShootStrategy(this);
        }

        // shoot strategy
        if(input.primaryShootPressed && currentStrategy != null) {
            currentStrategy.Shoot();
            AudioManager.instance.PlaySound("shoot");
        }
    }

    /*
    void Shoot() {

        PooledObject pooledObj = objPool.GetPooledObject();
        pooledObj.gameObject.SetActive(true);

        // Rigidbody bullet = Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation);
        Rigidbody bullet = pooledObj.GetComponent<Rigidbody>();
        bullet.transform.position = shootPoint.position;
        bullet.transform.rotation = shootPoint.rotation;

        bullet.velocity = shootPoint.forward * finalShootVelocity;

        //Destroy(bullet.gameObject, 5.0f);
        objPool.DestroyPooledObject(pooledObj, 5.0f);
    }
    */

    public Transform GetShootPoint() {
        return shootPoint;
    }

    public float GetShootVelocity() {
        finalShootVelocity = moveBehaviour.GetForwardSpeed() + shootVelocity;
        return finalShootVelocity;
    }
}
