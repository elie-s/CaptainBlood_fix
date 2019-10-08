using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RetroJam.CaptainBlood.GalaxyLib;

namespace RetroJam.CaptainBlood
{
    [CreateAssetMenu(fileName = "New Galaxy", menuName = "Galaxy")]
    public class GalaxySCO : ScriptableObject
    {
        public Dictionary<Vector2Int, Planet> galaxy = new Dictionary<Vector2Int, Planet>();
    }
}