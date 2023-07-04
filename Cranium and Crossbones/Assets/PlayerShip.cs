using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PlayerShip : Ship
{
    const float COOLDOWN = 3.0f;

    public float actualSpeed;
    public float targetSpeed;

    public float maxRotationValue = 450.0f;
    public float currentRotationValue = 0.0f;

    int wood;
    public Dictionary<string, string> inv;

    AmmoType currentAmmoType;

    [SerializeField] private Transform[] selectedCannon,portCannonFireTransform, starboardCannonFireTransform;
    private float portCannonCooldown, starboardCannonCooldown;

    [SerializeField] private GameObject roundShotPrefab, chainShotPrefab, grapeShotPrefab;

    [SerializeField] private Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        LoadShipStats("Player");
        Debug.Log("Speed: " + m_speed);
        Debug.Log("Hull: " + m_hull_integrity);
        Debug.Log("Sail: " + m_sail_integrity);
        Debug.Log("Manpower: " + m_manpower);

        actualSpeed = 0.0f;
        targetSpeed = 0.0f;

        currentAmmoType = AmmoType.ROUNDSHOT;
    }

    // Update is called once per frame
    void Update()
    {
        if(portCannonCooldown > 0.0f)
        {
            portCannonCooldown -= Time.deltaTime;
        }

        if(starboardCannonCooldown > 0.0f)
        {
            starboardCannonCooldown -= Time.deltaTime;
        }
    }

    //void LoadShipStats()
    //{
    //    //using (StreamReader r = new StreamReader("assets/data/PlayerShipStats.json"))
    //    //{
    //    //    string json = r.ReadToEnd();
    //    //    stats = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
    //    //}

    //    stats = JSONParser.ParseFromFile("assets/data/PlayerShipStats.json");

    //    Initialize(
    //        float.Parse(stats["speed"]), 
    //        int.Parse(stats["hull"]),
    //        int.Parse(stats["sail"]),
    //        int.Parse(stats["manpower"])
    //        );
    //}

    void SaveShipStats()
    {
        stats["speed"] = m_speed.ToString();
        stats["hull"] = m_hull_integrity.ToString();
        stats["sail"] = m_sail_integrity.ToString();
        stats["manpower"] = m_manpower.ToString();

        using(StreamWriter w = new StreamWriter("assets/data/PlayerShipStats.json"))
        {
            string json = JsonConvert.SerializeObject(stats);
            w.WriteLine(json);
        }
    }

    void loadInventory()
    {
        using (StreamReader r = new StreamReader("assets/data/PlayerShipInventory.json"))
        {
            string json = r.ReadToEnd();
            inv = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
        }

        wood = int.Parse(inv["wood"]);
    }
    void saveInventory()
    {
        inv["wood"] = wood.ToString();
        using (StreamWriter w = new StreamWriter("assets/data/PlayerShipInventory.json"))
        {
            string json = JsonConvert.SerializeObject(inv);
            w.WriteLine(json);
        }
    }

    public float GetMaxSpeed()
    {
        return m_speed;
    }

    public void Fire()
    {
        // Get mouse position to determine which cannons to fire
        Vector3 target = cam.ScreenToWorldPoint(Input.mousePosition);

        // If on the right of ship
        if (transform.InverseTransformPoint(target).x - transform.localPosition.x < 0)
        {
            Debug.Log("Firing Starboard");
            // Check Cooldown
            if(starboardCannonCooldown > 0)
            {
                return;
            }
            selectedCannon = starboardCannonFireTransform;
            starboardCannonCooldown = COOLDOWN;

        }
        // If on the left of ship
        else if (transform.InverseTransformPoint(target).x - transform.localPosition.x > 0)
        {
            Debug.Log("Firing Port");
            // Check Cooldown
            if (portCannonCooldown > 0)
            {
                return;
            }
            selectedCannon = portCannonFireTransform;
            portCannonCooldown = COOLDOWN;
        }

        switch (currentAmmoType)
        {
            case AmmoType.ROUNDSHOT:
            {
                foreach (Transform t in selectedCannon)
                    {
                        GameObject obj = Instantiate(roundShotPrefab, t.position, Quaternion.identity);
                        obj.GetComponent<RoundShot>().SetDirectionVector(target, true);
                        Debug.Log("Roundshot out");
                    }
                break;
            }
            case AmmoType.CHAINSHOT:
            {
                foreach (Transform t in selectedCannon)
                    {
                        GameObject obj = Instantiate(chainShotPrefab, t.position, Quaternion.identity);
                        obj.GetComponent<ChainShot>().SetDirectionVector(target);
                        Debug.Log("chainshot out");
                    }
                break;
            }
            case AmmoType.GRAPESHOT:
            {
                foreach(Transform t in selectedCannon)
                    {
                        GameObject obj = Instantiate(grapeShotPrefab, t.position, Quaternion.identity);
                        obj.GetComponent<GrapeShot>().SetDirectionVector(target);
                        Debug.Log("grapeshot out");
                    }
                break;
            }
        }
    }

    public void SwapAmmo(AmmoType ammoType)
    {
        currentAmmoType = ammoType;

        Debug.Log("Ammo type: " + ammoType.ToString());
    }

    public enum AmmoType
    {
        ROUNDSHOT,
        CHAINSHOT,
        GRAPESHOT
    };
}
