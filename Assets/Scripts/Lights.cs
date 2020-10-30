using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lights : Activable
{
    public GameObject fog;

    public void TurnLights()
    {
        ChangeObjState();
        LightSound();
        nightManager.SetEnergy(isActived ? 1 : -1);
        fog.SetActive(!isActived);
    }
    void LightSound()
    {
        if (isActived) audioMan.Play(soundName);
        else audioMan.Stop(soundName);
    }
}
