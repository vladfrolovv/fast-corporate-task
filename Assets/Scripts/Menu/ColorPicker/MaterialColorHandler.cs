using System;
using UnityEngine;
using UnityEngine.EventSystems;
namespace Menu.ColorPicker
{
    public class MaterialColorHandler : MonoBehaviour, IDropHandler
    {

        [SerializeField] private Material _material;

        public event Action<Color> ColorChanged;

        public void OnDrop(PointerEventData eventData)
        {
            if (eventData.pointerDrag == null) return;
            eventData.pointerDrag.TryGetComponent(out DnDColorPanelItem panelItem);
            if (panelItem == null) return;

            _material.color = panelItem.Color;
            ColorChanged?.Invoke(panelItem.Color);
        }
    }
}
