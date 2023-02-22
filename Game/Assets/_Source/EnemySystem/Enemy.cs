using UnityEngine;
using UnityEngine.AI;

namespace EnemySystem
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private int speed;
        [SerializeField] private Transform target;

        private NavMeshAgent _navMeshAgent;
    
        void Awake()
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _navMeshAgent.updateRotation = false;
            _navMeshAgent.updateUpAxis = false;

            _navMeshAgent.speed = speed;
        }

        void Update()
        {
            _navMeshAgent.SetDestination(target.position);
        }
    }
}
