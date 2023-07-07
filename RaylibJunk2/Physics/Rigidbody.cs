using System.Numerics;


namespace RaylibJunk2.Physics
{
    class Rigidbody : Component
    {

        public float mass { get; private set; }
        public Vector2 velocity { get; private set; }
        public Vector2 acceleration { get; private set; }
        public float gravityScale { get; private set; }
        public bool isKinematic { get; private set; }
        public float drag { get; private set; }



        public Rigidbody(GameObject parent) : base(parent)
        {
            mass = 1;
            velocity = Vector2.Zero;
            gravityScale = 1;
            isKinematic = false;
            drag = 0.05f;
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
            if (!isKinematic)
            {
                if (velocity.Length() != 0)
                    acceleration -= Vector2.Normalize(velocity) * drag * fixedDeltaTime;


                //TODO:
                //This will be where we apply gravity

                acceleration += GameManager.physicsManager.gravity * gravityScale * fixedDeltaTime;
                parent.transform.LocalPosition += velocity * fixedDeltaTime + acceleration;
            }


            
        }

        public void AddImpulse(Vector2 force)
        {
            velocity += force / mass;
        }

        public void AddForce(Vector2 force, float fixedDeltaTime)
        {
            velocity += force * fixedDeltaTime / mass;
        }

        private void RegisterRigidbody(Rigidbody body)
        {
            GameManager.physicsManager.RegisterRigidbody(body);
        }
    }
}
