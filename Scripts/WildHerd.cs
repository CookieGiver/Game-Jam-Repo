using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;
public class WildHerd : MonoBehaviour
{
    public int num;
    public double rad;
    public GameObject sheep;

    GameObject captureBtn;
    public GameObject eBtn;
    public int playerCollisions = 0;
    public bool playerInRange = false;
    public Vector3 center;

    float captureTime = 0;
    bool isCapturing = false;

    public GameObject captureBar;
    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject obj in Resources.FindObjectsOfTypeAll<GameObject>())
        {
            if (obj.name == "Load Bar")
            {
                captureBar = obj;
            }
        }

        print(captureBar);

        rad = Random.value * 2 + 1;
        num = (int)Math.Round(rad * rad / 2);

        for (int i = 0; i < num; i++)
        {
            Vector3 pos = Random.insideUnitCircle * (int)Math.Round(rad);
            Instantiate(sheep, pos+transform.position, transform.rotation, transform);
            center += pos + transform.position;
        }
        center /= num;
        captureBar.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (playerCollisions > 0 && playerInRange == false)
        {
            captureBtn = Instantiate(eBtn, center + Vector3.up, transform.rotation, transform);
            playerInRange = true;
        }
        else if (playerCollisions == 0 && playerInRange == true)
        {
            Destroy(captureBtn);
            playerInRange = false;
        }

        if (playerInRange)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                isCapturing = true;
                captureBar.SetActive(true);
            }
            if (Input.GetKeyUp(KeyCode.E))
            {
                isCapturing = false;
                captureTime = 0;
                captureBar.SetActive(false);
            }
            if (isCapturing)
            {
                captureTime += Time.deltaTime;
                captureBar.GetComponent<CaptureBar>().SetValue(captureTime);
                if (captureTime >= captureBar.GetComponent<Slider>().maxValue)
                {
                    captureBar.SetActive(false);
                    Destroy(captureBtn);
                    foreach (Transform child in transform)
                    {
                        child.GetComponent<Sheep>().harnessed = true;
                        child.GetComponent<Animator>().SetTrigger("Harness");
                        transform.DetachChildren();
                    }
                    Destroy(gameObject);
                }

            }
        }

    }

}
