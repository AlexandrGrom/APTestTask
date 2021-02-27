using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections;

public class Menu : ScreenElement
{
    [SerializeField] private Button play;
    [SerializeField] private float rotation = 360;
    [SerializeField] private float waitTime = 0.5f;
    [SerializeField] private TextMeshProUGUI timer;


    private void Awake()
    {
        play.onClick.AddListener(()=>StartCoroutine(Animation()));
    }

    IEnumerator Animation ()
    {
        play.transform.DOScale(Vector3.zero, 0.2f).SetEase(Ease.InBack);
        yield return new WaitForSeconds(0.2f);

        for (int i = 3; i >= 0; i--)
        {
            yield return new WaitForSeconds(waitTime/2);
            timer.text = i > 0 ? i.ToString() : "GO!";
            timer.transform.DOScale(Vector3.one, waitTime / 4).SetEase(Ease.InOutBounce);
            yield return new WaitForSeconds(waitTime/2);
            timer.transform.DORotate(Vector3.forward * rotation, waitTime, RotateMode.FastBeyond360).SetEase(Ease.InSine);
            timer.transform.DOScale(Vector3.zero, waitTime).SetEase(Ease.InSine);
            yield return new WaitForSeconds(waitTime);
        }

        GameStateManager.CurrentState = GameState.Game;
    }
}
