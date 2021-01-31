using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class enemyPositionRecord

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
        enemyPositionRecord p = obj as enemyPositionRecord;
        if ((System.Object)p == null)
            return false;
        return position == p.position;
    }


    public bool Equals(enemyPositionRecord o)
    {
        if (o == null)
            return false;


        //the distance between any food spawned
        return Vector3.Distance(this.position, o.position) < 5f;
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }




    public Vector3 Position { get => position; set => position = value; }
    public int PositionOrder { get => positionOrder; set => positionOrder = value; }
    public GameObject BreadcrumbBox { get => breadcrumbBox; set => breadcrumbBox = value; }
}

public class SpawnEnemyAI : MonoBehaviour
{
   // public GameObject enemyAI;
    public int snakelength;

    int pastpositionslimit = 100;

    //public Transform spawnPoint;

    GameObject enemyBox, breadcrumbBox, pathParent, timerUI;

    GameObject enemyHead;

    List<enemyPositionRecord> pastPositions;

    int positionorder = 0;

    bool firstrun = true;


    Color snakeColor;
    // Start is called before the first frame update
    void Start()
    {
        enemyBox = Resources.Load<GameObject>("Prefabs/AISnake");

        snakeColor = Color.white;

        pathParent = new GameObject();

        pathParent.transform.position = new Vector3(0f, 0f);

        pathParent.name = "Path Parent";

        breadcrumbBox = Resources.Load<GameObject>("Prefabs/Square");

        pastPositions = new List<enemyPositionRecord>();

        StartCoroutine(spawnEnemy());

        //enemyDrawTail(snakelength);
    }


    bool boxExists(Vector3 positionToCheck)
    {
        //foreach position in the list

        foreach (enemyPositionRecord p in pastPositions)
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
        enemyPositionRecord currentBoxPos = new enemyPositionRecord();

        currentBoxPos.Position = enemyHead.transform.position;
        positionorder++;
        currentBoxPos.PositionOrder = positionorder;

        if (!boxExists(enemyHead.transform.position))
        {
            currentBoxPos.BreadcrumbBox = Instantiate(breadcrumbBox, enemyHead.transform.position, Quaternion.identity);

            currentBoxPos.BreadcrumbBox.transform.SetParent(pathParent.transform);

            currentBoxPos.BreadcrumbBox.name = positionorder.ToString();

            currentBoxPos.BreadcrumbBox.GetComponent<SpriteRenderer>().color = Color.cyan;

            currentBoxPos.BreadcrumbBox.GetComponent<SpriteRenderer>().sortingOrder = -1;
        }

        pastPositions.Add(currentBoxPos);
        Debug.Log("Have made this many moves: " + pastPositions.Count);
    }

    public void cleanList()
    {
        for (int counter = pastPositions.Count - 1; counter > pastPositions.Count; counter--)
        {
            pastPositions[counter] = null;
        }
        //pastPositions.Clear();
    }

    public void enemyDrawTail(int length)
    {
        clearTail();

        if (pastPositions.Count > length)
        {
            //nope
            //I do have enough positions in the past positions list
            //the first block behind the player
            int tailStartIndex2 = pastPositions.Count - 1;
            int tailEndIndex = tailStartIndex2 - length;


            //if length = 4, this should give me the last 4 blocks
            for (int snakeblocks = tailStartIndex2; snakeblocks > tailEndIndex; snakeblocks--)
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
            for (int count = length; count > 0; count--)
            {
                enemyPositionRecord fakeBoxPos = new enemyPositionRecord();
                float ycoord = count * -1;
                fakeBoxPos.Position = new Vector3(4.5f, -2.5f);
                // Debug.Log(new Vector3(0f, ycoord));
                //fakeBoxPos.BreadcrumbBox = Instantiate(breadcrumbBox, fakeBoxPos.Position, Quaternion.identity);
                //fakeBoxPos.BreadcrumbBox.GetComponent<SpriteRenderer>().color = Color.yellow;
                pastPositions.Add(fakeBoxPos);
            }
            firstrun = false;
            enemyDrawTail(length);
            //Debug.Log("Not long enough yet");
        }

    }

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
                //playerBox.transform.position = spawnPoint.position;
                //GameObject.Find("GameManager").GetComponent<GameManager>().score -= 20;
                //clearTail();
                return true;
            }
        }
        return false;

    }

    public void clearTail()
    {
        cleanList();
        foreach (enemyPositionRecord p in pastPositions)
        {
            // Debug.Log("Destroy tail" + pastPositions.Count);
            Destroy(p.BreadcrumbBox);
        }
    }

    void Update()
    {
        
    }

    IEnumerator spawnEnemy()
    {
        yield return new WaitForSeconds(3f);
        foodGenerator food = Camera.main.GetComponent<foodGenerator>();
        Vector3 randomFoodPosition = food.allTheFood[Random.Range(0, food.allTheFood.Count - 1)].Position;
        enemyHead = Instantiate(enemyBox, randomFoodPosition, Quaternion.identity);
    }
}
