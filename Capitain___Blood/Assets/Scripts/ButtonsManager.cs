using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RetroJam.CaptainBlood.CursorLib;
using RetroJam.CaptainBlood.GalaxyLib;

namespace RetroJam.CaptainBlood
{
    public class ButtonsManager : EventsManager
    {
        [SerializeField] private SaveButtons save;
        [SerializeField] private GameManager manager;
        [SerializeField] private Transform cursor;

        [SerializeField] private Button[] main; // 0 = Teleport Out, 1 = Save, 2 = Planet, 3 = Galaxy, 4 = Question
        [SerializeField] private Button[] galaxy; // 0 = FTL, 1 = MainMenu
        [SerializeField] private Button[] planet; // 0 = Galaxy, 1 = Landing, 2 = Destroy, 3 = Intel, 4 = Main Menu
        [SerializeField] private Button[] landing;
        [SerializeField] private Button[] upcom; // 0 = Teleport In, 1 = MainMenu

        [SerializeField] private PlanetRenderer planetRenderer;
        [SerializeField] private CoordManager coordManager;

        // Start is called before the first frame update
        void Start()
        {
            SavingButtonsValues(save);
            planetRenderer.ApplyRender(manager.currentPlanet);

        }

        // Update is called once per frame
        void Update()
        {
            HandleButtons(manager.phase);
        }

        public void MainMenuButtons()
        {
            int index = -1;

            for (int i = 0; i < main.Length; i++)
            {
                if (main[i].IsCursorOver(cursor))
                {
                    index = i;
                    GameManager.events.CallPlayvalidSound();
                    break;
                } 
            }

            switch (index)
            {
                case -1:
                    GameManager.events.CallBlockedSound();
                    break;
                case 0:
                    Debug.Log("Teleporting Out.");
                    //Teleport Out
                    break;
                case 1:
                    Debug.Log("Saving the game.");
                    //Save Game;
                    break;
                case 2:
                    Debug.Log("Switching to Planet interface.");
                    planetRenderer.ApplyRender(manager.currentPlanet);
                    manager.SetPhase(Phase.Planet);
                    break;
                case 3:
                    Debug.Log("Switching to Galaxy interface.");
                    manager.SetPhase(Phase.Galaxy);
                    break;
                case 4:
                    Debug.Log("Quit");
                    Application.Quit();
                    break;
            }
        }

        public void GalaxyButtons()
        {
            int index = -1;

            for (int i = 0; i < galaxy.Length; i++)
            {
                if (galaxy[i].IsCursorOver(cursor))
                {
                    index = i;
                    GameManager.events.CallPlayvalidSound();
                    break;
                }
            }

            switch (index)
            {
                case -1:
                    GameManager.events.CallBlockedSound();
                    break;
                case 0:
                    Debug.Log("Hyperspace Jump.");
                    Debug.Log("Switching to Planet interface.");
                    manager.SetPlanet(coordManager.coord);
                    manager.SetPhase(Phase.Planet);
                    Cursor.blocked = true;
                    GameManager.events.CallInitializingFTL();
                    break;
                case 1:
                    Debug.Log("Switching to Main interface.");
                    manager.SetPhase(Phase.MainMenu);
                    break;
                case 2:
                    Debug.Log("Switching to Planet interface.");
                    manager.SetPhase(Phase.Planet);
                    break;
            }
        }

        public void PlanetButtons()
        {
            int index = -1;

            for (int i = 0; i < planet.Length; i++)
            {
                if (planet[i].IsCursorOver(cursor))
                {
                    index = i;
                    GameManager.events.CallPlayvalidSound();
                    break;
                }
            }

            switch (index)
            {
                case -1:
                    GameManager.events.CallBlockedSound();
                    break;
                case 0:
                    Debug.Log("Switching to Galaxy interface.");
                    manager.SetPhase(Phase.Galaxy);
                    break;
                case 1:
                    if(manager.currentPlanet.destroyed) break;
                    Debug.Log("Switching to Landing interface.");
                    manager.SetPhase(Phase.Landing);
                    GameManager.events.CallStartLanding();
                    break;
                case 2:
                    if(manager.currentPlanet.destroyed) break;
                    Debug.Log("Destroy Planet. #ThanksDirectorKrenic");
                    DestroyPlanet();
                    break;
                case 3:
                    if(manager.currentPlanet.destroyed) break;
                    Debug.Log("Intel of the Planet.");
                    //Intel of the planet
                    break;
                case 4:
                    Debug.Log("Switching to Main interface.");
                    manager.SetPhase(Phase.MainMenu);
                    break;
            }
        }

        public void LandingButtons()
        {

        }

        public void DestroyPlanet()
        {
            bool isDestroyed = Galaxy.planets[manager.currentPlanet.coordinates].destroyed;

            if(!isDestroyed)
            {
                Galaxy.planets[manager.currentPlanet.coordinates].destroyed = true;
                GameManager.events.CallDeathStarBehave();
            }
        }

        public void UpComButtons()
        {
            int index = -1;

            for (int i = 0; i < upcom.Length; i++)
            {
                if (upcom[i].IsCursorOver(cursor))
                {
                    index = i;
                    GameManager.events.CallPlayvalidSound();
                    break;
                }
            }

            switch (index)
            {
                case -1:
                    GameManager.events.CallBlockedSound();
                    break;
                case 0:
                    Debug.Log("Teleport In.");
                    //Teleport In
                    break;
                case 1:
                    Debug.Log("Switching to Main interface.");
                    manager.SetPhase(Phase.MainMenu);
                    break;
            }
        }

        public void HandleButtons(Phase _phase)
        {
            if (Input.GetButtonDown("Select1"))
            {
                switch (_phase)
                {
                    case Phase.MainMenu:
                        MainMenuButtons();
                        break;
                    case Phase.Galaxy:
                        GalaxyButtons();
                        break;
                    case Phase.FTL:
                        break;
                    case Phase.Planet:
                        PlanetButtons();
                        
                        break;
                    case Phase.Landing:
                        break;
                    case Phase.UpCom:
                        UpComButtons();
                        break;
                    default:
                        break;
                }
            }
        }

        public void SavingButtonsValues(SaveButtons _save)
        {
            _save.main = main;
            _save.galaxy = galaxy;
            _save.planet = planet;
            _save.landing = landing;
            _save.upcom = upcom;
        }

        public override void SlowingDown()
        {
            planetRenderer.ApplyRender(manager.currentPlanet);
        }
    }
}