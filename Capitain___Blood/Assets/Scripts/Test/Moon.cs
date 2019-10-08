using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RetroJam.CaptainBlood
{
    public class Moon : MonoBehaviour
    {
        [SerializeField] GameObject planet;
        [SerializeField] Vector3 axis;
        [SerializeField] float angle;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            Orbit();
        }

        public void Orbit()
        {
            transform.RotateAround(planet.transform.position, axis, angle);
        }
    }
}