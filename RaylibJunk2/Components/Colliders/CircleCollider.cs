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
        public CircleCollider(float radius, GameObject parent, int id, bool isTrigger = false) : base(parent, id, isTrigger)
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
                    if(isTrigger)
                        OnTriggerEnter(circle);
                    else
                        OnCollisionEnter(circle);
                    return true;
                }
            }


            //Cicle collider to AABB
            else if (other.type == Constants.ColliderType.BOX)
            {
                BoxCollider box = other as BoxCollider;
                var circleDistanceX = Math.Abs(parent.transform.LocalPosition.X - box.parent.transform.LocalPosition.X);
                var circleDistanceY = Math.Abs(parent.transform.LocalPosition.Y - box.parent.transform.LocalPosition.Y);

                if (circleDistanceX > box.scale.X / 2 + radius) 
                { 
                    return false; 
                }
                if (circleDistanceY > box.scale.Y / 2 + radius) 
                { 
                    return false; 
                }

                if (circleDistanceX <= box.scale.X / 2) 
                {
                    if (!overlaps.Contains(box))
                        overlaps.Add(box);
                    if (isTrigger)
                        OnTriggerEnter(box);
                    else
                        OnCollisionEnter(box);
                    return true; 
                }
                if (circleDistanceY <= box.scale.Y / 2)
                {
                    if (!overlaps.Contains(box))
                        overlaps.Add(box);
                    if (isTrigger)
                        OnTriggerEnter(box);
                    else
                        OnCollisionEnter(box);
                    return true;
                }

                var cornerDistance_sq = (circleDistanceX - box.scale.X / 2) * (circleDistanceX - box.scale.X / 2) +
                                     (circleDistanceY - box.scale.Y / 2) * (circleDistanceY - box.scale.Y / 2);

                bool colliding = cornerDistance_sq <= radius * radius;
                if (colliding)
                {
                    if (!overlaps.Contains(box))
                        overlaps.Add(box);
                    if (isTrigger)
                        OnTriggerEnter(box);
                    else
                        OnCollisionEnter(box);
                    return true;
                }
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
                var circleDistanceX = Math.Abs(parent.transform.LocalPosition.X - box.parent.transform.LocalPosition.X);
                var circleDistanceY = Math.Abs(parent.transform.LocalPosition.Y - box.parent.transform.LocalPosition.Y);

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
