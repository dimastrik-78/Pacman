using System.Collections;
using EnemySystem.State;
using Interface;
using UnityEngine;

namespace EnemySystem
{
    public class EnemyOrange : Enemy
    {
        private int _indexRoute;

        protected override void Awake()
        {
            base.Awake();
            
            NavMeshAgent.SetDestination(RouteList[_indexRoute].position);

            StartCoroutine(UpdateRoute());
        }

        protected override IEnumerator UpdateRoute()
        {
            yield return new WaitForSeconds(0.1f);
            
            if (NavMeshAgent.remainingDistance == 0)
            {
                if (EnemyStateMachine._currentPlayerState is DeadState)
                {
                    EnemyStateMachine.ChangeState(0);
                }
                
                _indexRoute++;
            
                if (_indexRoute >= RouteList.Count)
                {
                    _indexRoute = 0;
                }

                NavMeshAgent.SetDestination(RouteList[_indexRoute].position);
            }
            
            StartCoroutine(UpdateRoute());
        }
    }
}
