using System.Collections.Generic;
using BonusSystem;
using EnemySystem;
using PacmanSystem;
using UISystem;
using UnityEngine;
using UnityEngine.Audio;

namespace Core
{
    public class Bootstrapper : MonoBehaviour
    {
        [Header("Pacman")]
        [SerializeField] private GameObject prefabPacman;
        [SerializeField] private Transform spawnPoint;

        [Header("Enemy")] 
        [SerializeField] private List<Enemy> enemies;
        [SerializeField] private int givePointEnemy;
        
        [Header("Bonus")]
        [SerializeField] private int givePointBonus;
        [SerializeField] private SpawnUniqueBonus spawnUniqueBonus;
        [SerializeField] private int givePointUniqueBonus;

        [Header("Other")] 
        [SerializeField] private GameUI gameUI;
        [SerializeField] private float pauseGame;
        [SerializeField] private AudioMixer main;
        [SerializeField] private AudioSource loseAudio;
        
        private Game _game;
        
        void Awake()
        {
            main.SetFloat("Music", PlayerPrefs.GetFloat("Music"));
            main.SetFloat("Effects", PlayerPrefs.GetFloat("Effects"));
            
            _game = new Game(gameUI, spawnUniqueBonus, pauseGame, enemies, loseAudio);
            
            prefabPacman.GetComponent<PacmanInvoker>().SetData(spawnPoint, pauseGame, givePointBonus, givePointUniqueBonus, givePointEnemy);
        }
    }
}
