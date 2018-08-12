using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    public float speed = 1;
    private void Start()
    {
        transform.rotation= Quaternion.Euler(new Vector3(90, 0, 0));
    }

    void Update()
    {
        transform.Rotate(Vector3.forward, speed *Time.deltaTime*100);
        transform.Rotate(Vector3.left, speed*Time.deltaTime*100);
    }
}
