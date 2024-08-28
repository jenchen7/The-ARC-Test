using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootInteractor : Interactor
{      
    [SerializeField] private Input inputType;

    [Header("Shoot")]
    [SerializeField] private Rigidbody bulletPrefab;
    [SerializeField] private float shootVelocity;
    [SerializeField] private Transform shootPoint;
    [SerializeField] private PlayerMoveBehaviour moveBehaviour;

    private float finalShootVelocity;

    public enum Input {
        Primary,
        Secondary
    }

    public override void Interact() {
        if(inputType == Input.Primary && input.primaryShootPressed || inputType == Input.Secondary && input.secondaryShootPressed) {
            Shoot();
        }
    }

    void Shoot() {
        finalShootVelocity = moveBehaviour.GetForwardSpeed() + shootVelocity;

        Rigidbody bullet = Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation);
        bullet.velocity = shootPoint.forward * finalShootVelocity;
        Destroy(bullet.gameObject, 5.0f);
    }
}
