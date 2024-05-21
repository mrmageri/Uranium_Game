using System;
using Managers;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Transform[] wanderingPoints;
    public bool gameStarted = false;
    public bool gameStopped = false;
    public int daysWorked = 0;
    [SerializeField] private TMP_Text dayCountField;
    [SerializeField] private string dayCountPhrase;
    [SerializeField] private Transform endTransform;
    
    public int workMin;
    public int workSec;
    [SerializeField] private TMP_Text timeText;
    [SerializeField] private TMP_Text timeTVText;
    private int minLast = 0;
    private int secLast = 0;
    private string min;
    private string sec;

    private Player.Player player;
    private TickManager tickManager;
    
    public static GameManager instanceGameManager;
    
    GameManager()
    {
        instanceGameManager = this;
    }

    private void Awake()
    {
        AchievementsManager.achievementsManager.OpenAchievement(0);
        player = Player.Player.instancePlayer;
        tickManager = TickManager.instanceTickManager;
    }

    private void Start()
    {
        SetTime();
    }

    public void CountSec()
    {
        if(!gameStarted) return;
        secLast += 1;
        if (secLast + 1 == 60)
        {
            minLast++; 
            secLast = 0;
        }
        UpdateDays();
        min = minLast.ToString();
        sec = secLast.ToString();
        if (minLast < 10) min = "0" + minLast;
        if (secLast < 10) sec = "0" + secLast;
        timeText.text = min + ":" + sec;
        timeTVText. text =  min + ":" + sec;
    }

    public int GetTime()
    {
        return minLast * 60 + secLast;
    }

    public void SetTime()
    {
        minLast = 0;
        secLast = 0;
        sec = "00";
        min = "00";
        timeText.text = min + ":" + sec;
        UpdateDays();
    }

    public void EndGame()
    {
        gameStarted = false;
        tickManager.ResetMachines();
        player.transform.position = endTransform.position;
        gameStopped = true;
        sec = "00";
        min = "00";
        timeText.text = min + ":" + sec;
        UpdateDays();
    }

    public void Restart()
    {
        //TODO delete save
        SceneManager.LoadScene("MainScene");
        
    }
    

    private void UpdateDays()
    {
        if (secLast == 0 && minLast == workMin)
        {
            daysWorked++;
            secLast = 0;
            minLast = 0;
        }
        dayCountField.text = daysWorked + dayCountPhrase;
    }
}
