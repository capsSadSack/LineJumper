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
            Vector2 verticalVector = new Vector2(0, 1);

            if (Vector2.Angle(verticalVector, currentPosition) > 10)
            {
                var playerPos = new Vector2(player.transform.position.x, player.transform.position.y);
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
