using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public string username;
    //public float timer;
    public int snakeSize = 0;
    private bool started;
    float timerValue = 0;
    [SerializeField] Text timerText;
    GameObject playerBox, timerUI;

    private void Awake()
    {
        //StoreData();
        setUpSingleton();

    }

    private void setUpSingleton()
    {
        int numberOfGameManagers = FindObjectsOfType<GameManager>().Length;
        if (numberOfGameManagers > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }



   
    void Start()
    {
        timerText = GetComponent<Text>();
        StartCoroutine(timer());

        playerBox = Instantiate(Resources.Load<GameObject>("Prefabs/Square"), new Vector3(0f, 0f), Quaternion.identity);

        timerUI = Instantiate(Resources.Load<GameObject>("Prefabs/Timer"), new Vector3(0f, 0f), Quaternion.identity);
        //the default value for the timer is started
        timerUI.GetComponentInChildren<timerManager>().timerStarted = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator timer()
    {
        while (true)
        {
            if (started)
            {
                //measure the time
                timerValue++;

                float minutes = timerValue / 60f;
                float seconds = timerValue % 60f;

                timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);


                //code that is running every second
                yield return new WaitForSeconds(1f);
            }
            else
            {
                //don't measure the time
                timerValue = 0f;
                timerText.text = string.Format("{0:00}:{1:00}", 0f, 0f);
                yield return null;
            }
        }
    }
}
