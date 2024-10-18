using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(-50)]

public class SelectRandomObject : MonoBehaviour
{
    [SerializeField] private List<GameObject> objects;

    void Start() {
        objects[Random.Range(0, objects.Count-1)].transform.SetParent(transform);
    }
    public GameObject RandomObject() {
        return objects[Random.Range(0, objects.Count-1)];
    }
}
