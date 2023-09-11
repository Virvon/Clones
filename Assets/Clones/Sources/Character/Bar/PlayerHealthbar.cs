public class PlayerHealthbar : Healthbar
{
    public void Init(PlayerHealth playerHealth) => 
        TakeHealthble(playerHealth);
}
