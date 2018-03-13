using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Ball: MonoBehaviour
{
    public Text Score0, Score1;
    public Rigidbody2D player0, player1;
    public AudioClip[] effect;

    private Rigidbody2D myRigidbody;
    private Vector2 myDirection, initPos, distance;
    private AudioSource source;
    private float mySpeed = 8.5f;
    private int score0 = 0, score1 = 0;
    private int countdown = 0;

	private void Awake()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        source = GetComponent<AudioSource>();
        initPos = myRigidbody.position;
        InitDirection();
    }

    private void FixedUpdate()
    {
        WinState();

        if (countdown == 0)
            Move();
        else
            countdown--;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Top")
        {
            source.PlayOneShot(effect[0]);
            myDirection.y = -Mathf.Abs(myDirection.y);
        }
        if (other.gameObject.tag == "Bottom")
        {
            source.PlayOneShot(effect[0]);
            myDirection.y = Mathf.Abs(myDirection.y);
        }
        if (other.gameObject.tag == "Left")
        {
            source.PlayOneShot(effect[0]);
            myDirection.x = Mathf.Abs(myDirection.x);
        }
        if (other.gameObject.tag == "Right")
        {
            source.PlayOneShot(effect[0]);
            myDirection.x = -Mathf.Abs(myDirection.x);
        }
        if (other.gameObject.tag == "ScoreArea0")
        {
            source.PlayOneShot(effect[3]);
            countdown = 50;
            myRigidbody.MovePosition(initPos);
            mySpeed = 8.5f;
            InitDirection();
            Score0.text = "Score: " + ++score0;
        }
        if (other.gameObject.tag == "ScoreArea1")
        {
            source.PlayOneShot(effect[3]);
            countdown = 50;
            myRigidbody.MovePosition(initPos);
            mySpeed = 8.5f;
            InitDirection();
            Score1.text = "Score: " + ++score1;
        }

        if (other.gameObject.tag == "Player0")
        {
            myDirection.x = Mathf.Abs(myDirection.x);
            if (Skills_P0.sticky == false && Skills_P0.charge == false)
                source.PlayOneShot(effect[0]);
            if (Skills_P0.sticky == true)
            {
                source.PlayOneShot(effect[2]);
                Skills_P0.sticked = true;
                distance = myRigidbody.position - player0.position;
                mySpeed += 20.0f;
            }
            if (Skills_P0.charge == true)
            {
                source.PlayOneShot(effect[1]);
                mySpeed += 12.0f;
            }
        }
        if (other.gameObject.tag == "Player1")
        {
            myDirection.x = -Mathf.Abs(myDirection.x);
            if (Skills_P1.sticky == false && Skills_P1.charge == false)
                source.PlayOneShot(effect[0]);
            if (Skills_P1.sticky == true)
            {
                source.PlayOneShot(effect[2]);
                Skills_P1.sticked = true;
                distance = myRigidbody.position - player1.position;
                mySpeed += 20.0f;
            }
            if (Skills_P1.charge == true)
            {
                source.PlayOneShot(effect[1]);
                mySpeed += 12.0f;
            }
        }
    }

    private void InitDirection()
    {
        myDirection = new Vector2(Random.Range(-1.0f, 1.0f), 0);
        myDirection.Normalize();
        myDirection.y = Random.Range(-0.3f, 0.3f);
        myDirection.Normalize();
    }

    private void Move()
    {
        if (Skills_P0.sticked == false && Skills_P1.sticked == false)
        {
            Vector2 movement = myDirection * mySpeed * Time.deltaTime;
            myRigidbody.MovePosition(myRigidbody.position + movement);
            Decelerate();
        }
        else if (Skills_P0.sticked == true)
            myRigidbody.MovePosition(player0.position + distance);
        else if (Skills_P1.sticked == true)
            myRigidbody.MovePosition(player1.position + distance);
    }

    private void WinState()
    {
        if (score0 == 5)
            SceneManager.LoadScene("Win0", LoadSceneMode.Single);
        if (score1 == 5)
            SceneManager.LoadScene("Win1", LoadSceneMode.Single);
    }

    private void Decelerate()
    {
        if (mySpeed > 8.5f)
            mySpeed -= 0.06f;
        else if (mySpeed < 8.5f)
            mySpeed = 8.5f;
    }
}
