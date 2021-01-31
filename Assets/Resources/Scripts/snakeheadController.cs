using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Pathfinding;
using UnityEngine.SceneManagement;

public class snakeheadController : MonoBehaviour
{
    snakeGenerator mysnakegenerator;
    Transform sp;
    foodGenerator myfoodgenerator,myfoodgenerator2;
    GameManager gm;
    

   
    public Vector3 findClosestFood()
    {
        if (myfoodgenerator.allTheFood.Count > 0)
        {
            List<positionRecord> sortedFoods = myfoodgenerator.allTheFood.OrderBy(
        x => Vector3.Distance(this.transform.position, x.Position)
       ).ToList();
            return sortedFoods[0].Position;
        }
        return new Vector3(0f, 0f);
    }

    public IEnumerator automoveCoroutine()
    {
        while(true)
        {


            yield return null;
        }
        
    }



    private void Start()
    {
        mysnakegenerator = Camera.main.GetComponent<snakeGenerator>();
        myfoodgenerator = Camera.main.GetComponent<foodGenerator>();
        sp = mysnakegenerator.spawnPoint;
        
    }

    void checkBounds()
    {
        if ((transform.position.x < -(Camera.main.orthographicSize-0.5)) || (transform.position.x > (Camera.main.orthographicSize - 0.5)))
        {
            transform.position = new Vector3(-transform.position.x,transform.position.y);
        }

        if ((transform.position.y < -(Camera.main.orthographicSize - 0.5)) || (transform.position.y > (Camera.main.orthographicSize - 0.5)))
        {
            transform.position = new Vector3(transform.position.x, -transform.position.y);
        }
    }

    //when snake dies
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.transform.CompareTag("Obstacle"))
        {
            if (SceneManager.GetActiveScene().name == "Level1")
            {
                this.gameObject.transform.position = sp.position;
                mysnakegenerator.clearTail();
                mysnakegenerator.snakelength = 2;
                GameObject.Find("GameManager").GetComponent<GameManager>().score -= 20;
            }
           else if (SceneManager.GetActiveScene().name == "Level2")
            {
                this.gameObject.transform.position = sp.position;
                mysnakegenerator.clearTail();
                mysnakegenerator.snakelength = 6;
                GameObject.Find("GameManager").GetComponent<GameManager>().score -= 20;
            }
           else if (SceneManager.GetActiveScene().name == "Level3")
            {
                this.gameObject.transform.position = sp.position;
                mysnakegenerator.clearTail();
                mysnakegenerator.snakelength = 2;
                GameObject.Find("GameManager").GetComponent<GameManager>().score -= 20;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

        
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {

            Debug.LogWarning("Closest food" + findClosestFood());
            transform.position -= new Vector3(1f,0);
            checkBounds();
            myfoodgenerator.eatFood(this.transform.position);
            Debug.Log(mysnakegenerator.hitTail(this.transform.position, mysnakegenerator.snakelength));

        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            Debug.LogWarning("Closest food" + findClosestFood());
            transform.position += new Vector3(1f, 0);
            checkBounds();
            myfoodgenerator.eatFood(this.transform.position);
            Debug.Log(mysnakegenerator.hitTail(this.transform.position, mysnakegenerator.snakelength));
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Debug.LogWarning("Closest food" + findClosestFood());
            transform.position += new Vector3(0, 1f);
            checkBounds();
            myfoodgenerator.eatFood(this.transform.position);
            Debug.Log(mysnakegenerator.hitTail(this.transform.position, mysnakegenerator.snakelength));
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            Debug.LogWarning("Closest food" + findClosestFood());
            transform.position -= new Vector3(0, 1f);
            checkBounds();
            myfoodgenerator.eatFood(this.transform.position);
            Debug.Log(mysnakegenerator.hitTail(this.transform.position, mysnakegenerator.snakelength));
        }           //Debug.Log(mysnakegenerator.hitTail(this.transform.position, mysnakegenerator.snakelength)); 
    }
}
