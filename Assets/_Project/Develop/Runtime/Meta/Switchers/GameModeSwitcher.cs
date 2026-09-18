using Assets._Project.Develop.Runtime.Configs.Gameplay.CharsGenerator;
using Assets._Project.Develop.Runtime.Gameplay.Infrastructure;
using Assets._Project.Develop.Runtime.Utilities.ConfigsManagment;
using Assets._Project.Develop.Runtime.Utilities.CoroutinesManagment;
using Assets._Project.Develop.Runtime.Utilities.SceneManagment;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Meta.Infrastructure
{
    public class GameModeSwitcher
    {
        private ICoroutinesPerformer _coroutinesPerformer;
        private SceneSwitcherService _sceneSwitcherService;
        private ConfigsProviderService _configsProviderService;

        private bool _isRunning;

        public GameModeSwitcher(
            ICoroutinesPerformer coroutinesPerformer,
            SceneSwitcherService sceneSwitcherService,
            ConfigsProviderService configsProviderService
            )
        {
            _coroutinesPerformer = coroutinesPerformer;
            _sceneSwitcherService = sceneSwitcherService;
            _configsProviderService = configsProviderService;
        }

        public void Start()
        {
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