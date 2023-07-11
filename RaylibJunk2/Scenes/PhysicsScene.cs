using RaylibJunk2.Colliders;
using RaylibJunk2.Components;
using RaylibJunk2.Components.Physics;
using RaylibJunk2.Components.Scenes;
using RaylibJunk2.GameObjects;
using RaylibJunk2.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace RaylibJunk2.Scenes
{
    internal class PhysicsScene : Scene
    {
        public PhysicsScene(int index, bool current = false) : base(index, current)
        {
            float width = GameManager.instance.windowWidth;
            float height = GameManager.instance.windowHeight;
            GameObject testObject = new GameObject(new Components.Transform(new Vector2(width / 2, height / 2 - 300)));
            testObject.AddComponent(new SpriteRenderer(testObject));
            Rigidbody rb = new Rigidbody(testObject);

            CircleCollider cc = new CircleCollider(15, testObject, 0, false, rb);
            rb.AddCollider(cc);
            testObject.AddComponent(rb);
            testObject.AddComponent(cc);
            //cc.SubscribeEnter((other) => { Console.WriteLine("Enter"); });
            //cc.SubscribeStay((other) => { Console.WriteLine("Stay"); });
            
            
            GameObject kinematicTest = new GameObject(new Components.Transform(new Vector2(0, height / 2 + 100)));
            
            kinematicTest.AddComponent(new SpriteRenderer(kinematicTest));
            
            rb = new Rigidbody(kinematicTest, true);
            
            BoxCollider bx = new BoxCollider(kinematicTest, new Vector2(1000, 100), 1, true, rb);
            //cc = new CircleCollider(15, kinematicTest, 1, true);
            
            rb.AddCollider(bx);
            
            kinematicTest.AddComponent(rb);
            kinematicTest.AddComponent(bx);

            AddGameObjectToScene(testObject);
            AddGameObjectToScene(kinematicTest);
        }



    }
}
