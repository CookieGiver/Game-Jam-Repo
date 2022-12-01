using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public GameObject projectile;
    public Transform staffTop;

    public Transform staffBottom;
    public GameObject spell1Circle;
    public bool spell1Active = false;

    bool motionStopped = false;
    public Animator animator;
    Rigidbody2D rb;
    public int playerSpeed = 8;
    public bool wildSheepInRange = false;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Game Started");
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Q))
        {
            Instantiate(projectile, transform.position, transform.rotation);
        }

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Player_Idle") || animator.GetCurrentAnimatorStateInfo(0).IsName("Player_Fast"))
        {
            motionStopped = false;
        }
        else
        {
            motionStopped = true;
        }
        if (!motionStopped)
        {
            rb.velocity = new Vector2(playerSpeed * Input.GetAxisRaw("Horizontal"), playerSpeed * Input.GetAxisRaw("Vertical"));
        }

        if (rb.velocity.x > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        if (rb.velocity.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        animator.SetFloat("Speed", Math.Abs(rb.velocity.x));

        if (Input.GetKeyDown(KeyCode.F))
        {
            if (!spell1Active)
            {
                animator.SetTrigger("Spell1");
                spell1Active = true;
                Destroy(Instantiate(spell1Circle, staffBottom), 1);
                rb.velocity = Vector2.zero;
            }
            else
            {
                spell1Active = false;
            }
        }

    }
}
