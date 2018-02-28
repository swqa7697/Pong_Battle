using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Ball: MonoBehaviour
{
    public float mySpeed;
    public Text Score0;
    public Text Score1;

    private Rigidbody2D myRigidbody;
    private Vector2 myDirection;
    private Vector2 initPos;
    private int score0 = 0, score1 = 0;
    private int countdown = 0;

	private void Awake()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        initPos = myRigidbody.position;
        myDirection = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-0.5f, 0.5f));
        myDirection.Normalize();
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
        if (other.gameObject.tag == "Top_bottom_edge")
            myDirection.y = -myDirection.y;
        if (other.gameObject.tag == "Left_right_edge")
            myDirection.x = -myDirection.x;
        if (other.gameObject.tag == "Player")
            myDirection.x = -myDirection.x;
        if (other.gameObject.tag == "ScoreArea0")
        {
            countdown = 30;
            myRigidbody.MovePosition(initPos);
            Score0.text = "Score: " + ++score0;
        }
        if (other.gameObject.tag == "ScoreArea1")
        {
            countdown = 30;
            myRigidbody.MovePosition(initPos);
            Score1.text = "Score: " + ++score1;
        }
    }

    private void Move()
    {
        Vector2 movement = myDirection * mySpeed * Time.deltaTime;
        myRigidbody.MovePosition(myRigidbody.position + movement);
    }

    private void WinState()
    {
        if (score0 == 10)
            SceneManager.LoadScene("Win0", LoadSceneMode.Single);
        if (score1 == 10)
            SceneManager.LoadScene("Win1", LoadSceneMode.Single);
    }
}
