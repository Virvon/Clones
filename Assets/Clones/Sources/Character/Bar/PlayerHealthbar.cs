using Clones.Character.Player;

namespace Clones.Character.Bars
{
    public class PlayerHealthbar : Healthbar
    {
        public void Init(PlayerHealth playerHealth) =>
            TakeHealthble(playerHealth);
    }
}