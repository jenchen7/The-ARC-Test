using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{   
    [Header("Player Movement")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float turnSpeed;
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private float jumpForce;
    [SerializeField] private float sprintMultiplier;
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private bool invertMouse;

    [Header("Ground Checker")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayerMask;
    [SerializeField] private float groundCheckDistance;

    [Header("Shoot")]
    [SerializeField] private Rigidbody bulletPrefab;
    [SerializeField] private Rigidbody rocketPrefab;
    [SerializeField] private float shootForce;
    [SerializeField] private Transform shootPoint;

    [Header("Interact")]
    [SerializeField] private Camera cam;
    [SerializeField] private LayerMask interactionLayer;
    [SerializeField] private float interactionDistance;

    [Header("Pick and Drop")]
    [SerializeField] private Transform attachPoint;
    [SerializeField] private float pickupDistance;
    [SerializeField] private LayerMask pickupLayer;

    private CharacterController characterController;

    private float horizontal, vertical, mouseX, mouseY, camXRotation;
    private float moveMultiplier = 1;
    private Vector3 playerVelocity;
    private bool isGrounded;


    // raycast
    private RaycastHit raycastHit;
    private ISelectable selectable;

    // pick and drop
    private bool isPicked = false;
    private IPickable pickable;

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();  

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
        GroundCheck();
        MovePlayer();
        JumpCheck();
        RotatePlayer();

        ShootBullet();
        ShootRocket();

        Interact();
        PickAndDrop();
    }

    void GetInput() {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        mouseX = Input.GetAxis("Mouse X");
        mouseY = Input.GetAxis("Mouse Y");

        moveMultiplier = Input.GetButton("Sprint") ? sprintMultiplier : 1;
    }

    void MovePlayer() {
        characterController.Move((transform.forward * vertical + transform.right * horizontal) * moveSpeed * Time.deltaTime * moveMultiplier);

        // ground check
        if (isGrounded && playerVelocity.y < 0) {
            playerVelocity.y = -2f;
        }

        playerVelocity.y += gravity * Time.deltaTime;

        characterController.Move(playerVelocity * Time.deltaTime);
    }

    void GroundCheck() {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckDistance, groundLayerMask);
    }

    void JumpCheck() {
        if(Input.GetButtonDown("Jump")) {
            playerVelocity.y = jumpForce;
        }
    }

    void RotatePlayer() {
        transform.Rotate(Vector3.up * turnSpeed * Time.deltaTime * mouseX);

        camXRotation += Time.deltaTime * mouseY * turnSpeed * (invertMouse ? 1 : -1);
        camXRotation = Mathf.Clamp(camXRotation, -40f, 40f);

        cameraTransform.localRotation = Quaternion.Euler(camXRotation, 0, 0);
    }

    void ShootBullet() {
        if (Input.GetButtonDown("Fire1")) {
            Rigidbody bullet = Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation);
            bullet.AddForce(shootPoint.forward * shootForce);
            Destroy(bullet.gameObject, 5.0f);
        }
    }

    void ShootRocket() {
        if (Input.GetButtonDown("Fire2")) {
            Rigidbody rocket = Instantiate(rocketPrefab, shootPoint.position, shootPoint.rotation);
            rocket.AddForce(shootPoint.forward * shootForce);
            Destroy(rocket.gameObject, 5.0f);
        }
    }

    void Interact() {
        Ray ray = cam.ScreenPointToRay(new Vector3(Screen.width/2, Screen.height/2, 0));

        if (Physics.Raycast(ray, out raycastHit, interactionDistance, interactionLayer)) {
            selectable = raycastHit.transform.GetComponent<ISelectable>();

            if(selectable != null) {
                selectable.OnHoverEnter();

                if(Input.GetKeyDown(KeyCode.E)) {
                    selectable.OnSelect();
                }
            }
        }

        if(raycastHit.transform == null && selectable != null) {
            selectable.OnHoverExit();
            selectable = null;
        }
    }

    void PickAndDrop() {
        Ray ray = cam.ScreenPointToRay(new Vector3(Screen.width/2, Screen.height/2, 0));

        if (Physics.Raycast(ray, out raycastHit, pickupDistance, pickupLayer)) {
            if(Input.GetKeyDown(KeyCode.E) && !isPicked) {
                pickable = raycastHit.transform.GetComponent<IPickable>();

                if (pickable == null) 
                    return;

                pickable.OnPicked(attachPoint);
                isPicked = true;
                return;
            }
        }

        if (Input.GetKeyDown(KeyCode.E) && isPicked && pickable != null) {
            pickable.OnDropped();
            isPicked = false;
        }
    }
}
