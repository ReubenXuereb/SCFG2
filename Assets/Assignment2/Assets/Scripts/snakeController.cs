using System.Collections.Generic;
using UnityEngine;

public class snakeController : MonoBehaviour
{
    public List<Vector3> positions;

    bool Left = false;
    bool Right = false;
    bool Up = false;
    bool Down = false;
    bool Command = false;

    [SerializeField] GameObject snakeTail;
    public int snakeSize;

    float timer = 0;
    readonly float timerMax = 0.2f;

    void Start()
    {
        positions = new List<Vector3>();
        snakeSize = GameObject.Find("GameManager").GetComponent<GameManager2>().snakeSize;
        
        Left = true;
        Right = false;
        Up = false;
        Down = false;
    }

    void Update()
    {
        snakeControl();
        spawnTail();
    }

    void snakeControl()
    {
        if (!Command)
        {
            if (Left || Right)
            {
                if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
                {
                   
                    Left = false;
                    Right = false;
                    Up = true;
                    Down = false;
                    Command = true;
                }

                if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
                {
                   
                    Left = false;
                    Right = false;
                    Up = false;
                    Down = true;
                    Command = true;
                }
            }

            if (Up || Down)
            {
                if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
                {
                    
                    Left = false;
                    Right = true;
                    Up = false;
                    Down = false;
                    Command = true;
                }

                if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    
                    Left = true;
                    Right = false;
                    Up = false;
                    Down = false;
                    Command = true;
                }
            }
        }

        if ((timer -= Time.deltaTime) <= 0)
        {
            snakeMove();
            Command = false;
            timer = timerMax;
        }
    }

    void snakeMove()
    {
        if (Up)
        {
            Move(transform.position + Vector3.up);
        }
        if (Down)
        {
            Move(transform.position + Vector3.down);
        }
        if (Left)
        {
            Move(transform.position + Vector3.left);
        }
        if (Right)
        {
            Move(transform.position + Vector3.right);
        }
    }

    void Move(Vector3 direction)
    {
        transform.position = Vector3.MoveTowards(transform.position, direction, 1f);
        positions.Add(transform.position);
    }

   void spawnTail()
    {
        if (snakeSize > transform.parent.childCount)
        {
            Instantiate(snakeTail, new Vector3(50, 50, 0), Quaternion.identity).transform.parent = transform.parent;

           /* if (snakeSize >= 6)
            {
                GameObject.Find("EndTarget").GetComponent<ToNextLevel>().TurnOn();
            }*/
        }
    }

    void checkBounds()
    {
        if ((transform.position.x < -(Camera.main.orthographicSize - 1)) || (transform.position.x > (Camera.main.orthographicSize - 1)))
        {
            transform.position = new Vector3(-transform.position.x, transform.position.y);
        }

        if ((transform.position.y < -(Camera.main.orthographicSize - 1)) || (transform.position.y > (Camera.main.orthographicSize - 1)))
        {
            transform.position = new Vector3(transform.position.x, -transform.position.y);
        }
    }
}
