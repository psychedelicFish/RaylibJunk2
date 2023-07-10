using System.Numerics;
using System.Collections;
using System;
using RaylibJunk2.Components.Physics;
using RaylibJunk2.Components;

namespace RaylibJunk2.Managers
{
    internal class PhysicsManager
    {
        public readonly Vector2 gravity = new Vector2(0, 9.8f);

        List<Rigidbody> rigidbodies = new List<Rigidbody>();
          
        

        public PhysicsManager()
        {

        }

        //public void RegisterRigidbody(Rigidbody rb)
        //{
        //    rigidbodies.Add(rb);
        //}
        public void UnregisterRigidbody(Rigidbody rb)
        {
            rigidbodies.Remove(rb);
        }

        public void RegisterRigidbody(Rigidbody body)
        {
            rigidbodies.Add(body);
        }

        //Every 0.016;
        public void FixedUpdate(float fixedDelta)
        {
            foreach (Rigidbody rb in rigidbodies)
            {
                Parallel.For(0, rigidbodies.Count, i => { rigidbodies[i].CheckForCollisions(rb); });
                rb.UpdateStep(fixedDelta);
                
            }
        }

        public void ResolveCollsion(Rigidbody a, Rigidbody b)
        {
            Transform transformA = a.parent.transform;
            Transform transformB = b.parent.transform;

            float totalMass = (a.mass * -1) + (b.mass * -1);

            //Seperate out the objects using projection
            ///Transform 1
            transformA.LocalPosition = transformA.LocalPosition - (Vector2.Normalize(transformB.LocalPosition - transformA.LocalPosition) *
                         ((a.mass * -1) / totalMass));

            ///Transform 2
            transformB.LocalPosition = transformB.LocalPosition - (Vector2.Normalize(transformA.LocalPosition - transformB.LocalPosition) *
                         ((b.mass * -1) / totalMass));
        }


        //TODO: Would like to implement this, not sure how to at this stage
        public void ResolveCollisionsImpulse(Rigidbody a, Rigidbody b, ContactPoint contactPoint)
        {
            Transform transformA = a.parent.transform;
            Transform transformB = b.parent.transform;

            float totalMass = (a.mass * -1) + (b.mass * -1);

            //Seperate out the objects using projection
            ///Transform 1
            transformA.LocalPosition = transformA.LocalPosition - (contactPoint.normal *
                         contactPoint.penetration *
                         ((a.mass * -1) / totalMass));

            ///Transform 2
            transformB.LocalPosition = transformB.LocalPosition - (contactPoint.normal *
                         contactPoint.penetration *
                         ((b.mass * -1) / totalMass));



            //Start Calculating values needed for impulse
            Vector2 relativeA = contactPoint.positionA - transformA.LocalPosition;
            Vector2 relativeB = contactPoint.positionB - transformB.LocalPosition;

            //Do we need to use angular velocity? (I assume we do) We cant cross 2D vectors though right?
            /* Vector2 angVelocityA = Vector2.Cross(rbA.angularVelocity , relativeA); 
            * Vector2 angVelocityB = Vector2.Cross(rbB.angularVelocity , relativeB); */

            Vector2 fullVelocityA = a.velocity; /* + angVelocityA;*/
            Vector2 fullVelocityB = b.velocity; /* + angVelocityB;*/

            Vector2 contactVelocity = fullVelocityB - fullVelocityA;

            //Start building our Impulse Vector 
            float impulseForce = Vector2.Dot(contactVelocity, contactPoint.normal);

            //now work out the effect of Inertia 
            Vector2 inertiaA = new Vector2(0); /* = Vector2.Cross(a.InertiaTensor * Cross(relativeA, contactPoint.normal), relativeA); */
            Vector2 inertiaB = new Vector2(0); /* = Vector2.Cross(b.InertiaTensor * Cross(relativeB, contactPoint.normal), relativeB); */

            float angularEffect = Vector2.Dot(inertiaA + inertiaB, contactPoint.normal);

            float cRestitution = 0.66f;

            float j = (-(1.0f + cRestitution) * impulseForce) / (totalMass + angularEffect);

            Vector2 fullImpulse = contactPoint.normal * j;

            a.AddImpulse(-fullImpulse);
            b.AddImpulse(fullImpulse);

            //Apply angular impulse
            /* a.AddAngularImpulse(Vector2.Cross(relativeA, -fullImpulse));
             * b.AddAngularImpulse(Vector2.Cross(relactiveB, fullImpulse));
             * */


        }   
}
}
