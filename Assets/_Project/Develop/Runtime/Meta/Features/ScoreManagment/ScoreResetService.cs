using Assets._Project.Develop.Runtime.Configs.Gameplay.Score;
using Assets._Project.Develop.Runtime.Configs.Meta.Wallet;
using Assets._Project.Develop.Runtime.Meta.Features.Wallet;
using Assets._Project.Develop.Runtime.Utilities.ConfigsManagment;
using Assets._Project.Develop.Runtime.Utilities.CoroutinesManagment;
using Assets._Project.Develop.Runtime.Utilities.DataManagment.DataProviders;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Meta.Features.ScoreManagment
{
    public class ScoreResetService
    {
        private ScoreCounterService _scoreCounterService;
        private WalletService _walletService;
        private PlayerDataProvider _playerDataProvider;
        private ConfigsProviderService _configProviderService;
        private ICoroutinesPerformer _coroutinesPerformer;

        public ScoreResetService(
            ScoreCounterService scoreCounterService,
            WalletService walletService,
            PlayerDataProvider playerDataProvider,
            ConfigsProviderService configProviderService,
            ICoroutinesPerformer coroutinesPerformer)
        {
            _scoreCounterService = scoreCounterService;
            _walletService = walletService;
            _playerDataProvider = playerDataProvider;
            _configProviderService = configProviderService;
            _coroutinesPerformer = coroutinesPerformer;
        }

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.R))
                Reset();
        }

        private void Reset()
        {
            _scoreCounterService.Reset();

            ScoreResetCostConfig scoreResetCostConfig = _configProviderService.GetConfig<ScoreResetCostConfig>();

            bool isEnoughToReset = true;

            foreach (CurrencyConfig currency in scoreResetCostConfig.Values)
            {
                if (_walletService.Enough(currency.Type, currency.Value) == false)
                {
                    isEnoughToReset = false;
                    break;
                }
            }

            if (isEnoughToReset)
            {
                foreach (CurrencyConfig currency in scoreResetCostConfig.Values)
                    _walletService.Spend(currency.Type, currency.Value);

                Debug.Log("Прогресс игры сброшен.");

                _coroutinesPerformer.StartPerform(_playerDataProvider.Save());
            }
            else
            {
                Debug.Log("Не хватает монет!");
            }
        }
    }
}
