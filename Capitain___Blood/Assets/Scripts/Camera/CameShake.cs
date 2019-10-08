using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RetroJam.CaptainBlood
{
    public class CameShake : EventsManager
    {
        public IEnumerator Shake(float _duration, float _magnitude)
        {
            Vector3 originalPos = transform.localPosition;

            float elapsed = 0.0f;
            while (elapsed < _duration)
            {
                float x = Random.Range(-1,1) *_magnitude;
                float y = Random.Range(-1,1) *_magnitude;

                transform.localPosition = new Vector3(x, y, originalPos.z);

                elapsed += Time.deltaTime;

                yield return null;
            }

            transform.localPosition = originalPos;
        }


        
    }
}