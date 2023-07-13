using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rubbish : MonoBehaviour
{
    public Dialogue dialogue;
    // Start is called before the first frame update
    public void triggerDialogue1()
    {
        dialogue.StartDialogue("DT_01");
    }
    public void triggerDialogue2()
    {
        dialogue.StartDialogue("DT_02");
    }
    public void triggerDialogue3()
    {
        dialogue.StartDialogue("DT_03");
    }
    public void triggerDialogue4()
    {
        dialogue.StartDialogue("DT_04");
    }
    public void triggerDialogue5()
    {
        dialogue.StartDialogue("DT_05");
    }
    public void triggerDialogue6()
    {
        dialogue.StartDialogue("DT_06");
    }
}
