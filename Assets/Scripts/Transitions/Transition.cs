using System;
using DG.Tweening;
using UnityEngine;
namespace Transitions
{
    public class Transition : MonoBehaviour
    {

        [SerializeField] private RectTransform _eclipseRT;
        [SerializeField] private float _transitionTime;

        protected void Awake()
        {
            _eclipseRT.sizeDelta = new Vector2(0, Screen.height);
        }

        public void FadeIn(Action callback = null)
        {
            Sequence sequence = DOTween.Sequence();
            sequence.AppendCallback(delegate
            {
                _eclipseRT.anchoredPosition = Vector2.zero;
                _eclipseRT.pivot = new Vector2(1, .5f);
            });
            sequence.Append(_eclipseRT.DOSizeDelta(new Vector2(Screen.width, Screen.height), _transitionTime));
            sequence.AppendCallback(delegate
            {
                callback?.Invoke();
            });
        }

        public void FadeOut()
        {
            Sequence sequence = DOTween.Sequence();
            sequence.AppendCallback(delegate
            {
                _eclipseRT.anchoredPosition = Vector2.left * Screen.width;
                _eclipseRT.pivot = new Vector2(0, .5f);
            });
            sequence.Append(_eclipseRT.DOSizeDelta(new Vector2(0, Screen.height), _transitionTime));

            sequence.Play();
        }

    }
}
