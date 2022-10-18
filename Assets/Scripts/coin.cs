using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coin : MonoBehaviour
{
    AudioManager audio;
    public UserInterface userInterface;
    public GameObject coinfx;


    private void Start()
    {
        audio = FindObjectOfType<AudioManager>();
    }
    private void OnTriggerEnter(Collider other) {
        if(other.transform.tag == "Player"){
            
            audio.PlaySound("Coin");
            coinfx.SetActive(true);
            Destroy(gameObject, .3f);

        }
    }

    private void OnDestroy() {
        userInterface.coins++;
    }
 
}
