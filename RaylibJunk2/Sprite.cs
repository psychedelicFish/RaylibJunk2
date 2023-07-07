using Raylib_cs;

namespace RaylibJunk2
{
    internal class Sprite
    {
        public Texture2D texture { get; private set; }
        public Color color { get; private set; }

        public void LoadSprite(string filePath)
        {
            texture = Raylib.LoadTexture(filePath);
            color = Color.WHITE;
        }


    }

}
