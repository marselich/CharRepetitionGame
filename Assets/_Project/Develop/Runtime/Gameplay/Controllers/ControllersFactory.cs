using Assets._Project.Develop.Runtime.Gameplay.Generators;

namespace Assets._Project.Develop.Runtime.Gameplay.Controllers
{
    public class ControllersFactory
    {
        public Controller CreateInputCharsController(ICharsChecker charsChecker)
        {
            Controller controller = new InputCharsController(charsChecker);
            controller.Enable();

            return controller;
        }
    }
}