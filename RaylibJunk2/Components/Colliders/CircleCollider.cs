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
                        OnCollisionEnter(circle, Direction.UP, 0, new Vector2(0,0));
                    return true;
                }
            }


            //Cicle collider to AABB
            else if (other.type == Constants.ColliderType.BOX)
            {
                BoxCollider box = other as BoxCollider;
                //var circleDistanceX = Math.Abs(parent.transform.LocalPosition.X - box.parent.transform.LocalPosition.X);
                //var circleDistanceY = Math.Abs(parent.transform.LocalPosition.Y - box.parent.transform.LocalPosition.Y);

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

                //Get Centre point of circle 
                //Get Centre point of box
                //Get Diffrence between both centers
                //Clamp the difference between -half extents, half extents
                //Retrieve & Return the vector between centre circle & closest Point 
                //If length < radius collision!

                //    Vector2 direction = parent.transform.LocalPosition - box.parent.transform.LocalPosition;

                //    Vector2 clampDifference = Vector2.Clamp(direction, box.scale / -2, box.scale / 2);
                //    Vector2 closestPoint = box.parent.position + clampDifference;

                //    if (circleDistanceX > box.scale.X / 2 + radius) 
                //    { 
                //        return false; 
                //    }
                //    if (circleDistanceY > box.scale.Y / 2 + radius) 
                //    { 
                //        return false; 
                //    }

                //    if (circleDistanceX <= box.scale.X / 2) 
                //    {
                //        if (!overlaps.Contains(box))
                //            overlaps.Add(box);
                //        if (isTrigger)
                //            OnTriggerEnter(box);
                //        else

                //        return true; 
                //    }
                //    if (circleDistanceY <= box.scale.Y / 2)
                //    {
                //        if (!overlaps.Contains(box))
                //            overlaps.Add(box);
                //        if (isTrigger)
                //            OnTriggerEnter(box);
                //        else
                //            OnCollisionEnter(box, new Vector2(circleDistanceX, circleDistanceY).Length());
                //        return true;
                //    }

                //    var cornerDistance_sq = (circleDistanceX - box.scale.X / 2) * (circleDistanceX - box.scale.X / 2) +
                //                         (circleDistanceY - box.scale.Y / 2) * (circleDistanceY - box.scale.Y / 2);

                //    bool colliding = cornerDistance_sq <= radius * radius;
                //    if (colliding)
                //    {
                //        if (!overlaps.Contains(box))
                //            overlaps.Add(box);
                //        if (isTrigger)
                //            OnTriggerEnter(box);
                //        else
                //            OnCollisionEnter(box, new Vector2(circleDistanceX, circleDistanceY).Length());
                //        return true;
                //    }
                //}

                //return false;
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
