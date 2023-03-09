using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UISystem
{
    public class GameUI : MonoBehaviour
    {
        [SerializeField] private Text point;
        [SerializeField] private List<GameObject> hpPacman;
        [SerializeField] private GameObject losePanel;
        [SerializeField] private GameObject winPanel;

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
            losePanel.SetActive(true);
        }

        public void WinPanel()
        {
            winPanel.SetActive(true);
        }

        public void ResetGame()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public void MainMenu()
        {
            SceneManager.LoadScene(0);
        }
    }
}
