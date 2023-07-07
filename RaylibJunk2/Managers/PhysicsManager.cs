using System.Numerics;
using System;
using RaylibJunk2.Components.Physics;

namespace RaylibJunk2.Managers
{
    internal class PhysicsManager
    {
        public readonly Vector2 gravity = new Vector2(0, -9.8f);

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
                rb.UpdateStep(fixedDelta);
            }
        }
    }
}
