using DG.Tweening;
using TMPro;
using UnityEngine;
namespace Gameplay
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class CountdownView : MonoBehaviour
    {

        private TextMeshProUGUI _text;

        protected void Awake()
        {
            _text = GetComponent<TextMeshProUGUI>();
            AnimateCountdown(3);
        }

        private void AnimateCountdown(int startValue)
        {
            int currentValue = startValue;
            DOTween.To(() => currentValue, x => currentValue = x, 1, startValue * 1f)
                .SetEase(Ease.Linear)
                .OnUpdate(() =>
                {
                    _text.text = Mathf.CeilToInt(currentValue).ToString();
                    _text.transform.DOScale(1.5f, 0.32f).From(1f);
                    _text.DOColor(Color.red, 0.32f).From(Color.white);
                })
                .OnComplete(() =>
                {
                    _text.text = "GO!";

                    Sequence sequence = DOTween.Sequence();
                    sequence.AppendInterval(1f);
                    sequence.Append(_text.transform.DOScale(.75f, 0.32f).SetEase(Ease.OutQuint));
                    sequence.Join(_text.DOFade(0f, .32f).SetEase(Ease.OutQuint));
                    sequence.AppendCallback(delegate
                    {
                        gameObject.SetActive(false);
                    });
                    sequence.Play();
                });
        }

    }
}
