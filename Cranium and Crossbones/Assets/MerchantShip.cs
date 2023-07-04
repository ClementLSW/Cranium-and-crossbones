using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MerchantShip : Ship
{
    void LoadShipStats()
    {
        stats = JSONParser.ParseFromFile("MerchantShipStats");

        Initialize(
            float.Parse(stats["speed"]),
            int.Parse(stats["hull"]),
            int.Parse(stats["sail"]),
            int.Parse(stats["manpower"])
            );
    }

    // Start is called before the first frame update
    void Start()
    {
        LoadShipStats();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
