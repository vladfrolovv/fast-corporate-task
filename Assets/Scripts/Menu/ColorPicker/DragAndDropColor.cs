using System;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;
using Observable = UniRx.Observable;
namespace Menu.ColorPicker
{
    [RequireComponent(typeof(EventTrigger))]
    [RequireComponent(typeof(RectTransform))]
    public class DragAndDropColor : EventTrigger
    {

        private RectTransform _transform;
        private IDisposable _inputDisposable;

        public override void OnBeginDrag(PointerEventData eventData)
        {
            _inputDisposable = Observable.EveryUpdate().Subscribe(_ =>
            {
                Vector2 mousePosition;
                RectTransformUtility.ScreenPointToLocalPointInRectangle(
                    _transform.parent as RectTransform, // Parent RectTransform
                    Input.mousePosition, // Mouse position in screen coordinates
                    eventData.pressEventCamera, // Camera associated with the event
                    out mousePosition);
                _transform.localPosition = mousePosition;
            }).AddTo(this);
        }

        public override void OnEndDrag(PointerEventData eventData)
        {
            _inputDisposable?.Dispose();
            Reset();
        }

        protected void Awake()
        {
            _transform = GetComponent<RectTransform>();
        }

        private void Reset()
        {
            transform.localPosition = Vector3.zero;
        }

    }
}
