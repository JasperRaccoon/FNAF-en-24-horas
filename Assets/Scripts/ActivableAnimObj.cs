using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animation))]
public abstract class ActivableAnimObj : Activable
{
    Animation anim;
    public AnimationClip activeClip, unactiveClip;

    public override void ChangeObjState()
    {
        base.ChangeObjState();
        anim.Play(isActived ? activeClip.name : unactiveClip.name);
        if (string.IsNullOrEmpty(soundName)) return;
        audioMan.Play(soundName);
    }

    public override void StartMethods()
    {
        SetAnimations();
        base.StartMethods();
    }
    private void SetAnimations()
    {
        anim = GetComponent<Animation>();
        anim.AddClip(activeClip, activeClip.name);
        anim.AddClip(unactiveClip, unactiveClip.name);
    }
}
