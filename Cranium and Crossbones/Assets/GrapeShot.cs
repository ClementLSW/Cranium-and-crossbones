using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GrapeShot : MonoBehaviour
{

    private float decay;
    private Vector3 dir;
    private float projectileVelocity;

    private void Awake()
    {
        decay = 3.0f;
        projectileVelocity = 2.0f;
    }

    public void SetDirectionVector(Vector3 target)
    {
        target.z = this.transform.position.z;       // Very importante
        dir = (target - this.transform.position).normalized;
        Debug.Log(dir);
        Debug.Log(dir.magnitude);
        gameObject.GetComponent<Rigidbody2D>().velocity = dir * projectileVelocity;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Ship>())
        {
            Ship ship = collision.gameObject.GetComponent<Ship>();
            ship.TakeManpowerDamage(10);
        }
        else
        {
            despawn();
        }
    }

    private void Update()
    {
        if(decay <= 0)
        {
            despawn();
        }
        else
        {
            decay -= Time.deltaTime;
            //transform.Translate(dir * projectileVelocity * Time.deltaTime);
        }
    }

    void despawn()
    {
        Destroy(gameObject);
    }
}
