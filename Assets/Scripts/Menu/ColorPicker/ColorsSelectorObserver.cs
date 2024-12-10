using System;
using System.Collections.Generic;
using Attributes;
using UnityEngine;
namespace Menu.ColorPicker
{
    public class ColorsSelectorObserver : MonoBehaviour
    {

        [SerializeField] private List<ColorSelector> _colorSelectors = new ();

        public Action OnAllColorsSelected;

        protected void Awake()
        {
            _colorSelectors.ForEach(ColorSelectorBehaviour);
        }

        private void ColorSelectorBehaviour(ColorSelector colorSelector)
        {
            colorSelector.MaterialColorHandler.ColorChanged += delegate
            {
                colorSelector.Selected = true;
                if (_colorSelectors.TrueForAll(selector => selector.Selected))
                {
                    OnAllColorsSelected?.Invoke();
                }
            };
        }

        [Serializable]
        public class ColorSelector
        {
            [SerializeField] private MaterialColorHandler _materialColorHandler;
            [ReadOnly] [SerializeField]
            private bool _selected;

            public MaterialColorHandler MaterialColorHandler => _materialColorHandler;

            public bool Selected
            {
                get => _selected;
                set => _selected = value;
            }
        }

    }
}
