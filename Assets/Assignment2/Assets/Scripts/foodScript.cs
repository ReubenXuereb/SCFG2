using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class foodScript : MonoBehaviour
{

    /*List<Vector3> wallPositions;
    [SerializeField] GameObject food;
    //[SerializeField] GameObject food;
    //[SerializeField] int size;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(addFood());
    }


    public Vector3 randomGenerator()
    {
        Vector3 positions;
        int x, y;
        do
        {
            x = Random.Range(-9, 9);
            y = Random.Range(-9, 9);
            positions = new Vector3(x, y);
        }
        while (wallPositions.Contains(positions));

        return positions;
    }

    IEnumerator addFood()
    {
        while (true)
        {
            print("working");
            Vector3 positions = randomGenerator();
            wallPositions.Add(positions);
            GameObject obstacles = Instantiate(food, positions, Quaternion.identity);
        }
        yield return null;
    }
    /*void addFood()
    {
        for(int i = 0; 1 < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                Vector3 location = new Vector3((-7.5f + (i * 4)), (8.5f - (j * 4)), 0);
                if (Physics2D.OverlapCircleAll(location, 0.5f).Length == 0)
                {
                    Instantiate(food, location, Quaternion.identity);
                }
            }
        }
    }*/

    [SerializeField] GameObject food;

    void Start()
    {
        StartCoroutine(foodSpawner());
    }

   

    IEnumerator foodSpawner()
    {
        bool alternatey = false;
        for (float ycoord = -9.5f; ycoord <= 9.5f; ycoord++)
        {
            //for each row
            for (float xcoord = -9.5f; xcoord <= 9.5f; xcoord++)
            {
                GameObject sq = createSquare(xcoord, ycoord);
               // if (alternatey)
               // {
                    if ((Mathf.Floor(xcoord) % 5 == 0))
                    {
                        sq.GetComponent<SpriteRenderer>().color = Color.green;
                    }
                    else if((Mathf.Floor(ycoord) % 5 == 0))
                    {
                    sq.GetComponent<SpriteRenderer>().color = Color.green;
                    }
                   /* else
                    {
                       sq.GetComponent<SpriteRenderer>().color = Color.;
                    }*/
               // }
                /*else
                {
                    if ((Mathf.Floor(ycoord) % 4 == 0))
                    {
                        sq.GetComponent<SpriteRenderer>().color = Color.green;
                    }
                    else
                    {
                        sq.GetComponent<SpriteRenderer>().color = Color.white;
                    }
                }*/
                /*else
                {
                    if (Mathf.Floor(xcoord) % 4 == 0)
                    {
                       sq.GetComponent<SpriteRenderer>().color = Color.white;
                    }
                    else
                    {
                        sq.GetComponent<SpriteRenderer>().color = Color.green;
                    }
                }*/

                //yield return new WaitForSeconds(1f);
            }
            //alternatey = !alternatey;
            yield return new WaitForSeconds(1f);
        }
        yield return null;
    }

    GameObject createSquare(float xpos, float ypos)
    {
        return Instantiate(food, new Vector3(xpos, ypos), Quaternion.identity);
    }

}