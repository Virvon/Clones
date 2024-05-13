﻿using UnityEngine;

namespace Clones.GameLogic
{
    public class ScorePerItemsCounter : IScoreCounter
    {
        private readonly IItemsCounter _itemsCounter;
        private readonly int _scorePerItem;

        public int Score { get; private set; }

        public ScorePerItemsCounter(IItemsCounter itemsCounter, int scorePerItem)
        {
            _itemsCounter = itemsCounter;
            _scorePerItem = scorePerItem;

            _itemsCounter.ItemTaked += OnItemTaked;
        }

        ~ScorePerItemsCounter() =>
            _itemsCounter.ItemTaked -= OnItemTaked;

        private void OnItemTaked() =>
            Score += _scorePerItem;

        public void ShowInfo()
        {
            Debug.Log("счет за итемы " + Score);
        }
    }
}
