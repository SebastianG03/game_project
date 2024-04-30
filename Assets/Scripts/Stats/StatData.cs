using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.Stats
{
    public abstract class StatData : ScriptableObject
    {
        public Sprite Icon { get; private set; }
        public StatsType Stat { get; private set; }
        public string Description { get; private set; }
        
        public abstract void Initialize();
        public abstract void UpdateStat();
        public abstract void ResetStat();
        public abstract string GetStatName();
    }
}