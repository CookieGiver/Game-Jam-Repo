using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheepSpawner : MonoBehaviour
{
    public GameObject wildHerd;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Spawn", 10, 100);
    }

    Vector3 RandomPos()
    {
        float x = Random.Range(-30, 30);
        float y = Random.Range(-30, 30);
        Vector3 pos = new Vector3(x, y, 0);
        return pos;
    }

    void Spawn()
    {
        Vector3 pos = RandomPos();
        while (!(pos.y > 15 || pos.y < -9) && !(pos.x > 13 || pos.x < 20))
        {
            pos = RandomPos();
        }
        Instantiate(wildHerd, pos, transform.rotation);

    }
}
