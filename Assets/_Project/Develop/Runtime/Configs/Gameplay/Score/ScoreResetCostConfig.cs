using Assets._Project.Develop.Runtime.Configs.Meta.Wallet;
using System.Collections.Generic;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Configs.Gameplay.Score
{
    [CreateAssetMenu(menuName = "Configs/Gameplay/Score/ScoreResetCostConfig", fileName = "ScoreResetCostConfig")]
    public class ScoreResetCostConfig : ScriptableObject
    {
        [SerializeField] private List<CurrencyConfig> _values;

        public IEnumerable<CurrencyConfig> Values => _values;
    }
}