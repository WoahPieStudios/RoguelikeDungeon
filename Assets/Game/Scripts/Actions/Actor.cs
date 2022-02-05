using System.Linq;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace Game.Actions
{
    public class Actor : MonoBehaviour, IActor
    {
        List<IActorProperty> _ActorPropertyList = new List<IActorProperty>();

        protected virtual void Awake()
        {
            AddProperty(GetComponentsInChildren<IActorProperty>());
        }
        
        protected void AddProperty(params IActorProperty[] actorProperties)
        {
            _ActorPropertyList.AddRange(actorProperties.Where(a => !_ActorPropertyList.Contains(a)));
        }

        protected void RemoveProperty(params IActorProperty[] actorProperties)
        {
            _ActorPropertyList.RemoveAll(a => actorProperties.Contains(a));
        }

        public T GetProperty<T>() where T : IActorProperty
        {
            return (T)_ActorPropertyList.FirstOrDefault(g => g is T);
        }

        public T[] GetProperties<T>() where T : IActorProperty
        {
            return _ActorPropertyList.Where(g => g is T).Cast<T>().ToArray();
        }
    }
}