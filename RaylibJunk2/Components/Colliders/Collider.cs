
using Constants;
using RaylibJunk2.Components;
using RaylibJunk2.Components.Physics;
using RaylibJunk2.GameObjects;
using RaylibJunk2.Managers;
using System.Numerics;

namespace RaylibJunk2.Colliders
{
    internal class Collider : Component
    {
        public ColliderType type { get; protected set; }
        public bool isTrigger { get; protected set; }
        bool colliding = false;
        public Rigidbody? connectedRigidbody;
        public int id { get; private set; }
        
        protected List<Collider> overlaps = new List<Collider>();
        

        public delegate void TriggerCallback(Collider other);
        TriggerCallback? Enter, Exit, Stay;

        public delegate void CollisionCallback(Collider other, Direction direction, float penetration, Vector2 hitLocation);
        CollisionCallback? cEnter, cExit, cStay;

        public Collider(GameObject parent, int id, bool isTrigger = false, Rigidbody? connected = null) : base(parent)
        {
            this.id = id;
            this.isTrigger = isTrigger;
            connectedRigidbody = connected;
            

            if (connectedRigidbody != null && !connectedRigidbody.isKinematic)
            {
                SubscribeCollisionEnter(ResolveCollsion);
            }
        }

        public virtual bool CheckForCollisions(Collider other)
        {
            return false;
            //if colliding and not already in list  
            //call enter
        }

        public virtual bool CheckStillColliding(Collider other)
        {
            return false;
            //if no longer colliding remove from list 
            //call exit 
        }



        public override void Update(float deltaTime)
        {

            //CheckForCollisions();
            for (int i = 0; i < overlaps.Count; i++)
            {
                if (CheckStillColliding(overlaps[i]))
                {
                    if (isTrigger)
                        OnTriggerStay(overlaps[i]);
                }
                else
                {
                    if (isTrigger)
                    {
                        OnTriggerExit(overlaps[i]);
                    }
                    overlaps.Remove(overlaps[i]);
                }
            }

        }


        #region OnCollisions
        public void OnCollisionEnter(Collider other, Direction direction, float penetration, Vector2 hitLocation)
        {
            if (cEnter != null)
            {
                cEnter(other, direction, penetration, hitLocation);
            }
        }

        public void OnCollisionExit(Collider other, Direction direction, float penetration, Vector2 hitLocation)
        {
            if (cExit != null)
            {
                cExit(other, direction, penetration, hitLocation);
            }
        }

        #endregion



        #region OnTriggers
        public void OnTriggerEnter(Collider other)
        {
            if (Enter != null)
            {
                Enter(other);
            }
        }
        public void OnTriggerStay(Collider other)
        {
            if (Stay != null)
            {
                Stay(other);
            }
        }
        public void OnTriggerExit(Collider other)
        {
            if (Exit != null)
            {
                Exit(other);
            }
        }
        #endregion

        #region TriggerSubscribes
        public void SubscribeTriggerEnter(TriggerCallback callback)
        {
            Enter -= callback;
            Enter += callback;
        }

        public void SubscribeTriggerStay(TriggerCallback callback)
        {
            Stay -= callback;
            Stay += callback;
        }

        public void SubscribeTriggerExit(TriggerCallback callback)
        {
            Exit -= callback;
            Exit += callback;
        }

        public void UnSubscribeTriggerEnter(TriggerCallback callback)
        {
            Enter -= callback;
        }

        public void UnSubscribeTriggerStay(TriggerCallback callback)
        {
            Stay -= callback;
        }

        public void UnSubscribeTriggerExit(TriggerCallback callback)
        {
            Exit -= callback;
        }
        #endregion

        public void SubscribeCollisionEnter(CollisionCallback callback)
        {
            cEnter -= callback;
            cEnter += callback;
        }
        public void SubscribeCollisionExit(CollisionCallback callback)
        {
            cEnter -= callback;
            cEnter += callback;
        }

        public void UnSubscribeCollisionEnter(CollisionCallback callback)
        {
            cEnter -= callback;
           
        }
        public void UnSubscribeCollisionExit(CollisionCallback callback)
        {
            cEnter -= callback;
            
        }


        public void ResolveCollsion(Collider other, Direction direction, float penetration, Vector2 hitLocation)
        {
            if (connectedRigidbody != null && other.connectedRigidbody != null)
            {
                Console.WriteLine("Boing");

                 
                Vector2 normal = parent.transform.LocalPosition - hitLocation; 
                //float overlap = penetration;
                normal = Vector2.Normalize(normal);

                if(direction == Direction.LEFT)
                {
                    parent.transform.LocalPosition += new Vector2(penetration, 0);
                }
                else if(direction == Direction.RIGHT)
                {
                    parent.transform.LocalPosition -= new Vector2(penetration, 0);
                }
                else if(direction == Direction.UP)
                {
                    parent.transform.LocalPosition -= new Vector2(0, penetration);
                }
                else
                {
                    parent.transform.LocalPosition += new Vector2(0, penetration);
                }
                

                //if (!other.connectedRigidbody.isKinematic)
                //{
                //    other.parent.transform.LocalPosition += overlap * 0.5f * normal;
                //    parent.transform.LocalPosition -= overlap * 0.5f * normal;
                //}
                //else
                //{
                //    parent.transform.LocalPosition += overlap * normal;

                //}

                Vector2 relativeVelocity = connectedRigidbody.velocity - other.connectedRigidbody.velocity;
                //relativeVelocity = Vector2.Normalize(relativeVelocity);
                float velocityAlongNormal = Vector2.Dot(relativeVelocity, normal);
                connectedRigidbody.ResetVelocity();

                if (velocityAlongNormal < 0)
                {
                    Vector2 impulse = 0.7f * velocityAlongNormal * normal;
                    connectedRigidbody.AddImpulse(impulse);
                    if (!other.connectedRigidbody.isKinematic)
                        other.connectedRigidbody.AddImpulse(impulse);
                }

            }

        }


        public Direction VectorDirection(Vector2 target)
        {
            Vector2[] compass =
            {
                new Vector2(0.0f, 1.0f), //up
                new Vector2(1.0f, 0.0f), //right
                new Vector2(0.0f, -1.0f), //down
                new Vector2(-1.0f, 0.0f) //left
            };

            float max = 0.0f;
            int bestMatch = -1;
            for(int i = 0; i < 4; i++)
            {
                float dot = Vector2.Dot(Vector2.Normalize(target), compass[i]);
                if(dot > max)
                {
                    max = dot;
                    bestMatch = i;
                }
            }
            return (Direction)bestMatch;
        }
    }


    
}
