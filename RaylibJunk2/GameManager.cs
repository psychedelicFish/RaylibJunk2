using Raylib_cs;
using RaylibJunk2.Physics;

namespace RaylibJunk2
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
            fixedDelta = 1 / targetFPS;
            physicsManager = new PhysicsManager();
        }

        private void Draw()
        { 
            Raylib.ClearBackground(Color.MAGENTA);
            Raylib.BeginDrawing();
            sceneManager.Draw();
            Raylib.EndDrawing();
            
        }
        private void Update()
        {
            currentFixedCount += deltaTime;
            if(currentFixedCount > fixedDelta)
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
                deltaTime = end.Second - start.Second;
            }
        }

       


    }
}
