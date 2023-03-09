using UnityEngine;

namespace EnemySystem.State
{
    public class DeadState : AEnemyState
    {
        private readonly SpriteRenderer _sprite;
        private readonly CircleCollider2D _collider2D;
        private readonly Color _deadColor;
        
        public DeadState(EnemyStateMachine owner, SpriteRenderer sprite, CircleCollider2D collider2D, Color deadColor) : base(owner)
        {
            _sprite = sprite;
            _collider2D = collider2D;
            _deadColor = deadColor;
        }

        public override void Enter()
        {
            _collider2D.enabled = false;
            
            _sprite.color = _deadColor;
        }
        
    }
}