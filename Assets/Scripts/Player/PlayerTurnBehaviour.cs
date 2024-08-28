using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTurnBehaviour : MonoBehaviour
{   
    [SerializeField] private PlayerInput input;

    [Header("Player Turn")]
    [SerializeField] private float turnSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RotatePlayer();
    }

    void RotatePlayer() {
        transform.Rotate(Vector3.up * turnSpeed * Time.deltaTime * input.mouseX);
    }
}
