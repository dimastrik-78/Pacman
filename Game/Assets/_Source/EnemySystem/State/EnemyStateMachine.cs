using System;
using System.Collections.Generic;
using UnityEngine;

namespace EnemySystem.State
{
    public class EnemyStateMachine
    {
        public static Action<Type> OnChangeState;

        private Dictionary<int, AEnemyState> _states;
        private AEnemyState _currentPlayerState;
        // private Initializer _initializer;
        private int _stateID;

        public EnemyStateMachine()
        {
            // _initializer = new Initializer();
            
            // _initializer.PlayerState(out _states, this, 
            //     prefabBullet, spawnPointBullet,
            //     aura,
            //     playerSprite);
            
            ChangeState(_stateID);
            OnChangeState?.Invoke(_states[_stateID].GetType());
        }

        public void ChangeState()
        {
            _stateID++;
            if (_stateID >= _states.Count)
                _stateID = 0;
            
            ChangeState(_stateID);
            OnChangeState?.Invoke(_states[_stateID].GetType());
        }
        
        private void ChangeState(int id)
        {
            _currentPlayerState?.Exit();
            _currentPlayerState = _states[id];
            _currentPlayerState.Enter();
        }

        public void Update() =>
            _currentPlayerState.Update();
    }
}