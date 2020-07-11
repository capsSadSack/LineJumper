using System;
using UnityEngine;

namespace Assets.Scripts.NewGameScripts.Enemy.EnemyDecorators.Decorators
{
    public class FollowingPlayerEnemyDecorator : AEnemyDecorator
    {
        public override Enemy EnemyType => Enemy.Follower;

        private GameObject player;


        public FollowingPlayerEnemyDecorator(AEnemy enemy, GameObject player)
            : base(enemy)
        {
            this.player = player;
        }


        public override Vector2 GetJumpDirection(Vector2 currentPosition)
        {
            var playerPos = new Vector2(player.transform.position.x, player.transform.position.y);
            var fromEnemyToPlayerVector = playerPos - currentPosition;
            fromEnemyToPlayerVector.Normalize();

            var horizontalOrt = new Vector2(1, 0);
            var angle = Math.Abs(Vector2.Angle(fromEnemyToPlayerVector, horizontalOrt) % 180);
            if (angle > 10 && angle < 80)
            {
                return fromEnemyToPlayerVector;
            }
            else
            {
                return base.GetJumpDirection(currentPosition);
            }
        }
    }
}
