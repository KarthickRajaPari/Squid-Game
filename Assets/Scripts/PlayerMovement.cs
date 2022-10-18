using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerMovement : MonoBehaviour
{
    AudioManager audio;
    public PlayerList playerList;
    public UserInterface userInterface;
    public FixedTouchField TouchField;
    public Animator animator;
    public static bool isSettingsOpen = false;
    CharacterController PlayerController;
    bool isPlayerGrounded;
    Vector3 PlayerVelocity;
    float Gravity = -9.81f;
    float speed = 5.5f;

    float sideway = 0.15f;
    
    Vector3 move;

    void Awake() {
            
    }
    void Start()
    {
        audio = FindObjectOfType<AudioManager>();
        playerList.playerCount++;
        PlayerController = GetComponent<CharacterController>();

    }
    void Update()
    {
        gravity();
        
        Vector3 inputX = TouchField.TouchDist.x * Vector3.right * sideway;
        Vector3 move = inputX + Vector3.forward;

        if(TouchField.Pressed)
        {
            PlayerController.Move(move * speed * Time.deltaTime);
            animator.SetBool("isRun", true);
        }else
        {
            animator.SetBool("isRun", false);
        }

        if(transform.position.y < 0){
            
            Destroy(transform.gameObject);
        }

        
    }
    void gravity()
    {
        isPlayerGrounded = PlayerController.isGrounded;
        if(isPlayerGrounded && PlayerVelocity.y < 0){
            PlayerVelocity.y = 0;
        }
        
        PlayerVelocity.y += Gravity * Time.deltaTime;
        PlayerController.Move(PlayerVelocity * Time.deltaTime);
    }
    private void OnTriggerEnter(Collider other) 
    {
        if(other.transform.tag == "Player")
        {
            other.transform.parent = playerList.transform;
            other.transform.GetComponent<PlayerMovement>().enabled = true;
            other.transform.GetComponent<Collider>().isTrigger = true;

        }

        if(other.transform.tag == "Finish"){
            audio.PlaySound("Win");
            userInterface.LevelCompleted();
        }

        if(other.transform.tag == "Obstacle"){
            
            transform.parent = null;
            transform.gameObject.GetComponent<PlayerMovement>().enabled = false;
            transform.gameObject.GetComponent<Collider>().enabled = false;
            transform.gameObject.GetComponent<CharacterController>().enabled = false;
            animator.SetBool("isDying", true);
            Destroy(transform.gameObject, 3f);
        }

    }
    
    private void OnDestroy() {
        playerList.playerCount--;
    }

}
