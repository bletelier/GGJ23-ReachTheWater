using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointerBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.position = (Vector2)Camera.main.ScreenToViewportPoint(Input.mousePosition);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
}
