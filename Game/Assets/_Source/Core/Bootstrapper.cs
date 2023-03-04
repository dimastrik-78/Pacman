using PacmanSystem;
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
        [SerializeField] private int givePointEnemy;
        
        [Header("Bonus")]
        [SerializeField] private int givePointBonus;
        
        void Awake()
        {
            prefabPacman.GetComponent<PacmanInvoker>().SetPoint(givePointBonus, givePointEnemy);
        }
    }
}
