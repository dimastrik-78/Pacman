using EnemySystem;
using EnemySystem.State;
using Interface;
using UISystem;
using UnityEngine;
using UnityEngine.Serialization;
using Utils;

namespace PacmanSystem
{
    public class PacmanInvoker : MonoBehaviour
    {
        [SerializeField] private GameUI gameUI;
        [SerializeField] private Transform bonusList;
        [SerializeField] private int speed;
        [SerializeField] private LayerMask enemyLayer;
        [SerializeField] private LayerMask bonusLayer;

        private PacmanInput _pacmanInput;
        private int _hp = 3;
        private int _points;
        private int _getPointBonus;
        private int _getPointEnemy;

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

                if (_points >= bonusList.childCount)
                {
                    gameUI.WinPanel();
                    Time.timeScale = 0;
                }
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (enemyLayer.Contains(collision.gameObject.layer))
            {
                IDamage enemy = collision.gameObject.GetComponent<IDamage>();
                
                if (enemy.GetState() is DangerousState)
                {
                    _hp--;

                    gameUI.ChangeHpPacman();

                    if (_hp <= 0)
                    {
                        gameUI.LosePanel();
                        Time.timeScale = 0;
                    }
                }
                else
                {
                    _points += _getPointEnemy;
                    
                    enemy.GetDamage();
                    gameUI.ChangePoint(_points);
                }
            }
        }
        
        private void Movement()
        {
            transform.position = new Vector3(transform.position.x + _pacmanInput.Player.MoveX.ReadValue<float>() * speed * Time.deltaTime, 
                transform.position.y + _pacmanInput.Player.MoveY.ReadValue<float>() * speed * Time.deltaTime);
        }

        public void SetPoint(int bonusPoint, int enemyPoint)
        {
            _getPointBonus = bonusPoint;
            _getPointEnemy = enemyPoint;
        }
    }
}
