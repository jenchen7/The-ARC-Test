using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyHealth : MonoBehaviour
{   
    [SerializeField] private float maxHealth;

    //public Action<float> OnHealthUpdate;
    public UnityEvent OnDeath;

    public bool isDead { get; private set; }
    private float health;

    // Start is called before the first frame update
    void Start()
    {
        ResetHealth();
    }

    public void ResetHealth() {
        health = maxHealth;
        //OnHealthUpdate(maxHealth);
        //isDead = false;
    }

    public void DeductHealth(float value) {
        if(isDead) return;

        health -= value;

        if(health <= 0) {
            isDead = true;
            OnDeath?.Invoke();
            gameObject.SetActive(false);
            health = 0;
        }
        
        //OnHealthUpdate(health);
    }
}
