using UnityEngine;
namespace Menu.ColorPicker
{
    public class MaterialColorHandler : MonoBehaviour
    {

        [SerializeField] private Material _material;

        public void SetColor(Color color)
        {
            _material.color = color;
        }

    }
}
