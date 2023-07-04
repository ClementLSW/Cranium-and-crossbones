using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    GameObject target;
    Vector3 offset;

    private void Start() {
        target = GameObject.FindGameObjectWithTag("Player");
        offset = transform.position - target.transform.position;
    }


    void Update()
    {
        transform.position = offset + target.transform.position;
    }
}
