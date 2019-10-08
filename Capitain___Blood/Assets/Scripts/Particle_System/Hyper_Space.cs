using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RetroJam.CaptainBlood
{
    public class Hyper_Space : EventsManager
    {

        [SerializeField] ParticleSystem particleSystemStay;
        [SerializeField] ParticleSystem particleSystemLoop;
        [SerializeField] ParticleSystem particleSystemEnd;

        [SerializeField] public bool activated = false;
        [SerializeField] float timeWarp;



        // Start is called before the first frame update
        void Start()
        {
            particleSystemEnd.Pause();
        }

        // Update is called once per frame

        private void LateUpdate()
        {
            if (!activated)
            {
                StayStill();
            }
        }

        void Update()
        {

            if (activated)
            {
                Activation();



            }

        }


        void StayStill()
        {

             particleSystemStay.Pause();
             particleSystemLoop.Pause();




        }

        void Activation()
        {

            StartCoroutine(EndParticleEffect());

        }

        IEnumerator EndParticleEffect()
        {

            particleSystemStay.Play();
            particleSystemLoop.Emit(1);

            var main = particleSystemStay.main;
            if(!main.startDelay.Equals(10)) main.startDelay = 10;

            yield return new WaitForSeconds(timeWarp);

            if (particleSystemLoop.main.startLifetime.constant != 0)
            {
                var mainloop = particleSystemLoop.main;
                mainloop.startLifetime = 0;
            }

            yield return new WaitForSeconds(0.5f);

            particleSystemEnd.Play();

            var mainEnd = particleSystemEnd.main;
            mainEnd.startLifetime = 1;
            yield return new WaitForSeconds(1);
            mainEnd.startDelay = 10;
            GameManager.events.CallSlowingDown();

            yield return new WaitForSeconds(1);

            //mainEnd.startDelay = 10;
            activated = false;
            yield return new WaitForSeconds(3);
            Destroy(gameObject);

        }

        public override void InitializingFTL()
        {
            particleSystemLoop.Emit(1);
        }
    }
}
