using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
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
            Scene currentScene = SceneManager.GetActiveScene();

            if (currentScene.buildIndex == 0)
            {
                actions.Add("Jouer", Jouer);
                actions.Add("Quitter", Quitter);
                Debug.Log("ta mere elle est tellement chauve");
            }
            else
            {
                actions.Add("I", I);
                actions.Add("want", Want);
                actions.Add("bounty", Bounty);
                actions.Add("sex", Sex);
            }

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

        private void Quitter()
        { print("Quitter"); Application.Quit(); }
    }
}
