using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RetroJam.CaptainBlood
{
    [ExecuteInEditMode]
    public class Terrain_manager : EventsManager
    {
        #region Propreties
        public float speed = 0.01f;
        public int depth = 33;
        public int width = 65;
        public int height = 65;
        public float scale = 3.38f;

        public float offsetX;

        [Range(0.0f,3.0f)]public float multiplicateur = 1;

        #endregion

        landing_Control landingScript;
        [SerializeField] Script_ObjPattern[] scriptAble;
        [SerializeField][Range (0,4)] int patternIndex;
        //[SerializeField] TerrainGenerator terrainGenerator;
        float terrainParts;
        float startValue;

        int[] parts = new int[100];
       // public int countTerrains = 0;
        bool randomDone = false;
        bool routineRunning = false;

        void Start()
        {
            landingScript = FindObjectOfType<landing_Control>();
            //// Definir zones des terrains
            //terrainParts = landingScript.distanceLeft * 0.1f;
            //startValue = landingScript.distanceLeft;
        }

        public override void StartLanding()
        {
            speed = 0.01f;
            depth = 40;
            width = 129;
            height = 129;
            scale = 3.38f;
        }
        
        void Update()
        {
           TerrainPattern();
        }
        

        void TerrainPattern()
        {

            depth = scriptAble[patternIndex].depth;
            multiplicateur = scriptAble[patternIndex].multiplicateur;

           /*  if (landingScript.result > (startValue - terrainParts * 1) || landingScript.result <= (startValue - terrainParts * 9))
            {patternIndex = 0;}
            else if (landingScript.result <= (startValue - terrainParts * 1) && randomDone == false)
            {                
                patternIndex = parts[0];
            }
            else if(landingScript.result <= (startValue - terrainParts * 3) && randomDone == false)
            {
                patternIndex = parts[1];
            }
            else if(landingScript.result <= (startValue - terrainParts * 5) && randomDone == false)
            {
                patternIndex = parts[2];
            }
            else if(landingScript.result <= (startValue - terrainParts * 7) && randomDone == false)
            {
                patternIndex = parts[3];
            }*/

        }

        
        /*  ///////////////////////// Switch plusieurs Pattern de Map
        void TerrainBiomes()
        {
            depth = scriptAble[patternIndex].depth;
            multiplicateur = scriptAble[patternIndex].multiplicateur;

            float distance = terrainGenerator.offsetX;
            int nbterrainsFlown = Mathf.FloorToInt(distance / terrainParts);
            if(nbterrainsFlown != countTerrains &&  nbterrainsFlown-1 != countTerrains)
            {
                 countTerrains = nbterrainsFlown;
                 patternIndex = parts[nbterrainsFlown];
            }
        }

        public void SeedGenerator()
        {
            parts[0] = 0;
            
            for (int i = 2; i < Mathf.FloorToInt(startValue/terrainParts)-1; i+=2)
            {
                int val;
                do
                {
                    val = Random.Range(1,1);
                } while (val == parts[i-2]);

                parts[i] = val;
                parts[i+1] = val;
            
            }

            for (int i = Mathf.FloorToInt(startValue/terrainParts); i < parts.Length; i++)
            {
                parts[i] = 0;

            }
        }
        */

    }
}
