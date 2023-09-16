using Cinemachine;
using Clones.Animation;
using Clones.StaticData;
using UnityEngine;
using System;
using Object = UnityEngine.Object;
using Clones.Services;
using Clones.UI;

namespace Clones.Infrastructure
{
    public class GameFactory : IGameFactory
    {
        private GameObject _playerObject;

        private readonly IStaticDataService _staticData;
        private readonly IAssetProvider _assets;
        private readonly IInputService _inputService;
        private readonly IQuestsCreator _questsCreator;
        private readonly IDestroyDroppableReporter _destroyDroppableReporter;

        public GameFactory(IAssetProvider assets, IInputService inputService, IStaticDataService staticData, IQuestsCreator questsCreator, IDestroyDroppableReporter destroyDroppableReporter)
        {
            _assets = assets;
            _inputService = inputService;
            _staticData = staticData;
            _questsCreator = questsCreator;
            _destroyDroppableReporter = destroyDroppableReporter;
        }

        public void CreateHud()
        {
            var hud = _assets.Instantiate(AssetPath.Hud);

            hud.GetComponentInChildren<Freezbar>()
                .Init(_playerObject.GetComponentInChildren<PlayerFreezing>());

            hud.GetComponentInChildren<PlayerHealthbar>()
                .Init(_playerObject.GetComponent<PlayerHealth>());

            hud.GetComponentInChildren<QuestPanel>()
                .Init(_questsCreator, this);
        }

        public GameObject CreatePlayer()
        {
            _playerObject = _assets.Instantiate(AssetPath.Player);

            _playerObject.GetComponent<PlayerAnimationSwitcher>()
                .Init(_inputService);

            return _playerObject;
        }

        public void CreateWorldGenerator()
        {
            WorldGeneratorStaticData worldGeneratorData = _staticData.GetWorldGeneratorData();

            WorldGenerator worldGenerator = Object.Instantiate(worldGeneratorData.Prefab);
            worldGenerator.Init(this, _playerObject.transform, worldGeneratorData.GenerationBiomes, worldGeneratorData.ViewRadius, worldGeneratorData.CellSize);
        }

        public GameObject CreateTile(BiomeType type, Vector3 position, Quaternion rotation, Transform parent)
        {
            BiomeStaticData biomeData = _staticData.GetBiomeStaticData(type);

            var tile = Object.Instantiate(biomeData.Prefab, position, rotation, parent);

            tile.GetComponent<PreyResourcesSpawner>()?
                .Init(this, biomeData.PreyResourcesTamplates, biomeData.PercentageFilled);

            return tile;
        }

        public void CreateVirtualCamera()
        {
            _assets.Instantiate(AssetPath.VirtualCamera)
                .GetComponent<CinemachineVirtualCamera>()
                .Follow = _playerObject.transform;
        }

        public void CreatePreyResource(PreyResourceType type, Vector3 position, Quaternion rotation, Transform parent)
        {
            PreyResourceStaticData preyResourceData = _staticData.GetPreyResourceStaticData(type);

            var preyResource = Object.Instantiate(preyResourceData.Prefab, position, rotation, parent);

            preyResource.GetComponent<PreyResource>()
                .Init(preyResourceData.HitsCountToDie, preyResourceData.DroppetItem);

            _destroyDroppableReporter.AddDroppable(preyResource);
        }

        public GameObject CreateItem(ItemType type, Vector3 position)
        {
            ItemStaticData itemData = _staticData.GetItemStaticData(type);

            return Object.Instantiate(itemData.Prefab, position, Quaternion.identity);
        }

        public GameObject CreateQuestView(Quest quest, Transform parent)
        {
            var view = _assets.Instantiate(AssetPath.QuestView, parent);

            view.GetComponent<QuestView>()
                .Init(quest);

            return view;
        }
    }
}