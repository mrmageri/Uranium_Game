using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

namespace Managers
{
    public class AchievementsManager : MonoBehaviour
    {
        [SerializeField] private TMP_Text textField;
        [SerializeField] private GameObject[] achievementsObjects;
        public AchievementSave achievementSave;
        public List<Achievement> achievements = new List<Achievement>();
        private int currentAchievements = 0;
        
        public static AchievementsManager achievementsManager;

        AchievementsManager()
        {
            achievementsManager = this;
        }


        private void Awake()
        {
            LoadAchievements(achievementSave);
            OpenAchievement(0);
            currentAchievements = 0;
            for (int i = 0; i < achievementSave.achievements.Count; i++)
            { 
                achievementsObjects[i].SetActive(achievementSave.achievements[i] == achievements[i].maxProgress);
                if(achievementSave.achievements[i] == achievements[i].maxProgress) currentAchievements++;
            }
            UpdateText();
        }

        public void OpenAchievement(int number)
        {
            if(number >= achievementSave.achievements.Count) return;
            if(achievementSave.achievements[number] == achievements[number].maxProgress) return;
            achievementSave.achievements[number]++;
            if (achievementSave.achievements[number] >= achievements[number].maxProgress)
            {
                achievementSave.achievements[number] = achievements[number].maxProgress;
                achievementsObjects[number].SetActive(true);
                currentAchievements++;
                UpdateText();
            }
            SaveAchievements(achievementSave);
        }

        public static void LoadAchievements(AchievementSave save)
        {
            string path = Application.persistentDataPath + "achievements.savegame";
            if (File.Exists(path))
            {
                string json = File.ReadAllText(path);
                JsonUtility.FromJsonOverwrite(json,save);
            }
        }

        public static void SaveAchievements(AchievementSave achievementSave)
        {
            string dir = Application.persistentDataPath;
            if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);
            string json = JsonUtility.ToJson(achievementSave);
            
            File.WriteAllText(dir + "achievements.savegame",json);
        }

        private void UpdateText()
        {
            textField.text = currentAchievements + "/" + achievements.Count;
        }
    }
}
