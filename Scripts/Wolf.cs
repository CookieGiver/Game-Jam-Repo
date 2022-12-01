using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wolf : MonoBehaviour
{
    public int health = 100;

    float attackCooldown;
    float cooldown = 2;

    Transform prey;

    public Animator animator;
    public Rigidbody2D rb;
    Transform player;
    public float speed = 1;
    bool following = true;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player Controller").transform;
        //rb = GetComponent<Rigidbody2D>();
        FindPrey();
    }

    // Update is called once per frame
    void Update()
    {
        if (attackCooldown > 0)
        {
            attackCooldown -= Time.deltaTime;
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

        if (prey != null)
        {
            if (following && prey)
            {
                rb.velocity = new Vector2((prey.position - transform.position).normalized.x, (prey.position - transform.position).normalized.y);
            }
            if (Vector2.Distance(transform.position, prey.position) < 2.5)
            {
                if (attackCooldown <= 0)
                {
                    animator.SetTrigger("Attack");
                    attackCooldown = cooldown;
                    if (prey.GetComponent<Sheep>().TakeDamage(40, transform))
                    {
                        FindPrey();
                    }
                }
            }
        }
        else
        {
            FindPrey();
        }
    }
    void FindPrey()
    {
        Sheep[] sheep = FindObjectsOfType<Sheep>();
        if (sheep.Length > 0)
        {
            prey = sheep[0].transform;

            foreach (Sheep s in sheep)
            {
                if (Vector2.Distance(s.transform.position, transform.position) < Vector2.Distance(prey.position, transform.position))
                {
                    prey = s.transform;
                }
            }
        }
        else
        {
            prey = null;
        }
    }
    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
