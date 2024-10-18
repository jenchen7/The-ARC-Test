using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PressurePad : MonoBehaviour
{   
    [SerializeField] private float checkRadius;
    [SerializeField] private LayerMask pickupLayer;

    [SerializeField] private GameObject matchingObject = null;

    public UnityEvent OnCubePlaced;
    public UnityEvent OnCubeRemoved;

    private void OnCollisionEnter(Collision collision) {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, checkRadius, pickupLayer);

        foreach(var collider in hitColliders) {
            // Debug.Log("Collider in contact: " + collider.gameObject.name);
            if (collider.CompareTag("PickCube")) {
                if (matchingObject != null && (collider.gameObject != matchingObject && collider.transform.parent.gameObject != matchingObject))
                    return;

                Debug.Log("Correct cube placed");
                OnCubePlaced?.Invoke();
                break;
            }
        }
    }

    private void OnCollisionExit(Collision collision) {
        if (collision.gameObject.CompareTag("PickCube")) {
            OnCubeRemoved?.Invoke();
        }
    }
}
