using System.IO;
using UnityEngine;

namespace Managers
{
    public class SettingsSaver : MonoBehaviour
    {
        [HideInInspector]public float brightness;
        [HideInInspector]public float volume;
        [HideInInspector]public float sensitivityX;
        [HideInInspector]public float sensitivityY;
        
        public void SaveSetting()
        {
            SettingsManager settingsManager = SettingsManager.iSettingsManager;
            brightness = settingsManager.brightness;
            volume = settingsManager.volume;
            sensitivityX = settingsManager.sensitivityX;
            sensitivityY = settingsManager.sensitivityY;
            
            

            string dir = Application.persistentDataPath;
            if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);
            string json = JsonUtility.ToJson(this);
            
            File.WriteAllText(dir + "settings.savegame",json);
        }

        public bool LoadSetting()
        {
            string path = Application.persistentDataPath + "settings.savegame";
            if (File.Exists(path))
            {
                string json = File.ReadAllText(path);
                JsonUtility.FromJsonOverwrite(json, this);
            }
            else
            {
                return false;
            }

            SettingsManager settingsManager = SettingsManager.iSettingsManager;

            settingsManager.brightness = brightness;
            settingsManager.volume = volume;
            settingsManager.sensitivityX = sensitivityX;
            settingsManager.sensitivityY = sensitivityY;
            return true;
        }

        public static void DeleteSave()
        {
            string path = Application.persistentDataPath + "settings.savegame";
            File.Delete(path);
        }
    }
}
