using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialougeTrigger : MonoBehaviour
{
    public Dialouge dialogue;
    public bool triggerOnStart = false;
    public float typeSpeed = 1f;
    public float pitch = 0.8f;
    public bool endOnSentenceFinish = false;
    public bool isTurtleDisguise = false;

    private void Start()
    {
        if (triggerOnStart)
        {
            StartCoroutine(WaitAndTrigger());
        }
    }

    IEnumerator WaitAndTrigger()
    {
        yield return new WaitForSeconds(1f);
        TriggerDialouge();
    }

    public void TriggerDialouge()
    {
        FindObjectOfType<DialougeManager>().StartDialogue(dialogue, typeSpeed, pitch);
    }

}
