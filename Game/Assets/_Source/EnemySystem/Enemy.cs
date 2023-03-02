using System;
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
        [SerializeField] private float speed;
        [SerializeField] private Transform route;
        [SerializeField] protected LayerMask player;
        [SerializeField] protected SpriteRenderer sprite;
        [SerializeField] private Color baseColor;
        [SerializeField] private Color deadColor;
        [SerializeField] private Color vulnerableColor;
        
        protected readonly List<Transform> RouteList = new();
        protected readonly Random Random = new();
        
        protected NavMeshAgent NavMeshAgent;
        protected EnemyStateMachine EnemyStateMachine;
        
        private Vector3 _spawnPosition;

        public int GivePoint { get; set; }

        protected virtual void Awake()
        {
            FindRoute();

            _spawnPosition = transform.position;

            NavMeshAgent = GetComponent<NavMeshAgent>();
            NavMeshAgent.updateRotation = false;
            NavMeshAgent.updateUpAxis = false;

            NavMeshAgent.speed = speed;

            EnemyStateMachine = new EnemyStateMachine(sprite, baseColor, deadColor, vulnerableColor);
        }

        protected virtual IEnumerator UpdateRoute()
        {
            yield return new WaitForSeconds(0.1f);
            
            if (NavMeshAgent.remainingDistance == 0)
            {
                if (EnemyStateMachine._currentPlayerState is DeadState)
                {
                    EnemyStateMachine.ChangeState(0);
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
        
        public void GetDamage()
        {
            NavMeshAgent.SetDestination(_spawnPosition);

            EnemyStateMachine.ChangeState(2);
        }
    }
}
