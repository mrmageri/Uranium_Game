
using System;
using UnityEditor;

namespace Managers
{
    [Serializable]
    public class ItemSave
    {
        //public int persistentID;
        public string persistentID;
        //TODO
        public float posX;
        public float posY;
        public float posZ;
        public float rotX;
        public float rotY;
        public float rotZ;
        public float rotW;
    }
}