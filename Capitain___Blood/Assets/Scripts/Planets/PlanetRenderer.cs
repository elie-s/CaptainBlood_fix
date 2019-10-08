using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RetroJam.CaptainBlood.GalaxyLib;

namespace RetroJam.CaptainBlood
{
    public class PlanetRenderer : MonoBehaviour
    {
        [SerializeField] private Material material;
        [SerializeField] private Texture[] texture;
        [SerializeField] private MeshRenderer planetmesh;

        void Start()
        {
            material.mainTexture = texture[3];
        }

        public void ApplyRender(Planet _planet)
        {
            if(_planet.destroyed)
            {
                planetmesh.enabled = false;
            }
            else
            {
                if(!planetmesh.enabled) planetmesh.enabled = true;
                material.mainTexture = texture[(int)_planet.renderingValues.x];
                material.SetColor("_Color",ColorFromSeed((int)_planet.renderingValues.y));
                material.SetFloat("_HUEValue", HUEModif(_planet.renderingValues.y));

                Debug.Log("ApplyRenderer");
            }
            
        }

        private float HUEModif(float _seed)
        {
            return _seed/1000000;
        }
        private Color ColorFromSeed(int _seed)
        {
            float red = (Mathf.Floor(_seed / 10000) / 100) / 4 * 3 + .25f;
            float green = ((Mathf.Floor(_seed / 100) - Mathf.Floor(_seed / 10000) * 100) / 100)/ 4 * 3 + .25f;
            float blue = ((_seed - Mathf.Floor(_seed / 100) * 100) / 100)/ 4 * 3 + .25f;

            return new Color(red, green, blue);
        }
    }
}