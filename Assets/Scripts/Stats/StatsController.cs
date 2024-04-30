using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Stats
{
    [System.Serializable]
    [CreateAssetMenu(fileName = "StatsController", menuName = "Stats/StatsController")]
    public class StatsController : ScriptableObject
    {
        public Dictionary<StatData, float> instanceStats = new Dictionary<StatData, float>();
        public Dictionary<StatData, float> stats = new Dictionary<StatData, float>();

        public float GetStat(StatData stat)
        {
            return 0;
        }

        public void SetStat(StatData stat, float value)
        {

        }

        public void AddStat(StatData stat, float value)
        {

        }

        public void RemoveStat(StatData stat, float value)
        {

        }

        public void ResetStat(StatData stat)
        {

        }
        public void ResetAllStats()
        {
            
        }
    }
}