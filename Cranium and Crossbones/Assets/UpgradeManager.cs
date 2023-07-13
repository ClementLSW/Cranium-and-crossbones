using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class UpgradeManager : MonoBehaviour {

    #region Singleton
    private UpgradeManager(){}
    public static UpgradeManager instance;
    //public static UpgradeManager Instance {
    //    get {
    //        if (instance == null) {
    //            instance = new UpgradeManager();
    //        }
    //        else {
    //            Destroy(instance);
    //            instance = new UpgradeManager();
    //        }
    //        return instance;
    //    }
    //}
    #endregion

    Upgrades upgrades;
    List<Upgrades.Upgrade> upgrades_purchased = new List<Upgrades.Upgrade>();
    List<Upgrades.Upgrade> availableUpgrades = new List<Upgrades.Upgrade>();
    string best_hull_mod = "", best_sail_mod = "", best_manpower_mod = "";

    bool upgradeMenuIsActive;

    [SerializeField]
    GameObject UpgradeMenu;
    [SerializeField]
    private TextMeshProUGUI[] ResourceValues;
    [SerializeField]
    private Image[] UpgradeSprites;
    [SerializeField]
    private TextMeshProUGUI[] UpgradeNameTexts;
    [SerializeField]
    private TextMeshProUGUI[] UpgradeCostTexts;
    [SerializeField]
    private Button[] UpgradeButtons;
    private void Awake() {
        if (instance == null) {
        instance = this;
        }

    }
    private void Start()
    {
        upgradeMenuIsActive = false;
        upgrades = Upgrades.Instance;
        List<Upgrades.Upgrade> availableUpgrades = GetUpgradeAvailable();
    }
    
    private void GetUpgradesPurchased() {
        Dictionary<string, string> upgradePurchased = new Dictionary<string, string>();
        upgradePurchased = JSONParser.ParseFromFile("upgrades_purchased");

        foreach(string key in upgradePurchased.Keys) {
            if (upgradePurchased[key] == "TRUE") {
                upgrades_purchased.Add(upgrades.UpgradeList.Find(x => x.upgradeID == key));

                string[] identifier = key.Split("_");
                switch (identifier[1]) {
                    case "s":
                        if (best_sail_mod == "") {
                            best_sail_mod = key;
                        }else if (int.Parse(identifier[2]) > int.Parse(best_sail_mod.Split("_")[2])) {
                            best_sail_mod = key;
                        }
                        break;
                    case "h":
                        if (best_hull_mod == "") {
                            best_hull_mod = key;
                        }
                        else if (int.Parse(identifier[2]) > int.Parse(best_hull_mod.Split("_")[2])) {
                            best_hull_mod = key;
                        }
                        break;
                    case "c":
                        if (best_manpower_mod == "") {
                            best_manpower_mod = key;
                        }
                        else if (int.Parse(identifier[2]) > int.Parse(best_manpower_mod.Split("_")[2])) {
                            best_manpower_mod = key;
                        }
                        break;
                    default:
                        Debug.LogError("Something went very wrong");
                        break;
                }
            }
        }

        foreach (Upgrades.Upgrade upgrade in upgrades_purchased) {
            Debug.Log(upgrade.upgradeName);
        }
    }

    private void PurchaseUpgrade(Upgrades.Upgrade upgrade) {

        List<(int resource, int qty)> values = new List<(int resource, int qty)>();
        if(upgrade.woodCost != 0) {
            values.Add((0, upgrade.woodCost));
        }
        if (upgrade.clothCost != 0) {
            values.Add((1, upgrade.clothCost));
        }
        if (upgrade.gunCost != 0) {
            values.Add((2, upgrade.gunCost));
        }
        if (upgrade.doubloonCost != 0) {
            values.Add((3, upgrade.doubloonCost));
        }


        if (Inventory.Instance.ConsumeResource(values)) {
            upgrades_purchased.Add(upgrade);
            UpdateResourceBar();
        }
        else {
            Debug.Log("Insufficient Resources");
        }
        
    }

    private void SaveUpgrades() {
        Dictionary<string, string> output = new Dictionary<string, string>();
        foreach(Upgrades.Upgrade up in upgrades_purchased) {
            output.Add(up.upgradeID, "TRUE");
        }

        JSONParser.WriteToFile("upgrades_purchased", output);
    }

    public Dictionary<string, int> ApplyUpgrades() {
        int hull_mod = 0;
        int sail_mod = 0;
        int manpower_mod = 0;

        foreach (Upgrades.Upgrade u in upgrades_purchased) {
            hull_mod += u.hullModifier;
            sail_mod += u.sailModifier;
            manpower_mod += u.manpowerModifier;
        }

        Dictionary<string, int> mods = new Dictionary<string, int> {
            { "hull_mod", hull_mod },
            { "sail_mod", sail_mod },
            { "manpower_mod", manpower_mod }
        };

        return mods;
    }

    public List<Upgrades.Upgrade> GetUpgradeAvailable() {
        List<Upgrades.Upgrade> availableUpgrades = new List<Upgrades.Upgrade>();

        Upgrades upgrades = Upgrades.Instance;

        if(best_hull_mod == "") {
            availableUpgrades.Add(upgrades.UpgradeList.Find(x => x.upgradeID == "u_h_1"));
        }
        else {
            availableUpgrades.Add(upgrades.UpgradeList.Find(x => x.upgradePrerequisite == best_hull_mod));
        }

        if (best_sail_mod == "") {
            availableUpgrades.Add(upgrades.UpgradeList.Find(x => x.upgradeID == "u_s_1"));
        }
        else {
            availableUpgrades.Add(upgrades.UpgradeList.Find(x => x.upgradePrerequisite == best_sail_mod));
        }

        if (best_manpower_mod == "") {
            availableUpgrades.Add(upgrades.UpgradeList.Find(x => x.upgradeID == "u_c_1"));
        }
        else {
            availableUpgrades.Add(upgrades.UpgradeList.Find(x => x.upgradePrerequisite == best_manpower_mod));
        }

        return availableUpgrades;
    }

    public void ToggleUpgradeMenu() {
        if(upgradeMenuIsActive) {
            UpgradeMenu.SetActive(false);
        }
        else {
            RenderShopScreen();
        }
    }

    public void RenderShopScreen() {
        availableUpgrades = GetUpgradeAvailable();

        for (int i = 0; i< availableUpgrades.Count; i++) {
            Upgrades.Upgrade currUpgrade = availableUpgrades[i];
            Debug.Log("WHADAHEAIL " + currUpgrade.upgradeName);

            // Load Upgrade Sprites
            // UpgradeSprites[i].sprite = currUpgrade.upgradeSprite

            // Janky way of loading upgrade costs
            /*switch (i) {
                case 0:
                    Upgrade1NameTexts.text = currUpgrade.upgradeName;
                    break;
                case 1:
                    Upgrade2NameTexts.text = currUpgrade.upgradeName;
                    break;
                case 2:
                    Upgrade3NameTexts.text = currUpgrade.upgradeName;
                    break;
            }*/
            
            UpgradeNameTexts[i].text = currUpgrade.upgradeName;

            string upgradeCostTemp = "";
            if(currUpgrade.woodCost != 0) {
                upgradeCostTemp += "Wood: " + currUpgrade.woodCost + "\n";
            }
            if (currUpgrade.clothCost != 0) {
                upgradeCostTemp += "Cloth: " + currUpgrade.clothCost + "\n";
            }
            if (currUpgrade.gunCost != 0) {
                upgradeCostTemp += "Guns: " + currUpgrade.gunCost + "\n";
            }
            if (currUpgrade.doubloonCost != 0) {
                upgradeCostTemp += "Doubloons: " + currUpgrade.doubloonCost;
            }
            UpgradeCostTexts[i].GetComponent<TextMeshProUGUI>().text = upgradeCostTemp;
        }
        UpdateResourceBar();
        UpgradeMenu.SetActive(true);
    }

    public void BuyUpgrade(int i) {
        PurchaseUpgrade(availableUpgrades[i]);
    }

    private void UpdateResourceBar() {
        ResourceValues[0].text = Inventory.Instance.BaseInventory["Wood"].ToString();
        ResourceValues[1].text = Inventory.Instance.BaseInventory["Cloth"].ToString();
        ResourceValues[2].text = Inventory.Instance.BaseInventory["Guns"].ToString();
        ResourceValues[3].text = Inventory.Instance.BaseInventory["Doubloons"].ToString();
    }
}
