using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player0: MonoBehaviour
{
    public Material original, charged, sticky;
    public AudioClip[] effect;
    public RawImage[] icon;
    public Texture[] image;

    private Rigidbody2D myRigidbody;
    private Renderer rend;
    private AudioSource source;
    private float mySpeed = 3.5f, yMin = -4.05f, yMax = 4.05f;

    private void Awake()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        rend = GetComponent<Renderer>();
        source = GetComponent<AudioSource>();
    }

    private void FixedUpdate()
    {
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
        if (Input.GetKey(KeyCode.W))
            input += Vector2.up;
        if (Input.GetKey(KeyCode.S))
            input += Vector2.down;
        input.Normalize();

        Vector2 newPosition = myRigidbody.position + input * mySpeed * Time.deltaTime;
        newPosition = new Vector2(newPosition.x, Mathf.Clamp(newPosition.y, yMin, yMax));

        myRigidbody.MovePosition(newPosition);
    }

    private void Skills()
    {
        //Boost
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            if (Skills_P0.boostCD == 0)
            {
                icon[0].texture = image[3];
                source.PlayOneShot(effect[0]);
                Skills_P0.boostCD = 750;
                Skills_P0.boostActived = 250;
                Skills_P0.boost = true;
                mySpeed += 4.5f;
            }
        }
        //Sticky
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            if (Skills_P0.stickyCD == 0)
            {
                icon[1].texture = image[3];
                rend.material = sticky;
                source.PlayOneShot(effect[2]);
                Skills_P0.stickyCD = 1500;
                Skills_P0.sticky = true;
            }
        }
        //Expand
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            if (Skills_P0.expandCD == 0)
            {
                icon[2].texture = image[3];
                source.PlayOneShot(effect[3]);
                Skills_P0.expandCD = 2000;
                Skills_P0.expandActived = 400;
                Skills_P0.expand = true;
                myRigidbody.transform.localScale += new Vector3(0, 4.0f, 0);
                yMin = -2.02f;
                yMax = 2.02f;
            }
        }
    }

    private void CoolDowns()
    {
        //Boost
        if (Skills_P0.boostCD > 0)
        {
            Skills_P0.boostCD--;
            if (Skills_P0.boostCD == 0)
                icon[0].texture = image[0];
        }
        if (Skills_P0.boostActived > 0)
            Skills_P0.boostActived--;
        else
        {
            if (Skills_P0.boost)
            {
                source.PlayOneShot(effect[1]);
                Skills_P0.boost = false;
                mySpeed -= 4.5f;
            }
        }

        //Sticky
        if (Skills_P0.stickyCD > 0)
        {
            Skills_P0.stickyCD--;
            if (Skills_P0.stickyCD == 0)
                icon[1].texture = image[1];
        }

        //Expand
        if (Skills_P0.expandCD > 0)
        {
            Skills_P0.expandCD--;
            if (Skills_P0.expandCD == 0)
                icon[2].texture = image[2];
        }
        if (Skills_P0.expandActived > 0)
            Skills_P0.expandActived--;
        else
        {
            if (Skills_P0.expand)
            {
                source.PlayOneShot(effect[4]);
                Skills_P0.expand = false;
                myRigidbody.transform.localScale -= new Vector3(0, 4.0f, 0);
                yMin = -4.05f;
                yMax = 4.05f;
            }
        }

        //Charge
        if (Skills_P0.chargeActived > 0)
        {
            Skills_P0.chargeActived--;

            if (Skills_P0.chargeActived == 0)
            {
                rend.material = original;
                Skills_P0.charge = false;
            }
        }
    }

    private void Charge()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            rend.material = charged;
            source.PlayOneShot(effect[5]);
            Skills_P0.charge = true;
            Skills_P0.chargeActived = 5;
            if (Skills_P0.sticky)
            {
                Skills_P0.sticky = false;
                Skills_P0.sticked = false;
            }
        }
    }
}
