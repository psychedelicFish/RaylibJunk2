using RaylibJunk2.Components.Physics;
using RaylibJunk2.GameObjects;
using System.Numerics;

namespace RaylibJunk2.Colliders
{
    internal class BoxCollider : Collider
    {
        public Vector2 scale { get; private set; }


        public BoxCollider(GameObject parent, Vector2 scale, int id, bool isTrigger = false, Rigidbody? connected = null) : base(parent, id, isTrigger, connected)
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
            //Box V Circle Collision
            //https://www.geeksforgeeks.org/check-if-any-point-overlaps-the-given-circle-and-rectangle/
            if (other.type == Constants.ColliderType.CIRCLE)
            {
                CircleCollider circle = other as CircleCollider;
                var circleDistanceX = Math.Abs(circle.parent.transform.LocalPosition.X - parent.transform.LocalPosition.X - scale.X / 2);
                var circleDistanceY = Math.Abs(circle.parent.transform.LocalPosition.Y - parent.transform.LocalPosition.Y - scale.Y / 2);

                if (circleDistanceX > scale.X / 2 + circle.radius) { return false; }
                if (circleDistanceY > scale.Y / 2 + circle.radius) { return false; }

                if (circleDistanceX <= scale.X / 2) { return true; }
                if (circleDistanceY <= scale.Y / 2) { return true; }

                var cornerDistance_sq = (circleDistanceX - scale.X / 2) * (circleDistanceX - scale.X / 2) +
                                        (circleDistanceY - scale.Y / 2) * (circleDistanceY - scale.Y / 2);

                return cornerDistance_sq <= circle.radius * circle.radius;
            }
            //https://www.geeksforgeeks.org/find-two-rectangles-overlap/
            //BOX V BOX Collision
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
            Raylib_cs.Rectangle rect = new Raylib_cs.Rectangle(parent.position.X, parent.position.Y, scale.X, scale.Y); 
            Raylib_cs.Raylib.DrawRectanglePro(rect, new Vector2(0,0), 0, Raylib_cs.Color.GREEN);
            
            
        }
        
    }

}
