using UnityEngine;

namespace Managers
{
    [CreateAssetMenu(fileName = "Achievement", menuName = "ScriptableObject/Achievements", order = 0)]
    public class Achievement : ScriptableObject
    {
        public string achName;
        public string achDescription;
        public int maxProgress;
    }
}