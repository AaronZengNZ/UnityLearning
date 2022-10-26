using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockedScreen : MonoBehaviour
{
    public DialougeTrigger escaped;
    bool yes = true;
    // Start is called before the first frame update
    public void TextEdited(string text)
    {
        if(text == "" || text == " " || text == "not" || text == "never")
        {
            if (yes)
            {
                yes = false;
                escaped.TriggerDialouge();
            }
        }
    }
}
