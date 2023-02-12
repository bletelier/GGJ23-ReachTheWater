using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveBehaviour : MonoBehaviour
{
    private float lifeTime;
    void Start()
    {
        lifeTime = Time.time + 0.5f;
    }

    void Update()
    {
        if(Time.time > lifeTime) Destroy(gameObject);
    }
}
