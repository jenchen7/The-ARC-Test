using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickCube : MonoBehaviour, IPickable
{   
    private Rigidbody cubeRB;
    private Transform _parent;

    // Start is called before the first frame update
    void Start()
    {
        cubeRB = GetComponent<Rigidbody>();
        _parent = transform.parent;
    }
    
    public void OnPicked(Transform attachTransform) {
        transform.position = attachTransform.position;
        transform.rotation = attachTransform.rotation;
        transform.SetParent(attachTransform);

        cubeRB.isKinematic = true;
        cubeRB.useGravity = false;
    }

    public void OnDropped() {
        cubeRB.isKinematic = false;
        cubeRB.useGravity = true;
        transform.SetParent(_parent);

    }

    
 
}
