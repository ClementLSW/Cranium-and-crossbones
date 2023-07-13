using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageController : MonoBehaviour
{
    [SerializeField] Image Char;

    [SerializeField] Sprite[] imageUsed;

    //testing
    [SerializeField] GameObject dialogue;
    Dialogue dialogueRef;
    [SerializeField] string ImagePos;

    private void Awake()
    {
        dialogueRef = dialogue.GetComponent<Dialogue>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //if chartalking == left
        if(ImagePos == dialogueRef.SpritePosition[dialogueRef.index])
        {
            //then replace image
            Char.sprite = imageUsed[1];
        }

        //else
        else
        {
            Char.sprite = imageUsed[0];
        }
        //greyed
    }
}
