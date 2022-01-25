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
        countdown ??= GetComponent<Text>();
    }

    private void Start() 
    {
        StartCoroutine(Countdown());
    }

    private IEnumerator Countdown() 
    {
        const int count = 3;

        var yielder = new WaitForSeconds(1);

        for (int i = 0; i < count; i++) 
        {
            countdown.text = (count - i).ToString();

            yield return yielder;
        }

        OnCountdownFinished();
    }
}
