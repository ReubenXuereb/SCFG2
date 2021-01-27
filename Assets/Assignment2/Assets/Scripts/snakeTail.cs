using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class snakeTail : MonoBehaviour
{

    List<Vector3> snakeTailList;
    Vector3 snakeTailPos;

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move()
    {
        if (transform.parent.GetChild(0).GetComponent<snakeController>())
        {
            snakeTailList = transform.parent.GetChild(0).GetComponent<snakeController>().positions;
        }
       /* else
        {
            snakeTailList = transform.parent.GetChild(0).GetComponent<AIEnemy>().GetPath();
        }*/

        if((snakeTailList.Count - transform.GetSiblingIndex() + 1) <= 1)
        {
            snakeTailPos = new Vector3(50, 50, 0);
        }
        else
        {
            snakeTailPos = snakeTailList[snakeTailList.Count - (transform.GetSiblingIndex() + 1)];
        }

        transform.position = snakeTailPos;
    }





}
