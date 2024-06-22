using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        

        public Transform[] wanderingPoints;
        public bool gameStarted = false;
        public bool gameStopped = false;
        public int daysWorked = 0;
        [SerializeField] private TMP_Text dayCountField;
        [SerializeField] private string dayCountPhrase;
        [SerializeField] private Transform endTransform;

        [Header("On Day Switch")] 
        private List<DayMachine> dayMachines = new List<DayMachine>();
        
        public List<GameObject> items = new List<GameObject>();
    
        public int workMin;
        public int workSec;
        [SerializeField] private TMP_Text timeText;
        [SerializeField] private TMP_Text timeTVText;
        [HideInInspector] public int minLast = 0;
        [HideInInspector] public int secLast = 0;
        private string min;
        private string sec;

        private Player.Player player;
        private TickManager tickManager;
        private MoneyManager moneyManager;
    
        public static GameManager instanceGameManager;
    
        GameManager()
        {
            instanceGameManager = this;
        }

        private void Awake()
        {
            player = Player.Player.instancePlayer;
            tickManager = TickManager.instanceTickManager;
            moneyManager = MoneyManager.instanceMoneyManager;
            LevelSaveManager.instanceLevelSaveManager.LoadLevel();
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
            LevelSaveManager.DeleteSave();
            SceneManager.LoadScene("MainScene");
        }


        public void AddDayMachine(DayMachine newDayMachine)
        {
            dayMachines.Add(newDayMachine);
        }


        private void UpdateDays()
        {
            if (secLast == 0 && minLast == workMin)
            {
                daysWorked++;
                secLast = 0;
                minLast = 0;
                DaySwitchEvents();
            }
            dayCountField.text = daysWorked + dayCountPhrase;
        }

        private void DaySwitchEvents()
        {
            moneyManager.PayForDay();
            foreach (var elem in dayMachines)
            {
                elem.onDaySwitch();
            }
        }
    }
}
