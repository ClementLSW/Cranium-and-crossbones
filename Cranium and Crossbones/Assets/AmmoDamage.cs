using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class AmmoDamage : MonoBehaviour
{
    float roundshotDamage, chainshotDamage, grapeshotDamage;
    public Dictionary<string, string> stats;


    // Start is called before the first frame update
    void Start()
    {
        initialize();      
    }

    void initialize() {
        stats = JSONParser.ParseFromFile("AmmoDamageStats");
        roundshotDamage = float.Parse(stats["RoundShot"]);
        chainshotDamage = float.Parse(stats["ChainShot"]);
        grapeshotDamage = float.Parse(stats["GrapeShot"]);

        Debug.Log("R_Dmg: " + roundshotDamage);
        Debug.Log("C_Dmg: " + chainshotDamage); 
        Debug.Log("G_Dmg: " + grapeshotDamage);
    }

    public void updateDamageStats() {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
