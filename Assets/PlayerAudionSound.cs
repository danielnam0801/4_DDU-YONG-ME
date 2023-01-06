using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudionSound : AgendSound
{
    [SerializeField]
    protected AudioClip _stepClip;

    public void PlayStepSound()
    {
        PlayClipWithVariablePitch(_stepClip);
    }
}
