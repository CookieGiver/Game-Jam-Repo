using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PressurePlate : MonoBehaviour
{
    public TextMeshProUGUI text;
    public int points = 0;
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        animator.SetBool("Active", true);
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.name.Contains("Sheep"))
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                Destroy(collision.gameObject);
                points += 20;
                text.text = points.ToString();
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        animator.SetBool("Active", false);
    }
}
