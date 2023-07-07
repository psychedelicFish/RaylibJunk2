using RaylibJunk2.Components;
using System;
using System.Numerics;

namespace RaylibJunk2.GameObjects
{
    class GameObject
    {
        public Transform transform { get; private set; }
        public Vector2 position { get => transform.LocalPosition;}   

        protected List<Component> components = new List<Component>();

        public GameObject()
        {
            transform = new Transform();
        }

        public GameObject(Transform transform)
        {
            this.transform = transform;
        }

        public GameObject(GameObject parent)
        {
            transform = new Transform(parent.transform);
        }

        public void AddChild(GameObject toBeAdded)
        {
            transform.AddChild(toBeAdded.transform);
        }

        public virtual void Draw()
        { }

        public virtual void Update(float deltaTime)
        { }

        /// <summary>
        /// RETURNS THE FIRST COMPONENT OF TYPE IT FINDS
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="component"></param>
        /// <returns></returns>
        public Component? GetComponent<T>(T component)
        {
            if (!(component is Component))
            {
                return null;
            }
            for (int i = 0; i < components.Count; i++)
            {
                if (components[i] is T)
                {
                    return components[i];
                }
            }
            return null;
        }
        public void AddComponent(Component component)
        {
            components.Add(component);
        }
        public void RemoveComponent(Component component)
        {
            components.Remove(component);
        }

    }
}
