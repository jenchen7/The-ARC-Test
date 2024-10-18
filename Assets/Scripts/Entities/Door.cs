using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    // [SerializeField] private MeshRenderer doorRenderer;
    // [SerializeField] private Material defaultDoorMaterial, detectedDoorMaterial;
    [SerializeField] private Animator doorAnimator;

    [SerializeField] private bool isLocked = true;
    private float timer = 0;
    private const float waitTime = 1.0f;

    AudioSource _audioSource;
    private bool hasPlayed = false;

    void Start() {
        _audioSource = gameObject.GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other) {
        if (!isLocked && other.CompareTag("Player")) {
            timer = 0;
            // doorRenderer.material = detectedDoorMaterial;
        }
    }

    private void OnTriggerStay(Collider other) {
        if (isLocked)
            return;

        if (!other.CompareTag("Player")) {
            return;
        }

        timer += Time.deltaTime;

        if (timer >= waitTime) {
            timer = waitTime;
            //doorAnimator.SetBool("isOpen", true);
            OpenDoor(true);
        }
    }

    private void OnTriggerExit(Collider other) {
        OpenDoor(false);
        // doorAnimator.SetBool("isOpen", false);
        // doorRenderer.material = defaultDoorMaterial;
    }

    public void LockDoor() {
        isLocked = true;
    }

    public void UnlockDoor() {
        isLocked = false;
        AudioManager.instance.PlaySound("unlock", _audioSource);
    }

    public void OpenDoor(bool doorstate) {
        if(!isLocked)
            doorAnimator.SetBool("isOpen", doorstate);
            if(!hasPlayed || !doorstate){
                hasPlayed = true;
                AudioManager.instance.PlaySound("openDoor", _audioSource);
            }
    }

}
