using UnityEngine;
using static System.Math;

namespace Assets.Assets.Scripts
{
    public abstract class Item
    {
        public Vector3 Position { get; protected set; }
        public ItemState State { get; protected set; }

        protected GameObject gameObject;
        protected Vector3 jumpDirection;
        protected float jumpVelcoity = 15.0f;

        public Item(GameObject gameObject, float x, float y)
        {
            Position = new Vector3(x, y, 0);
            State = ItemState.Run;
            this.gameObject = gameObject;
        }

        public void StartMoving(Vector3 direction)
        {
            State = ItemState.Jump;
            jumpDirection = direction;
        }

        public virtual void Update(float deltaTime)
        {
            if (State == ItemState.Jump)
            {
                Position += jumpDirection * deltaTime * jumpVelcoity;
                if (Abs(Position.x) >= Sizes.EDGE_X)
                {
                    EndJump();
                }

                gameObject.transform.position = Position;
            }
        }

        protected virtual void EndJump()
        {
            State = ItemState.Run;
            Position = new Vector3(Sign(Position.x) * Sizes.EDGE_X, Position.y, Position.z);
        }
    }
}
