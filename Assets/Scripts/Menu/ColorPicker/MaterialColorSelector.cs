using DG.Tweening;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
namespace Menu.ColorPicker
{
    [RequireComponent(typeof(Image))]
    public class MaterialColorSelector : MonoBehaviour
    {

        private ColorsPanel _colorsPanel;
        private Image _image;

        [Inject]
        public void Construct(ColorsPanel colorsPanel)
        {
            _colorsPanel = colorsPanel;
        }

        protected void Awake()
        {
            _image = GetComponent<Image>();
            _colorsPanel.SelectedColor.Subscribe(delegate(Color color)
            {
                _image.DOColor(color, .16f);
            });
        }

    }
}
