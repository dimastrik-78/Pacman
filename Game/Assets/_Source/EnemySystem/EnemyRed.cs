using System.Collections;
using EnemySystem.State;
using UnityEngine;
using Utils;

namespace EnemySystem
{
    public class EnemyRed : Enemy
    {
        [SerializeField] private float searchRadius;
        [SerializeField] private CircleCollider2D circleCollider;
        [SerializeField] private LayerMask player;

        private Vector3 _playerPosition;
        private bool _seePlayer;

        protected override void Awake()
        {
            base.Awake();

            circleCollider.radius = searchRadius;
            
            NavMeshAgent.SetDestination(RouteList[Random.Next(0, RouteList.Count)].position);

            StartCoroutine(UpdateRoute());
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (player.Contains(col.gameObject.layer)
                && EnemyStateMachine.State() is DangerousState)
            {
                StartCoroutine(PlayerHunting(col.gameObject.transform));
                _seePlayer = true;
            }
        }

        private void OnTriggerExit2D(Collider2D col)
        {
            if (player.Contains(col.gameObject.layer))
            {
                StopCoroutine(PlayerHunting(col.gameObject.transform));
                _seePlayer = false;
            }
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
                    
                    circleCollider.enabled = true;
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
            
            StartCoroutine(UpdateRoute());
        }

        public override void GetDamage()
        {
            circleCollider.enabled = false;
            _seePlayer = false;
            
            base.GetDamage();
        }

        private IEnumerator PlayerHunting(Transform playerPosition)
        {
            yield return new WaitForSeconds(0.1f);

            if (EnemyStateMachine.State() is not DeadState)
            {
                _playerPosition = playerPosition.position;
            
                NavMeshAgent.SetDestination(_playerPosition);

                if (_seePlayer)
                {
                    StartCoroutine(PlayerHunting(playerPosition));
                }
            }
        }
    }
}
