using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{   
    [SerializeField] private Health playerHealth;

    [Header("UI Element")]
    public TMP_Text txtHealth;
    public GameObject gameOver;

    // Start is called before the first frame update
    void Start()
    {
        gameOver.SetActive(false);
    }

    private void OnEnable() {
        playerHealth.OnHealthUpdate += OnHealthUpdate;
        playerHealth.OnDeath += OnDeath;
        playerHealth.OnRestart += OnRestart;
    }

    private void OnDestroy() {
        playerHealth.OnHealthUpdate -= OnHealthUpdate;
    }

    void OnHealthUpdate(float health) {
        txtHealth.text = "Health: " + Mathf.Floor(health).ToString();
    }

    void OnDeath() {
        gameOver.SetActive(true);
    }

    void OnRestart() {
        gameOver.SetActive(false);
    }

}
