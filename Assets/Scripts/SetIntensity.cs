using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetIntensity : MonoBehaviour
{
    private float[] intensities = {1.0f,0.66f,0.33f,0.0f,0.0f};
    public float intensity;
    private int id = 0;

    void Awake(){
        intensity = intensities[id];
    }

    public void nextIntensity(){
        id+=1;
        intensity = intensities[id];
    }
}
