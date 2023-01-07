using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSound : AudioPlayer
{
    [SerializeField] private AudioClip _hitClip = null, _deathClip = null, _attackClip = null , _expClip;

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

    public void PlayExpClip()
    {
        PlayClip(_expClip);
    }
}
