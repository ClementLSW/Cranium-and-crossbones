using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    protected Vector3 dir;
    protected float projectileVelocity;
    protected bool isPlayerProjectile;
    protected int damage;
    public void SetDirectionVector(Vector3 target, bool isPlayer)
    {
        isPlayerProjectile = isPlayer;
        target.z = this.transform.position.z;       // Very importante
        dir = (target - this.transform.position).normalized;
        Debug.Log(dir);
        Debug.Log(dir.magnitude);
        gameObject.GetComponent<Rigidbody2D>().velocity = dir * projectileVelocity;
    }

    public void SetProjectileDamage(int dmg)
    {
        damage = dmg;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected void despawn()
    {
        Destroy(gameObject);
    }

    public enum AmmoType
    {
        ROUNDSHOT,
        CHAINSHOT,
        GRAPESHOT
    };
}
