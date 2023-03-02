﻿using PacmanSystem;
using UnityEngine;

namespace BonusSystem
{
    public class SelectionCoolBonus : SelectionUsualBonus
    {
        protected override void OnTriggerEnter2D(Collider2D other)
        {
            base.OnTriggerEnter2D(other);
            other.gameObject.GetComponent<PacmanInvoker>().ChangeState();
        }
    }
}