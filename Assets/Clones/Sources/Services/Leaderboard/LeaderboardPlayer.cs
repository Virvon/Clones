namespace Clones.Services
{
    public class LeaderboardPlayer
    {
        public int Rank { get; private set; }
        public string Name { get; private set; }
        public int Score { get; private set; }

        public LeaderboardPlayer(int rank, string name, int score)
        {
            Rank = rank;
            Name = name;
            Score = score;
        }
    }
}
