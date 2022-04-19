using System.Linq;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Game.Animations;
using Game.Properties;


namespace Game.Actions
{
    [RequireComponent(typeof(AnimationHandler))]
    public abstract class Action : MonoBehaviour, IAction
    {
        bool _IsActive = false;

        AnimationHandler _AnimationHandler;

        protected List<IProperty> propertyList => new List<IProperty>();

        public bool isActive => _IsActive;
        public AnimationHandler animationHandler => _AnimationHandler;

        public IProperty[] properties => propertyList.ToArray();

        protected virtual void Awake()
        {
            _AnimationHandler = GetComponent<AnimationHandler>();
        }

        protected virtual void OnUse()
        {
            _IsActive = true;
        }

        protected virtual bool CanUse()
        {
            return !_IsActive;
        }

        public void Use()
        {
            if(CanUse())
                OnUse();
        }

        public virtual void End()
        {
            _IsActive = false;
        }

        public bool ContainsProperty(string property)
        {
            return propertyList.Any(p => p.name == property);
        }

        public IProperty GetProperty(string property)
        {
            return propertyList.FirstOrDefault(p => p.name == property);
        }
    }
}