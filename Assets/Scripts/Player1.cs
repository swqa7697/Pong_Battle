using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player1: MonoBehaviour
{
    public Rigidbody2D ball;
    public Material original, charged, sticky;
    public AudioClip[] effect;
    public RawImage[] icon;
    public Texture[] image;

    [System.NonSerialized]
    public static bool isAI;

    private Rigidbody2D myRigidbody;
    private Renderer rend;
    private AudioSource source;
    private float mySpeed = 3.5f, yMin = -4.05f, yMax = 4.05f, endY;
    private bool AI_Boost = false, AI_Expand = false, AI_Alert = false;
    private Vector2 last, current;
    private int countdown = 5, boostDelay = 150;

    private void Awake()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        rend = GetComponent<Renderer>();
        source = GetComponent<AudioSource>();
    }

    private void FixedUpdate()
    {
        if (isAI)
            AI_Move();
        else
            Move();

        CoolDowns();
    }

    private void Update()
    {
        Skills();
        Charge();
    }

    private void Move()
    {
        Vector2 input = Vector2.zero;
        if (Input.GetKey(KeyCode.UpArrow))
            input += Vector2.up;
        if (Input.GetKey(KeyCode.DownArrow))
            input += Vector2.down;
        input.Normalize();

        Vector2 newPosition = myRigidbody.position + input * mySpeed * Time.deltaTime;
        newPosition = new Vector2(newPosition.x, Mathf.Clamp(newPosition.y, yMin, yMax));

        myRigidbody.MovePosition(newPosition);
    }

    private void AI_Move()
    {
        if (countdown == 5)
            last = ball.position;

        countdown--;

        if (countdown == 0)
        {
            current = ball.position;
            endY = FindY(last, current);
            countdown = 5;
        }

        Vector2 input = Vector2.zero;
        if (endY > myRigidbody.position.y && endY < 4.8f)
            input += Vector2.up;
        else if (endY < myRigidbody.position.y && endY > -4.8f)
            input += Vector2.down;
        input.Normalize();

        Vector2 newPosition = myRigidbody.position + input * mySpeed * Time.deltaTime;
        newPosition = new Vector2(newPosition.x, Mathf.Clamp(newPosition.y, yMin, yMax));

        myRigidbody.MovePosition(newPosition);
    }

    private float FindY(Vector2 last,Vector2 current)
    {
        float deltaX = current.x - last.x, deltaY = current.y - last.y;
        if (deltaX <= 0)
            return myRigidbody.position.y;
        else
            return current.y + (((8.7f - last.x) / deltaX) * deltaY);
    }

    private void Skills()
    {
        //Boost
        if (Input.GetKeyDown(KeyCode.Comma) || AI_Boost)
        {
            if (AI_Boost)
                AI_Boost = false;
            if (Skills_P1.boostCD == 0)
            {
                icon[0].texture = image[3];
                source.PlayOneShot(effect[0]);
                Skills_P1.boostCD = 750;
                Skills_P1.boostActived = 250;
                Skills_P1.boost = true;
                mySpeed += 4.5f;
            }
        }
        //Sticky
        if (Input.GetKeyDown(KeyCode.Period))
        {
            if (Skills_P1.stickyCD == 0)
            {
                icon[1].texture = image[3];
                rend.material = sticky;
                source.PlayOneShot(effect[2]);
                Skills_P1.stickyCD = 1500;
                Skills_P1.sticky = true;
            }
        }
        //Expand
        if (Input.GetKeyDown(KeyCode.Slash) || AI_Expand)
        {
            if (AI_Expand)
                AI_Expand = false;
            if (Skills_P1.expandCD == 0)
            {
                icon[2].texture = image[3];
                source.PlayOneShot(effect[3]);
                Skills_P1.expandCD = 2000;
                Skills_P1.expandActived = 400;
                Skills_P1.expand = true;
                myRigidbody.transform.localScale += new Vector3(0, 4.0f, 0);
                yMin = -2.02f;
                yMax = 2.02f;
            }
        }
    }

    private void CoolDowns()
    {
        //Boost
        if (Skills_P1.boostCD > 0)
        {
            Skills_P1.boostCD--;
            if (Skills_P1.boostCD == 0)
                icon[0].texture = image[0];
        }
        if (Skills_P1.boostActived > 0)
            Skills_P1.boostActived--;
        else
        {
            if (Skills_P1.boost)
            {
                source.PlayOneShot(effect[1]);
                Skills_P1.boost = false;
                mySpeed -= 4.5f;
            }
        }
        if (isAI && Skills_P1.boostCD == 0)
        {
            boostDelay--;
            if (boostDelay == 0)
            {
                AI_Boost = true;
                boostDelay = 150;
            }
        }

        //Sticky
        if (Skills_P1.stickyCD > 0)
        {
            Skills_P1.stickyCD--;
            if (Skills_P1.stickyCD == 0)
                icon[1].texture = image[1];
        }
        if (isAI && Skills_P0.sticked)
            AI_Alert = true;

        //Expand
        if (Skills_P1.expandCD > 0)
        {
            Skills_P1.expandCD--;
            if (Skills_P1.expandCD == 0)
                icon[2].texture = image[2];
        }
        if (Skills_P1.expandActived > 0)
            Skills_P1.expandActived--;
        else
        {
            if (Skills_P1.expand)
            {
                source.PlayOneShot(effect[4]);
                Skills_P1.expand = false;
                myRigidbody.transform.localScale -= new Vector3(0, 4.0f, 0);
                yMin = -4.05f;
                yMax = 4.05f;
            }
        }
        if (isAI && AI_Alert && Skills_P0.charge)
        {
            AI_Alert = false;
            AI_Expand = true;
        }

        //Charge
        if (Skills_P1.chargeActived > 0)
        {
            Skills_P1.chargeActived--;

            if (Skills_P1.chargeActived == 0)
            {
                rend.material = original;
                Skills_P1.charge = false;
            }
        }
    }

    private void Charge()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            rend.material = charged;
            source.PlayOneShot(effect[5]);
            Skills_P1.charge = true;
            Skills_P1.chargeActived = 5;
            if (Skills_P1.sticky == true)
            {
                Skills_P1.sticky = false;
                Skills_P1.sticked = false;
            }
        }
    }
}
