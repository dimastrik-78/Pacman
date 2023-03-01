using Interface;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

namespace EnemySystem
{
    public class Enemy : MonoBehaviour, IDamage
    {
        [SerializeField] private int speed;
        [SerializeField] protected LayerMask player;
        [SerializeField] private SpriteRenderer sprite;

        protected NavMeshAgent NavMeshAgent;

        private bool _canGetDamage;

        //private Transform _target;
        
        public int GivePoint { get; set; }

        void Awake()
        {
            NavMeshAgent = GetComponent<NavMeshAgent>();
            NavMeshAgent.updateRotation = false;
            NavMeshAgent.updateUpAxis = false;

            NavMeshAgent.speed = speed;
        }

        public void GetDamage()
        {
            NavMeshAgent.SetDestination(new Vector2(0, 0));
            sprite.color = new Color(0.5f, 0.5f, 0.5f, 0.5f);
        }
    }
}
