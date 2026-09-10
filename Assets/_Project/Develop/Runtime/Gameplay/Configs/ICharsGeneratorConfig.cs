using Assets._Project.Develop.Runtime.Gameplay.Generators;

namespace Assets._Project.Develop.Runtime.Gameplay.Configs
{
    public interface ICharsGeneratorConfig
    {
        CharsType CharsType { get; }
        int CharsCount { get; }
    }
}