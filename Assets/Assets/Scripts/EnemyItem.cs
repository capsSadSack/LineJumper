using System;
using UnityEngine;

namespace Assets.Assets.Scripts
{
    public class EnemyItem : Item
    {
        private bool _isAgressive;
        public bool IsAgressive
        {
            get { return _isAgressive; }
            set
            {
                _isAgressive = value;
                Color color = _isAgressive ? Color.red : Color.green;
                gameObject.GetComponentInChildren<SpriteRenderer>().color = color;
            }
        }
        public event EventHandler<System.Random> OnEndJump;

        private float timeToJump_Sec = 1.0f;
        private EnemyJump nextJump;

        public EnemyItem(GameObject gameObject, float x, float y)
            : base(gameObject, x, y)
        {
            IsAgressive = true;
        }

        public override void Update(float deltaTime)
        {
            timeToJump_Sec -= deltaTime;

            if (timeToJump_Sec <= 0 && State == ItemState.Run)
            {
                Jump();
            }

            base.Update(deltaTime);
        }

        private void Jump()
        {
            IsAgressive = nextJump.IsAgressive;
            jumpVelcoity = nextJump.JumpVelocity;

            Vector3 direction = nextJump.Destination - Position;
            direction.z = 0;
            direction.Normalize();

            StartMoving(direction);
        }

        public void SetNextJump(EnemyJump enemyJump)
        {
            timeToJump_Sec = enemyJump.TimeToJump_Sec;
            nextJump = enemyJump;
        }

        protected override void EndJump()
        {
            base.EndJump();

            OnEndJump?.Invoke(this, null);

            IsAgressive = true;
        }

        public void Destroy()
        {
            GameObject.Destroy(gameObject);
        }
    }
}
