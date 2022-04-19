using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Game.Characters;

namespace Game.Parties
{
    public class Party : MonoBehaviour
    {
        Hero _CurrentHero;

        List<Hero> _Heroes;

        public Hero currentHero => _CurrentHero;
        public Hero[] heroes => _Heroes.ToArray();

        public event Action<Hero> onSwitchCurrentHeroEvent;
        public event Action<Hero> onAddHeroEvent;
        public event Action<Hero> onRemoveHeroEvent;

        public void AddHero(Hero hero)
        {
            if(_Heroes.Contains(hero))
                return;

            _Heroes.Add(hero);

            onAddHeroEvent?.Invoke(hero);
        }

        public void RemoveHero(Hero hero)
        {
            if(!_Heroes.Contains(hero))
                return;

            _Heroes.Remove(hero);

            onRemoveHeroEvent?.Invoke(hero);
        }

        public void SwitchCurrentHero(int index)
        {
            if(index >= _Heroes.Count && index < 0)
                return;

            _CurrentHero = _Heroes[index];

            onSwitchCurrentHeroEvent?.Invoke(_CurrentHero);
        }
    }
}