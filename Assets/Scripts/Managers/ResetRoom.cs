using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetRoom : MonoBehaviour
{   
    [SerializeField] private GameObject roomObjects;
    [SerializeField] private GameObject roomObjectPrefab;

    private Transform _roomPoint;

    void Start() {
        _roomPoint = roomObjects.transform;
    }
    
    public void ResetObjects() {
        Destroy(roomObjects.gameObject);
        roomObjects = Instantiate(roomObjectPrefab, _roomPoint.position, Quaternion.identity);
    }
}
