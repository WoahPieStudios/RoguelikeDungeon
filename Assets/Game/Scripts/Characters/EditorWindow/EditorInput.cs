using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.CharactersEditor
{
    public static class EditorInput
    {
        private static List<KeyCode> _KeysPressedList = new List<KeyCode>();

        public static Vector2 mousePosition => Event.current.mousePosition;
        public static Vector2 deltaMousePostion => Event.current.delta;

        public static bool isShiftPressed => Event.current.shift;
        public static bool isCapsLockToggled => Event.current.capsLock;

        public static int consecutiveClickCount => Event.current.clickCount;

        private static void AddKeyPress(KeyCode key)
        {
            if(!_KeysPressedList.Contains(key))
                _KeysPressedList.Add(key);
        }

        private static void RemoveKeyPress(KeyCode key)
        {
            if(_KeysPressedList.Contains(key))
                _KeysPressedList.Remove(key);
        }
        
        public static bool GetMouseButtonDown(int button)
        {
            Event currentEvent = Event.current;

            return currentEvent.button == button && currentEvent.type == EventType.MouseDown;
        }
        
        public static bool GetMouseButtonUp(int button)
        {
            Event currentEvent = Event.current;

            return currentEvent.button == button && currentEvent.type == EventType.MouseUp;
        }
        
        public static bool GetMouseButtonDrag(int button)
        {
            Event currentEvent = Event.current;

            return currentEvent.button == button && currentEvent.type == EventType.MouseDrag;
        }

        public static bool GetKeyDown(KeyCode key)
        {
            Event currentEvent = Event.current;

            bool state = currentEvent.keyCode == key && currentEvent.type == EventType.KeyDown;

            if(state)
                AddKeyPress(key);

            return state;
        }

        public static bool GetKeyUp(KeyCode key)
        {
            Event currentEvent = Event.current;

            bool state = currentEvent.keyCode == key && currentEvent.type == EventType.KeyUp;

            if(state)
                RemoveKeyPress(key);

            return state;
        }

        /// <summary>
        /// Not Sure if this works
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool GetKey(KeyCode key)
        {
            Event currentEvent = Event.current;

            if(currentEvent.keyCode == key)
            {
                switch(currentEvent.type)
                {
                    case EventType.KeyUp:
                        RemoveKeyPress(key);
                        break;

                    case EventType.KeyDown:
                        AddKeyPress(key);
                        break;
                }
            }

            return _KeysPressedList.Contains(key);
        }
    }
}