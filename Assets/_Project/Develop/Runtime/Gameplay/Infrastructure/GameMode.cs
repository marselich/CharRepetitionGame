using Assets._Project.Develop.Runtime.Gameplay.Generators;
using System;

namespace Assets._Project.Develop.Runtime.Gameplay.Infrastructure
{
    public class GameMode
    {
        public event Action Wined;
        public event Action Defeated;

        private ICharsEqualed _charsEqualed;

        private bool _isRunning;

        public GameMode(ICharsEqualed charsEqualed)
        {
            _charsEqualed = charsEqualed;
        }

        public void Start()
        {
            _isRunning = true;
        }

        public void Update(float deltaTime)
        {
            if (_isRunning == false)
                return;

            if (_charsEqualed.IsCharsEquel == false)
                ProcessDefeat();

            if (_charsEqualed.IsCharsEquelInLength)
                ProcessWin();
        }

        private void ProcessEndGame()
        {
            _isRunning = false;
        }

        private void ProcessWin()
        {
            ProcessEndGame();
            Wined?.Invoke();
        }

        private void ProcessDefeat()
        {
            ProcessEndGame();
            Defeated?.Invoke();
        }
    }
}