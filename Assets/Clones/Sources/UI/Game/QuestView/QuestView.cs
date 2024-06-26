﻿using Clones.GameLogic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Clones.UI
{
    public class QuestView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _value;
        [SerializeField] private TMP_Text _questValue;
        [SerializeField] private TMP_Text _description;
        [SerializeField] private Image _image;

        private Quest _quest;

        public void Init(Quest quest)
        {
            _quest = quest;
            _description.text = quest.ItemName;
            _image.sprite = quest.Icon;

            UpdateInfo();
        }

        public void UpdateInfo()
        {
            _value.text = _quest.CurrentItemsCount.ToString();
            _questValue.text = _quest.TargetItemsCount.ToString();
        }
    }
}
