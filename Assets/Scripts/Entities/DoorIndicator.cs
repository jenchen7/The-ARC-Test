using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorIndicator : MonoBehaviour
{   
    [SerializeField] private MeshRenderer indicatorRenderer;

    public void UnlockedIndicator() {
        indicatorRenderer.material.color = Color.green;
    }

    public void LockedIndicator() {
        indicatorRenderer.material.color = Color.red;
    }
}
