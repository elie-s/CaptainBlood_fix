using System.IO;
using System.Runtime.Serialization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using TMPro;
using RetroJam.CaptainBlood.GalaxyLib;
using RetroJam.CaptainBlood.Lang;

namespace RetroJam.CaptainBlood
{
    public class GameManager : EventsManager
    {
        [SerializeField] bool loadFromSave;
        [SerializeField] public Phase phase;
        [SerializeField] private Menu menu;
        [SerializeField] private Cursor cursor;
        [SerializeField] private MissionsManager missions;

        [SerializeField] private TextMeshProUGUI currentX;
        [SerializeField] private TextMeshProUGUI currentY;
        

        [SerializeField] public Planet currentPlanet;
        [SerializeField] public Alien alien;
        [SerializeField] bool isInhabited;

        private Phase lastPhase;

        public bool initialized;

        public static Events events = new Events();

        private System.Diagnostics.Stopwatch sw;

        //[SerializeField] private GalaxySCO save;

        //public Vector2Int test;

        #region Classes
        [System.Serializable] public class Menu
        {
            public GameObject main, galaxy, planetMenu, landing, upCom, keyboard, planet;

            public void SetActive(Phase _phase)
            {
                main.SetActive(false);
                galaxy.SetActive(false);
                planetMenu.SetActive(false);
                landing.SetActive(false);
                upCom.SetActive(false);
                keyboard.SetActive(false);
                planet.SetActive(false);

                switch (_phase)
                {
                    case Phase.MainMenu:
                        main.SetActive(true);
                        return;
                    case Phase.Galaxy:
                        galaxy.SetActive(true);
                        return;
                    case Phase.FTL:
                        return;
                    case Phase.Planet:
                        planetMenu.SetActive(true);
                        planet.SetActive(true);
                        return;
                    case Phase.Landing:
                        landing.SetActive(true);
                        return;
                    case Phase.UpCom:
                        upCom.SetActive(true);
                        keyboard.SetActive(true);
                        landing.SetActive(true);
                        events.CallSetDialogueOfAlien();
                        return;
                    default:
                        return;
                }
            }
        }
        #endregion

        private void Awake()
        {
            sw = new System.Diagnostics.Stopwatch();

            sw.Start();
            
            //string savePlanets = File.ReadAllText(@"Saves\planets.json");
            //string saveAliens = File.ReadAllText(@"Saves\inhabitants.json");
            //JsonSerializerSettings setting = new JsonSerializerSettings();
            //setting.CheckAdditionalContent = false;

            //Words.InitializeWords();
            //if(Galaxy.planets.Count != 32256)Galaxy.GeneratePlanets();
            //Galaxy.Initialize();

            /*if(loadFromSave) Galaxy.Initialize(JsonConvert.DeserializeObject<Dictionary<Vector2Int, Planet>>(savePlanets, new PlanetLoading()), JsonConvert.DeserializeObject<Dictionary<Vector2Int, Alien>>(saveAliens, new AlienLoading()));
            else Galaxy.Initialize();*/
        }

        void Start()
        {
            

            sw.Stop();

            Debug.Log("Time to Initialize whole Game : " + sw.ElapsedMilliseconds + "ms.");
            currentPlanet = Galaxy.RandomInhabitedPlanet();
            alien = Galaxy.inhabitants[currentPlanet.coordinates];
            isInhabited = true;
            
        }

        void Update()
        {
            if(!initialized)
            {
                currentPlanet = Galaxy.planets[missions.missionFindCode.giver.coordinates];
                alien = missions.missionFindCode.giver;
                isInhabited = true;
                initialized = true;
            }


            Test();

            SavePlanets();

            CurrentCoordinates();
        }

        public void Test()
        {
            if(Input.GetKeyDown(KeyCode.T))
            {
                Debug.Log(currentPlanet.name[0] + " - " + currentPlanet.name[1] + " - " + currentPlanet.name[2]);
            }
        }

        public void SavePlanets()
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                Debug.Log("Files created in the \"Saves\" directory, saving informations in json-format.");

                using (StreamWriter planets = File.CreateText(@"Saves\planets.json"))
                {
                    planets.WriteLine(JsonConvert.SerializeObject(Galaxy.planets,Formatting.Indented ,new PlanetLoading()));
                }

                using (StreamWriter inhabitants = File.CreateText(@"Saves\inhabitants.json"))
                {
                    inhabitants.WriteLine(JsonConvert.SerializeObject(Galaxy.inhabitants, Formatting.Indented, new AlienLoading()));
                }

            }
        }

        public void SetCursorLimit(Phase _phase)
        {
            switch (_phase)
            {
                case Phase.MainMenu:
                    cursor.SetHeight(87);
                    break;
                case Phase.Galaxy:
                    cursor.SetHeight(48);
                    break;
                case Phase.FTL:
                    cursor.SetHeight(50);
                    break;
                case Phase.Planet:
                    cursor.SetHeight(50);
                    break;
                case Phase.Landing:
                    cursor.SetHeight(87);
                    break;
                case Phase.UpCom:
                    cursor.SetHeight(87);
                    break;
                default:
                    break;
            }
        }

        public void CurrentCoordinates()
        {
            currentX.text = currentPlanet.coordinates.x.ToString();
            currentY.text = currentPlanet.coordinates.y.ToString();
        }

        public void SetPlanet(Vector2Int _coord)
        {
            currentPlanet = Galaxy.planets[_coord];
            if (Galaxy.inhabitants.ContainsKey(_coord))
            {
                Debug.Log("Change Alien");
                alien = Galaxy.inhabitants[_coord];
                isInhabited = true;
            }
            else isInhabited = false;
        }

        public void SetPhase(Phase _phase)
        {
            SetCursorLimit(_phase);
            menu.SetActive(_phase);
            phase = _phase;
        }
    }
}