using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RetroJam.CaptainBlood.Upcom;

namespace RetroJam.CaptainBlood
{
    public class TestSentence : MonoBehaviour
    {
        public Sentence sentence;

        void Start()
        {
            sentence = new Sentence(Word.Great, Word.Weapon, Word.Beautiful, Word.Ship, Word.Me, Word.Destroy, Word.Home, Word.You);
        }
    }
}