using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    PlayerShip player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerShip>();
        Debug.Log(player.gameObject.name);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            player.Fire();
        }else if (Input.GetKeyDown("1"))
        {
            player.SwapAmmo(Ammo.AmmoType.ROUNDSHOT);
        }
        else if (Input.GetKeyDown("2"))
        {
            player.SwapAmmo(Ammo.AmmoType.CHAINSHOT);
        }
        else if (Input.GetKeyDown("3"))
        {
            player.SwapAmmo(Ammo.AmmoType.GRAPESHOT);
        }
    }
}
