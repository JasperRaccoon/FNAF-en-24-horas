using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Activable : MonoBehaviour
{
    [HideInInspector] public bool isActived = false;
    [HideInInspector] public NightVariables nightManager;

    [HideInInspector] public AudioManager audioMan;
    public string soundName;

    public virtual void ChangeObjState()
    {
        isActived = !isActived;
    }

    void Start()
    {
        StartMethods();
    }

    public virtual void StartMethods()
    {
        nightManager = FindObjectOfType<NightVariables>();
        audioMan = FindObjectOfType<AudioManager>();
        isActived = false;
    }
}
