using System;
public static class GameStateManager 
{
    public static Action<GameState> OnGameStateChange;

    private static GameState currentState;
    public static GameState CurrentState {
        get => currentState;
        set {
            currentState = value;
            OnGameStateChange?.Invoke(value);
        }
    }
}
