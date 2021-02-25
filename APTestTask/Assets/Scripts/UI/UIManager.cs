using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private Dictionary<GameState, ScreenElement> Screens = new Dictionary<GameState, ScreenElement>(); 

    private void Awake()
    {
        Screens = GetComponentsInChildren<ScreenElement>(true).ToDictionary(x => x.State, x => x);
        GameStateManager.OnGameStateChange += SwitchState;
        GameStateManager.CurrentState = GameState.Menu;
    }

    private void OnDestroy()
    {
        GameStateManager.OnGameStateChange -= SwitchState;
    }

    private void SwitchState(GameState state)
    {
        foreach (var screen in Screens)
        {
            screen.Value.gameObject.SetActive(false);
        }
        Screens[state].gameObject.SetActive(true);
    }
}
