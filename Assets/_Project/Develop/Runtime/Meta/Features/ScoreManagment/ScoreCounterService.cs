using Assets._Project.Develop.Runtime.Utilities.DataManagment.Data;
using Assets._Project.Develop.Runtime.Utilities.DataManagment.DataProviders;

namespace Assets._Project.Develop.Runtime.Meta.Features.ScoreManagment
{
    public class ScoreCounterService : IDataReader<PlayerData>, IDataWriter<PlayerData>
    {
        private int _winCount;
        private int _loseCount;

        public ScoreCounterService(PlayerDataProvider playerDataProvider)
        {
            playerDataProvider.RegisterWriter(this);
            playerDataProvider.RegisterReader(this);
        }

        public int WinCount => _winCount;

        public int LoseCount => _loseCount;

        public void AddWin() => _winCount++;

        public void AddLose() => _loseCount++;

        public void Reset()
        {
            _winCount = 0;
            _loseCount = 0;
        }

        public void ReadFrom(PlayerData data)
        {
            _winCount = data.WinCount;
            _loseCount = data.LoseCount;
        }

        public void WriteTo(PlayerData data)
        {
            data.WinCount = _winCount;
            data.LoseCount = _loseCount;
        }
    }
}