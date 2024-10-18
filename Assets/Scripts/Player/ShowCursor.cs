using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowCursor : MonoBehaviour
{
    public void Show(bool showCursor){
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = showCursor;
    }
}
