using System.Collections;
using UnityEngine;
using Random = System.Random;

namespace BonusSystem
{
    public class SpawnUniqueBonus : MonoBehaviour
    {
        [SerializeField] private GameObject prefabsBonus;
        [SerializeField] private Transform imageBonus;
        [SerializeField] private Transform[] spawnPoints;
        [SerializeField] private int minTimeSpawnBonus;
        [SerializeField] private int maxTimeSpawnBonus;
        [SerializeField] private AudioSource audioSource;

        private readonly Random _random = new();
        
        private int _countUniqueBonus;
        private int _countImage;
        private bool _pause;
        
        private void Awake()
        {
            StartCoroutine(SpawnBonus());
        }

        private IEnumerator SpawnBonus()
        {
            yield return new WaitForSeconds(_random.Next(minTimeSpawnBonus, maxTimeSpawnBonus + 1));

            if (!_pause
                && _countUniqueBonus < 4)
            {
                Instantiate(prefabsBonus, spawnPoints[_random.Next(0, spawnPoints.Length)]).GetComponent<SelectionUniqueBonus>().SetBonusUI(this);

                _countUniqueBonus++;
            }
        }

        public IEnumerator PauseSpawn(float pauseTime)
        {
            _pause = true;
            
            yield return new WaitForSeconds(pauseTime);
            
            _pause = false;
            
            StartCoroutine(SpawnBonus());
        }

        public void ImageBonus()
        {
            audioSource.Play();
            imageBonus.GetChild(_countImage).gameObject.SetActive(true);
            _countImage++;
        }
    }
}