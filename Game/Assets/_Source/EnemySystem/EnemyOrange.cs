using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnemySystem;

public class EnemyOrange : Enemy
{
    [SerializeField] private Transform[] route;

    private float _time = 1;
    private int _indexRoute = 0;

    private void Start()
    {
        _navMeshAgent.SetDestination(route[_indexRoute].position);
    }

    void Update()
    {
        _time -= Time.deltaTime;
        if (_navMeshAgent.remainingDistance == 0
            && _time <= 0)
        {
            _indexRoute++;
            
            if (_indexRoute >= route.Length)
            {
                _indexRoute = 0;
            }

            _navMeshAgent.SetDestination(route[_indexRoute].position);

            _time = 1;
        }
    }
}
