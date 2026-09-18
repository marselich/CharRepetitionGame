using Assets._Project.Develop.Runtime.Infrastructure.DI;
using Assets._Project.Develop.Runtime.Meta.Features.ScoreManagment;
using Assets._Project.Develop.Runtime.Meta.Features.Wallet;
using Assets._Project.Develop.Runtime.Utilities.ConfigsManagment;
using Assets._Project.Develop.Runtime.Utilities.CoroutinesManagment;
using Assets._Project.Develop.Runtime.Utilities.DataManagment.DataProviders;
using Assets._Project.Develop.Runtime.Utilities.SceneManagment;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Meta.Infrastructure
{
    public class MainMenuContextRegistrations
    {
        public static void Process(DIContainer c)
        {
            Debug.Log("Процесс регистрации на сцене меню");

            c.RegisterAsSingle(CreateGameModeSwitcher);

            c.RegisterAsSingle(CreateScoreDisplayer);

            c.RegisterAsSingle(CreateScoreResetService);
        }

        private static GameModeSwitcher CreateGameModeSwitcher(DIContainer c)
        {
            ICoroutinesPerformer coroutinesPerformer = c.Resolve<ICoroutinesPerformer>();
            SceneSwitcherService sceneSwitcherService = c.Resolve<SceneSwitcherService>();
            ConfigsProviderService configsProviderService = c.Resolve<ConfigsProviderService>();

            return new GameModeSwitcher(coroutinesPerformer, sceneSwitcherService, configsProviderService);
        }

        private static ScoreDisplayer CreateScoreDisplayer(DIContainer c)
         => new ScoreDisplayer(c.Resolve<ScoreCounterService>(), c.Resolve<WalletService>());

        private static ScoreResetService CreateScoreResetService(DIContainer c)
            => new ScoreResetService(
                c.Resolve<ScoreCounterService>(),
                c.Resolve<WalletService>(),
                c.Resolve<PlayerDataProvider>(),
                c.Resolve<ConfigsProviderService>(),
                c.Resolve<ICoroutinesPerformer>());
    }
}