using System.Collections.Generic;
using UnityEngine;

namespace EnemySystem.State
{
    public class EnemyStateMachine
    {
        protected internal AEnemyState CurrentPlayerState;
        
        private readonly Dictionary<int, AEnemyState> _states;
        
        private int _stateID;

        public EnemyStateMachine(SpriteRenderer sprite, CircleCollider2D collider2D, Color baseColor, Color deadColor, Color vulnerableColor)
        {
            _states = new Dictionary<int, AEnemyState>
            {
                { 0, new DangerousState(this, sprite, collider2D, baseColor) },
                { 1, new VulnerableState(this, sprite, vulnerableColor) },
                { 2, new DeadState(this, sprite, collider2D, deadColor) }
            };
            
            ChangeState(0);
        }

        public AEnemyState State()
        {
            return CurrentPlayerState;
        }
        
        public void ChangeState(int id)
        {
            CurrentPlayerState = _states[id];
            CurrentPlayerState.Enter();
        }
    }
}