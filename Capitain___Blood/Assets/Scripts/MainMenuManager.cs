using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using RetroJam.CaptainBlood.CursorLib;

namespace RetroJam.CaptainBlood
{  
    public class MainMenuManager : MonoBehaviour
    {
        [SerializeField] GameLoader gameLoader;
        [SerializeField] Transform cursor;

        [SerializeField] private Button quit;
        [SerializeField] private Button play;

        void Update()
        {
            ManageButtons();
        }

        void ManageButtons()
        {
            if(Input.GetButtonDown("Select1"))
            {
                if(quit.IsCursorOver(cursor))Application.Quit();                   
                else if (play.IsCursorOver(cursor)) PlayGame();
            }
        }

        public void PlayGame()
        {
            gameLoader.LoadGame(1);
        }
    }
}