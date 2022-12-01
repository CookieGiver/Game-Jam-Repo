using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    Transform enemy;
    public Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        FindWolves();
        if (enemy == null)
        {
            rb.velocity = 3 * new Vector2(Random.value, Random.value);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (enemy != null)
        {
            rb.velocity = 2*(enemy.position - transform.position).normalized;
        }
        transform.Rotate(new Vector3(0, 0, 1));
    }
    void FindWolves()
    {
        Wolf[] wolf = FindObjectsOfType<Wolf>();
        if (wolf.Length > 0)
        {
            enemy = wolf[0].transform;

            foreach (Wolf w in wolf)
            {
                if (Vector2.Distance(w.transform.position, transform.position) < Vector2.Distance(enemy.position, transform.position))
                {
                    enemy = w.transform;
                }
            }
        }
        else
        {
            enemy = null;
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.name.Contains("Sheep") && collision.name != "Player Controller")
        {
            try
            {
                collision.GetComponent<Wolf>().TakeDamage(20);
            }
            finally
            {
                Destroy(gameObject);
            }
        }
            
    }
}
