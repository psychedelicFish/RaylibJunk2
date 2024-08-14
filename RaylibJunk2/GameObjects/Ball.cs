using RaylibJunk2.Colliders;
using RaylibJunk2.Components;
using RaylibJunk2.Components.Physics;
using System.Numerics;

namespace RaylibJunk2.GameObjects
{
    //Example of how the system can be used to create objects/  
    internal class Ball : GameObject
    {
        
        public Ball(Transform transform, float radius, Vector2 intialVelocity)
        {
            this.transform = transform;
            AddComponent(new SpriteRenderer(this)); //Add a sprite renderer to the object
            
            Rigidbody rb = new Rigidbody(this); //Adds a rigidbody (this will also register itself with the physics manager)
            CircleCollider cc = new CircleCollider(radius, this, 0, false, rb); //Adds circle  collider to the object, connects itself with the rigidbody
            rb.AddCollider(cc); // Makes sure the rigidbody knows about its collider (could probably be added to the Collider constructor. 
            
            //Add the components to the components  list
            AddComponent(rb); 
            AddComponent(cc);
            
            //Apply a force to the ball
            rb.AddImpulse(intialVelocity);
        }


    }
}
