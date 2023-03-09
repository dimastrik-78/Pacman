using System.Collections;
using EnemySystem.State;
using UnityEngine;
using Utils;

namespace EnemySystem
{
    public class EnemyCyan : Enemy
    {
        [SerializeField] private float speedRotation;
        [SerializeField] private Transform playerPosition;
        [SerializeField] private Transform startLine;
        [SerializeField] private Transform endLine;
        [SerializeField] private LayerMask playerMask;

        private Vector2 _startLine;
        private Vector2 _endLine;
        private RaycastHit2D _hit;
        private Vector2 _playerPosition;

        protected override void Awake()
        {
            base.Awake();
            
            NavMeshAgent.SetDestination(RouteList[Random.Next(0, RouteList.Count)].position);

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

                switch (transform.position.x)
                {
                    case < -9.2f:
                        transform.position = new Vector3(9.2f, 0);
                        break;
                    case > 9.2f:
                        transform.position = new Vector3(-9.2f, 0);
                        break;
                }
                
                NavMeshAgent.SetDestination(RouteList[Random.Next(0, RouteList.Count)].position);
            }

            if (EnemyStateMachine.State() is DangerousState)
            {
                SearchPlayer();
            }
            
            StartCoroutine(UpdateRoute());
        }

        private IEnumerator PlayerHunting(Transform playerPosition)
        {
            yield return new WaitForSeconds(0.1f);
            
            _playerPosition = playerPosition.position;
            
            NavMeshAgent.SetDestination(_playerPosition);
        }

        private void SearchPlayer()
        {
            TurningTowardsTheTarget();
            
            _startLine = startLine.position;
            _endLine = endLine.position;
            _hit = Physics2D.Linecast(_startLine, _endLine);
            
            if (playerMask.Contains(_hit.collider.gameObject.layer))
            {
                StartCoroutine(PlayerHunting(playerPosition));
            }
        }
        
        private void TurningTowardsTheTarget() 
        {
            var direction = playerPosition.position - transform.position;
            transform.right = Vector2.Lerp(transform.right, direction, speedRotation * Time.deltaTime);
        }
    }
}
