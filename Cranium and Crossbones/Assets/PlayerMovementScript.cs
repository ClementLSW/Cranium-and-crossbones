using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Reporting;
using UnityEngine;

public class PlayerMovementScript : MonoBehaviour
{
    Rigidbody2D rb;
    PlayerShip player;
    float acceleration = 0.5f;
    Vector2 lookDir;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<PlayerShip>();
        rb = GetComponent<Rigidbody2D>();
        lookDir = - transform.up;
    }

    // Update is called once per frame
    void Update()
    {
        // Change Target Speed
        if (Input.GetKeyDown("w"))
        {
            IncreaseSpeed();
        }else if (Input.GetKeyDown("s"))
        {
            DecreaseSpeed();
        }else if (Input.GetKeyDown("a"))
        {
            // Rotate Counter Clockwise
        }else if (Input.GetKeyDown("d"))
        {
            // Rotate Clockwise
        }

        // Actual Speed will ramp up/down until reaching Target Speed
        if (Mathf.Abs(player.targetSpeed - player.actualSpeed) < acceleration) 
        {
            player.actualSpeed = player.targetSpeed;
        } else if (player.actualSpeed < player.targetSpeed)
        {
            player.actualSpeed += acceleration * Time.deltaTime;
        } else if (player.actualSpeed > player.targetSpeed)
        {
            player.actualSpeed -= acceleration * Time.deltaTime;
        }

        //Debug.Log("Actual Speed : " + player.actualSpeed);

        rb.velocity =  lookDir * player.actualSpeed;
    }

    void IncreaseSpeed()
    {
        Debug.Log("Increasing Speed");
        if(player.targetSpeed == 0.0f)
        {
            player.targetSpeed = player.GetMaxSpeed() / 2.0f;
        }else if(player.targetSpeed != player.GetMaxSpeed())
        {
            player.targetSpeed = player.GetMaxSpeed();
        }

        //Debug.Log(player.targetSpeed);
    }

    void DecreaseSpeed()
    {
        Debug.Log("Decreasing Speed");
        if (player.targetSpeed == player.GetMaxSpeed())
        {
            player.targetSpeed = player.GetMaxSpeed() / 2.0f;
        }
        else if (player.targetSpeed != 0)
        {
            player.targetSpeed = 0;
        }
        //Debug.Log(player.targetSpeed);
    }

    void RotateClockwise()
    {
        Debug.Log("Rotate Clockwise");
    }

    void RotateCounterClockwise()
    {
        Debug.Log("Rotate Counter Clockwise");
    }
}
