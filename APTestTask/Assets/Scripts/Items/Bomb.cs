using UnityEngine;

public class Bomb : Item
{
    public override void Collide()
    {
        GameStateManager.CurrentState = GameState.Lose;
        base.Collide();
    }

}
