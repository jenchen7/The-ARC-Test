using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PushButton : MonoBehaviour, ISelectable
{   
    [SerializeField] private Material defaultColour, hoverColour;
    [SerializeField] private MeshRenderer buttonRenderer;

    public UnityEvent onPush;

    public void OnSelect() {
        onPush?.Invoke();
    }

    public void OnHoverEnter() {
        buttonRenderer.material = hoverColour;
    }

    public void OnHoverExit() {
        buttonRenderer.material = defaultColour;
    }
}
