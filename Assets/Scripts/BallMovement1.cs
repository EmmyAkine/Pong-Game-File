using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallMovement1 : MonoBehaviour
{

    // Field for the ball game object
    [SerializeField] GameObject ball;

   
    // Field for the rigidbody component
    private Rigidbody2D rb;

    // boolean for gamestate
    bool isplaystate = false;
    bool atStart = false;
    bool isPlayer2serve = false;

    // Speed for ball speed and speed increment
    float ballSpeed = 50f;
    float speedIncrease = 1.5f;

    // Fields for score text and numbers
     [SerializeField] Text scoreOne;
    [SerializeField] Text scoreTwo;

    private int scoreOneIndex;
    private int scoreTwoIndex;

    // Fields for different game text Panel 
     [SerializeField] GameObject welcome;
     [SerializeField] GameObject player1Serve;
     [SerializeField] GameObject player2Serve;
     [SerializeField] GameObject player1Win;
     [SerializeField] GameObject player2Win;
     
     
    //Audio Reference
    [SerializeField] AudioSource paddleHit;
    [SerializeField] AudioSource wallHit;
    [SerializeField] AudioSource scoreHit;

   
   

    // Start is called before the first frame update
    void Start()
    { 
        // Enable welcome page
        welcome.SetActive(true);

        atStart = false;
        player1Serve.SetActive(false);

        // Grab the rigidbody component
        rb = GetComponent<Rigidbody2D>();

        // Set score numbers to be zero
        scoreOneIndex = 0;
        scoreTwoIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {

        // Method to enable player one serve text on enter key
        if (Input.GetKeyDown(KeyCode.Return) && atStart == false)
        {
            player1Serve.SetActive(true);
            welcome.SetActive(false);
            atStart = true;
        }

        // Method to start moving the ball
        else if(Input.GetKeyDown(KeyCode.Return) && isplaystate == false && atStart == true && isPlayer2serve == false)
        {
            player1Serve.SetActive(false);
            player2Serve.SetActive(false);
            welcome.SetActive(false);
            
            transform.position = new Vector3(0, 0, 0);
            BallMoveTwo();
            isplaystate = true;
        }

        // Method to move the ball in the opposite direction depending on who scores
        else if(Input.GetKeyDown(KeyCode.Return) && isplaystate == false && atStart == true && isPlayer2serve == true)
        {
            player1Serve.SetActive(false);
            player2Serve.SetActive(false);
            welcome.SetActive(false);
            player1Win.SetActive(false);
            player2Win.SetActive(false);
            
            transform.position = new Vector3(0, 0, 0);
            BallMoveOne();
            isPlayer2serve = false;
            isplaystate = true;
        }

        // To determine winner by maximum score of 5
        if (scoreOneIndex == 5  )
        {
            player1Serve.SetActive(false);
            player2Serve.SetActive(false);
            welcome.SetActive(false);
            transform.position = new Vector3(0, 0, 0);
           player2Win.SetActive(true);

            isplaystate = false;

            if (Input.GetKeyDown(KeyCode.Return))
            {
            player2Win.SetActive(false);
                scoreOneIndex = 0;
                scoreTwoIndex = 0;
                scoreOne.text = scoreOneIndex.ToString();
                scoreTwo.text = scoreTwoIndex.ToString();
            }
        }

         else if (scoreTwoIndex == 5  )
        {
            player1Serve.SetActive(false);
            player2Serve.SetActive(false);
            welcome.SetActive(false);
            transform.position = new Vector3(0, 0, 0);
            player1Win.SetActive(true);

            isplaystate = false;

             if (Input.GetKeyDown(KeyCode.Return))
            {
                player1Win.SetActive(false);
                scoreOneIndex = 0;
                scoreTwoIndex = 0;       
                scoreOne.text = scoreOneIndex.ToString();
                scoreTwo.text = scoreTwoIndex.ToString();

            }
        }
       
    }


    void BallMove()
    {
       
            int randomNumber = Random.Range(-8, 9);
             
            if (randomNumber > 0 || randomNumber == 0)
            {
            randomNumber = 8;
            rb.velocity = (new Vector2(randomNumber, Random.Range(-4.5f, 4.5f)) * ballSpeed * Time.deltaTime);

            }
            else if (randomNumber < 0)
            {
            randomNumber = -8;
            rb.velocity = (new Vector2(randomNumber, Random.Range(-4.5f, 4.5f)) * ballSpeed * Time.deltaTime);
               
            }
    }

    // Method to move the ball towards the left side
     void BallMoveOne()
    {
        rb.velocity = (new Vector2(-8 , Random.Range(-4.5f, 4.5f)) * ballSpeed * Time.deltaTime);
    }

    // Method to move the ball towards the right side
     void BallMoveTwo()
    {
        rb.velocity = (new Vector2(8 , Random.Range(-4.5f, 4.5f)) * ballSpeed * Time.deltaTime);
    }
    

    // To check for collision with the paddlle and reverse the x-direction
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Paddle")
        {
            if ( transform.position.x > 0)
            {
             BallMoveOne();
             ballSpeed += speedIncrease;    
             paddleHit.Play();
            }

            else 
            {
             BallMoveTwo();
             ballSpeed += speedIncrease;
             paddleHit.Play();
            }
        }

        if (collision.collider.tag == "Wall")
        {
          
           BounceOffWall();
           wallHit.Play();
               
        }

       
    }

    // Method to bounce of the wall
    void BounceOffWall()
    {
     // Reverse the y-component of the ball's velocity
     rb.velocity = new Vector2(rb.velocity.x, -rb.velocity.y * 100) ;
    }


    // To check if ball is past the paddle and make a score
    private void OnTriggerEnter2D(Collider2D collisionInfo) 
    {
        if (collisionInfo.gameObject.tag == "Goal")
        {
            
            ScoreoneIncrease();
            isplaystate = false;
            player1Serve.SetActive(true);
            scoreHit.Play();
            ballSpeed = 50f;
        }

        if (collisionInfo.gameObject.tag == "Goal2")
        {
            ScoretwoIncrease();
            isplaystate = false;
            player2Serve.SetActive(true);
            isPlayer2serve = true;
            scoreHit.Play();
            ballSpeed = 50f;
        }
    }


    // Increase score for player 1
    void ScoreoneIncrease()
    {
        scoreOneIndex += 1;
        scoreOne.text = scoreOneIndex.ToString();
    }

    // Increase score for player 2
    public void ScoretwoIncrease()
    {
        scoreTwoIndex += 1;
        scoreTwo.text = scoreTwoIndex.ToString();
    }
}
