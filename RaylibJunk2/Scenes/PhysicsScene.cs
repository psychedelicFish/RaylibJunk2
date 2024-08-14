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

            //Add Ball to scene
            Ball ball = new Ball(new Components.Transform(new Vector2(width / 2 + 100, height / 2 - 350)), 15, new Vector2(-100f, 0));
            Ball ball1 = new Ball(new Components.Transform(new Vector2(width / 2 + 200, height / 2 - 350)), 15, new Vector2(100, 0));
            Ball ball2 = new Ball(new Components.Transform(new Vector2(width / 2 - 100, height / 2 - 350)), 15, new Vector2(-100, 0));
            Ball ball3 = new Ball(new Components.Transform(new Vector2(width / 2 - 200, height / 2 - 350)), 15, new Vector2(100, 0));



            //Create bottom platform
            GameObject kinematicTest = new GameObject(new Components.Transform(new Vector2(0, height - 100)));
            
            kinematicTest.AddComponent(new SpriteRenderer(kinematicTest));
            
            Rigidbody rb = new Rigidbody(kinematicTest, true);
            
            BoxCollider bx = new BoxCollider(kinematicTest, new Vector2(1000, 100), 1, true, rb);
            
            rb.AddCollider(bx);
            
            kinematicTest.AddComponent(rb);
            kinematicTest.AddComponent(bx);


            //Create Right Platform
            GameObject kinematicTest2 = new GameObject(new Components.Transform(new Vector2(width - 100, 0)));
            rb = new Rigidbody(kinematicTest2, true);
            BoxCollider bx1 = new BoxCollider(kinematicTest2, new Vector2(100, 1000), 1, true, rb);
            rb.AddCollider(bx1);
            kinematicTest2.AddComponent(new SpriteRenderer(kinematicTest2));
            kinematicTest2.AddComponent(rb);
            kinematicTest2.AddComponent(bx1);


            //Create Left Platform
            GameObject kinematicTest3 = new GameObject(new Components.Transform(new Vector2(0, 0)));
            rb = new Rigidbody(kinematicTest3, true);
            BoxCollider bx2 = new BoxCollider(kinematicTest3, new Vector2(100, 1000), 1, true, rb);
            rb.AddCollider(bx2);
            kinematicTest2.AddComponent(new SpriteRenderer(kinematicTest3));
            kinematicTest2.AddComponent(rb);
            kinematicTest2.AddComponent(bx2);

            //Create Top Platform
            GameObject kinematicTest4 = new GameObject(new Components.Transform(new Vector2(0, 0)));
            rb = new Rigidbody(kinematicTest4, true);
            BoxCollider bx3 = new BoxCollider(kinematicTest4, new Vector2(1000, 100), 1, true, rb);
            rb.AddCollider(bx3);
            kinematicTest2.AddComponent(new SpriteRenderer(kinematicTest4));
            kinematicTest2.AddComponent(rb);
            kinematicTest2.AddComponent(bx3);


            AddGameObjectToScene(ball);
            AddGameObjectToScene(ball1);
            AddGameObjectToScene(ball2);
            AddGameObjectToScene(ball3);
            AddGameObjectToScene(kinematicTest);
            AddGameObjectToScene(kinematicTest2);
            AddGameObjectToScene(kinematicTest3);
            AddGameObjectToScene(kinematicTest4);
            
        }

        



    }
}
