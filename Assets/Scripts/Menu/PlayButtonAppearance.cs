using DG.Tweening;
using Menu.ColorPicker;
using UnityEngine;
using Zenject;
namespace Menu
{
    public class PlayButtonAppearance : MonoBehaviour
    {

        private ColorsSelectorObserver _colorsSelectorObserver;

        [Inject]
        public void Construct(ColorsSelectorObserver colorsSelectorObserver)
        {
            _colorsSelectorObserver = colorsSelectorObserver;
        }

        protected void Awake()
        {
            transform.localScale = Vector3.zero;
            _colorsSelectorObserver.OnAllColorsSelected += Appear;
        }

        protected void OnDestroy()
        {
            _colorsSelectorObserver.OnAllColorsSelected -= Appear;
        }

        private void Appear()
        {
            transform.DOScale(Vector3.one, .32f).SetEase(Ease.OutBack);
        }

    }
}
