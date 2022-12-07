using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    [SerializeField] private DrawScripts drawScripts;
    [SerializeField] private ThrowBallScripts throwBallScripts;
    [SerializeField] private GameObject entryPanel;
    [SerializeField] private GameObject finishPanel;
    [SerializeField] private GameObject drawRightPanel;
    [SerializeField] private GameObject voicePanel;
    [SerializeField] private TextMeshProUGUI[] scoreTexts;
    [SerializeField] private ParticleSystem goal;
    [SerializeField] private Image imageOff;
    [SerializeField] private Image imageOnn;
    AddMobScript addMobScript = new AddMobScript();
    

    //[SerializeField] private ParticleSystem hoopGoal;


    [SerializeField] private AudioSource[] Musics;

    public int goalsNumbers;
    
    void Start()
    {
        if (PlayerPrefs.HasKey("BestScore"))
        {
            scoreTexts[0].text = PlayerPrefs.GetInt("BestScore").ToString();
            scoreTexts[1].text = PlayerPrefs.GetInt("BestScore").ToString();
        }
        else
        {
            PlayerPrefs.SetInt("BestScore", 0);
            scoreTexts[0].text = "0";
            scoreTexts[1].text = "0";
        }
        addMobScript.RequestInterstitial();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void GoOn(Vector3 Pos)
    {
        goalsNumbers++;
        goal.transform.position = Pos;
        goal.gameObject.SetActive(true);
        Musics[1].Play();
        drawScripts.GoOn();
        throwBallScripts.GoOn();
        
        

        
    }
    public void GamePlay()
    {
        drawScripts.keyDraw = true;
        entryPanel.SetActive(false);
        throwBallScripts.GamePlay();
        drawRightPanel.SetActive(true);
    }
    public void GameOver()
    {
        Musics[2].Play();
        finishPanel.SetActive(true);
        drawRightPanel.SetActive(false);

        scoreTexts[1].text = PlayerPrefs.GetInt("BestScore").ToString();
        scoreTexts[2].text = goalsNumbers.ToString();
        
        
        if (goalsNumbers>PlayerPrefs.GetInt("BestScore"))
        {
            PlayerPrefs.SetInt("BestScore", goalsNumbers);
            Musics[3].Play();
            
        }
        throwBallScripts.BallStop();
        drawScripts.keyDraw = false;
        addMobScript.InterstitialAdShow();
    }
    public void TryAgain()
    {
       
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }
    public void AppQuit()
    {
        Application.Quit();
    }
    public void SettingsButton()
    {

        voicePanel.SetActive(true);
        entryPanel.SetActive(false);

    }
    public void VoiceOff()
    {
        AudioListener.volume = 0;
        imageOff.color = Color.green;
        imageOnn.color = Color.black;


    }
    public void VoiceOnn()
    {
        AudioListener.volume = 1;
        imageOff.color = Color.black;
        imageOnn.color = Color.green;


    }
    public void GoBackButton()
    {
        entryPanel.SetActive(true);
        voicePanel.SetActive(false);

    }
    
    
}
