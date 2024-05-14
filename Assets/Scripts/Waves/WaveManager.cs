using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using Variables;
using Random = UnityEngine.Random;

namespace Waves
{
    public class WaveManager : MonoBehaviour
    {
        public IntVariable WaveNumber;
        public Transform SpawnPoint;
        public List<Transform> Waypoints;
        public List<WaveTemplate> Templates;
        public Transform EnemiesParent;

        public IntVariable EnemiesWaiting;
        public IntVariable EnemiesOnField;
        public IntVariable Difficulty;

        public void Awake()
        {
            EnemiesWaiting.Value = 0;
            EnemiesOnField.Value = 0;
        }

        public void SpawnWave()
        {
            WaveNumber.AddValue(1);
            var templatesToChoose = Templates
                .Where(t => t.MinimumWaveNumber <= WaveNumber.Value)
                .ToListPooled();
            if (templatesToChoose.Count == 0)
            {
                Debug.LogError("No template to choose from");
                return;
            }

            var template = templatesToChoose[Random.Range(0, templatesToChoose.Count)];
            StartCoroutine(SpawnEnemies(template));
        }

        private IEnumerator SpawnEnemies(WaveTemplate template)
        {
            float hp = template.BaseHp * Mathf.Pow(1.2f, WaveNumber.Value - 1);
            float armor = template.BaseArmor * Mathf.Pow(1.2f, WaveNumber.Value - 1);
            int amount = template.BaseAmount + Difficulty.Value;
            EnemiesWaiting.AddValue(amount);

            SpawnEnemy(template.PrefabToSpawn, hp, armor, template.Speed);
            for (int i = 1; i < amount; i++)
            {
                yield return new WaitForSeconds(template.Interval);
                SpawnEnemy(template.PrefabToSpawn, hp, armor, template.Speed);
            }
        }

        private void SpawnEnemy(EnemyBehavior prefab, float hp, float armor, float speed)
        {
            EnemiesWaiting.SubtractValue(1);
            EnemiesOnField.AddValue(1);
            var enemy = Instantiate(prefab, SpawnPoint.position, SpawnPoint.rotation, EnemiesParent);
            enemy.MaxHp = hp;
            enemy.Armor = armor;
            enemy.Speed = speed;
            enemy.Waypoints = Waypoints.ToList();
        }
    }
}