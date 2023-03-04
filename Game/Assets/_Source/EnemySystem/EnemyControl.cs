using System;
using System.Collections;
using System.Collections.Generic;
using Interface;
using PacmanSystem;
using UnityEngine;

namespace EnemySystem
{
    public class EnemyControl : MonoBehaviour
    {
        [SerializeField] private float timeVulnerable;
        
        private List<IVulnerable> _enemies = new();

        private void Awake()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                _enemies.Add(transform.GetChild(i).GetComponent<IVulnerable>());
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
                enemy.DisableVulnerable();
            }
        }
    }
}