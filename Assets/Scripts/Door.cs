using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : ActivableAnimObj
{
    public Location doorLocation;
    public void DoorAction()
    {
        ChangeObjState();
        nightManager.SetEnergy(isActived ? 1 : -1);
        doorLocation.opened = !isActived;
    }
}
