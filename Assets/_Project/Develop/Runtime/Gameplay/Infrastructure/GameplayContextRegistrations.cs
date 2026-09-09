using Assets._Project.Develop.Runtime.Infrastructure.DI;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Gameplay.Infrastructure
{
    public class GameplayContextRegistrations
    {
        public static void Process(DIContainer c, GameplayInputArgs args)
        {
            Debug.Log("Процесс регистрации на сцене геймплея");
        }
    }
}
