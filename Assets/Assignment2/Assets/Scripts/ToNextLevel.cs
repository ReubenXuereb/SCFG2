using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToNextLevel : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.CompareTag("Player") && SceneManager.GetActiveScene().name == ("Level1"))
        {
            if(GameObject.Find("GameManager").GetComponent<GameManager>().snakeSize >= 6)
            {
                SceneManager.LoadScene("Level2");
            }
        }
        else if(collision.transform.CompareTag("Player") && SceneManager.GetActiveScene().name == ("Level2"))
        {
            SceneManager.LoadScene("Level3");
        }
    }
}
