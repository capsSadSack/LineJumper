using Assets.Scripts.NewGameScripts.Enemy.EnemyDecorators;
using Assets.Scripts.NewGameScripts.Enemy.EnemyDecorators.Decorators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                        AEnemy aggressiveFollowingEnemy = new AlwaysAggressiveEnemyDecorator(followingPlayerEnemy);
                        return aggressiveFollowingEnemy;
                    }
                case Enemy.Immortal:
                    {
                        // TODO: [CG, 2020.06.30] Заглушка - не реализован "бессмертный" враг
                        return new SimpleEnemy();
                    }
                default:
                    {
                        return new SimpleEnemy();
                    }
            }
        }
    }
}
