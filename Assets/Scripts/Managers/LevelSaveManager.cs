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
        public List<int> itemId = new List<int>();
        public List<ItemSave> itemSaves = new List<ItemSave>();
        public float brightness;
        public float volume;
        public float sensitivity_x;
        public float sensitivity_y;
        public int money;
        //public List<float> itemsPositionX = new List<float>();
        //public List<float> itemsPositionY = new List<float>();
        //public List<float> itemsPositionZ = new List<float>();

        public static LevelSaveManager instanceLevelSaveManager;
        
        LevelSaveManager()
        {
            instanceLevelSaveManager = this;
        }
        
        public void SaveLevel()
        {
            GameManager gm = GameManager.instanceGameManager;
            MoneyManager moneyM = MoneyManager.instanceMoneyManager;
            daysLived = gm.daysWorked;
            minLast = gm.minLast;
            secLast = gm.secLast;
            money = moneyM.money;
            
            TickManager tm = TickManager.instanceTickManager;
            machines.Clear();
            foreach (var t in tm.machines)
            {
                machines.Add(t.isBroken);
            }
            //TODO work on item position saving system
            itemId.Clear();
            itemSaves.Clear();
            var items = FindObjectsByType<Item>(FindObjectsSortMode.InstanceID);
            for (int i = 0; i < items.Length; i++)
            {
                itemId.Add(items[i].id);
                ItemSave item = new ItemSave();
                //item.persistentID = AssetDatabase.AssetPathToGUID(AssetDatabase.GetAssetPath(items[i].gameObject));
                //item.persistentID = AssetDatabase.AssetPathToGUID(AssetDatabase.GetAssetPath(items[i].gameObject));
                //item.persistentID = items[i].gameObject.GetInstanceID();
                item.posX = items[i].transform.position.x;
                item.posY = items[i].transform.position.y;
                item.posZ = items[i].transform.position.z;
                item.rotX = items[i].transform.rotation.x;
                item.rotY = items[i].transform.rotation.y;
                item.rotZ = items[i].transform.rotation.z;
                item.rotW = items[i].transform.rotation.w;
                itemSaves.Add(item);
            }
            

            string dir = Application.persistentDataPath;
            if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);
            string json = JsonUtility.ToJson(this);
            
            File.WriteAllText(dir + "/level.savegame",json);
        }

        public void LoadLevel()
        {
            string path = Application.persistentDataPath + "/level.savegame";
            if (File.Exists(path))
            {
                string json = File.ReadAllText(path);
                JsonUtility.FromJsonOverwrite(json, this);
            }
            else
            {
                return;
            }

            GameManager gm = GameManager.instanceGameManager;
            MoneyManager moneyM = MoneyManager.instanceMoneyManager;
            gm.daysWorked = daysLived;
            gm.minLast = minLast;
            gm.secLast = secLast;
            moneyM.money = money;
            

            TickManager tm = TickManager.instanceTickManager;
            for (int i = 0; i < tm.machines.Count; i++)
            {
                if (machines[i]) tm.machines[i].ResetBroken();
            }
            //TODO work on item position loading system

            var items = FindObjectsByType<Item>(FindObjectsSortMode.InstanceID);
            foreach (var elem in items)
            {
                Destroy(elem.gameObject);
            }

            for (int i = 0; i < itemId.Count; i++)
            {
                    Vector3 lastPoint = new Vector3(itemSaves[i].posX, itemSaves[i].posY, itemSaves[i].posZ);
                    Quaternion quaternion = new Quaternion(itemSaves[i].rotX,itemSaves[i].rotY,itemSaves[i].rotZ,itemSaves[i].rotW);
                    Instantiate(gm.items[itemId[i]].gameObject, lastPoint, quaternion);
                    //Instantiate(AssetDatabase.LoadAssetAtPath<GameObject>(itemSaves[i].persistentID), lastPoint, quaternion);
            }
        }

        public static void DeleteSave()
        {
            string path = Application.persistentDataPath + "/level.savegame";
            File.Delete(path);
        }
            
    }
}