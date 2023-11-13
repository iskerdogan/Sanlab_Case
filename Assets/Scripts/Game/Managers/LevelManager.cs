using System;
using UnityEngine;
using Zenject;

namespace Game.Managers
{
    public class LevelManager : MonoBehaviour
    {
        [Inject] private UIManager _uiManager;
        [Inject] private InputManager _inputManager;
        
        [SerializeField] private Level[] levels;
        
        public Level currenLevel;

        private void Start()
        {
            currenLevel = levels[0];
        }

        public void BuildLevel()
        {
            //
        }

        public void CheckLevelSuccess()
        {
            if (!currenLevel.CheckSlots()) return;
            _inputManager.SetSituation(false);
            _uiManager.OpenSuccessText();
        }
    }
}