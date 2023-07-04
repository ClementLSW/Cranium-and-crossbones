using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainShot : Ammo
{   
    private float decay;

    private void Awake()
    {
        decay = 3.0f;
        projectileVelocity = 2.0f;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(isPlayerProjectile ^ collision.gameObject.tag == "Player")
        {
            if (collision.gameObject.GetComponent<Ship>())
            {
                Ship ship = collision.gameObject.GetComponent<Ship>();
                if(damage > 0)
                {
                    ship.TakeSailDamage(damage);
                }
                else
                {
                    Debug.Log("Damage is not set properly. Current value: " + damage.ToString());
                }

            }
            despawn();
        }
        else
        {
            return;
        }
    }

    // Update is called once per frame
    void Update()
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
}
