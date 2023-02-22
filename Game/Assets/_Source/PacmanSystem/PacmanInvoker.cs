using System;
using UISystem;
using UnityEngine;

namespace PacmanSystem
{
    public class PacmanInvoker : MonoBehaviour
    {
        [SerializeField] private UIPacman ui;
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

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer == 8)
            {
                _points++;
                
                ui.ChangePoint(_points);
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.layer == 7)
            {
                _hp--;
                
                ui.ChangeHpPacman();
            }
        }
    }
}
