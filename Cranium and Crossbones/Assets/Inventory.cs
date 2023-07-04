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


    Dictionary<string, int> ShipInventory;
    Dictionary<string, int> BaseInventory;

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
        text.text = "";
        foreach (string key in ShipInventory.Keys)
        {
            text.text += key + ": " + ShipInventory[key] + "\n";
        }
        text.text += "Total Cargo Weight: " + GetShipInventoryWeight();
    }

    #endregion

    #region Base Inventory

    public void TransferShipToBase()
    {
        foreach (string key in ShipInventory.Keys)
        {
            if (BaseInventory.ContainsKey(key))
            {
                BaseInventory[key] += ShipInventory[key];
            }
            else
            {
                BaseInventory.Add(key, ShipInventory[key]);
            }

            ShipInventory[key] = 0;
        }
    }

    public void GetBaseInventory(TMP_Text text)
    {
        text.text = "";
        foreach (string key in BaseInventory.Keys)
        {
            text.text += key + ": " + BaseInventory[key] + "\n"; 
        }

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
