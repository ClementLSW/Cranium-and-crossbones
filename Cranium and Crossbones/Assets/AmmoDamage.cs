using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class AmmoDamage : MonoBehaviour
{
    public static AmmoDamage Instance
    {
        get; private set;
    }

    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
            initialize();
        }
    }

    int roundshotDamage, chainshotDamage, grapeshotDamage;
    public Dictionary<string, string> stats;


    // Start is called before the first frame update
    //void Start()
    //{
    //    initialize();      
    //}

    void initialize() {
        stats = JSONParser.ParseFromFile("AmmoDamageStats");
        roundshotDamage = int.Parse(stats["RoundShot"]);
        chainshotDamage = int.Parse(stats["ChainShot"]);
        grapeshotDamage = int.Parse(stats["GrapeShot"]);

        Debug.Log("R_Dmg: " + roundshotDamage);
        Debug.Log("C_Dmg: " + chainshotDamage); 
        Debug.Log("G_Dmg: " + grapeshotDamage);
    }

    public int getDamageValue(Ammo.AmmoType ammoType)
    {
        int i = 0;
        switch (ammoType)
        {
            case Ammo.AmmoType.ROUNDSHOT:
                i = roundshotDamage;
                break;
            case Ammo.AmmoType.CHAINSHOT:
                i = chainshotDamage;
                break;
            case Ammo.AmmoType.GRAPESHOT:
                i = grapeshotDamage;
                break;
        }

        return i;
    }

    private void updateDamageStats() {

    }
}
