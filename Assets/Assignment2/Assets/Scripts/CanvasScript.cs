using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasScript : MonoBehaviour
{
    [SerializeField] Text scoreText;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        setScore();
    }
    void setScore()
    {
        scoreText.text = "SCORE: " + GameObject.Find("GameManager").GetComponent<GameManager>().score.ToString();
    }

}
