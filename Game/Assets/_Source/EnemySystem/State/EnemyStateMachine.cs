using System.Collections.Generic;
using UnityEngine;

namespace EnemySystem.State
{
    public class EnemyStateMachine
    {
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
        }

        public AEnemyState State()
        {
            return _currentPlayerState;
        }
        
        public void ChangeState(int id)
        {
            // _currentPlayerState?.Exit();
            _currentPlayerState = _states[id];
            _currentPlayerState.Enter();
        }
    }
}