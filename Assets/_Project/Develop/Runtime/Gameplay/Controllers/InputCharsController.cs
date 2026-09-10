using Assets._Project.Develop.Runtime.Gameplay.Generators;
using System.Linq;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Gameplay.Controllers
{
    public class InputCharsController : Controller
    {
        private ICharsChecker _charsChecker;

        private string _input;

        public InputCharsController(ICharsChecker charsChecker)
        {
            _charsChecker = charsChecker;
            _input = "";
        }

        protected override void UpdateLogic(float deltaTime)
        {
            if (string.IsNullOrEmpty(Input.inputString) == false)
            {
                _input += Input.inputString.First();

                Debug.Log("Ваше сообщение: " + _input);
            }

            _charsChecker.Check(_input);
        }
    }
}