using UnityEngine;

namespace EnemySystem.State
{
    public class DeadState : AEnemyState
    {
        private SpriteRenderer _sprite;
        private Color _deadColor;
        
        public DeadState(EnemyStateMachine owner, SpriteRenderer sprite, Color deadColor) : base(owner)
        {
            _sprite = sprite;
            _deadColor = deadColor;
        }

        public override void Enter()
        {
            _sprite.color = _deadColor;
        }
        
        public override void Exit()
        {
            Owner.ChangeState(0);
        }
    }
}