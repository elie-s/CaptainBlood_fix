using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace RetroJam.CaptainBlood
{
    //[ExecuteInEditMode]
    public class Transistion : EventsManager
    {
        PostProcessVolume volume;
        LensDistortion lensLayer = null;
        public Material TransitionMaterial;

        public float time;
        public float intensityDebug;
        bool done;

        bool distortionIn;
        bool distortionOut;


        // Start is called before the first frame update
        void Start()
        {
            volume = GetComponentInChildren<PostProcessVolume>();

            volume.profile.TryGetSettings(out lensLayer);

            TransitionMaterial.shader = Shader.Find("Custom/BattleTransitions");
        }

        private void Update()
        {
            if (distortionIn) DistortionIn();
            if (distortionOut) DistortionOut();
        }

        void DistortionIn()
        {
            time += Time.deltaTime * 2f;
            lensLayer.intensity.value = -(Mathf.Pow(time, 3)) / 2;

            intensityDebug = lensLayer.intensity.value;

            //TransitionShader(time);

            if (lensLayer.intensity.value < -100)
            {
                lensLayer.intensity.value = -60;
                distortionIn = false;
                //distortionOut = true;
                //GameManager.events.CallSlowingDown();

                time = 0;
            }
        }

        void DistortionOut()
        {
            time += Time.deltaTime * 4f;
            lensLayer.intensity.value += time;

            intensityDebug = lensLayer.intensity.value;

            if (lensLayer.intensity.value > 0)
            {
                distortionOut = false;
                lensLayer.intensity.value = 0;
                time = 0;
            }
        }

        void lensDistord()
        {
            lensLayer.intensity.value = Mathf.Lerp(0, -50, time);
            

            if (Input.GetKey(KeyCode.Space))
            {

                time += Time.deltaTime / 0.5f;
                done = false;
            }
            else if (Input.GetKeyUp(KeyCode.Space))
            {
                done = true;
            }

            if (done)
            {
                time = Mathf.Lerp(time, 0, Time.deltaTime / 1.2f);
            }

            if (time < 0.05f && done == true)
            {
                time = 0;
            }
        }

        public void TransitionShader(float _time)
        {
            _time /= 5;
            float shininess = Mathf.Lerp(0, 1, _time * 0.35f);
            TransitionMaterial.SetFloat("_Cutoff", shininess);
        }



        /*void OnRenderImage(RenderTexture src, RenderTexture dst)
        {
            if (TransitionMaterial != null)
                Graphics.Blit(src, dst, TransitionMaterial);
        }*/

        public override void InitializingFTL()
        {
            distortionIn = true;
        }

        public override void SlowingDown()
        {
            distortionOut = true;
        }
    }
}