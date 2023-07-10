using System.Numerics;
using RaylibJunk2.GameObjects;

namespace RaylibJunk2
{
    struct ContactPoint
    {
        public ContactPoint(Vector2 positionA, Vector2 positionB, Vector2 normal, float p)
        {
            this.positionA = positionA;
            this.positionB = positionB;
            this.normal = normal;
            this.penetration = p;
        }

        public Vector2 positionA;
        public Vector2 positionB;
        public Vector2 normal;
        public float penetration;
    }

    struct CollisionInfo
    {
        public GameObject objectA;
        public GameObject objectB;

        public ContactPoint point;

        public CollisionInfo(GameObject a, GameObject b, Vector2 localA, Vector2 localB, Vector2 normal, float p)
        {
            this.objectA = a;
            this.objectB = b;
            point = new ContactPoint(localA, localB, normal, p);    
        }
    }
}
