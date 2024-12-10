using System;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;
using Object = UnityEngine.Object;
namespace Menu.ColorPicker
{
    public class ColorsPanel : IDisposable
    {

        private readonly List<DnDColorPanelItem> _panelColors = new();

        private readonly DnDColorPanelItem _panelItemPrefab;
        private readonly ColorsPanelParent _panelParent;

        private readonly ReactiveProperty<Color> _selectedColor = new(Color.white);
        public IReadOnlyReactiveProperty<Color> SelectedColor => _selectedColor;

        public ColorsPanel(DnDColorPanelItem item, ColorsPanelParent parent, ColorsConfig config)
        {
            _panelItemPrefab = item;
            _panelParent = parent;

            Observable.NextFrame().Subscribe(delegate
            {
                config.Colors.ToList().ForEach(CreateColorPanelItem);
            });
        }

        private void CreateColorPanelItem(Color color)
        {
            DnDColorPanelItem panelItem = Object.Instantiate(_panelItemPrefab, Vector3.zero, Quaternion.identity);
            panelItem.InitColor(color);
            panelItem.transform.parent = _panelParent.transform;

            _panelColors.Add(panelItem);
        }

        public void Dispose()
        {
        }

    }
}
