using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupInteractor : Interactor
{   
    [Header("Pick and Drop")]
    [SerializeField] private Camera cam;
    [SerializeField] private Transform attachPoint;
    [SerializeField] private float pickupDistance;
    [SerializeField] private LayerMask pickupLayer;

    // pick and drop
    private RaycastHit raycastHit;
    private bool isPicked = false;
    private IPickable pickable;

    public override void Interact() {
        Ray ray = cam.ScreenPointToRay(new Vector3(Screen.width/2, Screen.height/2, 0));

        if (Physics.Raycast(ray, out raycastHit, pickupDistance, pickupLayer)) {
            if(input.activatePressed && !isPicked) {
                pickable = raycastHit.transform.GetComponent<IPickable>();

                if (pickable == null) 
                    return;

                pickable.OnPicked(attachPoint);
                isPicked = true;
                AudioManager.instance.PlaySound("pickup");
                return;
            }
        }

        if (input.activatePressed && isPicked && pickable != null) {
            pickable.OnDropped();
            isPicked = false;
            AudioManager.instance.PlaySound("drop");
        }
    }
}
