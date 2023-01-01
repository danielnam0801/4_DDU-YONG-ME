using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgendSound : AudioPlayer
{
    [SerializeField] private AudioClip _hitClip = null, _deathClip = null, _attackClip = null;
        
    public void PlayHitSound()
    {
        PlayClip(_hitClip);
    }

    public void PlayDeathSound()
    {
        PlayClip(_deathClip);
    }

    public void PlayAttackSound()
    {
        PlayClipWithVariablePitch(_attackClip);
    }
}
