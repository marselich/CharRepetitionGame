namespace Assets._Project.Develop.Runtime.Gameplay.Controllers
{
    public abstract class Controller
    {
        private bool _isEnable;

        public virtual void Enable() => _isEnable = true;

        public virtual void Disable() => _isEnable = false;

        public void Update(float deltaTime)
        {
            if (_isEnable == false)
                return;

            UpdateLogic(deltaTime);
        }

        protected abstract void UpdateLogic(float deltaTime);
    }
}