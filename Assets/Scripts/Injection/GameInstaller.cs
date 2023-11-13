using Game.Managers;
using UnityEngine;
using Zenject;

namespace Injection
{
     public class GameInstaller : MonoInstaller
     {
          [SerializeField] private InputManager inputManager;
          [SerializeField] private CameraManager cameraManager;
          [SerializeField] private LevelManager levelManager;
          [SerializeField] private UIManager uiManager;
          public override void InstallBindings()
          {
               Container.BindInterfacesAndSelfTo<InputManager>().FromInstance(inputManager).AsSingle();
               Container.BindInterfacesAndSelfTo<CameraManager>().FromInstance(cameraManager).AsSingle();
               Container.BindInterfacesAndSelfTo<LevelManager>().FromInstance(levelManager).AsSingle();
               Container.BindInterfacesAndSelfTo<UIManager>().FromInstance(uiManager).AsSingle();
               //Container.BindInterfacesAndSelfTo<GridManager>().FromInstance(gridManager).AsSingle();
               //Container.BindInterfacesAndSelfTo<GameManager>().FromInstance(gameManager).AsSingle();
               //Container.BindInterfacesAndSelfTo<ScoreManager>().FromInstance(scoreManager).AsSingle();
          }
     }
}
