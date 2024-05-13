using TMPro;
using UnityEngine;

namespace Clones.UI
{
    public class LeaderboardElement : MonoBehaviour
    {
        [SerializeField] private TMP_Text _rank;
        [SerializeField] private TMP_Text _name;
        [SerializeField] private TMP_Text _score;

        public void Init(int rank, string name, int score)
        {
            _rank.text = rank.ToString();
            _name.text = name;
            _score.text = score.ToString();
        }
    }
}
