using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DollHead : MonoBehaviour
{
    Animator animator;
    public GameObject laserEye1;
    public GameObject laserEye2;
    bool isRotate;

    void Start()
    {
        animator = GetComponent<Animator>();

    }


    void Update()
    {
        
        isRotate = animator.GetBool("isRotate");
        if (isRotate)
        {
            laserEye1.gameObject.SetActive(true);
            laserEye2.gameObject.SetActive(true);
        }
        else
        {
            laserEye1.gameObject.SetActive(false);
            laserEye2.gameObject.SetActive(false);
        }

    }
    
}
