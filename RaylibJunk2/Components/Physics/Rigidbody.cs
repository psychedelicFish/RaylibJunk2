using RaylibJunk2.Colliders;
using RaylibJunk2.GameObjects;
using RaylibJunk2.Managers;
using System.Numerics;


namespace RaylibJunk2.Components.Physics
{
    class Rigidbody : Component
    {

        public float mass { get; private set; }
        public Vector2 velocity { get; private set; }
        public Vector2 acceleration { get; private set; }
        public float gravityScale { get; private set; }
        public bool isKinematic { get; private set; }
        public float drag { get; private set; }

        protected List<Collider> colliders = new List<Collider>();



        public Rigidbody(GameObject parent) : base(parent)
        {
            mass = 1;
            velocity = Vector2.Zero;
            gravityScale = 1;
            isKinematic = false;
            drag = 0.5f;
            acceleration = Vector2.Zero;
            RegisterRigidbody(this);
        }
        public Rigidbody(GameObject parent, bool isKinematic) : base(parent)
        {
            mass = 1;
            velocity = Vector2.Zero;
            gravityScale = 1;
            this.isKinematic = isKinematic;
            drag = 0.5f;
            acceleration = Vector2.Zero;
            RegisterRigidbody(this);
        }

        public Rigidbody(GameObject parent, float mass, Vector2 velocity, float gravityScale, float drag, bool isKinematic) : base(parent)
        {
            this.mass = mass;
            this.velocity = velocity;
            this.gravityScale = gravityScale;
            this.isKinematic = isKinematic;
            this.drag = drag;
            acceleration = Vector2.Zero;
            RegisterRigidbody(this);
        }

        public void UpdateStep(float fixedDeltaTime)
        {
            //IsKinematic == true means physics is not applied to this object 
            if (!isKinematic)
            {
               
                //Calculating acceleration to be applied by gravity. 
                acceleration = GameManager.physicsManager.gravity * gravityScale;
                if (velocity.Length() != 0)
                    velocity -= Vector2.Normalize(velocity) * drag; //Apply drag to the object

                velocity += acceleration; //Apply acceleration

                parent.transform.LocalPosition += velocity * fixedDeltaTime; //Apply velocity 
            }



        }

        public void ChangeVelocity(Vector2 change)
        {
            velocity = change;
        }

        //Add Impulse
        public void AddImpulse(Vector2 force)
        {
            velocity += force * -mass; // multiply by negative mass to avoid divide by zero
        }

        public void AddForce(Vector2 force, float fixedDeltaTime)
        {
            velocity += force * fixedDeltaTime * -mass; // multiply by negative mass to avoid divide by zero
        }

        //Function used to register the rigidbody with the physics manager
        private void RegisterRigidbody(Rigidbody body)
        {
            GameManager.physicsManager.RegisterRigidbody(body); 
        }

        public void AddCollider(Collider collider)
        {
            colliders.Add(collider);
        }

        public void ResetVelocity()
        {
            velocity = Vector2.Zero;
        }
        
        //Function that runs through its colliders and checks for collisions
        public void CheckForCollisions(Rigidbody rb)
        {
            //If we are checking against ourself OR we have no colliders OR the other body has not colliders
            if (rb == this || colliders.Count <= 0 || rb.colliders.Count <= 0) 
            {
                return;
            }
            foreach(Collider c in colliders)
            {
                foreach(Collider o in rb.colliders)
                {
                    c.CheckForCollisions(o);
                }
            }
            
        }
    }
}
