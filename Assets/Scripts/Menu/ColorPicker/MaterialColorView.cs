using TMPro;
using UnityEngine;
namespace Menu.ColorPicker
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class MaterialColorView : MonoBehaviour
    {

        private TextMeshProUGUI _text;

        [SerializeField] private MaterialColorHandler _materialColorHandler;

        protected void Awake()
        {
            _text = GetComponent<TextMeshProUGUI>();
            _materialColorHandler.ColorChanged += OnColorChanged;
        }

        protected void OnDestroy()
        {
            _materialColorHandler.ColorChanged -= OnColorChanged;
        }

        private void OnColorChanged(Color color)
        {
            _text.color = color;
        }

    }
}
