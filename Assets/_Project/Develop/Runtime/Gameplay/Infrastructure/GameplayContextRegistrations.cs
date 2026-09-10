using Assets._Project.Develop.Runtime.Gameplay.Controllers;
using Assets._Project.Develop.Runtime.Gameplay.Generators;
using Assets._Project.Develop.Runtime.Infrastructure.DI;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Gameplay.Infrastructure
{
    public class GameplayContextRegistrations
    {
        public static void Process(DIContainer c, GameplayInputArgs args)
        {
            Debug.Log("Процесс регистрации на сцене геймплея");

            c.RegisterAsSingle<CharsGenerator>(CreateCharsGenerator);
            c.RegisterAsSingle<ControllersFactory>(CreateControllersFactory);
        }

        private static CharsGenerator CreateCharsGenerator(DIContainer c) => new CharsGenerator();

        private static ControllersFactory CreateControllersFactory(DIContainer c) => new ControllersFactory();

    }
}
