using Game.Part;
using UnityEngine;
using Zenject;

namespace Game.Managers
{
    
    public class DragManager:MonoBehaviour
    {
        [Inject] private InputManager _inputManager;

        [SerializeField] private float dragSensitivity = 5f;
        [SerializeField] private float mountDuration = 1f;
        [SerializeField] private float demountDuration = .2f;
        
        private bool _isDragging;
        private Mountable _currentMountable;
        private Camera _mainCamera;

        private void Start()
        {
            Subscribe();
            _mainCamera = Camera.main;
        }
        
        void Update()
        {
            if (!_isDragging) return;
            DragMountable(Input.mousePosition);
        }

        void DragMountable(Vector3 mousePosition)
        {
            Vector3 destinationPosition =_mainCamera.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, _mainCamera.nearClipPlane + .1f));
            _currentMountable.Take(destinationPosition,dragSensitivity);
        }
        
        void Subscribe()
        {
            _inputManager.PartClicked += PartClicked;
            _inputManager.ClickedUp += ClickedUp;
        }

        void Unsubscribe()
        {
            _inputManager.PartClicked -= PartClicked;
            _inputManager.ClickedUp -= ClickedUp;
        }

        private void PartClicked(Mountable mountable)
        {
            _currentMountable = mountable;
            _isDragging = true;
        }

        private void ClickedUp()
        {
            _isDragging = false;
            if (!_currentMountable) return;
            _inputManager.SetSituation(false);
            _currentMountable.ClickedUp(mountDuration,demountDuration);
            _currentMountable = null;
        }

        private void OnDestroy()
        {
            Unsubscribe();
        }
    }
}