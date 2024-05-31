using Clones.Services;
using Clones.UI;

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
