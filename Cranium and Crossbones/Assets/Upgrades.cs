using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static Upgrades;

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
                instance.InitializeUpgrades();
            }

            return instance;
        }
    }

    public struct Upgrade
    {
        public string upgradeID;
        public string upgradeName;
        public string upgradeSprite;
        public string upgradePrerequisite;
        public int woodCost;
        public int clothCost;
        public int gunCost;
        public int doubloonCost;
        public int hullModifier;
        public int sailModifier;
        public int manpowerModifier;
        public bool isFinalTier;

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

        foreach (Upgrade u in UpgradeList)
        {
            Debug.Log(
                u.upgradeID +
                u.upgradeName +
                u.upgradeSprite +
                u.upgradePrerequisite +
                u.woodCost +
                u.clothCost +
                u.gunCost +
                u.doubloonCost +
                u.hullModifier +
                u.sailModifier +
                u.manpowerModifier +
                u.isFinalTier
            );
        }
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
