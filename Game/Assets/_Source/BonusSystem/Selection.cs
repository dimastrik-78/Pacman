using System;
using UnityEngine;

namespace BonusSystem
{
    public class Selection : MonoBehaviour
    {
        void Start()
        {
            
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.layer == 6)
            {
                gameObject.SetActive(false);
            }
        }
    }
}
