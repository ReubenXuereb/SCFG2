using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class playerHighScores
{
    public string n;
    public int s;
    public float t;
}



public class HighscoreScipt : MonoBehaviour
{

    List<playerHighScores> playerHSList;

    public string[] names;
    public int[] scores;
    public float[] times;
    GameManager gm;
    playerHighScores playerHS;
    // Start is called before the first frame update
    void Start()
    {
        playerHSList = new List<playerHighScores>();
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        names = PlayerPrefsX.GetStringArray("Names", "", 1);
        scores = PlayerPrefsX.GetIntArray("Scores", 0, 1);
        times = PlayerPrefsX.GetFloatArray("Times", 0f, 1);

        if(names[0] == "")
        {
            names[0] = gm.username;
            scores[0] = gm.score;
            times[0] = gm.time;

            PlayerPrefsX.SetStringArray("Names", names);
            PlayerPrefsX.SetIntArray("Scores", scores);
            PlayerPrefsX.SetFloatArray("Times", times);

        }
        else
        {
            playerHS = new playerHighScores();
            for(int i = 0; i < names.Length; i++)
            {
                playerHS = new playerHighScores();
                playerHS.n = names[i];
                playerHS.s = scores[i];
                playerHS.t = times[i];
                playerHSList.Add(playerHS);
            }



            
            playerHS = new playerHighScores();
            playerHS.n = gm.username;
            playerHS.s = gm.score;
            playerHS.t = gm.time;

            playerHSList.Add(playerHS);

            System.Array.Resize(ref names, names.Length + 1);
            System.Array.Resize(ref scores, scores.Length + 1);
            System.Array.Resize(ref times, times.Length + 1);

            names[names.Length - 1] = gm.username;
            scores[scores.Length - 1] = gm.score;
            times[times.Length - 1] = gm.time;

            PlayerPrefsX.SetStringArray("Names", names);
            PlayerPrefsX.SetIntArray("Scores", scores);
            PlayerPrefsX.SetFloatArray("Times", times);

        }
        sortList();
        Scoreboard();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void sortList()
    {
        playerHSList.Sort((a, b) => a.t.CompareTo(b.t));
    }

    void Scoreboard()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).GetChild(0).GetComponent<Text>().text = "Username: " + playerHSList[i].n;
            transform.GetChild(i).GetChild(1).GetComponent<Text>().text = "Score: " + playerHSList[i].s;
            transform.GetChild(i).GetChild(2).GetComponent<Text>().text = "Time: " + playerHSList[i].t;
        }
        
    }

}
