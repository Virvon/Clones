using Clones.GameLogic;
using Clones.Services;
using Clones.UI;
using UnityEngine;

namespace Clones.Infrastructure
{
    public interface IUiFactory : IService
    {
        GameObject CreateHud(IQuestsCreator questsCreator, GameObject playerObject);
        GameObject CreateQuestView(Quest quest, Transform parent);
        void CreateControl(Player player);
        void CreateGameRevivleView(IPlayerRevival playerRevival);
        void CreateGameOverView();
        IOpenableView CreateEducationOverView();
        DialogPanel CreateDialogPanel(string path);
        FrameFocus CreateFrameFocus();
    }
}