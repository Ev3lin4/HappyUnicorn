using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class CountdownText : MonoBehaviour 
{
    public delegate void CountdownFinished();
    public static event CountdownFinished OnCountdownFinished;
    
    public Text countdown;

    private void OnValidate()
    {
        countdown = countdown == null ? GetComponent<Text>() : countdown;
    }

    private void Start() 
    {
        StartCoroutine(Countdown());
    }

    private IEnumerator Countdown() 
    {
        int count = 0;
        
        for (int i = 0; i < 3; i++) 
        {
            countdown.text = (count - i).ToString();

            yield return new WaitForSeconds(1);
        }

        OnCountdownFinished();
    }
}
