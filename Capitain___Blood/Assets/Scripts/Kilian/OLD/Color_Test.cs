using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Color_Test : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Renderer>().material.color = Random.ColorHSV(1f, 1f, 0.9f, 1f, 0.1f, 1f);

    }

    // Update is called once per frame
    void Update()
    {

    }
}
