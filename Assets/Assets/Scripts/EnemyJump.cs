using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Assets.Scripts
{
    public struct EnemyJump
    {
        public bool IsAgressive;
        public Vector3 Destination;
        public float TimeToJump_Sec;
        public float JumpVelocity;
    }
}
