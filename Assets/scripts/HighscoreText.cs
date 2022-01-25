using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class HighscoreText : MonoBehaviour
{
    public Text highscore;

    private void OnValidate()
    {
        highscore = highscore == null ? GetComponent<Text>() : highscore;
    }
    
    private void Awake() 
    {
        highscore.text = PlayerPrefs.GetInt("HighScore").ToString();
    }
}
