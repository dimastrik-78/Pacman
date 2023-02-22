using System;
using UISystem;
using UnityEngine;

namespace PacmanSystem
{
    public class PacmanInvoker : MonoBehaviour
    {
        [SerializeField] private UIPacman ui;
        [SerializeField] private Transform bonusList;
        [SerializeField] private int speed;

        private PacmanInput _pacmanInput;

        private int _hp = 3;
        private int _points;

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

        private void Movement()
        {
            transform.position = new Vector3(transform.position.x + _pacmanInput.Player.MoveX.ReadValue<float>() * speed * Time.deltaTime, transform.position.y + _pacmanInput.Player.MoveY.ReadValue<float>() * speed * Time.deltaTime);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.layer == 8)
            {
                _points++;
                
                ui.ChangePoint(_points);

                if (_points >= bonusList.childCount)
                {
                    ui.WinPanel();
                    Time.timeScale = 0;
                }
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.layer == 7)
            {
                _hp--;
                
                ui.ChangeHpPacman();

                if (_hp <= 0)
                {
                    ui.LosePanel();
                    Time.timeScale = 0;
                }
            }
        }
    }
}
