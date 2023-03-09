using System.Collections;
using System.Collections.Generic;
using EnemySystem.State;
using Interface;
using UnityEngine;
using UnityEngine.AI;
using Random = System.Random;

namespace EnemySystem
{
    public class Enemy : MonoBehaviour, IDamage
    {
        [SerializeField] protected float recoveryTime;
        [SerializeField] protected SpriteRenderer sprite;

        [SerializeField] private Transform route;
        [SerializeField] private float speed;
        [SerializeField] private Color baseColor;
        [SerializeField] private Color deadColor;
        [SerializeField] private Color vulnerableColor;
        [SerializeField] private CircleCollider2D baseCollider;
        
        protected readonly List<Transform> RouteList = new();
        protected readonly Random Random = new();
        
        protected NavMeshAgent NavMeshAgent;
        protected EnemyStateMachine EnemyStateMachine;
        protected Vector3 SpawnPosition;
        
        protected virtual void Awake()
        {
            FindRoute();

            SpawnPosition = transform.position;

            NavMeshAgent = GetComponent<NavMeshAgent>();
            NavMeshAgent.updateRotation = false;
            NavMeshAgent.updateUpAxis = false;

            NavMeshAgent.speed = speed;

            EnemyStateMachine = new EnemyStateMachine(sprite, baseCollider, baseColor, deadColor, vulnerableColor);
        }

        protected virtual IEnumerator UpdateRoute()
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
            
            StartCoroutine(UpdateRoute());
        }

        private void FindRoute()
        {
            for (int i = 0; i < route.childCount; i++)
            {
                RouteList.Add(route.GetChild(i));
            }
        }

        public AEnemyState GetState()
        {
            return EnemyStateMachine.State();
        }
        
        public virtual void GetDamage()
        {
            NavMeshAgent.SetDestination(SpawnPosition);

            EnemyStateMachine.ChangeState(2);
        }

        public void EnableVulnerable()
        {
            EnemyStateMachine.ChangeState(1);
        }

        public void DisableVulnerable()
        {
            EnemyStateMachine.ChangeState(0);
        }

        public virtual IEnumerator Restart(float pauseTime)
        {
            NavMeshAgent.enabled = false;
            
            yield return new WaitForSeconds(2f);
            
            transform.position = SpawnPosition;
            
            EnemyStateMachine.ChangeState(0);
            
            yield return new WaitForSeconds(pauseTime);
            
            NavMeshAgent.enabled = true;
            
            NavMeshAgent.SetDestination(RouteList[Random.Next(0, RouteList.Count)].position);
        }
    }
}
