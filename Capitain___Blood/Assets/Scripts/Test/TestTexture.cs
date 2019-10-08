using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

namespace RetroJam.CaptainBlood
{
    public class TestTexture : MonoBehaviour
    {
        [SerializeField] private FBMValues values;

        public float debugY;

        private Texture2D texture;

        public int height;

        // Start is called before the first frame update
        void Start()
        {
            InitializeTexture();
            
        }

        // Update is called once per frame
        void Update()
        {
            TestLine();
        }

        public void TestLine()
        {
            CleanImage();
            DrawLine();

            texture.Apply(true);
        }

        public void HeightOfLine()
        {
            if (Input.GetKeyDown(KeyCode.UpArrow)) height++;
            else if (Input.GetKeyDown(KeyCode.DownArrow)) height--;

            height = Mathf.Clamp(height, 0, 99);
        }

        public void InitializeTexture()
        {
            texture = new Texture2D(256, 126);

            GetComponent<RawImage>().material.mainTexture = texture;
        }

        public void CleanImage()
        {
            for (int x = 0; x < 256; x++)
            {
                for (int y = 0; y < 126; y++)
                {
                    texture.SetPixel(x, y, Color.black);
                }
            }
        }

        public void DrawLine()
        {
            for (int x = 0; x < 256; x++)
            {
                Debug.Log(fbm(x));

                texture.SetPixel(x, Mathf.Clamp(fbm(x),0,125), Color.blue);
            }
        }

        int fbm(float _x)
        {
            double x = System.Convert.ToDouble(_x);
            x = x * values.factorX;
         int octaves = values.octaves;
         double lacunarity = values.lacunarity / values.Factor;
         double gain = values.gain / values.Factor;
         double amplitude = values.amplitude / values.Factor;
         double frequency = values.frequency / values.Factor;

        double y=0;
            double shift = 100;
            for (int i = 0; i < octaves; ++i)
            {
                y += amplitude * noise(frequency * (x+1));
                frequency *= lacunarity;
                x = x * 2 + shift;
                amplitude *= gain;
            }

            y *= values.factorY;
            return System.Convert.ToInt32(System.Math.Round(y));
        }

        double noise(double _x)
        {
            double i = System.Math.Floor(_x);
            double f = i-_x;
            double u = f * f * (3 - 2 * f);
            return hash(i)*(1-u)+ hash(i + 1)*u;
        }

        double hash(double n)
        {
            double result = Mathf.Sin((float)n) * 1e4;

            return result-System.Math.Floor(result);
        }
        //float hash(vec2 p) { return fract(1e4 * sin(17.0 * p.x + p.y * 0.1) * (0.1 + abs(sin(p.y * 13.0 + p.x)))); }

    }
}