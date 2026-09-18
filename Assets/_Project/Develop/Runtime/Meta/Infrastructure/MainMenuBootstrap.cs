using Assets._Project.Develop.Runtime.Infrastructure;
using Assets._Project.Develop.Runtime.Infrastructure.DI;
using Assets._Project.Develop.Runtime.Meta.Features.ScoreManagment;
using Assets._Project.Develop.Runtime.Utilities.SceneManagment;
using System.Collections;

namespace Assets._Project.Develop.Runtime.Meta.Infrastructure
{
    public class MainMenuBootstrap : SceneBootstrap
    {
        private DIContainer _container;
        private GameModeSwitcher _gameModeSwitcher;
        private ScoreDisplayer _scoreDisplayer;
        private ScoreResetService _scoreResetService;

        public override void ProcessRegistrations(DIContainer container, IInputSceneArgs sceneArgs = null)
        {
            _container = container;

            MainMenuContextRegistrations.Process(container);
        }

        public override IEnumerator Initialize()
        {
            _gameModeSwitcher = _container.Resolve<GameModeSwitcher>();

            _scoreDisplayer = _container.Resolve<ScoreDisplayer>();

            _scoreResetService = _container.Resolve<ScoreResetService>();

            yield break;
        }

        public override IEnumerator Run()
        {
            _gameModeSwitcher.Start();

            yield break;
        }

        private void Update()
        {
            _scoreDisplayer?.Update();
            _scoreResetService?.Update();
            _gameModeSwitcher?.Update();
        }
    }
}