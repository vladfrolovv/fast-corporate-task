using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
namespace Menu.ColorPicker
{
    public class DnDColorPanelItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {

        [SerializeField] private Image _view;

        private Transform _standardParent;

        public Color Color { get; private set; }

        public void InitColor(Color color)
        {
            Color = color;
            _view.color = color;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            _standardParent = _view.transform.parent;

            _view.transform.SetParent(transform.root);
            _view.transform.SetAsLastSibling();

            _view.raycastTarget = false;
        }

        public void OnDrag(PointerEventData eventData)
        {
            _view.transform.position = Input.mousePosition;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            Reset();
        }

        private void Reset()
        {
            _view.transform.SetParent(_standardParent);
            _view.transform.localPosition = Vector3.zero;
            _view.raycastTarget = true;
        }

    }
}
