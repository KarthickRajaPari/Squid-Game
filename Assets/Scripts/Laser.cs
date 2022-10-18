using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    LineRenderer lr;
    void Start()
    {
        lr = GetComponent<LineRenderer>();
    }


    void Update()
    {
        lr.SetPosition(0, lr.transform.position);
        RaycastHit hit;

        float x = Random.Range(-30, 30);
        float y = Random.Range(-30, 30);


        transform.Rotate(x, y, 0);
        
        if(Physics.Raycast(transform.position, transform.forward, out hit))
        {
            if (hit.collider)
            {
                lr.SetPosition(1, hit.point);
            }
            
        }
        else
        {
            lr.SetPosition(1, transform.forward * 10f);
        }
    }
}
