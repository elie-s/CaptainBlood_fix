using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationCurveStuff : MonoBehaviour
{
    public int numberOfKeys;
    public float keySpeed;

    public MovingAnimationCurve movingCurve;
    //public AnimationCurve curve = AnimationCurve.EaseInOut(0,0,1,1);

    KeyframeBehaviour[] keyframeBehaviours;

    public void Start()
    {
        movingCurve = new MovingAnimationCurve();
        movingCurve.curve = GetRandomCurve(numberOfKeys);
        movingCurve.behaviours = new KeyframeBehaviour[numberOfKeys];
        for (int i=0; i<numberOfKeys; i++)
        {
            movingCurve.behaviours[i] = new KeyframeBehaviour();
            movingCurve.behaviours[i].speed = keySpeed;
            movingCurve.behaviours[i].index = i;
        }


    }

    public AnimationCurve GetRandomCurve(int howManyPoints)
    {
        if (howManyPoints < 2) return AnimationCurve.EaseInOut(0,0,1,1);

        Keyframe[] keys = new Keyframe[howManyPoints];

        keys[0] = new Keyframe(0, 0, 0, 0);
        keys[keys.Length-1] = new Keyframe(1, 0, 0, 0);

        if (howManyPoints > 2)
            for (int i = 1; i < keys.Length-1; i++)
                keys[i] = new Keyframe(Random.value, Random.value*2f-1f, 0, 0);

        AnimationCurve result = new AnimationCurve(keys);

        return result;
    }

    void Update()
    {
        movingCurve.Update();
    }

}

[System.Serializable]
public struct MovingAnimationCurve
{
    public AnimationCurve curve;
    public KeyframeBehaviour[] behaviours;

    public void Update()
    {
        Keyframe[] keys = new Keyframe[behaviours.Length]; 
        for (int i=0; i < behaviours.Length; i++)
        {
            if (i == 0 || i == behaviours.Length-1)
            {
                keys[i] = new Keyframe(curve.keys[i].time, curve.keys[i].value, 0, 0);
                continue;
            }
            behaviours[i].Update();
            keys[i] = new Keyframe (curve.keys[i].time, curve.keys[i].value + behaviours[i].speed * Time.deltaTime, 0, 0);
        } 
        
        curve = new AnimationCurve(keys);
    }
}

[System.Serializable]
public struct KeyframeBehaviour
{
    public float speed;
    public float timerUntilSpeedChange;

    public int index;

    public void Update()
    {
        timerUntilSpeedChange -= Time.deltaTime;

        if (timerUntilSpeedChange < 0)
        {
            timerUntilSpeedChange = Random.Range(2f, 4f);
            speed *= -1;
            Debug.Log("change // "+index.ToString());
        }

    }
}
