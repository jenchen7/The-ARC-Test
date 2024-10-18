using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{   
    [SerializeField] private AudioSource playerSFX;
    [SerializeField] private AudioClip pickup, drop, shoot, unlock, openDoor;

    public static AudioManager instance;
    
    private void Awake() {
        if(instance != null && instance != this) {
            Destroy(instance);
            return;
        }

        instance = this;
    }

    public void PlaySound(string soundClip) {
        switch(soundClip) {
            case "pickup":
                playerSFX.PlayOneShot(pickup);
                break;
            case "drop":
                playerSFX.PlayOneShot(drop);
                break;
            case "shoot":
                playerSFX.PlayOneShot(shoot);
                break;
        }
    }

    public void PlaySound(string soundClip, AudioSource _audioSource) {
        switch(soundClip) {
            case "unlock":
                _audioSource.PlayOneShot(unlock);
                break;
            case "openDoor":
                _audioSource.PlayOneShot(openDoor);
                break;
        }
    }
}
