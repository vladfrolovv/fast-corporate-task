using UnityEngine;
using UnityEngine.UI;
namespace Menu
{
    [RequireComponent(typeof(GridLayoutGroup))]
    public class AdaptiveGrid : MonoBehaviour
    {

        [SerializeField] private int _columnsCount = 3;
        [SerializeField] private Vector2 _spacing;

        private RectTransform _container;
        private GridLayoutGroup _gridLayoutGroup;

        protected void Awake()
        {
            _gridLayoutGroup = GetComponent<GridLayoutGroup>();
            _container = GetComponent<RectTransform>();
        }

        protected void Update()
        {
            UpdateGrid();
        }

        private void UpdateGrid()
        {
            Vector2 containerSize = _container.rect.size;

            float cellWidth = (containerSize.x - (_spacing.x * (_columnsCount - 1))) / _columnsCount;
            float cellHeight = (containerSize.y - (_spacing.y * (_columnsCount - 1))) / _columnsCount;

            _gridLayoutGroup.cellSize = new Vector2(cellWidth, cellHeight);
            _gridLayoutGroup.spacing = _spacing;
        }

    }
}
