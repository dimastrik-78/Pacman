using System;
using System.Collections;
using DG.Tweening;
using EnemySystem.State;
using Interface;
using UISystem;
using UnityEngine;
using Utils;

namespace PacmanSystem
{
    public class PacmanInvoker : MonoBehaviour
    {
        public static event Action<int> OnCollisionEnemy;
        
        [SerializeField] private GameUI gameUI;
        [SerializeField] private AudioSource die;
        [SerializeField] private AudioSource baseMusic;
        [SerializeField] private int speed;
        [SerializeField] private LayerMask enemyLayer;
        [SerializeField] private LayerMask bonusLayer;
        [SerializeField] private LayerMask uniqueBonusLayer;
        [SerializeField] private Color baseColor;
        [SerializeField] private SpriteRenderer spriteRenderer;

        private PacmanInput _pacmanInput;
        private Transform _spawnPosition;
        private int _hp = 4;
        private int _points;
        private int _getPointBonus;
        private int _getPointUniqueBonus;
        private int _getPointEnemy;
        private float _pauseTime;
        
        [HideInInspector] public int X = 1;

        private void Awake()
        {
            _pacmanInput = new PacmanInput();
        }

        private void OnEnable()
        {
            _pacmanInput.Enable();
        }

        private void OnDisable()
        {
            _pacmanInput.Disable();
        }

        private void FixedUpdate()
        {
            Movement();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (bonusLayer.Contains(other.gameObject.layer))
            {
                _points += _getPointBonus;

                gameUI.ChangePoint(_points);
            }

            if (uniqueBonusLayer.Contains(other.gameObject.layer))
            {
                _points += _getPointUniqueBonus;

                gameUI.ChangePoint(_points);
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (enemyLayer.Contains(collision.gameObject.layer))
            {
                IDamage enemy = collision.gameObject.GetComponent<IDamage>();
                
                if (enemy.GetState() is DangerousState)
                {
                    StartCoroutine(GetDamage());
                }
                else
                {
                    _points += _getPointEnemy * X;
                    X *= 2;
                    
                    enemy.GetDamage();
                    gameUI.ChangePoint(_points);
                }
            }
        }

        private IEnumerator GetDamage()
        {
            baseMusic.Stop();
            
            _hp--;
            
            die.Play();
            
            _pacmanInput.Disable();
            
            OnCollisionEnemy?.Invoke(_hp);

            spriteRenderer.DOColor(new Color(baseColor.r, baseColor.g, baseColor.b, 0), 2f).OnComplete(() =>
            {
                gameUI.ChangeHpPacman();

                transform.position = _spawnPosition.position;

                spriteRenderer.color = baseColor;
            });
            
            yield return new WaitForSeconds(_pauseTime + 2);
            
            baseMusic.Play();
            
            _pacmanInput.Enable();
        }
        
        private void Movement()
        {
            transform.position = new Vector3(transform.position.x + _pacmanInput.Player.MoveX.ReadValue<float>() * speed * Time.deltaTime, 
                transform.position.y + _pacmanInput.Player.MoveY.ReadValue<float>() * speed * Time.deltaTime);

            Teleport();
        }

        private void Teleport()
        {
            switch (transform.position.x)
            {
                case < -9f:
                    transform.position = new Vector2(9f, 0);
                    break;
                case > 9f:
                    transform.position = new Vector2(-9f, 0);
                    break;
            }
        }

        public void SetData(Transform spawnPosition, float pauseTime, int bonusPoint, int uniqueBonusPoint, int enemyPoint)
        {
            _spawnPosition = spawnPosition;

            _pauseTime = pauseTime;
            
            _getPointBonus = bonusPoint;
            _getPointUniqueBonus = uniqueBonusPoint;
            _getPointEnemy = enemyPoint;
        }
    }
}
