using System.Collections;
using DG.Tweening;
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

            // StartCoroutine(ChangeColor());
            // DOTweenCYInstruction.WaitForStart
            // ChangeColor();
        }

        private void ChangeColor()
        {
            if (_sprite.color == _vulnerableColor)
            {
                DOTween.Sequence().SetDelay(1f);
                _sprite.color = Color.white;
            }
            else
            {
                DOTween.Sequence().SetDelay(1f).onComplete();
                _sprite.color = _vulnerableColor;
            }
            
            // StartCoroutine(ChangeColor());
        }

        public override void Exit()
        {
            Owner.ChangeState(0);
        }
    }
}