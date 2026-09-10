using Assets._Project.Develop.Runtime.Gameplay.Configs;
using Assets._Project.Develop.Runtime.Gameplay.Infrastructure;
using Assets._Project.Develop.Runtime.Infrastructure.DI;
using Assets._Project.Develop.Runtime.Utilities.ConfigsManagment;
using Assets._Project.Develop.Runtime.Utilities.CoroutinesManagment;
using Assets._Project.Develop.Runtime.Utilities.SceneManagment;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Meta.Infrastructure
{
    public class GameModeSwitcher
    {
        private DIContainer _container;
        private ICoroutinesPerformer _coroutinesPerformer;
        private SceneSwitcherService _sceneSwitcherService;
        private ConfigsProviderService _configsProviderService;

        private bool _isRunning;

        public GameModeSwitcher(DIContainer container)
        {
            _container = container;
        }

        public void Initialize()
        {
            _coroutinesPerformer = _container.Resolve<ICoroutinesPerformer>();
            _sceneSwitcherService = _container.Resolve<SceneSwitcherService>();
            _configsProviderService = _container.Resolve<ConfigsProviderService>();

            _isRunning = true;
        }

        public void Update()
        {
            if (_isRunning == false)
                return;

            if (Input.GetKeyDown(KeyCode.Alpha1))
                SwitchToGameplayScene(_configsProviderService.GetConfig<DigitsGeneratorConfig>());

            if (Input.GetKeyDown(KeyCode.Alpha2))
                SwitchToGameplayScene(_configsProviderService.GetConfig<LettersGeneratorConfig>());
        }

        private void SwitchToGameplayScene(ICharsGeneratorConfig config)
            => _coroutinesPerformer.StartPerform(_sceneSwitcherService.ProcessSwitchTo(Scenes.Gameplay, new GameplayInputArgs(config)));
    }
}