using Assets.Assets.Scripts.Difficulties;
using Assets.Scripts.NewGameScripts.Enemy.AggressionChanging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.NewGameScripts.Enemy.EnemyDecorators
{
    public class SimpleEnemy : AEnemy
    {
        public override Enemy EnemyType => Enemy.Simple;

        private float maxVelocity;
        private float minVelocity;

        private Aggression currentAggression = Aggression.Aggressive;
        private IAggressionController aggressionController = new ChangingAggressionController();


        public SimpleEnemy()
        {
            PlayerPrefsDifficultyAccess difficultyAccess = new PlayerPrefsDifficultyAccess();
            var difficulty = difficultyAccess.GetDifficulty();

            DifficultySettings difficultySettings = DifficultiesSettingsStorage.Settings[difficulty];

            maxVelocity = difficultySettings.MaxVelocity;
            minVelocity = difficultySettings.MinVelocity;
        }


        public override bool GetAggression()
        {
            currentAggression = aggressionController.GetAggression();
            return currentAggression == Aggression.Aggressive;
        }

        public override Vector2 GetJumpDirection(Vector2 currentPosition)
        {
            float topLimitY = GetTopLimitY(currentPosition);
            float bottomLimitY = GetBottomLimitY(currentPosition);

            float angleFromVertical_Degree = UnityEngine.Random.Range(topLimitY, bottomLimitY);
            float angleFromVertical_Radian = angleFromVertical_Degree * Mathf.Deg2Rad;
            float x = Mathf.Sin(angleFromVertical_Radian);
            float y = Mathf.Cos(angleFromVertical_Radian);

            if (currentPosition.x > 0)
            {
                x *= -1;
            }

            return new Vector2(x, y);
        }

        private float GetTopLimitY(Vector2 currentPosition)
        {
            float x1 = 0;
            float y1 = 50;

            float x2 = 4;
            float y2 = 90;

            if (currentPosition.y > x2)
            {
                return y2;
            }
            else if (currentPosition.y < x1)
            {
                return y1;
            }
            else
            {
                float topLimitY = y1 + (y1 - y2) / (x1 - x2) * currentPosition.y;
                return topLimitY;
            }
        }

        private float GetBottomLimitY(Vector2 currentPosition)
        {
            float x1 = 0;
            float y1 = 130;

            float x2 = -4;
            float y2 = 90;

            if (currentPosition.y < x2)
            {
                return y2;
            }
            else if (currentPosition.y > x1)
            {
                return y1;
            }
            else
            {
                float bottomLimitY = y1 + (y1 - y2) / (x1 - x2) * currentPosition.y;
                return bottomLimitY;
            }
        }

        public override float GetVelocityMagnitude()
        {
            return UnityEngine.Random.Range(minVelocity, maxVelocity);
        }

        public override bool IsGoingToJump()
        {
            return UnityEngine.Random.value > 0.66;
        }
    }
}
