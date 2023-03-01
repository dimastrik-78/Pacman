using EnemySystem;
using Interface;
using UISystem;
using UnityEngine;
using Utils;

namespace PacmanSystem
{
    public class PacmanInvoker : MonoBehaviour
    {
        [SerializeField] private GameUI gameUI;
        [SerializeField] private Transform bonusList;
        [SerializeField] private int speed;
        [SerializeField] private LayerMask enemy;
        [SerializeField] private LayerMask usualBonus;
        [SerializeField] private LayerMask coolBonus;

        private PacmanInput _pacmanInput;
        private int _hp = 3;
        private int _points;
        private bool _canDamage;

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
            if (usualBonus.Contains(other.gameObject.layer))
            {
                _points++;
                
                gameUI.ChangePoint(_points);

                if (_points >= bonusList.childCount)
                {
                    gameUI.WinPanel();
                    Time.timeScale = 0;
                }
            }
            
            if (coolBonus.Contains(other.gameObject.layer))
            {
                _points++;
                
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
            if (enemy.Contains(collision.gameObject.layer))
            {
                if (!_canDamage)
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
                    Enemy enemyObject = collision.gameObject.GetComponent<Enemy>();
                    enemyObject.GetDamage();
                    gameUI.ChangePoint(enemyObject.GivePoint);
                }
            }
        }
        
        private void Movement()
        {
            transform.position = new Vector3(transform.position.x + _pacmanInput.Player.MoveX.ReadValue<float>() * speed * Time.deltaTime, 
                transform.position.y + _pacmanInput.Player.MoveY.ReadValue<float>() * speed * Time.deltaTime);
        }

        public void ChangeState()
        {
            _canDamage = true;
        }
    }
}
