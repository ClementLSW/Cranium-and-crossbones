using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Upgrades : MonoBehaviour
{
    private Upgrades() { }
    private static Upgrades instance = null;

    public static Upgrades Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new Upgrades();
            }

            return instance;
        }
    }

    public struct Upgrade
    {
        string upgradeID;
        string upgradeName;
        string upgradeSprite;
        string upgradePrerequisite;
        int woodCost;
        int clothCost;
        int gunCost;
        int doubloonCost;
        int hullModifier;
        int sailModifier;
        int manpowerModifier;
        bool isFinalTier;

        public Upgrade(KeyValuePair<string, string> upgradeDict)
        {
            upgradeID = upgradeDict.Key;

            Dictionary<string, string> details = JSONParser.ParseFromJSONString(upgradeDict.Value);

            upgradeName = details["upgradeName"];
            upgradeSprite = details["upgradeSprite"];
            if (details["upgradePreRequisite"] == "NULL")
            {
                upgradePrerequisite = null;
            }
            else
            {
                upgradePrerequisite = details["upgradePreRequisite"];
            }
            woodCost = int.Parse(details["woodCost"]);
            clothCost = int.Parse(details["clothCost"]);
            gunCost = int.Parse(details["gunCost"]);
            doubloonCost = int.Parse(details["doubloonCost"]);
            hullModifier = int.Parse(details["hullModifier"]);
            sailModifier = int.Parse(details["sailModifier"]);
            manpowerModifier = int.Parse(details["manpowerModifier"]);
            isFinalTier = (details["finalTier"] == "TRUE") ? true : false;
        }
    }

    public List<Upgrade> UpgradeList;

    private void InitializeUpgrades()
    {
        UpgradeList = new List<Upgrade>();
        UpgradeList = JSONParser.ParseUpgrades("Upgrades");
        //Dictionary<string, string> upgradeDict = JSONParser.ParseFromFile("Upgrades");
        //KeyValuePair<string, string> kvp = new KeyValuePair<string, string>() { ""}

        //Upgrade u = new Upgrade(upgradeDict);


    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
