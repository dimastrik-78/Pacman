using UnityEngine;

namespace Core
{
    public class Bootstrapper : MonoBehaviour
    {
        [Header("Pacman")]
        [SerializeField] private GameObject prefabPacman;
        [SerializeField] private Transform spawnPoint;

        [Header("Enemy")] 
        [SerializeField] private GameObject[] prefabsEnemy;
        [SerializeField] private Transform[] enemySpawnPoints;
        
        [Header("Bonus")]
        [SerializeField] private int givePoint;
        
        void Awake()
        {
            
        }
    }
}
