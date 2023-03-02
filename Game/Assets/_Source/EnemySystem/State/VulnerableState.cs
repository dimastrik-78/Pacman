using UnityEngine;

namespace EnemySystem.State
{
    public class VulnerableState : AEnemyState
    {
        private SpriteRenderer _sprite;
        private Color _vulnerableColor;
        
        public VulnerableState(EnemyStateMachine owner, SpriteRenderer sprite, Color vulnerableColor) : base(owner)
        {
            _sprite = sprite;
            _vulnerableColor = vulnerableColor;
        }

        public override void Enter()
        {
            _sprite.color = _vulnerableColor;
        }

        public override void Exit()
        {
            Owner.ChangeState(0);
        }
    }
}