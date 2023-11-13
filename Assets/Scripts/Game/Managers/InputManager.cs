using System;
using Game.Part;
using UnityEngine;
using Zenject;

namespace Game.Managers
{
    public class InputManager: MonoBehaviour
    {
        public event Action Clicked;
        public event Action ClickedUp;        
        public event Action<Mountable> PartClicked;

        private bool _isActive = true;
        private Camera _mainCamera;
        private int _targetLayer = 1 << 7;
        
        void Start()
        {
            _mainCamera = Camera.main;
        }
        
        void Update()
        {
            if (!_isActive) return;
            CheckInput();
        }
        
        void CheckInput()
        {
            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit hit;
                Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out hit,10,_targetLayer))
                {
                    var mountableComponent = hit.collider.GetComponent<Mountable>();
                    if (mountableComponent != null)
                    {
                        OnPartClicked(mountableComponent);
                    }
                }
                else
                {
                    OnClicked();
                }
            }
            if (Input.GetMouseButtonUp(0))
            {
                OnClickedUp();
            }
        }
        private void OnClicked()
        {
            Clicked?.Invoke();
        }
        private void OnClickedUp()
        {
            ClickedUp?.Invoke();
        }
        
        private void OnPartClicked(Mountable mountable)
        {
            PartClicked?.Invoke(mountable);
        }
        
        public void SetSituation(bool situation)
        {
            _isActive = situation;
        }
    }
}