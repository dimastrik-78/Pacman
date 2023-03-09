using System.Collections;
using System.Collections.Generic;
using EnemySystem.State;
using PacmanSystem;
using UnityEngine;

namespace EnemySystem
{
    public class EnemyControl : MonoBehaviour
    {
        [SerializeField] private float timeVulnerable;
        [SerializeField] private PacmanInvoker pacman;
        
        private readonly List<Enemy> _enemies = new();

        private void Awake()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                _enemies.Add(transform.GetChild(i).GetComponent<Enemy>());
            }
        }

        public void GetVulnerable()
        {
            foreach (var enemy in _enemies)
            {
                enemy.EnableVulnerable();
            }

            StartCoroutine(TimeEnableVulnerable());
        }

        private IEnumerator TimeEnableVulnerable()
        {
            yield return new WaitForSeconds(timeVulnerable);

            foreach (var enemy in _enemies)
            {
                if (enemy.GetState() is VulnerableState)
                {
                    enemy.DisableVulnerable();
                }
            }
            
            pacman.X = 1;
        }
    }
}