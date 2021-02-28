using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;


public class DataManager : MonoBehaviour
{
    public static Action UpdateData;

    [SerializeField] private int startTimeValue;
    [SerializeField] private Vector2Int startGoalValue;
    [SerializeField] private int step = 15;
    public static int LeftTime { get; private set; }
    public static int Goal{ get; private set; }
    public static int Counter { get; private set; }
    public static int StartTimeValue { get; private set; }
    private static int Step { get; set; }

    private void Awake()
    {
        Counter = 0;
        Step = step;
        StartTimeValue = startTimeValue;
        LeftTime = startTimeValue;
        Goal = Random.Range(startGoalValue.x, startGoalValue.y + 1);
        StartCoroutine(Progresstion());
    }

    private IEnumerator Progresstion()
    {
        yield return new WaitUntil(() => GameStateManager.CurrentState == GameState.Game);
        while (LeftTime >= 0)
        {
            WaitForSeconds waitTime = new WaitForSeconds(1);
            yield return waitTime;
            UpdateData?.Invoke();
            LeftTime--;
        }

        yield return new WaitUntil(() => ItemSpawner.ItemsInSceneCounter <= 0);
        GameStateManager.CurrentState = Counter >= Goal ? GameState.Win : GameState.Lose;

    }

    public static void DecrementTime(int time)
    {
        LeftTime -= time;
        UpdateData?.Invoke();
    }
    public static void IncrementCounter()
    {
        Counter++;
        if (Counter >= Goal)
        {
            GameStateManager.CurrentState = GameState.Win ;
        }
        UpdateData?.Invoke();

    }

    public static int EvaluateSpeedByTime()
    {
        return (StartTimeValue - LeftTime) / Step + 1;
    }
}
