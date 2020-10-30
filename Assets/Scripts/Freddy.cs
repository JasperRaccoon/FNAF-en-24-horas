using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Freddy : Animatronic
{
    public IEnumerator PoweoutJumpscare(float secs)
    {
        GoToLocation(3);
        audioMan.Stop(walkSoundName);
        yield return new WaitForSeconds(secs);
        GoToLocation(0);
        audioMan.Stop(walkSoundName);
        yield return new WaitForSeconds(2f);
        Jumpscare();
    }
}
