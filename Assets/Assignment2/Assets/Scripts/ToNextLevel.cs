using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ToNextLevel : MonoBehaviour
{

    GameManager gm;
    [SerializeField] InputField usernameField;

    private void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
    }


    public void startGame()
    {
        gm.username = usernameField.GetComponent<InputField>().text;
        SceneManager.LoadScene("Level1");
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player") && SceneManager.GetActiveScene().name == ("Level1") && GetComponent<SpriteRenderer>().color == Color.yellow)
        {
            SceneManager.LoadScene("Level2");
        }
        if (collision.transform.CompareTag("Player") && SceneManager.GetActiveScene().name == ("Level2") && GetComponent<SpriteRenderer>().color == Color.yellow)
        {
            SceneManager.LoadScene("Level3");
        } 
        if (collision.transform.CompareTag("Player") && SceneManager.GetActiveScene().name == ("Level3") && GetComponent<SpriteRenderer>().color == Color.yellow)
        {
            print("hitting Level3");
            SceneManager.LoadScene("EndingScene");
        }
    }

    private void setTextForEndingScene()
    {
        if (SceneManager.GetActiveScene().name == "EndingScene")
        {
            if (GameObject.Find("GameManager") != null)
            {
                GameObject.Find("playerUsername").GetComponent<Text>().text = "Username: " + GameObject.Find("GameManager").GetComponent<GameManager>().username.ToString();
                GameObject.Find("playerScore").GetComponent<Text>().text = "Score: " + GameObject.Find("GameManager").GetComponent<GameManager>().score.ToString();
                GameObject.Find("playerTime").GetComponent<Text>().text = "Time: " + GameObject.Find("GameManager").GetComponent<GameManager>().time.ToString();
            }
        }
    }

    public void restartLevel()
    {
        SceneManager.LoadScene("Level1");
        GameObject.Find("GameManager").GetComponent<GameManager>().resetStats();
    }

    public void backToStartMenu()
    {
        Destroy(GameObject.Find("GameManager"));
        SceneManager.LoadScene("StartScene");
    }

    private void Update()
    {
        setTextForEndingScene();
    }
}
