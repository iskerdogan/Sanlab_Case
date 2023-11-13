using Cinemachine;
using UnityEngine;

namespace Game.Managers
{
    public class CameraManager : MonoBehaviour
    {
        [SerializeField] private CinemachineVirtualCamera mainCamera;
        [SerializeField] private CinemachineVirtualCamera[] slotsCamera;

        public void ChangeCamera(CinemachineVirtualCamera destinationCamera)
        {
            CloseAllSlotsCamera();
            mainCamera.Priority = 0;
            destinationCamera.Priority = 10;
        }

        public void MainCamera()
        {
            CloseAllSlotsCamera();
            mainCamera.Priority = 10;
        }

        private void CloseAllSlotsCamera()
        {
            foreach (var camera in slotsCamera)
            {
                camera.Priority = 0;
            }
        }
    }
}