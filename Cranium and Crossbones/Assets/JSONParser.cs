using JetBrains.Annotations;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using UnityEngine;
using static Dialogue;
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

    [Serializable]
    public class Wrapper<T>
    {
        public T[] Array;
    }

    [Serializable]
    public class Container
    {
        //public string id;
        public Dictionary<string, string> keyValuePairs;
    }

    public static List<Upgrades.Upgrade> ParseUpgrades(string filename) {

        Debug.Log("Starting to Parse Updates");
        List<Upgrades.Upgrade> upgrades = new List<Upgrades.Upgrade>();
        Debug.Log("List of upgrades created");

        using (StreamReader r = new StreamReader("assets/data/" + filename + ".json"))
        {
            string json = r.ReadToEnd();
            Debug.Log(json);

            upgrades = JsonConvert.DeserializeObject<List<Upgrades.Upgrade>>(json);
            foreach(Upgrades.Upgrade u in upgrades)
            {
                Debug.Log(u.upgradeID + " - " + u.upgradeName);
            }
        }
        return upgrades;
    }

    public static List<DialogueLine> ParseDialogue(string filename)
    {
        List<DialogueLine> dialogues;

        using (StreamReader r = new StreamReader("assets/data/" + filename + ".json"))
        {
            string json = r.ReadToEnd();

            dialogues = JsonConvert.DeserializeObject<List<DialogueLine>>(json);
        }

        /*foreach (DialogueLine d in dialogues)
        {
            Debug.Log(d.characterId + ": " + d.dialogue);
        }*/

        return dialogues;
    }

    public static List<Character> ParseCharacters(string filename)
    {
        List<Character> characters;

        using (StreamReader r = new StreamReader("assets/data/" + filename + ".json"))
        {
            string json = r.ReadToEnd();
            characters = JsonConvert.DeserializeObject<List<Character>>(json);
        }

        return characters;
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
