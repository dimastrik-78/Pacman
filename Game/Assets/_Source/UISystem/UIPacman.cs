using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UISystem
{
    public class UIPacman : MonoBehaviour
    {
        [SerializeField] private Text point;
        [SerializeField] private List<GameObject> hpPacman;
        [SerializeField] private GameObject losePanel;
        [SerializeField] private GameObject winPanel;
        
        private void Awake()
        {
            
        }

        public void ChangePoint(int points)
        {
            point.text = $"Points: {points}";
        }

        public void ChangeHpPacman()
        {
            hpPacman[^1].SetActive(false);
            hpPacman.RemoveAt(hpPacman.Count - 1);
        }

        public void LosePanel()
        {
            losePanel.SetActive(false);
        }

        public void WinPanel()
        {
            winPanel.SetActive(true);
        }
    }
}
