using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager2 : MonoBehaviour
{

    public string username;
    public float timer;
    public int snakeSize = 0;
    private bool started;

    private void Awake()
    {
        //StoreData();
        setUpSingleton();

    }

    private void setUpSingleton()
    {
        int numberOfGameManagers = FindObjectsOfType<GameManager2>().Length;
        if (numberOfGameManagers > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
