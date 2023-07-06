using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
/// <summary>
/// Custom Suite of tools for reading and writing to JSON
/// </summary>
public class JSONParser
{ 

    // Static Variable for turning on and off debugging
    // Manually change it please
    private static bool debug = false;

    /// <summary>
    /// Reads a single level JSON file and returns a key value pair of strings in a 
    /// dictionary. Ideal for usecases like parsing dialogue
    /// </summary>
    /// <param name="filename"></param>
    /// <returns></returns>
    public static Dictionary<string, string> ParseFromFile(string filename)
    {
        Dictionary<string, string> result = new Dictionary<string, string>();

        using (StreamReader r = new StreamReader("assets/data/" + filename + ".json"))
        {
            string json = r.ReadToEnd();
            result = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
        }

        if (debug)
        {
            foreach (var entry in result)
            {
                Debug.Log(entry.Key.ToString() + ": " + entry.Value.ToString());
            }
            
        }

        if (result.Count < 1)
        {
            Debug.Log("Results were empty");
            return null;
        }
        return result;
    }

    public class Wrapper<T>
    {
        public T[] Array;
    }

    public static List<Upgrades.Upgrade> ParseUpgrades(string filename) {
        
        Wrapper<Upgrades.Upgrade> result;

        List<Upgrades.Upgrade> upgrades = new();

        using (StreamReader r = new StreamReader("assets/data/" + filename + ".json"))
        {
            string json = r.ReadToEnd();
            result = JsonConvert.DeserializeObject<Wrapper<Upgrades.Upgrade>>(json);
        }
        foreach (Upgrades.Upgrade upgrade in result.Array)
        {
            upgrades.Add(upgrade);
        }

        return upgrades;
    }

    public static Dictionary<string,string> ParseFromJSONString(string json)
    {
        Dictionary<string, string> result = new Dictionary<string, string>();
        result = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
        return result;
    }

    public static void WriteToFile(string filename, Dictionary<string, string> output)
    {
        using (StreamWriter w = new StreamWriter("assets/data/" + filename + ".json"))
        {
            string json = JsonConvert.SerializeObject(output);
            w.Write(json);
        }

        return;
    }

}
