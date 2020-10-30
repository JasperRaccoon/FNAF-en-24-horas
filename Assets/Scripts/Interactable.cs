using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interactable : ActivableAnimObj
{
    public UnityEvent action;

    private void OnMouseDown()
    {
        CheckAction();
    }
    void CheckAction()
    {
        action.Invoke();
        ChangeObjState();
    }
}
