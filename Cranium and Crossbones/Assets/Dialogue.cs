using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;

public class Dialogue : MonoBehaviour
{
    public GameObject dialogBox;
    public TextMeshProUGUI nameTextComponent;
    public TextMeshProUGUI textComponent;
    public string[] lines; //Take from JSON
    public string[] SpritePosition; //Take from JSON 
    public float textSpeed;

    public Image leftSprite, rightSprite1, rightSprite2, rightSprite3;
    public Sprite[] leftSprites, right1Sprites, right2Sprites, right3Sprites;

    public int index;

    public struct Character{
        public string npcId;
        public string npcName;
        public string npcSpriteInactive;
        public string npcSpriteActive;
    }

    public struct DialogueLine
    {
        public string dialogueId;
        public string speakerName;
        public string spriteLeft;
        public string spriteRight1;
        public string spriteRight2;
        public string spriteRight3;
        public string dialogue;
        public string nextLine;
        public string dialogTriggerID;
        public string audioClip;
    }

    public List<DialogueLine> dialogs;
    public List<Character> characters;
    public List<Character> activeCharacters;

    public DialogueLine globalCurrentLine;

    // Start is called before the first frame update
    void Start()
    {
        dialogs = JSONParser.ParseDialogue("dialogue");
        characters = JSONParser.ParseCharacters("npcs");
        textComponent.text = string.Empty;
        //StartDialogue();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            if(textComponent.text == globalCurrentLine.dialogue)
            {
                if(globalCurrentLine.nextLine != "")
                {
                    DialogueLine nxt = dialogs.Find(x => x.dialogueId == globalCurrentLine.nextLine);
                    textComponent.text = string.Empty;
                    StartCoroutine(TypeLine(nxt));
                    globalCurrentLine = nxt;
                }
                else
                {
                    dialogBox.SetActive(false);
                }
            }
            else
            {
                StopAllCoroutines();
                textComponent.text = globalCurrentLine.dialogue;
            }
            /*if (textComponent.text == lines[index])
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                textComponent.text = lines[index];
            }*/
        }
    }

    public void LoadSprites(string dt)
    {
        DialogueLine currLine = dialogs.Find(x => x.dialogTriggerID == dt);
        while (currLine.nextLine != "")
        {
            // Add all sprites
            leftSprites +=  
        }
    }

    public void StartDialogue(string dialogTrigger)
    {
        dialogBox.SetActive(true);
        textComponent.text = "";
        DialogueLine currentLine = dialogs.Find(x => x.dialogTriggerID == dialogTrigger);
        //index = 0;
        globalCurrentLine = currentLine;
        StartCoroutine(TypeLine(currentLine));
        
    }

    IEnumerator TypeLine(DialogueLine currentLine)
    {
        nameTextComponent.text = currentLine.speakerName;
        string line = currentLine.dialogue;
        foreach (char c in line.ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }

        /*foreach (char c in lines[index].ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }*/
    }

/*    void NextLine()
    {
        if(index < lines.Length - 1)
        {
            index++;
            textComponent.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            gameObject.SetActive(false);
        }
    }*/
}
