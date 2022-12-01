using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RBTest : MonoBehaviour
{
    public Rigidbody2D rb;
    Transform player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player Controller").transform;

    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2((player.position - transform.position).normalized.x, (player.position - transform.position).normalized.y);
    }
}

