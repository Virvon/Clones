using Clones.GameLogic;
using Clones.Services;
using UnityEngine;

namespace Clones.Infrastructure
{
    public interface IUiFactory : IService
    {
        GameObject CreateHud(IQuestsCreator questsCreator, GameObject playerObject);
        GameObject CreateQuestView(Quest quest, Transform parent);
    }
}