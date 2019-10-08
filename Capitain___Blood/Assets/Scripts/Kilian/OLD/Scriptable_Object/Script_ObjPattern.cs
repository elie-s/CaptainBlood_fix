using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RetroJam.CaptainBlood
{

    [CreateAssetMenu(fileName = "NewTerrainPattern", menuName = "Terrain_Pattern")]
    public class Script_ObjPattern : ScriptableObject
    {
        public int depth;
        public float multiplicateur;
    }
}
