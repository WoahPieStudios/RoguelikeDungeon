using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace Game.CharactersEditor
{
    public static class Select
    {
        static List<object> _SelectionList = new List<object>();

        public static object[] selection => _SelectionList.ToArray();

        public static event Action<object[]> onAddSelection;
        public static event Action<object[]> onRemoveSelection;

        public static bool Contains(object selection)
        {
            return _SelectionList.Contains(selection);
        }

        public static void AddSelection(object selection)
        {
            if(!Contains(selection))
            {
                _SelectionList.Add(selection);

                onAddSelection?.Invoke(new object[] { selection });
            }
        }

        public static void AddSelection(object[] selections)
        {
            selections = selections.Where(select => !Contains(select)).ToArray();

            if(selections.Length > 0)
            {
                _SelectionList.AddRange(selections); 

                onAddSelection?.Invoke(selections);
            }
        }

        public static void RemoveSelection(object selection)
        {
            if(Contains(selection))
            {
                _SelectionList.Remove(selection);

                onRemoveSelection?.Invoke(new object[] { selection });
            }
        }

        public static void RemoveSelection(object[] selections)
        {
            selections = selections.Where(select => Contains(select)).ToArray();

            if(selections.Length > 0)
            {
                _SelectionList = _SelectionList.Except(selections).ToList(); 

                onRemoveSelection?.Invoke(selections);
            }
        }

        public static void RemoveAllSelection()
        {
            onRemoveSelection?.Invoke(selection);

            _SelectionList.Clear();
        }
    }
}