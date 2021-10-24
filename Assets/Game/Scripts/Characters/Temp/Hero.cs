using System;
using UnityEngine;
using UnityEngine.Rendering;

namespace Game.Characters.Temp
{
    public class Hero : Character
    {
        [Header("Hero Class", order = 0)]
        
        [Header("Settings", order = 1)]
        public bool isPlayerControlled;
        
        [Header("Movement")]
        [SerializeField] private float followSpeed;
        private float baseSpeed;
        
        [Header("Rendering")]
        private SortingGroup _sortingGroup;

        protected override void Awake()
        {
            base.Awake();
            _sortingGroup = GetComponent<SortingGroup>();
        }

        private void Start()
        {
            baseSpeed = movementSpeed;
        }

        private void Update()
        {
            movementSpeed = isPlayerControlled ? baseSpeed : followSpeed;
        }

        public void ChangeSortingOrder(int order)
        {
            _sortingGroup.sortingOrder = order;
        }
    }
}
