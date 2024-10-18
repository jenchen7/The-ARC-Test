using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyPad : MonoBehaviour
{   
    [SerializeField] private GameObject matchingObject = null;

    public UnityEvent OnEnemyEntered;

    private void OnTriggerEnter(Collider other) {
        //Debug.Log("Collided with" + other.gameObject.name);
        if(other.CompareTag("Enemy")) {
            //Debug.Log("Enemy entered");
            OnEnemyEntered?.Invoke();
        }
    }

}
