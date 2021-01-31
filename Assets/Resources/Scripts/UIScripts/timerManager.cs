using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class timerManager : MonoBehaviour
{

    public bool timerStarted;

    float timerValue;

    Text timerText;

    GameManager gm;

    IEnumerator timer()
    {
        while(true)
        { 
            if (timerStarted)
            {
                //measure the time
                gm.time++;

                float minutes = Mathf.FloorToInt(gm.time / 60f);
                float seconds = Mathf.FloorToInt(gm.time % 60f);

                timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);


                //code that is running every second
                yield return new WaitForSeconds(1f);
            }
            else
            {
                //don't measure the time
                gm.time = 0f;
                timerText.text = string.Format("{0:00}:{1:00}", 0f, 0f);
                yield return null;

            }
            
        }
    }

    
    // Start is called before the first frame update
    void Start()
    {
        //the text component attached to THIS object
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        timerText = GetComponent<Text>();
        StartCoroutine(timer()); 
    }

    private void Update()
    {
        timerValue = gm.time;
    }


}
