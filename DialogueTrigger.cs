using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{

    public Dialogue dialogue;
    public bool timerTrigger;
    public int timerSeconds;
    public static bool conversationOver = false;
    public GameObject currentConver;
    public string converType;
    public void TriggerDialogue()
    {
        DialogueManager.timerIsUsed = timerTrigger;
        DialogueManager.timeToNext = timerSeconds;
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
        DialogueManager.converType = converType;
    }

    private void Update()
    {
        if (conversationOver == true)
        {
            currentConver.SetActive(false);
            conversationOver = false;
        }
    }
}
