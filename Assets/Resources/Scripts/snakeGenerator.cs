using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class positionRecord

{


    //the place where I've been
    Vector3 position;
    //at which point was I there?
    int positionOrder;

    GameObject breadcrumbBox;

    public void changeColor()
    {
        this.BreadcrumbBox.GetComponent<SpriteRenderer>().color = Color.black;
    }


//==
//1.Equals(1) = true

    //this method exists in every object in C sharp


    public override bool Equals(System.Object obj)
    {
        if (obj == null)
            return false;
        positionRecord p = obj as positionRecord;
        if ((System.Object)p == null)
            return false;
        return position == p.position;
    }


    public bool Equals(positionRecord o)
    {
        if (o == null)
            return false;

        
            //the distance between any food spawned
            return Vector3.Distance(this.position,o.position) < 5f;
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }




    public Vector3 Position { get => position; set => position = value; }
    public int PositionOrder { get => positionOrder; set => positionOrder = value; }
    public GameObject BreadcrumbBox { get => breadcrumbBox; set => breadcrumbBox = value; }
}

public class snakeGenerator : MonoBehaviour
{

    public int snakelength;

    foodGenerator fgen;
    snakeheadController snakeController;


    int pastpositionslimit = 100;

    public Transform spawnPoint;

    GameObject playerBox,breadcrumbBox,pathParent,timerUI;

    public GameObject snakeHead;

    public GameObject portal;

    List<positionRecord> pastPositions;

    int positionorder = 0;

    bool firstrun = true;


    Color snakeColor;



    IEnumerator waitToGenerateFood()
    {
        while (true)
        {
            if (!firstrun)
            {
                StartCoroutine(fgen.generateFood());
                break;
            }

            yield return null;

        }
        yield return null;
    }


    // Start is called before the first frame update
    void Start()
    {

        snakeColor = Color.green;

        playerBox = Instantiate(snakeHead, spawnPoint.position, Quaternion.identity);
       

        timerUI = Instantiate(Resources.Load<GameObject>("Prefabs/Timer"), new Vector3(-8f, 0f), Quaternion.identity);

        //the default value for the timer is started
        timerUI.GetComponentInChildren<timerManager>().timerStarted = true;

       


        pathParent = new GameObject();

        pathParent.transform.position = new Vector3(0f, 0f);

        pathParent.name = "Path Parent";

        
        breadcrumbBox = Resources.Load<GameObject>("Prefabs/Square");

       /* playerBox.GetComponent<SpriteRenderer>().color = Color.black;
        playerBox.AddComponent<BoxCollider2D>();
        playerBox.GetComponent<BoxCollider2D>().isTrigger = true;


        //move the box with the arrow keys
        playerBox.AddComponent<snakeheadController>();
        playerBox.AddComponent<Rigidbody2D>();
        playerBox.GetComponent<Rigidbody2D>().isKinematic = true;*/



        playerBox.name = "Black player box";

        pastPositions = new List<positionRecord>();

        fgen = Camera.main.GetComponent<foodGenerator>();

        StartCoroutine(waitToGenerateFood());

        drawTail(snakelength);
       
    }


    public void toNextLevel() 
    {

        if (SceneManager.GetActiveScene().name == "Level1")
        {
            if (snakelength >= 6)
            {
                GameObject.Find("Portal").GetComponent<SpriteRenderer>().color = Color.yellow;
            }
            else if(snakelength < 6)
            {
                GameObject.Find("Portal").GetComponent<SpriteRenderer>().color = Color.red;
            }
        }

        if (SceneManager.GetActiveScene().name == "Level2")
        {
            if (snakelength >= 10)
            {
                GameObject.Find("Portal").GetComponent<SpriteRenderer>().color = Color.yellow;
            }
            else if (snakelength <= 6)
            {
                GameObject.Find("Portal").GetComponent<SpriteRenderer>().color = Color.red;
            }
        }

        if (SceneManager.GetActiveScene().name == "Level3")
        {
            if (snakelength >= 6)
            {
                GameObject.Find("Portal").GetComponent<SpriteRenderer>().color = Color.yellow;
            }
            else if (snakelength < 6)
            {
                GameObject.Find("Portal").GetComponent<SpriteRenderer>().color = Color.red;
            }
        }
    }


    

    // Update is called once per frame

    bool boxExists(Vector3 positionToCheck)
    {
        //foreach position in the list

        foreach (positionRecord p in pastPositions)
        {
            
            if (p.Position == positionToCheck)
            {
                Debug.Log(p.Position + "Actually was a past position");
                if (p.BreadcrumbBox != null)
                {
                    Debug.Log(p.Position + "Actually has a red box already");
                    //this breaks the foreach so I don't need to keep checking
                    return true;
                }
            }
        }

        return false;
    }


   public void savePosition()
    {
        positionRecord currentBoxPos = new positionRecord();

        currentBoxPos.Position = playerBox.transform.position;
        positionorder++;
        currentBoxPos.PositionOrder = positionorder;

        if (!boxExists(playerBox.transform.position))
        {
            currentBoxPos.BreadcrumbBox = Instantiate(breadcrumbBox, playerBox.transform.position, Quaternion.identity);

            currentBoxPos.BreadcrumbBox.transform.SetParent(pathParent.transform);

            currentBoxPos.BreadcrumbBox.name = positionorder.ToString();

            currentBoxPos.BreadcrumbBox.GetComponent<SpriteRenderer>().color = Color.red;

            currentBoxPos.BreadcrumbBox.GetComponent<SpriteRenderer>().sortingOrder = -1;
        }

        pastPositions.Add(currentBoxPos);
        Debug.Log("Have made this many moves: " + pastPositions.Count);
    }


    public void cleanList()
    {
        for(int counter = pastPositions.Count - 1 ; counter > pastPositions.Count;counter--)
         {
             pastPositions[counter] = null;
         }
        //pastPositions.Clear();
    }

    
   /* public void changeSnakeColor(int length,Color color)
    {
        int tailStartIndex = pastPositions.Count - 1;
        int tailEndIndex = tailStartIndex - length;
        
        snakeColor = color;

        for (int snakeblocks = tailStartIndex;snakeblocks>tailEndIndex;snakeblocks--)
        {
        
            pastPositions[snakeblocks].BreadcrumbBox.GetComponent<SpriteRenderer>().color = color;
        }
    }*/

    public void drawTail(int length)
    {
        clearTail();

        if (pastPositions.Count>length)
        {
            //nope
            //I do have enough positions in the past positions list
            //the first block behind the player
            int tailStartIndex = pastPositions.Count - 1;
            int tailEndIndex = tailStartIndex - length;
          

            //if length = 4, this should give me the last 4 blocks
            for (int snakeblocks = tailStartIndex;snakeblocks>tailEndIndex;snakeblocks--)
            {
                //prints the past position and its order in the list
                //Debug.Log(pastPositions[snakeblocks].Position + " " + pastPositions[snakeblocks].PositionOrder);

                Debug.Log(snakeblocks);

                pastPositions[snakeblocks].BreadcrumbBox = Instantiate(breadcrumbBox, pastPositions[snakeblocks].Position, Quaternion.identity);
                pastPositions[snakeblocks].BreadcrumbBox.GetComponent<SpriteRenderer>().color = snakeColor;

            }
        } 

        if (firstrun)
        {
            
            //I don't have enough positions in the past positions list
            for(int count =length;count>0;count--)
            {
                positionRecord fakeBoxPos = new positionRecord();
                float ycoord = count * -1;
                fakeBoxPos.Position = new Vector3(0f, ycoord);
               // Debug.Log(new Vector3(0f, ycoord));
                //fakeBoxPos.BreadcrumbBox = Instantiate(breadcrumbBox, fakeBoxPos.Position, Quaternion.identity);
                //fakeBoxPos.BreadcrumbBox.GetComponent<SpriteRenderer>().color = Color.yellow;
                pastPositions.Add(fakeBoxPos);

                 


            }
            firstrun = false;
            drawTail(length);
            //Debug.Log("Not long enough yet");
        }


        //tail for enemyAI
    }
    


    //if hit tail returns true, the snake has hit its tail
    public bool hitTail(Vector3 headPosition, int length)
    {
        int tailStartIndex = pastPositions.Count - 1;
        int tailEndIndex = tailStartIndex - length;

        //I am checking all the positions in the tail of the snake
        for (int snakeblocks = tailStartIndex; snakeblocks > tailEndIndex; snakeblocks--)
        {
            if ((headPosition == pastPositions[snakeblocks].Position) && (pastPositions[snakeblocks].BreadcrumbBox != null))
            {
                //  Debug.Log("Hit Tail");
               
                if (SceneManager.GetActiveScene().name == "Level1")
                {
                    playerBox.transform.position = spawnPoint.position;
                    clearTail();
                    snakelength = 2;
                    GameObject.Find("GameManager").GetComponent<GameManager>().score -= 20;
                }
                else if (SceneManager.GetActiveScene().name == "Level2")
                {
                    playerBox.transform.position = spawnPoint.position;
                    clearTail();
                    snakelength = 6;
                    GameObject.Find("GameManager").GetComponent<GameManager>().score -= 20;
                }
                else if (SceneManager.GetActiveScene().name == "Level3")
                {
                    playerBox.transform.position = spawnPoint.position;
                    clearTail();
                    snakelength = 2;
                    GameObject.Find("GameManager").GetComponent<GameManager>().score -= 20;
                }
                return true;
            }
        }
       return false;

    }



   public void clearTail()
    {
        cleanList();
        foreach (positionRecord p in pastPositions)
        {
           // Debug.Log("Destroy tail" + pastPositions.Count);
            Destroy(p.BreadcrumbBox);
        }
    }

   



    void Update()
    {
        if (Input.anyKeyDown && !((Input.GetMouseButtonDown(0)
            || Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(2))) && !Input.GetKeyDown(KeyCode.X) && !Input.GetKeyDown(KeyCode.Z) && !Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("a key was pressed "+Time.time);
          
            savePosition();

            //draw a tail of length 4
            drawTail(snakelength);

        }

        /*if (Input.GetKeyDown(KeyCode.X))
        {
            StartCoroutine(reverseMoves());
        }*/


        if (Input.GetKeyDown(KeyCode.Z))
        {
            clearTail();
        }

       /* if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(snakeController.automoveCoroutine());
          //  StartCoroutine(Task5());
        }*/

        toNextLevel();


    }
}
