using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundShot : MonoBehaviour
{
    private float decay;
    private Vector3 dir;
    private float projectileVelocity;
    private bool isPlayerProjectile;

    private void Awake()
    {
        decay = 5.0f;
        projectileVelocity = 2.0f;
    }
    public void SetDirectionVector(Vector3 target, bool isPlayer)
    {
        isPlayerProjectile = isPlayer;
        target.z = this.transform.position.z;       // Very importante
        dir = (target - this.transform.position).normalized;
        Debug.Log(dir);
        Debug.Log(dir.magnitude);
        gameObject.GetComponent<Rigidbody2D>().velocity = dir * projectileVelocity;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        Debug.Log("Collision with " + collision.gameObject.name);

        if(isPlayerProjectile ^ collision.gameObject.tag == "Player") {
            if (collision.gameObject.GetComponent<Ship>())
            {
                Ship ship = collision.gameObject.GetComponent<Ship>();
                ship.TakeHullDamage(20);
            }
            despawn();
        }
        else {
            return;
        }

    }

    private void Update()
    {
        if (decay <= 0)
        {
            despawn();
        }
        else
        {
            decay -= Time.deltaTime;
        }
    }

    void despawn()
    {
        Destroy(gameObject);
    }
}
