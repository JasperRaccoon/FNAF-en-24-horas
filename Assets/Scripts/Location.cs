using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Location : MonoBehaviour
{
    int currentChars = 0;
    public int maxChars;
    [HideInInspector] public bool opened = true;
    public Transform[] pos;

    private void Start()
    {
        opened = true;
    }
    public bool Enterable()
    {
        return (currentChars <= maxChars) && opened;
    }
    public void EnterToLocation()
    {
        currentChars++;
    }
    public void ExitLocation()
    {
        currentChars--;
    }
    public Transform GetEmptyPos()
    {
        Debug.Log("currentChars = " + currentChars);
        int posInt = (currentChars == 0) ? 0 : currentChars - 1;
        Debug.Log("posInt = " + posInt);
        if (posInt >= pos.Length) posInt = UnityEngine.Random.Range((int)0, pos.Length);
        return pos[posInt];
    }
}
