using RaylibJunk2.GameObjects;
using System.Numerics;

namespace RaylibJunk2.Colliders
{
    internal class BoxCollider : Collider
    {
        public Vector2 scale { get; private set; }


        public BoxCollider(GameObject parent, Vector2 scale, int id) : base(parent, id)
        {
            this.scale = scale;
            type = Constants.ColliderType.BOX;
        }

        public override bool CheckForCollisions(Collider other)
        {
            for (int i = 0; i < overlaps.Count; i++)
            {
                if (overlaps[i].id == other.id)
                {
                    return true;
                }
            }

            if (other.type == Constants.ColliderType.CIRCLE)
            {
                CircleCollider circle = other as CircleCollider;
                var circleDistanceX = Math.Abs(circle.parent.transform.LocalPosition.X - parent.transform.LocalPosition.X);
                var circleDistanceY = Math.Abs(circle.parent.transform.LocalPosition.Y - parent.transform.LocalPosition.Y);

                if (circleDistanceX > scale.X / 2 + circle.radius) { return false; }
                if (circleDistanceY > scale.Y / 2 + circle.radius) { return false; }

                if (circleDistanceX <= scale.X / 2) { return true; }
                if (circleDistanceY <= scale.Y / 2) { return true; }

                var cornerDistance_sq = (circleDistanceX - scale.X / 2) * (circleDistanceX - scale.X / 2) +
                                        (circleDistanceY - scale.Y / 2) * (circleDistanceY - scale.Y / 2);

                return cornerDistance_sq <= circle.radius * circle.radius;
            }

            else if (other.type == Constants.ColliderType.BOX)
            {
                BoxCollider box = other as BoxCollider;

                return parent.transform.LocalPosition.X + scale.X > box.parent.transform.LocalPosition.X &&
                        parent.transform.LocalPosition.X < box.parent.transform.LocalPosition.X + scale.X &&
                        parent.transform.LocalPosition.Y + scale.Y > box.parent.transform.LocalPosition.Y &&
                        parent.transform.LocalPosition.Y < box.parent.transform.LocalPosition.Y + scale.Y;
            }


            return false;
        }
        
        public override void Draw()
        {
            Raylib_cs.Raylib.DrawRectangleV(parent.position, scale, Raylib_cs.Color.BLUE);
            
        }
        
    }

}
