using UnityEngine;
using Utils;

namespace BonusSystem
{
    public class SelectionUniqueBonus : MonoBehaviour
    {
        [SerializeField] protected LayerMask player;

        private SpawnUniqueBonus _bonusImage;
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (player.Contains(other.gameObject.layer))
            {
                gameObject.SetActive(false);
                _bonusImage.ImageBonus();
            }
        }

        public void SetBonusUI(SpawnUniqueBonus bonusImage)
        {
            _bonusImage = bonusImage;
        }
    }
}