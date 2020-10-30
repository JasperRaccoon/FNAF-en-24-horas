using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class ButtonExt
{
    public static void ClickButton(this Button button)
    {
        UnityEngine.EventSystems.ExecuteEvents.Execute(button.gameObject, new UnityEngine.EventSystems.BaseEventData(UnityEngine.EventSystems.EventSystem.current), UnityEngine.EventSystems.ExecuteEvents.submitHandler);
    }
}
