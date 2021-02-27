using TMPro;
using UnityEngine;
using DG.Tweening;
using System;

public class Game : ScreenElement
{
    [SerializeField] private TextMeshProUGUI time; 
    [SerializeField] private TextMeshProUGUI progress; 
    [SerializeField] private RectTransform Hud;

    private void Awake()
    {
        DataManager.UpdateData += OnUpdateData;
    }

    private void OnDestroy()
    {
        DataManager.UpdateData -= OnUpdateData;
    }

    private void OnUpdateData()
    {
        int seconds = DataManager.LeftTime % 60;
        if (seconds < 0) seconds = 0;
        string secondsString = (seconds < 10 ? "0" : "") + seconds;

        if (DataManager.LeftTime < 15)
        {
            time.color = Color.red;
        }
        time.text = $"{ DataManager.LeftTime / 60} : {secondsString}";
        progress.text = $"{DataManager.Counter}/{DataManager.Goal}";
    }

    private void OnEnable()
    {
        OnUpdateData();
        Hud.DOAnchorPos(Vector2.zero, 0.2f).SetEase(Ease.OutSine);
    }


}
