using System.Collections;
using UnityEngine;
using Utils;

namespace BonusSystem
{
    public class SelectionUsualBonus : MonoBehaviour
    {
        [SerializeField] protected LayerMask player;
        
        [SerializeField] private Color firstColor;
        [SerializeField] private Color secondColor;
        [SerializeField] private LeftBonuses leftBonuses;
        [SerializeField] private AudioSource audioSource;

        private SpriteRenderer _sprite;
        
        private void Awake()
        {
            _sprite = GetComponent<SpriteRenderer>();
            
            StartCoroutine(ChangeColor());
        }

        protected virtual void OnTriggerEnter2D(Collider2D other)
        {
            if (player.Contains(other.gameObject.layer))
            {
                audioSource.Play();
                leftBonuses.Invoke();
                gameObject.SetActive(false);
            }
        }

        private IEnumerator ChangeColor()
        {
            yield return new WaitForSeconds(1f);

            if (_sprite.color == firstColor)
            {
                _sprite.color = secondColor;
            }
            else
            {
                _sprite.color = firstColor;
            }
            
            StartCoroutine(ChangeColor());
        }
    }
}
