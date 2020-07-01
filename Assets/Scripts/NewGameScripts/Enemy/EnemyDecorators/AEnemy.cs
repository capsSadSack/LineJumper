using UnityEngine;

namespace Assets.Scripts.NewGameScripts.Enemy.EnemyDecorators
{
    public abstract class AEnemy
    {
        //protected abstract IAggressionController aggressionController { get; }


        public abstract Vector2 GetJumpDirection(Vector2 currentPosition);

        public abstract float GetVelocityMagnitude();

        public abstract bool GetAggression();

        public abstract bool IsGoingToJump();
        //{
        //    Aggression aggression = aggressionController.GetAggression();
        //    return aggression == Aggression.Aggressive;
        //}
    }
}
