using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RetroJam.CaptainBlood
{
    [System.Serializable,CreateAssetMenu(menuName ="FBM", fileName ="New FBM")]
    public class FBMValues : ScriptableObject
    {
        [SerializeField, Range(0, 10)] public int octaves = 10;
        [SerializeField, Range(0, 10)] public float lacunarity = 2.640f;
        [SerializeField, Range(-10, 10)] public float gain = -0.360f;
        [SerializeField, Range(0, 1f)] public float amplitude = 0.952f;
        [SerializeField, Range(0, 1000)] public float frequency = 0.08f;
        [SerializeField, Range(0, 1)] public float factorY;
        [SerializeField, Range(0, 1024)] public float factorX;
        [SerializeField, Range(0, 1024)] public float Factor;

    }
}