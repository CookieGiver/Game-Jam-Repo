using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class Sheep : MonoBehaviour
{
    float oldTime;
    float newTime;
    public int health = 100;

    public Transform enemy;
    float startleTimer = 0;

    PlayerScript player;
    public GameObject wolf;
    public GameObject explosion;

    public Animator playerAnim;
    Transform playerController;
    Transform staff;

    public Animator animator;
    Rigidbody2D rb;

    public GameObject eBtn;
    public bool harnessed = false;
    public bool mesmered;

    public int weight;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player Controller").GetComponent<PlayerScript>();
        weight = (int)Math.Round(Random.value * 200 + 100);

        rb = GetComponent<Rigidbody2D>();
        staff = GameObject.Find("Staff Bottom").transform;
        playerController = GameObject.Find("Player Controller").transform;
        playerAnim = playerController.GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        newTime = (Time.time / 40)%24;
        if (newTime > 12 && oldTime < 12)
        {
            if (Random.value < 0.1)
            {
                Morph();
            }
        }

        //startle effect after taking damage
        if (startleTimer > 0)
        {
            startleTimer -= Time.deltaTime;
            rb.velocity = 4.5f * (transform.position - playerController.position).normalized;
        }


        if (mesmered)
        {
            if (!playerAnim.GetCurrentAnimatorStateInfo(0).IsName("Player_Spell1"))
            {
                if (Input.GetKeyDown(KeyCode.F) && !player.spell1Active)
                {
                    mesmered = false;
                    GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 255);
                }
                rb.velocity = (playerController.position - transform.position).normalized;
            }
        }


        // the conditional statement uses spell1Active == true, because it activates after the player script meaning, by that time it is activated.
        if (Input.GetKeyDown(KeyCode.F) && Math.Pow((transform.position.x - staff.position.x) / 2, 2) + Math.Pow(transform.position.y - staff.position.y, 2) <= 6 && playerController.GetComponent<PlayerScript>().spell1Active == true && harnessed)
        {
            mesmered = true;
            GetComponent<SpriteRenderer>().color = new Color32(245, 255, 140, 255);
        }


        if (Math.Abs(rb.velocity.x) > Math.Abs(rb.velocity.y))
        {
            animator.SetFloat("Speed", Math.Abs(rb.velocity.x));

        }
        else
        {
            animator.SetFloat("Speed", Math.Abs(rb.velocity.y));
        }

        if (rb.velocity.x > 0.3)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        if (rb.velocity.x < -0.3)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            Morph();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Player Controller" && transform.root != transform)
        {
            gameObject.GetComponentInParent<WildHerd>().playerCollisions++;

        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.name == "Player Controller" && transform.root != transform)
        {
            gameObject.GetComponentInParent<WildHerd>().playerCollisions--;

        }
    }
    public void Morph()
    {
        Instantiate(explosion, transform.position, transform.rotation);
        Instantiate(wolf, transform.position, transform.rotation);
        Destroy(gameObject);
    }

    public bool TakeDamage(int damage, Transform predator)
    {
        enemy = predator;
        health -= damage;
        if (health <= 0)
        {
            Destroy(gameObject);
            return true;
        }
        else
        {
            startleTimer = 1.5f;
            return false;
        }
    }
}
