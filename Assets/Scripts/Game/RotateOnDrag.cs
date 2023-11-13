using System;
using Game.Managers;
using UnityEngine;
using Zenject;

namespace Game
{
    public class RotateOnDrag:MonoBehaviour
    {
        [Inject] private InputManager _inputManager;
        
        [SerializeField] private float rotationSpeed = 5.0f;
        
        private bool _isDragging;

        private void Start()
        {
            Subscribe();
        }
        
        void Update()
        {
            if (!_isDragging) return;
            
            float rotationX = Input.GetAxis("Mouse X") * rotationSpeed;
            float rotationY = Input.GetAxis("Mouse Y") * rotationSpeed;
            transform.Rotate(Vector3.up, rotationX);
            transform.Rotate(Vector3.right, -rotationY);
        }
        
        void Subscribe()
        {
            _inputManager.ClickedUp += ClickedUp;
            _inputManager.Clicked += Clicked;
        }
        
        void Unsubscribe()
        {
            _inputManager.ClickedUp -= ClickedUp;
            _inputManager.Clicked -= Clicked;
        }

        private void Clicked()
        {
            _isDragging = true;
        }

        private void ClickedUp()
        {
            _isDragging = false;
        }

        private void OnDestroy()
        {
            Unsubscribe();
        }
    }
}