using System.Collections.Generic;
using System.IO;
using Items;
using UnityEngine;

namespace Managers
{
    public class LevelSaveManager : MonoBehaviour
    {
        public int daysLived;
        public int minLast;
        public int secLast;
        public List<bool> machines = new List<bool>();
        public List<float> itemsPositionX = new List<float>();
        public List<float> itemsPositionY = new List<float>();
        public List<float> itemsPositionZ = new List<float>();

        public static LevelSaveManager instanceLevelSaveManager;
        
        LevelSaveManager()
        {
            instanceLevelSaveManager = this;
        }
        
        public void SaveLevel()
        {
            GameManager gm = GameManager.instanceGameManager;
            daysLived = gm.daysWorked;
            minLast = gm.minLast;
            secLast = gm.secLast;
            
            TickManager tm = TickManager.instanceTickManager;
            foreach (var t in tm.machines)
            {
                machines.Add(t.isBroken);
            }
            //TODO work on item position saving system
            /*
            var items = FindObjectsByType<Item>(FindObjectsSortMode.InstanceID);
            for (int i = 0; i < items.Length; i++)
            {
                itemsPositionX.Add(items[i].transform.position.x);
                itemsPositionY.Add(items[i].transform.position.y);
                itemsPositionZ.Add(items[i].transform.position.z);
            }
            */

            string dir = Application.persistentDataPath;
            if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);
            string json = JsonUtility.ToJson(this);
            
            File.WriteAllText(dir + "level.savegame",json);
        }

        public void LoadLevel()
        {
            string path = Application.persistentDataPath + "level.savegame";
            if (File.Exists(path))
            {
                string json = File.ReadAllText(path);
                JsonUtility.FromJsonOverwrite(json,this);
            }
            else
            {
                return;
            }
            
            GameManager gm = GameManager.instanceGameManager;
            gm.daysWorked = daysLived;
            gm.minLast = minLast;
            gm.secLast = secLast;
            
            TickManager tm = TickManager.instanceTickManager;
            for (int i = 0; i < tm.machines.Length; i++)
            {
                if(machines[i]) tm.machines[i].ResetBroken();
            }
            //TODO work on item position loading system
            /*
             var items = FindObjectsByType<Item>(FindObjectsSortMode.InstanceID);
            for (int i = 0; i < items.Length; i++)
            {
                items[i].transform.position = new Vector3(itemsPositionX[i],itemsPositionY[i],itemsPositionZ[i]);
            }
            */
        }
    }
}