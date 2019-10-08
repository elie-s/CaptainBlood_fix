using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RetroJam.CaptainBlood
{
    public class SoundManager : EventsManager
    {
        [SerializeField] AudioSource sounds;
        [SerializeField] AudioClip[] clips;
        // Start is called before the first frame update

         public override void PlayDestroySound()
         {
             sounds.pitch = 1;
             sounds.clip = clips[0];
             sounds.Play();
         }

         public override void PlayValidSound()
         {
             sounds.pitch = 1.15f;
             sounds.clip = clips[1];
             sounds.Play();
         }

         public override void PlayBlockedSound()
         {
             sounds.pitch = 0.85f;
             sounds.clip = clips[1];
             sounds.Play();
         }
    }
}