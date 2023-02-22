using UnityEngine;
using UnityEngine.AI;

namespace EnemySystem
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private int speed;

        protected NavMeshAgent _navMeshAgent;

        //private Transform _target;

        void Awake()
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _navMeshAgent.updateRotation = false;
            _navMeshAgent.updateUpAxis = false;

            _navMeshAgent.speed = speed;
        }
    }
}
