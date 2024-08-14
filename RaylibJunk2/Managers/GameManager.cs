using Raylib_cs;
using RaylibJunk2.Components.Scenes;
using RaylibJunk2.GameObjects;
using RaylibJunk2.Components;
using System.Numerics;
using RaylibJunk2.Components.Physics;
using RaylibJunk2.Colliders;
using RaylibJunk2.Scenes;
using System.Diagnostics.Contracts;

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

        public float currentFixedCount { get; private set; } = 0.0f;


        public GameManager() : this(450, 450, 165)
        {
            if (instance == null)
            {
                instance = this;
            }

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
            fixedDelta = 1f/fps;
            physicsManager = new PhysicsManager();
            sceneManager = new SceneManager();
            Raylib.SetTargetFPS(targetFPS);
            Raylib.InitWindow(windowWidth, windowHeight, "PhysicsThingo");

            //Test falling empty sprite
            PhysicsScene scene = new PhysicsScene(0, true);
            sceneManager.AddToScenes(scene, true);

            
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
                physicsManager.FixedUpdate(fixedDelta);
                currentFixedCount -= fixedDelta;
            }

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
                deltaTime = (float)end.Millisecond - (float)start.Millisecond / 1000;
            }
            End();
        }

        public void End()
        {
            Raylib.CloseWindow();
        }


        public static void AddGameObjectToCurrentScene(GameObject go)
        {
            sceneManager.CurrentScene.AddGameObjectToScene(go);
        }

    }
}
