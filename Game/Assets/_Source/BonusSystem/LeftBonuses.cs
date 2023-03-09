using UISystem;
using UnityEngine;

namespace BonusSystem
{
    public class LeftBonuses : MonoBehaviour
    {
        [SerializeField] private GameUI gameUI;
        [SerializeField] private AudioSource winAudio;
        
        private int _left;
        
        private void Awake()
        {
            _left = transform.childCount;
            Debug.Log(_left + " всего");
        }

        private void Check()
        {
            Debug.Log(_left);
            if (_left <= 0)
            {
                winAudio.Play();

                gameUI.WinPanel();
                
                Time.timeScale = 0;
            }
        }

        public void Invoke()
        {
            _left--;
            Check();
        }
    }
}