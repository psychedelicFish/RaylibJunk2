using Raylib_cs;
using RaylibJunk2.Managers;

namespace RaylibJunk2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            GameManager gameManager = new GameManager();
            Raylib.SetTargetFPS(gameManager.targetFPS);
            Raylib.InitWindow(gameManager.windowWidth, gameManager.windowHeight, "game");

            
        }
    }
}