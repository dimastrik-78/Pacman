using UnityEngine;

namespace EnemySystem.State
{
    public class VulnerableState : AEnemyState
    {
        private readonly SpriteRenderer _sprite;
        private readonly Color _vulnerableColor;
        
        public VulnerableState(EnemyStateMachine owner, SpriteRenderer sprite, Color vulnerableColor) : base(owner)
        {
            _sprite = sprite;
            _vulnerableColor = vulnerableColor;
        }

        public override void Enter()
        {
            _sprite.color = _vulnerableColor;
        }
    }
}