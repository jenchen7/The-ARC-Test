using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PooledObject : MonoBehaviour
{   
    ObjectPool associatedPool;

    [SerializeField] private float damage;

    private float timer;
    private bool setToDestroy = false;
    private float destroyTime = 0;

    public void SetObjectPool(ObjectPool pool) {
        associatedPool = pool;
        timer = 0;
        destroyTime = 0;
        setToDestroy = false;
    }

    void Update() {
        if(setToDestroy) {
            timer += Time.deltaTime;

            if(timer >= destroyTime) {
                timer = 0;
                setToDestroy = false;
                Destroy();
            }
        }
    }
    
    public void Destroy() {
        if(associatedPool != null) {
            associatedPool.RestoreObject(this);
        }
    }

    public void Destroy(float time) {
        setToDestroy = true;
        destroyTime = time;
    }

    private void OnCollisionEnter(Collision other) {
        Debug.Log($"Collided with {other.gameObject.name}");

        EnemyHealth enemyHealth = other.gameObject.GetComponent<EnemyHealth>();

        if (enemyHealth != null) {
            enemyHealth.DeductHealth(damage);
            Destroy();
        }
    }
}
