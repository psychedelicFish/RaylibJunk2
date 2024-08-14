using Raylib_cs;
using RaylibJunk2.Managers;

namespace RaylibJunk2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            GameManager gameManager = new GameManager(900,900, 120);
            gameManager.Run();

            
        }
    }
}