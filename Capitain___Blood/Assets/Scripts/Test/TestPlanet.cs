using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RetroJam.CaptainBlood.GalaxyLib;
using RetroJam.CaptainBlood.Lang;

namespace RetroJam.CaptainBlood
{
    public class TestPlanet : MonoBehaviour
    {
        public Word[] ownName;
        public Vector2Int coordinates;
        public Word race;

        // Start is called before the first frame update
        void Start()
        {
            Planet planet = new Planet(coordinates, race);

            for (int i = 0; i < planet.name.Length; i++)
            {
                Debug.Log(planet.name[i].ToText());
            }

            Galaxy.Initialize();
        }

        // Update is called once per frame
        void Update()
        {
            SavePlanets();
        }

        public void SavePlanets()
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                Debug.Log("Files created in the \"Saves\" directory, saving informations in json-format.");

                using (StreamWriter test = File.CreateText(@"Saves\planets.json"))
                {
                    //test.WriteLine(JsonUtility.ToJson(Galaxy.grid,true));
                }

            }
        }
    }
}