using System.Collections.Generic;
using UnityEngine;
namespace Menu.ColorPicker
{
    [CreateAssetMenu(fileName = "ColorsConfig", menuName = "SO/ColorsConfig")]
    public class ColorsConfig : ScriptableObject
    {

        [SerializeField] private List<Color> _colors = new ();

        public IReadOnlyList<Color> Colors => _colors;

    }
}
