using TMPro;
using UnityEngine;
using DG.Tweening;

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


        time.text = $"{ DataManager.LeftTime / 60} : {secondsString}";
        progress.text = $"{DataManager.Counter}/{DataManager.Goal}";
    }

    private void OnEnable()
    {
        OnUpdateData();
        Hud.DOAnchorPos(Vector2.zero, 0.2f).SetEase(Ease.OutSine);
    }

    private void Update()
    {
        if (DataManager.LeftTime < 15)
        {
            time.color = Color.red;
        }
        if (DataManager.LeftTime < 15)
        {
            float speed = 10;
            float range = 0.1f;
            time.transform.localScale = Vector3.one + Vector3.one * range *  Mathf.Sin(Time.time * speed);
        }
    }


}
