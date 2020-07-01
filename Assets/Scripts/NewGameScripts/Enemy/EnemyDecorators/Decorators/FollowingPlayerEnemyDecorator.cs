using System;
using UnityEngine;

namespace Assets.Scripts.NewGameScripts.Enemy.EnemyDecorators.Decorators
{
    public class FollowingPlayerEnemyDecorator : AEnemyDecorator
    {
        private GameObject player;


        public FollowingPlayerEnemyDecorator(AEnemy enemy, GameObject player)
            : base(enemy)
        {
            this.player = player;
        }


        public override Vector2 GetJumpDirection(Vector2 currentPosition)
        {
            var playerPos = new Vector2(player.transform.position.x, player.transform.position.y);
            var angle = Math.Abs(Vector2.Angle(playerPos, currentPosition) % 180);
            if (angle > 10 && angle < 80 || angle > 110 && angle < 170)
            {
                var fromEnemyToPlayerVector = playerPos - currentPosition;
                fromEnemyToPlayerVector.Normalize();

                return fromEnemyToPlayerVector;
            }
            else
            {
                return base.GetJumpDirection(currentPosition);
            }
        }
    }
}
