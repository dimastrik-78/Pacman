using UnityEngine;

namespace EnemySystem.State
{
    public class DangerousState : AEnemyState
    {
        private readonly SpriteRenderer _sprite;
        private readonly CircleCollider2D _collider2D;
        private readonly Color _baseColor;
        
        public DangerousState(EnemyStateMachine owner, SpriteRenderer sprite, CircleCollider2D collider2D, Color baseColor) : base(owner)
        {
            _sprite = sprite;
            _collider2D = collider2D;
            _baseColor = baseColor;
        }

        public override void Enter()
        {
            _collider2D.enabled = true;
            
            _sprite.color = _baseColor;
        }
    }
}