using Assets._Project.Develop.Runtime.Infrastructure;
using Assets._Project.Develop.Runtime.Infrastructure.DI;
using Assets._Project.Develop.Runtime.Utilities.SceneManagment;
using System;
using System.Collections;
using UnityEngine;


namespace Assets._Project.Develop.Runtime.Gameplay.Infrastructure
{
    public class GameplayBootstrap : SceneBootstrap
    {
        private DIContainer _container;
        private GameplayInputArgs _inputArgs;

        private GameCycle _gameCycle;

        public override void ProcessRegistrations(DIContainer container, IInputSceneArgs sceneArgs = null)
        {
            _container = container;

            if (sceneArgs is not GameplayInputArgs gameplayInputArgs)
                throw new ArgumentException($"{nameof(sceneArgs)} is not match with {typeof(GameplayInputArgs)} type");

            _inputArgs = gameplayInputArgs;

            GameplayContextRegistrations.Process(container, _inputArgs);
        }

        public override IEnumerator Initialize()
        {
            Debug.Log($"Вы попали на уровень: {_inputArgs.CharsGeneratorConfig.CharsType.ToString()}");

            _gameCycle = _container.Resolve<GameCycle>();

            yield break;
        }

        public override IEnumerator Run()
        {
            yield return _gameCycle.Prepare();

            yield return _gameCycle.Launch();

            yield break;
        }

        private void Update()
        {
            _gameCycle?.Update(Time.deltaTime);
        }

        private void OnDestroy()
        {
            _gameCycle.Dispose();
        }
    }
}