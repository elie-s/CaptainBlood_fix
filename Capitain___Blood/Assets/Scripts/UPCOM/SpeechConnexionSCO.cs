using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RetroJam.CaptainBlood.Lang
{
    [CreateAssetMenu(fileName = "new Connexions", menuName = "Language/Speech Connexions")]
    public class SpeechConnexionSCO : ScriptableObject
    {
        public SpeechConnexion[] connexions;
    }
}