using Assets._Project.Develop.Runtime.Gameplay.Configs;
using Assets._Project.Develop.Runtime.Gameplay.Controllers;
using Assets._Project.Develop.Runtime.Gameplay.Generators;
using Assets._Project.Develop.Runtime.Infrastructure.DI;
using Assets._Project.Develop.Runtime.Utilities.CoroutinesManagment;
using Assets._Project.Develop.Runtime.Utilities.SceneManagment;
using System;
using System.Collections;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Gameplay.Infrastructure
{
    public class GameCycle : IDisposable
    {
        private DIContainer _container;

        private ICharsGeneratorConfig _charsGeneratorConfig;
        private CharsGenerator _charsGenerator;
        private GameMode _gameMode;
        private ICoroutinesPerformer _coroutinesPerformer;
        private SceneSwitcherService _sceneSwitcherService;
        private Controller _controller;
        private ControllersFactory _controllersFactory;

        public GameCycle(DIContainer container, ICharsGeneratorConfig charsGeneratorConfig)
        {
            _container = container;
            _charsGeneratorConfig = charsGeneratorConfig;
        }

        public IEnumerator Prepare()
        {
            _charsGenerator = _container.Resolve<CharsGenerator>();

            _coroutinesPerformer = _container.Resolve<ICoroutinesPerformer>();

            _sceneSwitcherService = _container.Resolve<SceneSwitcherService>();

            _controllersFactory = _container.Resolve<ControllersFactory>();

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
            Debug.Log("Вы выиграли!");
            OnGameModeEnded(true);
        }

        private void OnGameModeDefeated()
        {
            Debug.Log("Вы проиграли :(");
            OnGameModeEnded(false);
        }

        private void OnGameModeEnded(bool isWin)
        {
            if (_gameMode != null)
            {
                _gameMode.Wined -= OnGameModeWined;
                _gameMode.Defeated -= OnGameModeDefeated;

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