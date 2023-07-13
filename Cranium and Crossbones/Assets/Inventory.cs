using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Inventory : MonoBehaviour
{
    private Inventory() { }
    private static Inventory instance = null;

    public static Inventory Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new Inventory();
            }

            return instance;
        }
    }


    public Dictionary<string, int> ShipInventory;
    public Dictionary<string, int> BaseInventory;

    TMP_Text ship_inv_text;
    TMP_Text base_inv_text;

    # region Init

    public void Init()
    {
        ShipInventory = new Dictionary<string, int>
        {
            { "Wood", 0 },
            { "Cloth", 0 },
            { "Guns", 0 },
            { "Doubloons", 0 }
        };

        try
        {
            loadBaseInventory();
        }
        catch (System.Exception)
        {
            Debug.Log("Cannot Load Base Inventory from file");
            throw;
        }
        
    }

    private void loadBaseInventory()
    {
        Dictionary<string, string> Temp = new Dictionary<string, string>();
        BaseInventory = new Dictionary<string, int>();

        Temp = JSONParser.ParseFromFile("BaseInventory");

        foreach(string key in Temp.Keys)
        {
            BaseInventory.Add(key, int.Parse(Temp[key]));
        }
    }

    #endregion


    #region Ship Inventory

    public void AddResource((int resource, int qty) values)
    {
        switch (values.resource)
        {
            case 0:
                ShipInventory["Wood"] += values.qty; 
                break;
            case 1:
                ShipInventory["Cloth"] += values.qty;
                break;
            case 2:
                ShipInventory["Guns"] += values.qty;
                break;
        }
    }

    public void DiscardResource((int resource, int qty) values)
    {
        switch (values.resource)
        {
            case 0:
                if(ShipInventory["Wood"] > values.qty)
                {
                    ShipInventory["Wood"] -= values.qty;
                }
                else
                {
                    ShipInventory["Wood"] = 0;
                }
                break;
            case 1:
                if (ShipInventory["Cloth"] > values.qty)
                {
                    ShipInventory["Cloth"] -= values.qty;
                }
                else
                {
                    ShipInventory["Cloth"] = 0;
                }
                break;
            case 2:
                if (ShipInventory["Guns"] > values.qty)
                {
                    ShipInventory["Guns"] -= values.qty;
                }
                else
                {
                    ShipInventory["Guns"] = 0;
                }
                break;
        }
    }

    public void DebugSpawnResourceInShipInventory(int resource/*, int qty*/)
    {
        switch (resource)
        {
            case 0:
                if (ShipInventory.ContainsKey("Wood"))
                {
                    ShipInventory["Wood"]++;
                    //ShipInventory["Wood"] += qty;
                }
                else
                {
                    ShipInventory.Add("Wood", 1);
                    //ShipInventory.Add("Wood", qty);
                }
                break;
            case 1:
                if (ShipInventory.ContainsKey("Cloth"))
                {
                    ShipInventory["Cloth"]++;
                    //ShipInventory["Cloth"] += qty;
                }
                else
                {
                    ShipInventory.Add("Cloth", 1);
                    //ShipInventory.Add("Cloth", qty);
                }
                break;
            case 2:
                if (ShipInventory.ContainsKey("Guns"))
                {
                    ShipInventory["Guns"]++;
                    //ShipInventory["Guns"] += qty;
                }
                else
                {
                    ShipInventory.Add("Guns", 1);
                    //ShipInventory.Add("Guns", qty);
                }
                break;
            //case 3:
            //    if (ShipInventory.ContainsKey("Doubloons"))
            //    {
            //        ShipInventory["Doubloons"] += qty;
            //    }
            //    else
            //    {
            //        ShipInventory.Add("Doubloons", qty);
            //    }
            //    break;
        }
    }

    public void AddDoubloonToShip(int qty)
    {
        if (ShipInventory.ContainsKey("Doubloons"))
        {
            ShipInventory["Doubloons"] += qty;
        }
        else
        {
            ShipInventory.Add("Doubloons", qty);
        }
    }

    public int GetShipInventoryWeight()
    {
        int sum = 0;
        foreach (string key in ShipInventory.Keys)
        {
            if(key != "Doubloons")
            {
                sum += ShipInventory[key];
            }
        }

        return sum;
    }

    public void GetShipInventory(TMP_Text text)
    {
        ship_inv_text = text;

        text.text = "";
        foreach (string key in ShipInventory.Keys)
        {
            text.text += key + ": " + ShipInventory[key] + "\n";
        }
        text.text += "Total Cargo Weight: " + GetShipInventoryWeight();
    }

    #endregion

    #region Base Inventory

    public bool ValidateResourceAvailability(List<(int resource, int qty)> values)
    {
        foreach ((int resource, int qty) value in values)
        {
            switch (value.resource)
            {
                case 0:
                    if (BaseInventory["Wood"] < value.qty)
                    {
                        return false;
                    }
                    break;
                case 1:
                    if (BaseInventory["Cloth"] < value.qty)
                    {
                        return false;
                    }
                    break;
                case 2:
                    if (BaseInventory["Guns"] < value.qty)
                    {
                        return false;
                    }
                    break;
                case 3:
                    if (BaseInventory["Doubloons"] < value.qty) {
                        return false;
                    }
                    break;
            }
        }
        return true;
    }

    public bool ConsumeResource(List<(int resource, int qty)> values)
    {
        if (ValidateResourceAvailability(values)) {
            foreach ((int resource, int qty) value in values)
            {
                switch (value.resource)
                {
                    case 0:
                        BaseInventory["Wood"] -= value.qty;
                        break;
                    case 1:
                        BaseInventory["Cloth"] -= value.qty;
                        break;
                    case 2:
                        BaseInventory["Guns"] -= value.qty;
                        break;
                    case 3:
                        BaseInventory["Doubloons"] -= value.qty;
                        break;
                }
            }
            return true;
        }
        else {
            return false;
        }
        
    }

    public void TransferShipToBase()
    {
        foreach (string key in ShipInventory.Keys)
        {
            if (BaseInventory.ContainsKey(key))
            {
                Debug.Log("Transferring " + ShipInventory[key] + " " + key + " to base.");
                BaseInventory[key] += ShipInventory[key];
            }
        }

        ShipInventory = new Dictionary<string, int>
        {
            { "Wood", 0 },
            { "Cloth", 0 },
            { "Guns", 0 },
            { "Doubloons", 0 }
        };

        GetShipInventory(ship_inv_text);
        DebugGetBaseInventory(base_inv_text);
    }

    public void DebugGetBaseInventory(TMP_Text text)
    {
        base_inv_text = text;

        text.text = "";
        foreach (string key in BaseInventory.Keys)
        {
            text.text += key + ": " + BaseInventory[key] + "\n"; 
        }
    }

    public void SaveBaseInventory()
    {
        Dictionary<string, string> output = new Dictionary<string, string>();

        foreach(string key in BaseInventory.Keys)
        {
            output.Add(key, BaseInventory[key].ToString());
        }

        JSONParser.WriteToFile("BaseInventory", output);

        Debug.Log("Saved Base Inventory");
    }

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
