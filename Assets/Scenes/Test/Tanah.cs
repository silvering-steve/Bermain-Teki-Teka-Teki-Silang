using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tanah : MonoBehaviour
{
    public float speed;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.left * (speed * Time.deltaTime));
    }
}
