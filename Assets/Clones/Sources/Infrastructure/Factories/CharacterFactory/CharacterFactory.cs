using Clones.Animation;
using Clones.StaticData;
using UnityEngine;
using Clones.GameLogic;
using Object = UnityEngine.Object;
using Clones.Data;
using Clones.StateMachine;
using Clones.Types;
using Clones.SFX;
using Clones.Services;

namespace Clones.Infrastructure
{
    public class CharacterFactory : ICharacterFactory
    {
        private const string UiLayer = "UI";

        private readonly IPersistentProgressService _persistentPorgress;
        private readonly IMainMenuStaticDataService _mainMenuStaticDataService;
        private readonly IInputService _inputService;

        private GameObject _playerObject;
        private Transform _modelWandBone;

        public CharacterFactory(IPersistentProgressService persistentPorgress, IMainMenuStaticDataService mainMenuStaticDataService, IInputService inputService)
        {
            _persistentPorgress = persistentPorgress;
            _mainMenuStaticDataService = mainMenuStaticDataService;
            _inputService = inputService;
        }

        public GameObject CreateCharacter(IPartsFactory partsFactory, IItemsCounter itemsCounter)
        {
            CloneData cloneData = _persistentPorgress.Progress.AvailableClones.GetSelectedCloneData();
            WandData wandData = _persistentPorgress.Progress.AvailableWands.GetSelectedWandData();
            CloneStaticData cloneStaticData = _mainMenuStaticDataService.GetClone(_persistentPorgress.Progress.AvailableClones.SelectedClone) ?? _mainMenuStaticDataService.GetClone(CloneType.Normal);
            WandStaticData wandStaticData = GetWandStaticData();

            int health;
            int damage;
            float attackCooldown;

            if (cloneData != null && wandData != null)
            {
                health = cloneData.Health + (int)(cloneData.Health * wandData.WandStats.HealthIncreasePercentage / 100f);
                damage = cloneData.Damage + (int)(cloneData.Damage * wandData.WandStats.DamageIncreasePercentage / 100f);
                attackCooldown = cloneData.AttackCooldown * (1 - wandData.WandStats.AttackCooldownDecreasePercentage / 100f);
            }
            else
            {
                health = cloneStaticData.Helath;
                damage = cloneStaticData.Damage;
                attackCooldown = cloneStaticData.AttackCooldown;
            }

            _playerObject = Object.Instantiate(cloneStaticData.Prefab);

            Player player = _playerObject.GetComponent<Player>();

            player
                .GetComponent<Player>()
                .Init(cloneStaticData.MovementSpeed, attackCooldown);


            PlayerAnimationSwitcher playerAnimationSwitcher = _playerObject.GetComponent<PlayerAnimationSwitcher>();

            if (wandData != null)
                playerAnimationSwitcher.Init(_inputService, player, wandData.WandStats.AttackCooldownDecreasePercentage);
            else
                playerAnimationSwitcher.Init(_inputService, player);

            _playerObject
                .GetComponent<DropCollecting>()
                .Init(itemsCounter, cloneStaticData.DropCollectingRadius);

            _playerObject
                .GetComponent<PlayerHealth>()
                .Init(health);

            _playerObject
                .GetComponent<PlayerStateMashine>()
                .Init(_inputService);

            _playerObject
                .GetComponent<MovementState>()
                .Init(_inputService, player, cloneStaticData.RotationSpeed);

            _playerObject
                .GetComponent<MiningState>()
                .Init(cloneStaticData.MiningRadius, cloneStaticData.RotationSpeed);

            _playerObject
                .GetComponent<EnemiesAttackState>()
                .Init(cloneStaticData.AttackRadius, cloneStaticData.RotationSpeed);

            _playerObject
                .GetComponent<Wand>()
                .Init(partsFactory, wandStaticData.Bullet, damage, wandStaticData.KnockbackForse, wandStaticData.KnockbackOffset, player);

            _playerObject
                .GetComponentInChildren<MovementSound>()
                .Init(player);

            _playerObject
                .GetComponentInChildren<CharacterShootSound>()
                .Init(wandStaticData.ShootAudio, wandStaticData.ShootAudioVolume);

            _playerObject
                .GetComponent<PlayerRebornEffect>()
                .Init(cloneStaticData.RebornEffect, cloneStaticData.RebornEffectOffset);

            return _playerObject;
        }

        public GameObject CreateWand(Transform bone)
        {
            WandStaticData wandStaticData = GetWandStaticData();

            return Object.Instantiate(wandStaticData.Prefab, bone);
        }

        public GameObject CreateWandModel()
        {
            GameObject wand = CreateWand(_modelWandBone);

            wand.layer = LayerMask.NameToLayer(UiLayer);

            foreach (var element in wand.GetComponentsInChildren<Transform>())
                element.gameObject.layer = LayerMask.NameToLayer(UiLayer);

            return wand;
        }

        public GameObject CreateCloneModel(Transform parent)
        {
            CloneStaticData cloneStaticData = _mainMenuStaticDataService.GetClone(_persistentPorgress.Progress.AvailableClones.SelectedClone);

            GameObject clone = Object.Instantiate(cloneStaticData.Model, parent);

            _modelWandBone = clone.GetComponent<WandBone>().Bone;

            return clone;
        }

        private WandStaticData GetWandStaticData() =>
           _mainMenuStaticDataService.GetWand(_persistentPorgress.Progress.AvailableWands.SelectedWand) ?? _mainMenuStaticDataService.GetWand(WandType.BranchWand);
    }
}