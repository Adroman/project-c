using UnityEngine;

namespace Waves
{
    [CreateAssetMenu(menuName = "Project C/Wave Template")]
    public class WaveTemplate : ScriptableObject
    {
        public EnemyBehavior PrefabToSpawn;
        public int BaseAmount;
        public float BaseHp;
        public float BaseArmor;
        public float Speed;
        public int MinimumWaveNumber;
        public float Interval;
    }
}