
using Constants;
using RaylibJunk2.Components;
using RaylibJunk2.GameObjects;

namespace RaylibJunk2.Colliders
{
    internal class Collider : Component
    {
        public ColliderType type { get; protected set; }
        bool colliding = false;
        public int id { get; private set; }
        protected List<Collider> overlaps = new List<Collider>();

        public delegate void CollisionCallback(Collider other);
        CollisionCallback? Enter, Exit, Stay;

        public Collider(GameObject parent, int id) : base(parent)
        {
            this.id = id;

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


        public override void Update()
        {

            //CheckForCollisions();
            for (int i = 0; i < overlaps.Count; i++)
            {
                if (CheckStillColliding(overlaps[i]))
                {
                    OnCollisionStay(overlaps[i]);
                }
            }

        }
        public void OnCollisionEnter(Collider other)
        {
            if (Enter != null)
            {
                Enter(other);
            }
        }
        public void OnCollisionStay(Collider other)
        {
            if (Stay != null)
            {
                Stay(other);
            }
        }
        public void OnCollisionExit(Collider other)
        {
            if (Exit != null)
            {
                Exit(other);
            }
        }

        public void SubscribeEnter(CollisionCallback callback)
        {
            Enter -= callback;
            Enter += callback;
        }

        public void SubscribeStay(CollisionCallback callback)
        {
            Stay -= callback;
            Stay += callback;
        }

        public void SubscribeExit(CollisionCallback callback)
        {
            Exit -= callback;
            Exit += callback;
        }

        public void UnSubscribeEnter(CollisionCallback callback)
        {
            Enter -= callback;
        }

        public void UnSubscribeStay(CollisionCallback callback)
        {
            Stay -= callback;
        }

        public void UnSubscribeExit(CollisionCallback callback)
        {
            Exit -= callback;
        }



    }
}
