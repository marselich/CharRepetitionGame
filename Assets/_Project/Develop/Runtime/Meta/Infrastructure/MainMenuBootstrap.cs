using Assets._Project.Develop.Runtime.Infrastructure;
using Assets._Project.Develop.Runtime.Infrastructure.DI;
using Assets._Project.Develop.Runtime.Utilities.SceneManagment;
using System.Collections;

namespace Assets._Project.Develop.Runtime.Meta.Infrastructure
{
    public class MainMenuBootstrap : SceneBootstrap
    {
        private DIContainer _container;
        private GameModeSwitcher _gameModeSwitcher;

        public override void ProcessRegistrations(DIContainer container, IInputSceneArgs sceneArgs = null)
        {
            _container = container;

            MainMenuContextRegistrations.Process(container);
        }

        public override IEnumerator Initialize()
        {
            _gameModeSwitcher = new GameModeSwitcher(_container);

            _gameModeSwitcher.Initialize();
            yield break;
        }

        public override IEnumerator Run()
        {
            yield break;
        }

        private void Update() => _gameModeSwitcher?.Update();
    }
}