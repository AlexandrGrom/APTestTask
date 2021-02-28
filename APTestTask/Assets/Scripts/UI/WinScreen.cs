using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinScreen : ScreenElement
{
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private TextMeshProUGUI currencyText;
    [SerializeField] private TextMeshProUGUI goal;
    [SerializeField] private Image back;
    [SerializeField] private Button restart;
    [SerializeField] private int count;
    [SerializeField] private float duration;

    private void Awake()
    {
        restart.onClick.AddListener(() => { DOTween.KillAll(); SceneManager.LoadScene(SceneManager.GetActiveScene().name); });
    }

    private void OnEnable()
    {
        ResetAll();
        Animation();
    }

    private void ResetAll()
    {
        restart.transform.localScale = Vector3.zero;
        text.transform.localScale = Vector3.up;
        back.color = new Color(0, 0, 0, 0);
        goal.transform.localScale = Vector3.up;
        currencyText.transform.localScale = Vector3.up;
        goal.text = $"you got: {DataManager.Counter} / {DataManager.Goal}";
    }

    private void Animation()
    {
        Sequence sequence = DOTween.Sequence();
        sequence.Append(back.DOFade(0.2f, duration).SetEase(Ease.OutBack));
        sequence.Append(text.transform.DOScale(Vector3.one, duration).SetEase(Ease.OutBack));
        sequence.Append(goal.transform.DOScale(Vector3.one, duration).SetEase(Ease.OutBack));
        sequence.Append(currencyText.transform.DOScale(Vector3.one, duration).SetEase(Ease.OutBack));

        currencyText.text = "you earned: " + 0.ToString();
        float value = 0;

        sequence.Insert(1,
            DOTween.To
            (
                () =>
                value,
                x => value = x,
                count,
                1
            )
            .OnUpdate
            (
                () =>
                {
                    currencyText.text = "you earned: " + ((int)value).ToString();
                }
            )
            .OnComplete
            (
                () =>
                {
                    currencyText.text = "you earned: " + count.ToString();
                    currencyText.transform.DOScale(Vector3.one * 1.3f, 0.2f)
                    .OnComplete
                    (
                        () =>
                        {
                            currencyText.transform.DOScale(Vector3.one, 0.2f);
                        }
                    );
                }
            )
        );

        sequence.Append(restart.transform.DOScale(Vector3.one, duration).SetEase(Ease.OutBack));
    }
}
