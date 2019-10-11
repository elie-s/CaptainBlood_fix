using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RetroJam.CaptainBlood
{
    [ExecuteInEditMode]
public class TerrainGenerator : MonoBehaviour
{
        Terrain_manager terrain_man;

        #region Preference
        [Header("PREFRENCES")]     
        public float Speed = 5;            

        [Space]

        public int depth = 20;  //height from above

        public int width = 20;     //make a int named width and set it to a default of 256
        public int height = 20;    //make int named height and set it to a default of 256 [Length of terrain]

        public float Scale = 1;

        public float offsetX = 100;
        public float offsetY = 100;

        [SerializeField] float startOffset ;
        public float xCord;
        public float yCord;

        [Space (15)]
        [SerializeField] bool terrain1;
        [SerializeField] bool terrain2;
        [SerializeField] bool terrain3;

        [SerializeField] float multiplicator;
        float factor = 1;
        #endregion

        [Space(10)]
        [SerializeField] Shader CalculShader;

        public void Start()
        {
            terrain_man = GetComponentInParent<Terrain_manager>();

        }


        void LateUpdate()
        {
            Terrain terrain = GetComponent<Terrain>();      //for Terrain Data
            terrain.terrainData = GenerateTerrain(terrain.terrainData);
   

            ValueUpdate();
        }

        void ValueUpdate ()
        {
            Speed = terrain_man.speed;
            depth = terrain_man.depth;
            width = terrain_man.width;
            height = terrain_man.height;
            Scale = terrain_man.scale; 
            multiplicator = terrain_man.multiplicateur;
        }


        TerrainData GenerateTerrain(TerrainData terrainData)
        {
            terrainData.heightmapResolution = width + 1;

            terrainData.size = new Vector3(width, depth, height);

            terrainData.SetHeights(0, 0, GenerateHeights());
            return terrainData;
        }

        float[,] GenerateHeights()
        {
            float[,] heights = new float[width, height];
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    heights[x, y] = CalculateHeight(x, y);      //generate some perlin noise value
                }
            }

            return heights;
        }

        float CalculateHeight(int x, int y)
        {

            if(terrain2 || terrain3) factor = multiplicator;

            xCord = (float)x / width * Scale*multiplicator + offsetX + startOffset*factor ;
            yCord = (float)y / height * Scale*multiplicator + offsetY;

            return Perlin.Fbm(xCord, yCord, 4);
            //return Mathf.PerlinNoise(xCord, yCord);

        }
    }
}
