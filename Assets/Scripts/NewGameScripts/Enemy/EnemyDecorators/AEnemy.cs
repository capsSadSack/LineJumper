using System.Xml.Serialization;
using UnityEngine;

namespace Assets.Scripts.NewGameScripts.Enemy.EnemyDecorators
{
    public abstract class AEnemy
    {
        public abstract Enemy EnemyType { get; }

        public abstract Vector2 GetJumpDirection(Vector2 currentPosition);

        public abstract float GetVelocityMagnitude();

        public abstract bool GetAggression();

        public abstract bool IsGoingToJump();
    }
}
