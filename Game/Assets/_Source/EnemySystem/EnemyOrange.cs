using System.Collections;
using Interface;
using UnityEngine;

namespace EnemySystem
{
    public class EnemyOrange : Enemy
    {
        [SerializeField] private Transform[] route;
        
        private int _indexRoute;

        private void Start()
        {
            NavMeshAgent.SetDestination(route[_indexRoute].position);

            StartCoroutine(UpdateRoute());
        }

        IEnumerator UpdateRoute()
        {
            yield return new WaitForSeconds(0.1f);
            
            if (NavMeshAgent.remainingDistance == 0)
            {
                _indexRoute++;
            
                if (_indexRoute >= route.Length)
                {
                    _indexRoute = 0;
                }

                NavMeshAgent.SetDestination(route[_indexRoute].position);
            }
            
            StartCoroutine(UpdateRoute());
        }
    }
}
