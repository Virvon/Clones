using Clones.Services;
using Clones.UI;
using UnityEngine;

namespace Clones.EducationLogic
{
    public class ShowControlHandler : EducationHandler
    {
        private readonly IInputService _inputService;
        private readonly DialogPanel _dialogPanel;

        public ShowControlHandler(IInputService inputService, DialogPanel dialogPanel)
        {
            _inputService = inputService;
            _dialogPanel = dialogPanel;
        }

        public override void Handle()
        {
            _dialogPanel.Open();
            Debug.Log("Проведите мышью или пальцем по экрану для движения");
            _inputService.Activated += OnInputActivated;
        }

        private void OnInputActivated()
        {
            _dialogPanel.Close();
            _inputService.Activated -= OnInputActivated;
            Successor.Handle();
        }
    }
}
