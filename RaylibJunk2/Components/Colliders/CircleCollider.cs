using Constants;
using RaylibJunk2.Components.Physics;
using RaylibJunk2.GameObjects;
using System.Numerics;

namespace RaylibJunk2.Colliders
{
    internal class CircleCollider : Collider
    {
        public float radius;
        public CircleCollider(GameObject parent, int id) : base(parent, id)
        {
            type = Constants.ColliderType.CIRCLE;
        }
        public CircleCollider(float radius, GameObject parent, int id, bool isTrigger = false, Rigidbody? connected = null) : base(parent, id, isTrigger, connected)
        {
            this.radius = radius;
            
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


        //Circle to circle collision
        //https://codeguppy.com/blog/how-to-implement-collision-detection-between-two-circles-using-p5.js/index.html#:~:text=If%20the%20distance%20is%20greater,then%20the%20circles%20are%20colliding.
        //https://www.geeksforgeeks.org/check-two-given-circles-touch-intersect/
            if (other.type == Constants.ColliderType.CIRCLE)
            {
                var circle = other as CircleCollider;
                if (Vector2.Distance(circle.parent.transform.LocalPosition, parent.transform.LocalPosition) < radius + circle.radius)
                {
                    if (!overlaps.Contains(circle))
                        overlaps.Add(circle);
                    if (isTrigger)
                        OnTriggerEnter(circle);
                    else
                    {
                        Vector2 direction = Vector2.Normalize(parent.position - circle.parent.position);
                        float distance = (parent.position - circle.parent.position).Length();
                        float radiusCombined = radius + circle.radius;
                        float depth = radiusCombined - distance;


                        OnCollisionEnter(circle, Direction.NONE, depth, direction * depth);
                    }
                    return true;
                }
            }


            //Cicle collider to AABB
            //https://www.geeksforgeeks.org/check-if-any-point-overlaps-the-given-circle-and-rectangle/
            else if (other.type == Constants.ColliderType.BOX)
            {
                BoxCollider box = other as BoxCollider;


                Vector2 ballCentre = parent.transform.LocalPosition;// - new Vector2(radius);
                
                Vector2 boxHalfExtents = new Vector2(box.scale.X / 2, box.scale.Y / 2);
                Vector2 boxCentre = new Vector2(box.parent.transform.LocalPosition.X + boxHalfExtents.X,
                        box.parent.transform.LocalPosition.Y + boxHalfExtents.Y);

                Vector2 difference = ballCentre - boxCentre;
                Vector2 clamped = Vector2.Clamp(difference, -boxHalfExtents, boxHalfExtents);
                Vector2 closest = boxCentre + clamped;

                difference = closest - ballCentre;

                if (difference.Length() < radius)
                {
                    Direction d = VectorDirection(difference);
                    float pen = 0;
                    if (d == Direction.LEFT || d == Direction.RIGHT)
                    {
                        pen = radius - Math.Abs(difference.X);
                    }
                    else
                    {
                        pen = radius - Math.Abs(difference.Y);
                    }

                    OnCollisionEnter(box, d, pen, closest);
                    return true;
                }
                return false;
            
            }
            return false;
        }

        public override bool CheckStillColliding(Collider other)
        {

            if (other.type == Constants.ColliderType.CIRCLE)
            {
                var circle = other as CircleCollider;
                if (Vector2.Distance(circle.parent.transform.LocalPosition, parent.transform.LocalPosition) < radius + circle.radius)
                {
                    return true;
                }
            }


            //Cicle collider to AABB
            else if (other.type == Constants.ColliderType.BOX)
            {
                BoxCollider box = other as BoxCollider;
                var circleDistanceX = Math.Abs(parent.transform.LocalPosition.X - box.parent.transform.LocalPosition.X - box.scale.X);
                var circleDistanceY = Math.Abs(parent.transform.LocalPosition.Y - box.parent.transform.LocalPosition.Y - box.scale.Y);

                


                if (circleDistanceX > box.scale.X / 2 + radius) { return false; }
                if (circleDistanceY > box.scale.Y / 2 + radius) { return false; }

                if (circleDistanceX <= box.scale.X / 2) { return true; }
                if (circleDistanceY <= box.scale.Y / 2) { return true; }

                var cornerDistance_sq = (circleDistanceX - box.scale.X / 2) * (circleDistanceX - box.scale.X / 2) +
                                     (circleDistanceY - box.scale.Y / 2) * (circleDistanceY - box.scale.Y / 2);

                return cornerDistance_sq <= radius * radius;
            }

            return false;
            //if no longer colliding remove from list 
            //call exit 
        }

        public override void Draw()
        {
            Raylib_cs.Raylib.DrawCircleV(parent.position, radius, Raylib_cs.Color.BLUE);
        }

    }
}
