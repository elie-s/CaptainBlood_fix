using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RetroJam.CaptainBlood
{
    
    public class PlanetMovements : EventsManager
    {
        [SerializeField] private GameManager manager;
        [SerializeField] private float speedOfRotation;
        [SerializeField] private float framer;
        [SerializeField] CameShake shaking;

        private float buffer = 0;

        [SerializeField] private float graal;
        [SerializeField, Range(0, 1)] private float speed;
        private float time;

        System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();

        [SerializeField] private bool isArriving;
        [SerializeField] private bool isAccelerating;

        #region Destruction Variables
        enum DestroyingPhase {none, PlanetDissolve, Explosion}
        
        bool madeSound;
        bool destroying;
        [SerializeField]DestroyingPhase phase;
        float scaleWave=1;
        float scaleMask=.5f;

        [SerializeField] PlanetRenderer planetManager;
        [SerializeField] GameObject explosionWave;
        [SerializeField] Transform waveMask;
        [SerializeField] float explosionWaveSpeed;
        [SerializeField] float textureValue=0;
        [SerializeField] Material planetTexture;
        #endregion

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            //Lag();
            //TransformManager();

            Movement();
            ExplosionManager();

            DebugInput();

            if (isArriving && !isAccelerating) PlanetArrival();
            else if (isAccelerating && !isArriving) PlanetDeparture();
        }

        public void Lag()
        {
            buffer += Time.deltaTime;

            if (buffer > framer)
            {
                transform.Rotate(Vector3.up, speedOfRotation);
                TransformManager();
                buffer = 0;
            }
        }

        public void Movement()
        {
            transform.Rotate(Vector3.up, speedOfRotation);

            TransformManager();
        }

        public void TransformManager()
        {
            transform.localPosition = new Vector2(-4 * graal, graal);
            transform.localScale = new Vector3(4 * graal, 4 * graal, 4 * graal);
        }

#region Arrival/Departure methods
        public void PlanetArrival()
        {
            time += Time.deltaTime * speed;

            graal = Mathf.Clamp((Mathf.Log10(time) + 1.5f) / 1.5f, 0, 1);

            if(graal == 1)
            {
                time = 0;
                isArriving = false;
                Cursor.blocked = false;
                sw.Stop();
                UnityEngine.Debug.Log("duration: "+sw.ElapsedMilliseconds+"ms.");
            }
        }

        public void PlanetDeparture()
        {
            time += Time.deltaTime * 3 *.2f;

            graal = Mathf.Clamp(Mathf.Pow(time,2.5f)+1, 1, 4);

            /*if (graal > 3)
            {
                GameManager.events.CallFTLDistortionIn();
                GameManager.events.CallStartFTL();
            }*/

            if (graal == 4)
            {
                time = 0;
                isAccelerating = false;
                graal = 0;
                GameManager.events.CallFTLDistortionIn();
                GameManager.events.CallStartFTL();
            }
        }
#endregion
        public void DebugInput()
        {
            if(Input.GetKeyDown(KeyCode.F)&&!isAccelerating&&!isArriving)
            {
                if (graal == 0) isArriving = true;
                else isAccelerating = true;
            }
        }
#region Destroying Planet Methods
        public void ExplosionManager()
        {
            switch (phase)
            {
                case DestroyingPhase.none:

                    break;
                case DestroyingPhase.PlanetDissolve:
                    TextureEffects();
                    break;
                case DestroyingPhase.Explosion:
                    ShockWave();
                    break;
            }
        }
        public void ShockWave()
        {
            if(!explosionWave.activeSelf) explosionWave.SetActive(true);
            scaleWave+=Time.deltaTime*explosionWaveSpeed;
            explosionWave.transform.localScale =new Vector3(scaleWave, scaleWave, scaleWave);

            if(scaleWave > 4.5f) textureValue += Time.deltaTime/scaleMask;
            planetTexture.SetFloat("_Dissolve", textureValue);

            if(scaleWave > 4 && !madeSound) 
            {
                GameManager.events.CallPlayDestroySound();
                madeSound = true;
            }
            
            if(scaleWave>7.5f) scaleMask += Time.deltaTime*(scaleMask/2);
            waveMask.localScale = new Vector3(scaleMask, scaleMask, scaleMask);

            if(scaleMask > 1)
            {
                planetManager.ApplyRender(manager.currentPlanet);

                scaleMask = .5f;
                scaleWave = 1;
                explosionWave.SetActive(false);
                phase = DestroyingPhase.none;
                madeSound = false;

                planetTexture.SetColor("_Color_Dissolve", Color.black);
                planetTexture.SetFloat("_Bordure", 0);
                planetTexture.SetFloat("_Dissolve", 0);
                textureValue = 0;
            }
        }

        public void TextureEffects()
        {   
            textureValue += Time.deltaTime/2;

            Color currentColor = planetTexture.GetColor("_Color_Dissolve");
            planetTexture.SetColor("_Color_Dissolve", Color32.Lerp(Color.black, Color.white, textureValue));
            planetTexture.SetFloat("_Bordure", textureValue);

            if(textureValue > .05f && textureValue < .65f)
            {
                if(!explosionWave.activeSelf) explosionWave.SetActive(true);
                scaleWave+=Time.deltaTime*explosionWaveSpeed*2;
                explosionWave.transform.localScale =new Vector3(scaleWave, scaleWave, scaleWave);
                if(scaleWave>5.5f) scaleMask += textureValue-.05f;
                waveMask.localScale = new Vector3(scaleMask, scaleMask, scaleMask);
            }
            if( scaleMask > 1.5f || textureValue > .65f)
            {
                explosionWave.SetActive(false);
                scaleMask = .5f;
                scaleWave = 1; 
            }



            if(textureValue > 1)
            {
                scaleMask = .5f;
                scaleWave = 1;
                explosionWave.SetActive(false);
                textureValue = 0;
                phase = DestroyingPhase.Explosion;
            }
        }
#endregion
        public override void InitializingFTL()
        {
            Debug.Log("Starting Acceleration");
            isAccelerating = true;
            sw.Start();
            //StartCoroutine(shaking.Shake(17.5f, .05f));
        }

        public override void SlowingDown()
        {
            Debug.Log("Starting slowing down");
            isArriving = true;
        }

        public override void DeathStarBehave()
        {
            phase = DestroyingPhase.PlanetDissolve;
        }

    }
}