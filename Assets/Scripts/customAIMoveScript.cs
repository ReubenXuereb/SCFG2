using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Pathfinding;


public class customAIMoveScript : MonoBehaviour
{
    //the object that we are using to generate the path
    Seeker seeker;

    //path to follow stores the path
    Path pathToFollow;

    //a reference from the UI to the green box
    Transform target;

    //a reference to PointGraphObject
    GameObject graphParent;

    //the node of the graph that is going to correspond with the green box
    GameObject targetNode;

    public GameObject enemyAI;

    List<Vector3> posns;

    int enemyTail;

    SpawnEnemyAI spawnAI;
    


    // Start is called before the first frame update
    void Start()
    {
        
        Debug.Log(this.name);

        //the instance of the seeker attached to this game object
        seeker = GetComponent<Seeker>();


        //node target by name
        //targetNode = GameObject.Find("TargetNode");

        //find the parent node of the point graph
         graphParent = GameObject.Find("AStarGrid");
        //graphParent = GameObject.Find("AStarGrid");
        //we scan the graph to generate it in memory
        graphParent.GetComponent<AstarPath>().Scan();
        
        //generate the initial path
        //pathToFollow = seeker.StartPath(transform.position, target.position);

       

        //update the graph as soon as you can.  Runs indefinitely
        StartCoroutine(updateGraph());

        //move the red robot towards the green enemy

        
        spawnAI = GameObject.Find("Main Camera").GetComponent<SpawnEnemyAI>();
        enemyTail = spawnAI.snakelength;
    }

 
   
    private void Update()
    {
        findTarget();


        if(Input.GetKeyDown(KeyCode.Space))
        {
            LineRenderer lineRenderer = GetComponent<LineRenderer>();
            
            lineRenderer.positionCount = posns.Count;
            lineRenderer.SetPositions(pathToFollow.vectorPath.ToArray());
        }
      
    }

    private void findTarget()
    {
        if (target == null)
        {
            if (GameObject.FindGameObjectWithTag("Player"))
            {
                target = GameObject.FindGameObjectWithTag("Player").transform;
                pathToFollow = seeker.StartPath(transform.position, target.position);
                StartCoroutine(moveTowardsEnemy(this.transform));
            }
        }
    }

    IEnumerator updateGraph()
    {
        while (true)
        {
            //targetNode.transform.position = target.position;
            graphParent.GetComponent<AstarPath>().Scan();
            yield return null;

        }

    }

    /*IEnumerator spawnEnemy()
    {
        yield return new WaitForSeconds(3f);
        Instantiate(enemyAI, new Vector3(4.5f, -2.5f, 0), Quaternion.identity);
    }*/


    
    IEnumerator moveTowardsEnemy(Transform t)
    {

        while (true)
        {

            posns = pathToFollow.vectorPath;
            Debug.Log("Positions Count: " + posns.Count);

            for (int counter = 0; counter < posns.Count; counter++)
            {
                // Debug.Log("Distance: " + Vector3.Distance(t.position, posns[counter]));
                if (posns[counter] != null) { 
                    while (Vector3.Distance(t.position, posns[counter]) >= 0.5f)
                    {
                        t.position = Vector3.MoveTowards(t.position, posns[counter], 1f);
                        //since the enemy is moving, I need to make sure that I am following him
                        pathToFollow = seeker.StartPath(t.position, target.position);
                        //wait until the path is generated
                        yield return seeker.IsDone();
                        //if the path is different, update the path that I need to follow
                        posns = pathToFollow.vectorPath;
                        spawnAI.savePosition();
                        spawnAI.enemyDrawTail(enemyTail);


                        //  Debug.Log("@:" + t.position + " " + target.position + " " + posns[counter]);
                        yield return new WaitForSeconds(0.5f);

                    }

                }
                //keep looking for a path because if we have arrived the enemy will anyway move away
                //This code allows us to keep chasing
                pathToFollow = seeker.StartPath(t.position, target.position);
                yield return seeker.IsDone();
                posns = pathToFollow.vectorPath;
                //yield return null;

            }
            yield return null;
        }
    }

    


}


