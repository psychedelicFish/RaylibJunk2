using RaylibJunk2.Components;
using System;
using System.Numerics;

namespace RaylibJunk2.GameObjects
{
    //Base Gameobject Class
    class GameObject
    {
        public Transform transform { get; protected set; } //Transform getter with protected set
        public Vector2 position { get => transform.LocalPosition;}   

        protected List<Component> components = new List<Component>(); //List of components that are attached. Modeled after the Unity Component System

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


        //Draw -> Handles the components draw. 
        public virtual void Draw()
        { 
           for(int i = 0; i < components.Count; i++)
            {
                components[i].Draw();
            }
        
        }
        //Update -> Handles the components update
        public virtual void Update(float deltaTime)
        {

            for (int i = 0; i < components.Count; i++)
            {
                components[i].Update(deltaTime);
            }


        }

        //Get Component Function, Returns the first component of the type wanted 
        //Else returns null
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
