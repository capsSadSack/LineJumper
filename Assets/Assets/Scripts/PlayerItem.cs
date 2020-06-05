using UnityEngine;
using static System.Math;

namespace Assets.Assets.Scripts
{
    public class PlayerItem : Item
    {
        public PlayerItem(GameObject gameObject, float x, float y)
            : base(gameObject, x, y)
        { }

        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);

            if (Abs(Position.y) >= Sizes.EDGE_Y)
            {
                State = ItemState.Fall;
            }
        }
    }
}
