using Assets._Project.Develop.Runtime.Meta.Features.Wallet;
using System;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Meta.Features.ScoreManagment
{
    public class ScoreDisplayer
    {
        private ScoreCounterService _scoreCounterService;
        private WalletService _walletService;

        public ScoreDisplayer(ScoreCounterService scoreCounterService, WalletService walletService)
        {
            _scoreCounterService = scoreCounterService;
            _walletService = walletService;
        }

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.S))
                Debug.Log(DisplayInfo());
        }

        private string DisplayInfo()
        {
            string temp = $"Побед: {_scoreCounterService.WinCount}; Поражений: {_scoreCounterService.LoseCount};\n";

            temp += "Кошелек:\n";

            foreach (CurrencyTypes type in Enum.GetValues(typeof(CurrencyTypes)))
                temp += $"{type}: {_walletService.GetCurrency(type).Value};\n";

            return temp;
        }
    }
}