using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Health : MonoBehaviour
{   
    [SerializeField] private float maxHealth;

    public Action<float> OnHealthUpdate;
    public Action OnDeath, OnRestart;

    public bool isDead { get; private set; }
    private float health;

    // Start is called before the first frame update
    void Start()
    {
        ResetHealth();
    }

    public void ResetHealth() {
        health = maxHealth;
        OnHealthUpdate(maxHealth);
        isDead = false;
        OnRestart();
    }

    public void DeductHealth(float value) {
        if(isDead) return;

        health -= value;

        if(health <= 0) {
            isDead = true;
            OnDeath();
            health = 0;
        }
        
        OnHealthUpdate(health);
    }
}
