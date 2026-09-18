using Assets._Project.Develop.Runtime.Gameplay.Controllers;
using Assets._Project.Develop.Runtime.Gameplay.Generators;
using Assets._Project.Develop.Runtime.Infrastructure.DI;
using Assets._Project.Develop.Runtime.Meta.Features.ScoreManagment;
using Assets._Project.Develop.Runtime.Meta.Features.Wallet;
using Assets._Project.Develop.Runtime.Utilities.ConfigsManagment;
using Assets._Project.Develop.Runtime.Utilities.CoroutinesManagment;
using Assets._Project.Develop.Runtime.Utilities.DataManagment.DataProviders;
using Assets._Project.Develop.Runtime.Utilities.SceneManagment;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Gameplay.Infrastructure
{
    public class GameplayContextRegistrations
    {
        private static GameplayInputArgs _args;

        public static void Process(DIContainer c, GameplayInputArgs args)
        {
            Debug.Log("Процесс регистрации на сцене геймплея");

            _args = args;

            c.RegisterAsSingle<CharsGenerator>(CreateCharsGenerator);
            c.RegisterAsSingle<ControllersFactory>(CreateControllersFactory);
            c.RegisterAsSingle<GameCycle>(CreateGameCycle);
        }

        private static CharsGenerator CreateCharsGenerator(DIContainer c) => new CharsGenerator();

        private static ControllersFactory CreateControllersFactory(DIContainer c) => new ControllersFactory();

        private static GameCycle CreateGameCycle(DIContainer c)
            => new GameCycle(
                _args.CharsGeneratorConfig,
                c.Resolve<CharsGenerator>(),
                c.Resolve<ICoroutinesPerformer>(),
                c.Resolve<SceneSwitcherService>(),
                c.Resolve<ControllersFactory>(),
                c.Resolve<ConfigsProviderService>(),
                c.Resolve<WalletService>(),
                c.Resolve<ScoreCounterService>(),
                c.Resolve<PlayerDataProvider>()
                );
    }
}
