using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Windows.Speech;

namespace RetroJam.CaptainBlood
{
    public class RecoVocale : MonoBehaviour
    {
        private KeywordRecognizer keyReco;
        private Dictionary<string, Action> actions = new Dictionary<string, Action>();

        MainMenuManager MenuMana;

    void Start()
        {
            actions.Add("I", I);
            actions.Add("want", Want);
            actions.Add("bounty", Bounty);
            actions.Add("sex", Sex);

            actions.Add("Jouer", Jouer);

            keyReco = new KeywordRecognizer(actions.Keys.ToArray());
            keyReco.OnPhraseRecognized += Reco;
            keyReco.Start();

            MenuMana = FindObjectOfType<MainMenuManager>();
        }

        //private void Update()
        //{
        //    Debug.Log();
        //}

        private void Reco(PhraseRecognizedEventArgs speech)
        {
            actions[speech.text].Invoke();
        }

        private void I()
        { print("I"); }

        private void Want()
        { print("want"); }

        private void Bounty()
        { print("bounty"); }

        private void Sex()
        { print("sex"); }

        private void Jouer()
        { print("Jouer"); MenuMana.PlayGame(); }
    }
}
