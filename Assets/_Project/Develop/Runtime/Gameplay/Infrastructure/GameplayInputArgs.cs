using Assets._Project.Develop.Runtime.Gameplay.Configs;
using Assets._Project.Develop.Runtime.Utilities.SceneManagment;

namespace Assets._Project.Develop.Runtime.Gameplay.Infrastructure
{
    public class GameplayInputArgs : IInputSceneArgs
    {
        public GameplayInputArgs(ICharsGeneratorConfig charsGeneratorConfig)
        {
            CharsGeneratorConfig = charsGeneratorConfig;
        }

        public ICharsGeneratorConfig CharsGeneratorConfig { get; private set; }
    }
}