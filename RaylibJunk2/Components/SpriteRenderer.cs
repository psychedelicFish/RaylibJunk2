using RaylibJunk2.GameObjects;
using Raylib_cs;
using RaylibJunk2.Managers;

namespace RaylibJunk2.Components
{
    internal class SpriteRenderer : Component
    {
        Sprite? currentSprite;

        public SpriteRenderer(GameObject parent) : base(parent)
        {
            currentSprite = new Sprite(@"G:\AIE\2023\MidYearDip2023\Raylib Junk\RaylibJunk2\RaylibJunk2\bin\Spites\0020.png");
        }

        public override void Draw()
        {
            if (currentSprite != null)
            {
                Raylib.DrawTextureV(currentSprite.texture, parent.transform.LocalPosition, currentSprite.color);
            }
            else
            {
                Rectangle rec = new Rectangle(parent.position.X, parent.position.Y, 25, 25);
                Raylib.DrawRectanglePro(rec, new System.Numerics.Vector2(0,0) + new System.Numerics.Vector2(25 / 2f,25 / 2f), 0, Color.MAGENTA);
                
                

                //Raylib.DrawRectangleV(parent.position, new System.Numerics.Vector2(25, 25), Color.MAGENTA);
            }
                
        }

    }
}
