using EnemySystem;
using UnityEngine;
using Utils;

namespace BonusSystem
{
    public class SelectionCoolBonus : SelectionUsualBonus
    {
        [SerializeField] private EnemyControl enemiesControl;
        
        protected override void OnTriggerEnter2D(Collider2D other)
        {
            if (player.Contains(other.gameObject.layer))
            {
                gameObject.SetActive(false);
                
                enemiesControl.GetVulnerable();
            }
        }
    }
}