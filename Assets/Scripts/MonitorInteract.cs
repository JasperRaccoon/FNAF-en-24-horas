using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MonitorInteract : Activable, IPointerEnterHandler
{
    public GameObject monitorUI;
    PlayerScript playerScript;

    public override void StartMethods()
    {
        base.StartMethods();
        playerScript = FindObjectOfType<PlayerScript>();
    }

    public override void ChangeObjState()
    {
        base.ChangeObjState();
        audioMan.Play(soundName);
        monitorUI.SetActive(isActived);
        nightManager.SetEnergy(isActived ? 1 : -1);
        playerScript.SetPlayerState(isActived ? PlayerState.showingUI : PlayerState.normal);
    }

    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        ChangeObjState();
    }
}
