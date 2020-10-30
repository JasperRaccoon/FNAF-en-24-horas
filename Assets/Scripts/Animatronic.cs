using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Animatronic : MonoBehaviour
{
    public int AIlevel;
    public Location[] path;
    [HideInInspector] public Location currentLoc;
    int currentLocIndex = 0;

    [HideInInspector] public AudioManager audioMan;
    public string walkSoundName;

    NightVariables nightMan;

    void Start()
    {
        nightMan = FindObjectOfType<NightVariables>();
        audioMan = FindObjectOfType<AudioManager>();
        GoToLocation(0);
        audioMan.Stop(walkSoundName);
        StartCoroutine(Work());
    }
    IEnumerator Work()
    {
        if (AIlevel == 0) yield break;
        while (true)
        {
            float secs = 80f / AIlevel;
            yield return new WaitForSeconds(secs);
            bool randomness = UnityEngine.Random.Range((int)1, 3) >= 2;
            if (randomness) Movement();
        }
    }
    public virtual void Movement()
    {
        int locationIndex = GetLocationIndex();
        if (locationIndex < path.Length - 1)
        {
            switch (path[locationIndex++].Enterable())
            {
                case true:
                    if (locationIndex < path.Length) GoToLocation(locationIndex++);
                    if ((GetLocationIndex() + 1) == path.Length) StartCoroutine(PrepareForJumpscare());
                    break;
                case false:
                    ComeBack();
                    break;
            }
        }
    }
    public void ComeBack()
    {
        for (int i = 0; i <= 3; i++) if (GetLocationIndex() > 0) GoToLocation(GetLocationIndex() - 1);
    }
    public void GoToLocation(int index)
    {
        Debug.Log("going to: " + path[index].name);

        currentLocIndex = index;

        audioMan.Play(walkSoundName);

        currentLoc?.ExitLocation();
        currentLoc = path[index];

        currentLoc.EnterToLocation();

        Transform desiredPos = currentLoc.GetEmptyPos();
        SetTransform(desiredPos);
    }
    public void SetTransform(Transform desiredPos)
    {
        transform.position = desiredPos.position;
        transform.rotation = desiredPos.rotation;
    }
    public int GetLocationIndex()
    {
        return currentLocIndex;
    }
    public virtual IEnumerator PrepareForJumpscare()
    {
        yield return new WaitForSeconds(5f);
        if (path[GetLocationIndex()].opened) Jumpscare();
        else ComeBack();
    }
    public virtual void Jumpscare()
    {
        MonitorInteract monitor = FindObjectOfType<MonitorInteract>();
        if (monitor.isActived) monitor.ChangeObjState();
        Destroy(monitor);        

        audioMan.Play("Jumpscare");

        Transform jumpscarePos = GameObject.FindGameObjectWithTag("JumpscarePos").transform;
        SetTransform(jumpscarePos);
        transform.SetParent(jumpscarePos);

        CameraShake.CamShake(500000f, 2f, this);

        StartCoroutine(nightMan.Lose());
    }
}
