using System.Numerics;
using System.Collections;
using System;
using RaylibJunk2.Components.Physics;
using RaylibJunk2.Components;

namespace RaylibJunk2.Managers
{
    //Class is created on the GameManager singleton and accessed from there. 
    internal class PhysicsManager
    {
        //Basic  Global gravity 
        public readonly Vector2 gravity = new Vector2(0, 9.8f);

        List<Rigidbody> rigidbodies = new List<Rigidbody>();

        public void UnregisterRigidbody(Rigidbody rb)
        {
            rigidbodies.Remove(rb);
        }

        //Register the body with the physics manager
        public void RegisterRigidbody(Rigidbody body)
        {
            rigidbodies.Add(body);
        }

        //Fixed update step, Calls Update step on each rigidbody 
        public void FixedUpdate(float fixedDelta)
        {
            foreach (Rigidbody rb in rigidbodies)
            {
                //Checks to see if rigidbodies are colliding with other rigidbodies. Could use some optimisation
                Parallel.For(0, rigidbodies.Count, i => { rigidbodies[i].CheckForCollisions(rb); });
                rb.UpdateStep(fixedDelta);

            }
        }

    } 
}
