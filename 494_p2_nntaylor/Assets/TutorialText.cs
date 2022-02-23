using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TutorialText : MonoBehaviour
{
    public Text tutText;
    public string[] tutTextInput;
    private int i = 1;
    
    private void Start()
    {
        Player_Health.instance.invincible = true;
    }

    public void NextTextBlock()
    {
        if (i == tutTextInput.Length) 
            SceneManager.LoadScene("MainMenu");
        
        /* else */
        tutText.text = tutTextInput[i];
        ++i;
    }
}
