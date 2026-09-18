using Assets._Project.Develop.Runtime.Gameplay.Generators;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Configs.Gameplay.CharsGenerator
{
    [CreateAssetMenu(menuName = "Configs/Gameplay/CharsGenerator/DigitsGeneratorConfig", fileName = "DigitsGeneratorConfig")]
    public class DigitsGeneratorConfig : ScriptableObject, ICharsGeneratorConfig
    {
        public CharsType CharsType { get; } = CharsType.Digits;
        [field: SerializeField] public int CharsCount { get; private set; }
    }
}