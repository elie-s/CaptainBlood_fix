using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RetroJam.CaptainBlood
{
    //[ExecuteInEditMode]

    public class TerrainGenerator : MonoBehaviour
    {
        //Terrain generation stuff
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
        Terrain_manager terrain_man;
        [Space(10)]

        //Compute Shader stuff
        float[,] heights;
        [SerializeField] ComputeShader CalculShader;
        ComputeBuffer myBufferXBase;
        ComputeBuffer myBufferYBase;
        ComputeBuffer floatBufferBase;
        ComputeBuffer myBufferX;
        ComputeBuffer myBufferY;
        ComputeBuffer floatBuffer;
        int indexOfKernel;
        bool doneOnce = false;

        float[] dataVectorX = new float[2];
        float[] dataVectorY = new float[2];
        float[] dataHeight = new float[2];

        public void Awake()
        {
            terrain_man = GetComponentInParent<Terrain_manager>();

            indexOfKernel = CalculShader.FindKernel("CSMain");
            myBufferXBase = new ComputeBuffer(1, 8);
            myBufferYBase = new ComputeBuffer(1, 8);
            floatBufferBase = new ComputeBuffer(1, 8);

        }


        void Update()
        {

            Debug.Log(myBufferX.count + " myBufferX");
            Debug.Log(myBufferY.count + " myBufferY");
            Debug.Log(floatBuffer.count + " floatBuffer");

            if (myBufferX == null)
            {
                //myBufferX = new ComputeBuffer(1, 8);
            }
            if (myBufferY == null)
            {
                //myBufferY = new ComputeBuffer(1, 8);
            }
            if (floatBuffer == null)
            {
                //floatBuffer = new ComputeBuffer(1, 8);
            }


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
            heights = new float[width, height];
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


            dataVectorX[0] = xCord;
            dataVectorY[0] = yCord;

            myBufferX = myBufferXBase;
            myBufferY = myBufferYBase;
            floatBuffer = floatBufferBase;

            //if (doneOnce == false)
            //{
            myBufferX.SetData(dataVectorX);
                myBufferY.SetData(dataVectorY);
                floatBuffer.SetData(dataHeight);

                CalculShader.SetBuffer(indexOfKernel, "dataHeight", floatBuffer);
                CalculShader.SetBuffer(indexOfKernel, "dataVectorX", myBufferX);
                CalculShader.SetBuffer(indexOfKernel, "dataVectorY", myBufferY);

                CalculShader.Dispatch(indexOfKernel, 1, 1, 1);

                floatBuffer.GetData(dataHeight);
                myBufferX.GetData(dataVectorX);
                myBufferY.GetData(dataVectorY);

                myBufferY.Release();
                myBufferX.Release();
                floatBuffer.Release();


                //Debug.Log(dataHeight[0] + " dataHeight");
                //Debug.Log(dataVectorX[0] + " dataVectorX");
                //Debug.Log(dataVectorY[0] + " dataVectorY");
                doneOnce = true;
            //}


            return dataHeight[0];
            //return Mathf.PerlinNoise(xCord, yCord);


        }


    }
}
