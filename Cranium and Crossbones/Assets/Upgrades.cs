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
                instance.InitializeUpgrades();
            }

            return instance;
        }
    }
    public struct Upgrade {
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

        public Upgrade(string id) {
            upgradeID = id;
            upgradeName = "";
            upgradeSprite = "";
            upgradePrerequisite = "";
            woodCost = 0;
            clothCost = 0;
            gunCost = 0;
            doubloonCost = 0;
            hullModifier = 0;
            sailModifier = 0;
            manpowerModifier = 0;
            isFinalTier = false;
        }
    }

    public List<Upgrade> UpgradeList;

    private void InitializeUpgrades()
    {
        UpgradeList = new List<Upgrade>();
        UpgradeList = JSONParser.ParseUpgrades("Upgrades");
        Debug.Log(UpgradeList);
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
