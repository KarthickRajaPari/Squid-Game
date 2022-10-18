using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using TMPro;
using UnityEngine.SceneManagement;
using System;
using System.IO;

public class UserInterface : MonoBehaviour
{
    private PlayerMovement playerMovement;
    public AudioManager audioManager;
    public bool isMute;
    public int level;
    public int score;
    public int coins;
    float currentTime;
    float startMinute = 0.25f;
    
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI coinText;
    public TextMeshProUGUI levelDisplay;
    public TextMeshProUGUI currentTimeText;

    public GameObject muteIcon;
    public GameObject homePage;
    public GameObject victoryPanel;
    public GameObject greenLight;
    public GameObject redLight;
    public GameObject Light;
    public GameObject Timer;

    public Animator animator;

    void Awake() {
        CreateData();
        LoadData();
    }
    void Start()
    {
        playerMovement = FindObjectOfType<PlayerMovement>();    
        currentTime = startMinute * 60;
        coins = 0;
        audioManager.PlaySound("BG");
        levelDisplay.text = "LEVEL" + " " + (level).ToString();
        scoreText.text = score.ToString();
        Debug.Log(Application.persistentDataPath);
    }

    private void Update()
    {

        if (currentTime <= 0)
        {
            StartCoroutine(RedLight());
            
        }
        else
        {
            GreenLight();
        }

    }
    void CreateData(){
        SaveSystem.CreateUserData(this);
        
    }
    void SaveData()
    {
        SaveSystem.saveUserData(this);
    }
    void LoadData()
    {

        Data data = SaveSystem.loadUserData();
        isMute = data.isMute;
        level = data.level;
        score = data.score;

        if (isMute)
        {
            MuteSound();
        }
        else
        {
            UnMuteSound();
        }
    }

    public void MuteSound()
    {
        muteIcon.SetActive(true);
        isMute = true;
        AudioListener.pause = isMute;
        SaveData();
    }
    public void UnMuteSound()
    {
        muteIcon.SetActive(false);
        isMute = false;
        AudioListener.pause = isMute;
        SaveData();
    }

    void OnApplicationQuit()
    {
        SaveData();
    }

    public void RateUs()
    {
        Application.OpenURL("https://play.google.com/store/apps/details?id=com.KarthickRajaP.RedLightGreenLightSquidGame");
    }
    public void PrivacyPolicy()
    {
        Application.OpenURL("https://pages.flycricket.io/redlight-greenlight/privacy.html");
    }

    public void PlayGame()
    {
        homePage.SetActive(false);
        Light.SetActive(true);
        Timer.SetActive(true);
        SceneManager.LoadScene(level);
    }

    public void LevelLost()
    {
        //AdManager.ShowImageAD();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LevelCompleted()
    {
        Light.SetActive(false);
        Timer.SetActive(false);
        victoryPanel.SetActive(true);
        coinText.text = coins.ToString();
    }

    public void NextLevel()
    {
        //AdManager.ShowVideoAD();
        if (level < SceneManager.sceneCountInBuildSettings - 1)
        {
            victoryPanel.SetActive(false);
            score = score + coins;
            scoreText.text = score.ToString();
            level++;
            SaveData();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            coins = 0;
            levelDisplay.text = "LEVEL" + " " + (level).ToString();
            scoreText.text = score.ToString();
        }
        

    }
    public void buttonClickSound()
    {
        audioManager.PlaySound("ButtonClick");       
    }

    public void GreenLight()
    {
        
        greenLight.SetActive(true);
        redLight.SetActive(false);
        animator.SetBool("isRotate", false);

        currentTimeText.color = Color.green;
        currentTime = currentTime - Time.deltaTime;
        TimeSpan time = TimeSpan.FromSeconds(currentTime);
        currentTimeText.text = time.Minutes.ToString() + ":" + time.Seconds.ToString();

    }
    IEnumerator RedLight()
    {
        redLight.SetActive(true);
        greenLight.SetActive(false);
        animator.SetBool("isRotate", true);

        currentTimeText.color = Color.red;
        if (playerMovement.TouchField.Pressed)
        {
            LevelLost();
        }

        yield return new WaitForSeconds(5);

        currentTime = startMinute * 60;


    }
}