using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.CharactersEditor
{
    public static class EditorInput
    {
        public static Vector2 mousePosition => Event.current.mousePosition;
        
        public static bool MouseDown(int button)
        {
            Event currentEvent = Event.current;

            return currentEvent.button == button && currentEvent.type == EventType.MouseDown;
        }
        
        public static bool MouseUp(int button)
        {
            Event currentEvent = Event.current;

            return currentEvent.button == button && currentEvent.type == EventType.MouseUp;
        }
        
        public static bool MouseDrag(int button)
        {
            Event currentEvent = Event.current;

            return currentEvent.button == button && currentEvent.type == EventType.MouseDrag;
        }
    }
}