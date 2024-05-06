using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

namespace Waves
{
    public class WaveManager : MonoBehaviour
    {
        public int WaveNumber;
        public Transform SpawnPoint;
        public List<Transform> Waypoints;
        public List<WaveTemplate> Templates;
        public Transform EnemiesParent;

        private void Start()
        {
            SpawnWave();
        }
        
        public void SpawnWave()
        {
            WaveNumber++;
            var templatesToChoose = Templates
                .Where(t => t.MinimumWaveNumber <= WaveNumber)
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
            float hp = template.BaseHp * Mathf.Pow(1.1f, WaveNumber - 1);
            float armor = template.BaseArmor * Mathf.Pow(1.1f, WaveNumber - 1);
            float amount = template.BaseAmount; // + modifier

            SpawnEnemy(template.PrefabToSpawn, hp, armor, template.Speed);
            for (int i = 1; i < amount; i++)
            {
                yield return new WaitForSeconds(template.Interval);
                SpawnEnemy(template.PrefabToSpawn, hp, armor, template.Speed);
            }
        }

        private void SpawnEnemy(EnemyBehavior prefab, float hp, float armor, float speed)
        {
            var enemy = Instantiate(prefab, SpawnPoint.position, SpawnPoint.rotation, EnemiesParent);
            enemy.MaxHp = hp;
            enemy.Armor = armor;
            enemy.Speed = speed;
            enemy.Waypoints = Waypoints.ToList();
        }
    }
}