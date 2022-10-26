using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialougeManager : MonoBehaviour
{

    private Queue<string> sentences;

    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialougeText;
    public Animator animator;
    public Animator turtleAnimator;
    public Animator screenCover;
    public GameObject speakButton;
    public GameObject sceneCoverer;
    public AudioSource blipSfx;
    public float blipEveryNumberOfChar = 3f;
    public GameObject bigScreenShakeButton;
    public GameObject blockedScreen;
    public DialougeTrigger creator2;
    public GameObject genericSlider;
    public GameObject platformerPlayer;
    public GameObject turtle;
    public DialougeTrigger creator5;
    public GameObject spikes;
    public DialougeTrigger creator6;
    public GameObject flag;
    public DialougeTrigger creator7;
    public GameObject flag2;
    public DialougeTrigger creator8;
    public DialougeTrigger turtle2;
    public GameObject victoryScreen;


    bool fast;

    bool isRunningDialogue = false;

    string currentEvent;

    float typeSpeed = 0.02f;

    float currentChar = 0;

    float pitch;

    bool off;

    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();
        speakButton.SetActive(false);
        bigScreenShakeButton.SetActive(false);
        blockedScreen.SetActive(false);
        platformerPlayer.SetActive(false);
        spikes.SetActive(false);
        flag.SetActive(false);
        flag2.SetActive(false);
        victoryScreen.SetActive(false);
    }

    public void StartDialogue(Dialouge dialogue, float speed, float pitch, bool fastTransition, bool kindaOff)
    {
        animator.SetBool("IsOpen", true);
        typeSpeed = 0.02f / speed;
        bigScreenShakeButton.SetActive(false);
        this.pitch = pitch;
        fast = fastTransition;
        off = kindaOff;
        if (kindaOff)
        {
            nameText.text = "The Tortoise";
        }
        else
        {
            nameText.text = dialogue.name;
        }
        currentEvent = dialogue.specialEvent;

        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }
        StartCoroutine(WaitAndStart());
    }


    IEnumerator WaitAndStart()
    {
        dialougeText.text = "";
        yield return new WaitForSeconds(1f);
        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        blipSfx.pitch = pitch;
        if (isRunningDialogue == false)
        {
            if (sentences.Count == 0)
            {
                EndDialogue();



                return;
            }

            string sentence = sentences.Dequeue();
            StopAllCoroutines();
            StartCoroutine(TypeSentence(sentence));
        }
    }

    //make addition function
    //do something

    IEnumerator TypeSentence(string sentence)
    {
        if (off)
        {
            off = false;
            nameText.text = "The Creator?";
        }
        turtleAnimator.SetBool("IsTalking", true);
        isRunningDialogue = true;
        dialougeText.text = "";
        if (currentEvent == "turtle 1")
        {
            speakButton.SetActive(false);
        }
        foreach (char letter in sentence.ToCharArray())
        {
            string character = letter.ToString();
            if (character != " ")
            {
                currentChar += 1;
            }
            if (currentChar == blipEveryNumberOfChar) {
                currentChar = 0;
                blipSfx.Play();
            }
            dialougeText.text += letter;
            yield return new WaitForSeconds(typeSpeed);
        }
        isRunningDialogue = false;
        turtleAnimator.SetBool("IsTalking", false);
        if (fast)
        {
            EndDialogue();
        }
    }

    void EndDialogue()
    {
        if (currentEvent == "open scene")
        {
            speakButton.SetActive(true);
            screenCover.SetBool("SceneOpen", true);
            sceneCoverer.SetActive(false);
        }
        if (currentEvent == "turtle 1")
        {
            bigScreenShakeButton.SetActive(true);
        }
        if (currentEvent == "turtle 2")
        {
            bigScreenShakeButton.SetActive(true);
        }
        if(currentEvent == "turtle 3")
        {
            blockedScreen.SetActive(true);
            genericSlider.SetActive(false);
        }
        if(currentEvent == "creator 2")
        {
            genericSlider.SetActive(true);
        }
        if(currentEvent == "escaped")
        {
            blockedScreen.SetActive(false);
            StartCoroutine(WaitAndActivatePlatformer());
            turtle.SetActive(false);

        }
        if(currentEvent == "creator 5")
        {
            StartCoroutine(WaitAndActivateSpikes());
        }
        if(currentEvent == "creator 6")
        {
            Debug.Log("creator 6");
            StartCoroutine(WaitAndActivateFlag());
        }
        if(currentEvent == "creator 7")
        {
            StartCoroutine(WaitAndActivateRealFlag());
        }
        if(currentEvent == "creator 8")
        {
            turtle.SetActive(true);
            flag2.SetActive(false);
            flag.SetActive(false);
            spikes.SetActive(false);
            StartCoroutine(DoTurtleTrhingy());
        }
        if(currentEvent == "vicotry")
        {
            victoryScreen.SetActive(true);
        }
        animator.SetBool("IsOpen", false);
        if (fast)
        {
            StartCoroutine(Wait());

        }
    }

    IEnumerator DoTurtleTrhingy()
    {
        yield return new WaitForSeconds(2f);
        turtle2.TriggerDialouge();
    }
    
    IEnumerator WaitAndActivatePlatformer()
    {
        yield return new WaitForSeconds(2f);
        platformerPlayer.SetActive(true);
        creator5.TriggerDialouge();
    }

    IEnumerator WaitAndActivateSpikes()
    {
        yield return new WaitForSeconds(4f);
        spikes.SetActive(true);
        creator6.TriggerDialouge();
    }

    IEnumerator WaitAndActivateFlag()
    {
        yield return new WaitForSeconds(6f);
        creator7.TriggerDialouge();
        flag.SetActive(true);
    }

    IEnumerator WaitAndActivateRealFlag()
    {
        yield return new WaitForSeconds(4f);
        creator8.TriggerDialouge();
        flag2.SetActive(true);
    }

    IEnumerator Wait()
    {
        var timescale = Time.timeScale;
        Time.timeScale = 10f;
        yield return new WaitForSeconds(2f);
        Time.timeScale = timescale;
        yield return new WaitForSeconds(2f);
        creator2.TriggerDialouge();
    }
}
