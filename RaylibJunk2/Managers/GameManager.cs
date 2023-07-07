using Raylib_cs;
using RaylibJunk2.Components.Scenes;
using RaylibJunk2.GameObjects;
using RaylibJunk2.Components;
using System.Numerics;
using RaylibJunk2.Components.Physics;
using RaylibJunk2.Colliders;

namespace RaylibJunk2.Managers
{
    internal class GameManager
    {
        public static GameManager instance;
        public static PhysicsManager physicsManager;
        public static SceneManager sceneManager;
        public int windowWidth { get; private set; }
        public int windowHeight { get; private set; }
        public int targetFPS { get; private set; }

        public float fixedDelta { get; private set; }
        public float deltaTime { get; private set; }

        private float currentFixedCount = 0.0f;


        public GameManager() : this(450, 450, 165)
        {
            instance = this;
            windowWidth = 450;
            windowHeight = 450;
            targetFPS = 165;
        }
        public GameManager(int width, int height, int fps)
        {
            instance = this;
            windowWidth = width;
            windowHeight = height;
            targetFPS = fps;
            fixedDelta = 1.0f/(float)fps;
            physicsManager = new PhysicsManager();
            sceneManager = new SceneManager();
            Raylib.SetTargetFPS(targetFPS);
            Raylib.InitWindow(windowWidth, windowHeight, "game");

            //Test falling empty sprite
            Scene scene = new Scene(0, true);
            GameObject testObject = new GameObject(new Components.Transform(new Vector2(width/2, height / 2)));
            testObject.AddComponent(new SpriteRenderer(testObject));
            Rigidbody rb = new Rigidbody(testObject);
            
            CircleCollider cc = new CircleCollider(15 ,testObject, 0, true);
            rb.AddCollider(cc);
            testObject.AddComponent(rb);
            testObject.AddComponent(cc);
            cc.SubscribeEnter((other) => { Console.WriteLine("Enter"); });
            cc.SubscribeStay((other) => { Console.WriteLine("Stay"); });
            cc.SubscribeExit((other) => { Console.WriteLine("Exit"); });
            GameObject kinematicTest = new GameObject(new Components.Transform(new Vector2(width / 2, height / 2 + 50.0f)));
            kinematicTest.AddComponent(new SpriteRenderer(kinematicTest));
            rb = new Rigidbody(kinematicTest, true);
            BoxCollider bx = new BoxCollider(kinematicTest, new Vector2(25, 25), 1);
            //cc = new CircleCollider(15, kinematicTest, 1, true);
            rb.AddCollider(bx);
            kinematicTest.AddComponent(rb);
            kinematicTest.AddComponent(bx);
            
            scene.AddGameObjectToScene(testObject);
            scene.AddGameObjectToScene(kinematicTest);
        }



        private void Draw()
        {
            Raylib.ClearBackground(Color.BLACK);
            Raylib.BeginDrawing();
            
            sceneManager.Draw();
            
            Raylib.EndDrawing();

        }
        private void Update()
        {
            currentFixedCount += deltaTime;
            if (currentFixedCount > fixedDelta)
            {
                currentFixedCount -= fixedDelta;
            }
            physicsManager.FixedUpdate(deltaTime);

            sceneManager.Update(deltaTime);

        }

        public void Run()
        {
            DateTime start, end;

            while (!Raylib.WindowShouldClose())
            {
                start = DateTime.UtcNow;


                Update();

                Draw();
                end = DateTime.UtcNow;
                deltaTime = end.Second - start.Second;
            }
            End();
        }

        public void End()
        {
            Raylib.CloseWindow();
        }




    }
}
