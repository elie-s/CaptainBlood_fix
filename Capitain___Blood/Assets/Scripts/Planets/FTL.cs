using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RetroJam.CaptainBlood
{
    public class FTL : EventsManager
    {
        [SerializeField] private Object ftl;

        bool once = true;

        // Start is called before the first frame update
        void Start()
        {
            Instantiate(ftl, transform);
        }

        // Update is called once per frame
        void Update()
        {
            
        }

        public override void SlowingDown()
        {
            if (once)
            {
                Instantiate(ftl, transform);
                once = false;
            }
        }

        public override void StartFTL()
        {
            once = true;
            transform.Find("FTL(Clone)").GetComponent<Hyper_Space>().activated = true;
        }

    }
}