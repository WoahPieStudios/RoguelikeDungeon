using UnityEngine.Rendering;

namespace Game.Characters
{
    public class Hero : Character
    {
        private SortingGroup _sortingGroup;

        protected override void Awake()
        {
            base.Awake();
            _sortingGroup = GetComponent<SortingGroup>();
        }

        public void ChangeSortingOrder(int order)
        {
            _sortingGroup.sortingOrder = order;
        }
        
    }
}
