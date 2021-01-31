using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleScript : MonoBehaviour
{
    [SerializeField] Vector3[] waypoints;

    private int counter;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(moveToWaypoint());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator moveToWaypoint() 
    {
        while (true)
        {
           transform.position = Vector3.MoveTowards(transform.position, waypoints[counter], 1f);
            yield return new WaitForSeconds(1f);
            if(transform.position == waypoints[counter])
            {
                counter++;
                if(counter >= waypoints.Length)
                {
                    counter = 0;
                }
            }
        }
    }


}
