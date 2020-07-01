using UnityEngine;

namespace Assets.Scripts.NewGameScripts.Enemy.EnemyDecorators
{
    public class AEnemyDecorator : AEnemy
    {
        protected AEnemy baseEnemy;

        public AEnemyDecorator(AEnemy enemy)
        {
            baseEnemy = enemy;
        }

        public override Vector2 GetJumpDirection(Vector2 currentPosition)
        {
            return baseEnemy.GetJumpDirection(currentPosition);
        }

        public override float GetVelocityMagnitude()
        {
            return baseEnemy.GetVelocityMagnitude();
        }

        public override bool GetAggression()
        {
            return baseEnemy.GetAggression();
        }

        public override bool IsGoingToJump()
        {
            return baseEnemy.IsGoingToJump();
        }
    }
}
