using UnityEngine;
using Cinemachine;

namespace Game.Part
{
    public class PartSlot : MonoBehaviour
    {
        public CinemachineVirtualCamera mountableCam;
        public Transform startPosition;
        public bool isFull;
        private MeshRenderer _modelMeshRenderer;

        private void Start()
        {
            _modelMeshRenderer = GetComponent<MeshRenderer>();
            _modelMeshRenderer.enabled = false;
        }

        public void SetActivityFadeModel(bool active)
        {
            _modelMeshRenderer.enabled = active;
        }
    }
}