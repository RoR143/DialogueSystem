using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public AudioSource audioSource;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;

    public Animator animator;
    public Animator animatorWaves;

    public static float timeToNext;
    public float timeRemaining;
    public bool timerIsRunning = false;
    public bool conversationDone;
    public GameObject button;
    public static string converType;

    public static bool timerIsUsed;
    private Queue<string> sentences;
    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();
    }

    public void StartDialogue (Dialogue dialogue)
    {
        if (timerIsUsed)
        {
            button.SetActive(false);
        }
        animator.SetBool("IsOpen", true);
        animatorWaves.SetBool("IsOpen", true);

        nameText.text = dialogue.name;

        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }
        DisplayNextSentence();
    }

    public void DisplayNextSentence ()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
        if (timerIsUsed)
        {
            timeRemaining = timeToNext;
            timerIsRunning = true;
        }
    }

    IEnumerator TypeSentence (string sentence)
    {
        audioSource.Play();
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSecondsRealtime(0.03f);
        }
        audioSource.Stop();
    }
    void EndDialogue()
    {
        animator.SetBool("IsOpen", false);
        animatorWaves.SetBool("IsOpen", false);
        Debug.Log("End of conversation.");
        timerIsRunning = false;
        if (timerIsUsed)
        {
            button.SetActive(true);
        }
        conversationDone = true;
        DialogueTrigger.conversationOver = conversationDone;
        if(converType == "FirstCall")
        {

        }
        if(converType == "BadEnd")
        {
            Application.Quit();
        }
    }

    private void Update()
    {
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
            }
            else
            {
                timerIsRunning = false;
                DisplayNextSentence();
            }

        }
    }
}
