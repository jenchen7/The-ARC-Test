using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LevelManager : MonoBehaviour
{   
    [SerializeField] private bool isFinalLevel;
    [SerializeField] private bool isgameOver;

    [SerializeField] private ResetRoom resetRoom = null;
    [SerializeField] private RespawnPlayer respawnPlayer = null;

    public UnityEvent onLevelStart;
    public UnityEvent onLevelEnd;

    public void StartLevel() {
        onLevelStart?.Invoke();
    }

    public void EndLevel() {
        onLevelEnd?.Invoke();

        if(isFinalLevel) {
            GameManager.instance.ChangeState(GameManager.GameState.GameEnd, this);
        }
        if(isgameOver) {
            GameManager.instance.LoadLastLevel();
        }
        else {
            GameManager.instance.ChangeState(GameManager.GameState.LevelEnd, this);
        }
    }

    // game over
    public void FailedLevel() {
        GameManager.instance.ChangeState(GameManager.GameState.GameOver, this);
    }

    public void ResetLevel() {
        resetRoom.ResetObjects();
        respawnPlayer.Respawn();
    }
    
}
