using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Characters
{
    [CreateAssetMenu(menuName = "Data/Test")]
    public class Test : Attack<Enemy>
    {
        public override bool CanUse()
        {
            throw new System.NotImplementedException();
        }

        public override void Tick()
        {
            throw new System.NotImplementedException();
        }
    }
}