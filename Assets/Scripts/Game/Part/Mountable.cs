using System;
using DG.Tweening;
using Game.Managers;
using UnityEngine;
using Zenject;

namespace Game.Part
{
    public abstract class Mountable : MonoBehaviour
    {
        [Inject] protected InputManager _inputManager;
        [Inject] protected CameraManager _cameraManager;
        [Inject] protected LevelManager _levelManager;

        [SerializeField] protected PartSlot targetSlot;

        private float _mountDuration;
        private float _demountDuration;
        private Vector3 _startPosition;
        private Camera _mainCamera;
        private PartSlot _currentSlot;
        private bool _isMatch;
        private int _targetLayer = 1 << 8;

        private void Start()
        {
            _startPosition = transform.position;
            _mainCamera = Camera.main;
        }
        
        public void Take(Vector3 destinationPosition,float dragSensitivity)
        {
            transform.position = Vector3.Lerp(transform.position, destinationPosition, Time.deltaTime * dragSensitivity);
            
            CheckRaycastHit();
        }
        
        void CheckRaycastHit()
        {
            RaycastHit hit;
            Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
            
            if (Physics.Raycast(ray, out hit, 10, _targetLayer))
            {
                var partSlot = hit.collider.GetComponent<PartSlot>();
                if (partSlot != null)
                {
                    PartMatch(partSlot);
                }
            }
            else
            {
                SetTargetSlotActivity(false);
                _isMatch = false;
            }
        }

        void SetTargetSlotActivity(bool isActive)
        {
            if (!_currentSlot) return;
            _currentSlot.SetActivityFadeModel(isActive);
        }

        public void ClickedUp(float mountDuration,float demountDuration)
        {
            SetDurations(mountDuration, demountDuration);
            if (!_isMatch)
            {
                Demount();
                return;
            }
            Mount();
        }

        void SetDurations(float mountDuration, float demountDuration)
        {
            _mountDuration = mountDuration;
            _demountDuration = demountDuration;
        }

        protected virtual void Mount()
        {
            _cameraManager.ChangeCamera(_currentSlot.mountableCam);
            transform.DOMove(_currentSlot.startPosition.transform.position, _mountDuration/2).OnComplete(() =>
            {
                transform.DOMove(_currentSlot.transform.position, _mountDuration/2)
                    .OnComplete(() =>
                    {
                        _cameraManager.MainCamera();
                        _inputManager.SetSituation(true);
                        _currentSlot.isFull = true;
                        _levelManager.CheckLevelSuccess();
                    });
            });
            _currentSlot.SetActivityFadeModel(false);
        }

        protected virtual void Demount()
        {
            if (_currentSlot) _currentSlot.isFull = false;
            transform.DOMove(_startPosition, _demountDuration).OnComplete(() => _inputManager.SetSituation(true));;
        }

        void PartMatch(PartSlot partSlot)
        {
            if (!CheckPartMatch(partSlot)) return;
            _currentSlot = partSlot;
            partSlot.SetActivityFadeModel(true);
            _isMatch = true;
        }

        protected virtual bool CheckPartMatch(PartSlot partSlot)
        {
            return partSlot == targetSlot;
        }
    }
}