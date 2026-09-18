using Assets._Project.Develop.Runtime.Configs.Gameplay.CharsGenerator;
using Assets._Project.Develop.Runtime.Configs.Gameplay.Score;
using Assets._Project.Develop.Runtime.Configs.Meta.Wallet;
using Assets._Project.Develop.Runtime.Gameplay.Controllers;
using Assets._Project.Develop.Runtime.Gameplay.Generators;
using Assets._Project.Develop.Runtime.Meta.Features.ScoreManagment;
using Assets._Project.Develop.Runtime.Meta.Features.Wallet;
using Assets._Project.Develop.Runtime.Utilities.ConfigsManagment;
using Assets._Project.Develop.Runtime.Utilities.CoroutinesManagment;
using Assets._Project.Develop.Runtime.Utilities.DataManagment.DataProviders;
using Assets._Project.Develop.Runtime.Utilities.SceneManagment;
using System;
using System.Collections;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Gameplay.Infrastructure
{
    public class GameCycle : IDisposable
    {
        private ICharsGeneratorConfig _charsGeneratorConfig;
        private CharsGenerator _charsGenerator;
        private ICoroutinesPerformer _coroutinesPerformer;
        private SceneSwitcherService _sceneSwitcherService;
        private ControllersFactory _controllersFactory;
        private ConfigsProviderService _configsProviderService;
        private WalletService _walletService;
        private ScoreCounterService _scoreCounterService;
        private PlayerDataProvider _playerDataProvider;

        private GameMode _gameMode;
        private Controller _controller;
        private ScoreRewardConfig _winLoseRewardConfig;

        public GameCycle(
            ICharsGeneratorConfig charsGeneratorConfig,
            CharsGenerator charsGenerator,
            ICoroutinesPerformer coroutinesPerformer,
            SceneSwitcherService sceneSwitcherService,
            ControllersFactory controllersFactory,
            ConfigsProviderService configsProviderService,
            WalletService walletService,
            ScoreCounterService scoreCounterService,
            PlayerDataProvider playerDataProvider)
        {
            _charsGeneratorConfig = charsGeneratorConfig;
            _charsGenerator = charsGenerator;
            _coroutinesPerformer = coroutinesPerformer;
            _sceneSwitcherService = sceneSwitcherService;
            _controllersFactory = controllersFactory;
            _configsProviderService = configsProviderService;
            _walletService = walletService;
            _scoreCounterService = scoreCounterService;
            _playerDataProvider = playerDataProvider;
        }

        public IEnumerator Prepare()
        {
            _winLoseRewardConfig = _configsProviderService.GetConfig<ScoreRewardConfig>();
            yield break;
        }

        public IEnumerator Launch()
        {
            _charsGenerator.Generate(_charsGeneratorConfig.CharsType, _charsGeneratorConfig.CharsCount);

            _controller = _controllersFactory.CreateInputCharsController(_charsGenerator);

            Debug.Log("Сгенерированное слово: " + _charsGenerator.GenerateString);

            _gameMode = new GameMode(_charsGenerator);

            _gameMode.Wined += OnGameModeWined;
            _gameMode.Defeated += OnGameModeDefeated;

            _gameMode.Start();

            yield break;
        }
        public void Update(float deltaTime)
        {
            _controller?.Update(deltaTime);
            _gameMode?.Update(deltaTime);
        }

        public void Dispose()
        {
            _gameMode.Wined -= OnGameModeWined;
            _gameMode.Defeated -= OnGameModeDefeated;
        }

        private void OnGameModeWined()
        {
            foreach (CurrencyConfig currency in _winLoseRewardConfig.WinRewards)
            {
                _walletService.Add(currency.Type, currency.Value);
                _scoreCounterService.AddWin();
            }

            Debug.Log("Вы выиграли!");
            OnGameModeEnded(true);
        }

        private void OnGameModeDefeated()
        {
            foreach (CurrencyConfig currency in _winLoseRewardConfig.LoseRewards)
            {
                _walletService.Spend(currency.Type, currency.Value);
                _scoreCounterService.AddLose();
            }

            Debug.Log("Вы проиграли :(");
            OnGameModeEnded(false);
        }

        private void OnGameModeEnded(bool isWin)
        {
            if (_gameMode != null)
            {
                _gameMode.Wined -= OnGameModeWined;
                _gameMode.Defeated -= OnGameModeDefeated;

                _coroutinesPerformer.StartPerform(_playerDataProvider.Save());

                if (isWin)
                    _coroutinesPerformer.StartPerform(EndWinGameProcess());
                else
                    _coroutinesPerformer.StartPerform(EndDefeatGameProcess());
            }
        }

        private IEnumerator EndDefeatGameProcess()
        {
            yield return EndGameProcess();

            Debug.Log("Переигрываем");

            yield return Launch();
        }

        private IEnumerator EndWinGameProcess()
        {
            yield return EndGameProcess();

            Debug.Log("Переход на сцену меню");

            yield return _sceneSwitcherService.ProcessSwitchTo(Scenes.MainMenu);
        }

        private IEnumerator EndGameProcess()
        {
            Debug.Log("Нажмите пробел чтобы продолжить");

            yield return new WaitWhile(() => Input.GetKeyDown(KeyCode.Space) == false);
        }
    }
}