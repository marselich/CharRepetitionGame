using Assets._Project.Develop.Runtime.Infrastructure.DI;
using Assets._Project.Develop.Runtime.Utilities.ConfigsManagment;
using Assets._Project.Develop.Runtime.Utilities.CoroutinesManagment;
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
        }

        private static GameModeSwitcher CreateGameModeSwitcher(DIContainer c)
        {
            ICoroutinesPerformer coroutinesPerformer = c.Resolve<ICoroutinesPerformer>();
            SceneSwitcherService sceneSwitcherService = c.Resolve<SceneSwitcherService>();
            ConfigsProviderService configsProviderService = c.Resolve<ConfigsProviderService>();

            return new GameModeSwitcher(coroutinesPerformer, sceneSwitcherService, configsProviderService);
        }
    }
}