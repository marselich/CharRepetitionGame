using Assets._Project.Develop.Runtime.Gameplay.Generators;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Gameplay.Configs
{
    [CreateAssetMenu(menuName = "Configs/Gameplay/LettersGeneratorConfig", fileName = "LettersGeneratorConfig")]
    public class LettersGeneratorConfig : ScriptableObject, ICharsGeneratorConfig
    {
        public CharsType CharsType { get; } = CharsType.Letters;
        [field: SerializeField] public int CharsCount { get; private set; }
    }
}
