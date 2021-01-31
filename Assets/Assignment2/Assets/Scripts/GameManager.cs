using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

  

    public string username = "";
    public int score = 0;
    public float time = 0f;
    
   

    private void Awake()
    {
       
        setUpSingleton();
        /*PlayerPrefsX.SetStringArray("Name", new string[10]);
        PlayerPrefsX.SetIntArray("Score", new int[10]);
        PlayerPrefsX.SetIntArray("Time", new int[10]);*/
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
       
    }

    // Update is called once per frame
    void Update()
    {
       
    }


    public void resetStats()
    {
        score = 0;
        time = 0;
    }

    /*void showHighScore()
    {
        PlayerPrefsX.SetStringArray("Name", new string[10]);
        PlayerPrefsX.SetIntArray("Score", new int[10]);
        PlayerPrefsX.SetIntArray("Time", new int[10]);
    }*/

 
}
