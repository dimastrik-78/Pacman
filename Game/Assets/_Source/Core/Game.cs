using System.Collections.Generic;
using BonusSystem;
using EnemySystem;
using PacmanSystem;
using UISystem;
using UnityEngine;

namespace Core
{
    public class Game
    {
        private readonly GameUI _gameUI;
        private readonly SpawnUniqueBonus _spawnUniqueBonus;
        private readonly float _pauseGame;
        private readonly List<Enemy> _enemies;
        private readonly AudioSource _loseAudio;

        public Game(GameUI gameUI, SpawnUniqueBonus spawnUniqueBonus, float pauseGame, List<Enemy> enemies, AudioSource loseAudio)
        {
            _gameUI = gameUI;
            _spawnUniqueBonus = spawnUniqueBonus;
            _pauseGame = pauseGame;
            _enemies = enemies;
            _loseAudio = loseAudio;
            
            PacmanInvoker.OnCollisionEnemy += RestartLevel;

            Time.timeScale = 1;
        }

        private void RestartLevel(int hp)
        {
            CheckHp(hp);
            
            foreach (var enemy in _enemies)
            {
                enemy.StartCoroutine(enemy.Restart(_pauseGame));
            }

            _spawnUniqueBonus.StartCoroutine(_spawnUniqueBonus.PauseSpawn(_pauseGame));
        }

        private void CheckHp(int hp)
        {
            if (hp <= 0)
            {
                LoseGame();
            }
        }

        private void LoseGame()
        {
            _loseAudio.Play();
            
            _gameUI.LosePanel();
            
            PacmanInvoker.OnCollisionEnemy -= RestartLevel;

            Time.timeScale = 0;
        }
    }
}