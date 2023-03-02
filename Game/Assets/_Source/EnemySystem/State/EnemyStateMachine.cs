using System;
using System.Collections.Generic;
using UnityEngine;
using Utils;

namespace EnemySystem.State
{
    public class EnemyStateMachine
    {
        // public static Action<Type> OnChangeState;

        private Dictionary<int, AEnemyState> _states;
        protected internal AEnemyState _currentPlayerState;
        private int _stateID;

        public EnemyStateMachine(SpriteRenderer sprite, Color baseColor, Color deadColor, Color vulnerableColor)
        {
            _states = new Dictionary<int, AEnemyState>
            {
                { 0, new DangerousState(this, sprite, baseColor) },
                { 1, new VulnerableState(this, sprite, vulnerableColor) },
                { 2, new DeadState(this, sprite, deadColor) }
            };
            
            ChangeState(0);
            // OnChangeState?.Invoke(_states[_stateID].GetType());
        }

        public void ExitState()
        {
            _currentPlayerState?.Exit();
            // ChangeState(_stateID);
            // OnChangeState?.Invoke(_states[_stateID].GetType());
        }
        
        public void ChangeState(int id)
        {
            // _currentPlayerState?.Exit();
            _currentPlayerState = _states[id];
            _currentPlayerState.Enter();
        }
    }
}