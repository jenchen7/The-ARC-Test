using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ObjectivesManager : MonoBehaviour
{   
    [SerializeField] private List<GameObject> _objectives = new List<GameObject>();
    private List<GameObject> objectives;

    public UnityEvent OnAllCompleted;

    // Start is called before the first frame update
    void Start()
    {   
        objectives = new List<GameObject>(_objectives); // copies list to not interfere with serialized objects
    }

    public void RemoveObject(GameObject obj) {
        objectives.Remove(obj);
        if (objectives.Count <= 0) {
            OnAllCompleted?.Invoke();
            Debug.Log("All objectives completed.");
        }
    }

    public void AddObject(GameObject obj) {
        objectives.Add(obj);
    }
}
