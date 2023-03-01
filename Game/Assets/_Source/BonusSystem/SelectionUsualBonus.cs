using System;
using UnityEngine;
using Utils;

namespace BonusSystem
{
    public class SelectionUsualBonus : MonoBehaviour
    {
        public int GivePoint;
        
        [SerializeField] private LayerMask player;
        
        protected void OnTriggerEnter2D(Collider2D other)
        {
            if (player.Contains(other.gameObject.layer))
            {
                gameObject.SetActive(false);
            }
        }
    }
}
