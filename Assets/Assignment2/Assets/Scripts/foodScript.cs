using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class foodScript : MonoBehaviour
{

    /*List<Vector3> wallPositions;
    [SerializeField] GameObject food;*/
    [SerializeField] GameObject food;
    [SerializeField] int size;
    // Start is called before the first frame update
    void Start()
    {
        addFood();
    }


    /*public Vector3 randomGenerator()
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

    public void addFood()
    {
        while (true)
        {
            print("working");
            Vector3 positions = randomGenerator();
            wallPositions.Add(positions);
            GameObject obstacles = Instantiate(food, positions, Quaternion.identity);
        }
    }*/
    void addFood()
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
    }
}