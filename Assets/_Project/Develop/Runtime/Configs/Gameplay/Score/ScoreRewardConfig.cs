using Assets._Project.Develop.Runtime.Configs.Meta.Wallet;
using System.Collections.Generic;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Configs.Gameplay.Score
{
    [CreateAssetMenu(menuName = "Configs/Gameplay/Score/ScoreRewardConfig", fileName = "ScoreRewardConfig")]
    public class ScoreRewardConfig : ScriptableObject
    {
        [SerializeField] private List<CurrencyConfig> _winRewardValues;
        [SerializeField] private List<CurrencyConfig> _loseRewardValues;

        public IEnumerable<CurrencyConfig> WinRewards => _winRewardValues;
        public IEnumerable<CurrencyConfig> LoseRewards => _loseRewardValues;
    }
}