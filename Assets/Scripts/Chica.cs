using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chica : Animatronic
{
    public override void Movement()
    {
        base.Movement();
        if (path[GetLocationIndex()].name.Equals("Kitchen")) audioMan.Play("Kitchen");
    }
}
