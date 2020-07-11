using Assets.Scripts.NewGameScripts.Enemy.EnemyDecorators;
using Assets.Scripts.NewGameScripts.Enemy.EnemyDecorators.Decorators;
using UnityEngine;

namespace Assets.Scripts.NewGameScripts.Enemy
{
    public class EnemyFabric : MonoBehaviour
    {
        public GameObject player;


        public EnemyFabric(GameObject player)
        {
            this.player = player;
        }


        public AEnemy CreateEnemy(Enemy enemy)
        {
            switch (enemy)
            {
                case Enemy.Simple:
                    {
                        return new SimpleEnemy();
                    }
                case Enemy.Follower:
                    {
                        AEnemy simpleEnemy = new SimpleEnemy();
                        AEnemy followingPlayerEnemy = new FollowingPlayerEnemyDecorator(simpleEnemy, player);
                        AEnemy alwaysJumpingEnemy = new AlwaysJumpingEnemyDecorator(followingPlayerEnemy);
                        AEnemy aggressiveFollowingEnemy = new AlwaysAggressiveEnemyDecorator(alwaysJumpingEnemy);
                        return aggressiveFollowingEnemy;
                    }
                case Enemy.Immortal:
                    {
                        AEnemy simpleEnemy = new SimpleEnemy();
                        AEnemy alwaysJumpingEnemy = new AlwaysJumpingEnemyDecorator(simpleEnemy);
                        AEnemy aggressiveEnemy = new AlwaysAggressiveEnemyDecorator(alwaysJumpingEnemy);
                        AEnemy immortalEnemy = new ImmortalEnemyDecorator(aggressiveEnemy);

                        return immortalEnemy;
                    }
                default:
                    {
                        return new SimpleEnemy();
                    }
            }
        }
    }
}
