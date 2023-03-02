using UnityEngine;

namespace EnemySystem.State
{
    public class DangerousState : AEnemyState
    {
        private SpriteRenderer _sprite;
        private Color _baseColor;
        
        public DangerousState(EnemyStateMachine owner, SpriteRenderer sprite, Color baseColor) : base(owner)
        {
            _sprite = sprite;
            _baseColor = baseColor;
        }

        public override void Enter()
        {
            _sprite.color = _baseColor;
        }
        
        public override void Exit()
        {
            Owner.ChangeState(1);
        }
    }
}