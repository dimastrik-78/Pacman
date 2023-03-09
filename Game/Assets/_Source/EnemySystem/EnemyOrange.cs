using System.Collections;
using EnemySystem.State;
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
                if (EnemyStateMachine.CurrentPlayerState is DeadState)
                {
                    yield return new WaitForSeconds(recoveryTime);
                    
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

        public override IEnumerator Restart(float pauseTime)
        {
            NavMeshAgent.enabled = false;
            
            yield return new WaitForSeconds(2f);
            
            transform.position = SpawnPosition;
            
            EnemyStateMachine.ChangeState(0);
            
            yield return new WaitForSeconds(pauseTime);
            
            NavMeshAgent.enabled = true;

            _indexRoute = 0;
            NavMeshAgent.SetDestination(RouteList[_indexRoute].position);
        }
    }
}
