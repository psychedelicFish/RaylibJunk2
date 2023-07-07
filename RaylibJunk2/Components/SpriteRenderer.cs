using RaylibJunk2.GameObjects;
using Raylib_cs;

namespace RaylibJunk2.Components
{
    internal class SpriteRenderer : Component
    {
        Sprite? currentSprite;

        public SpriteRenderer(GameObject parent) : base(parent)
        {
            currentSprite = null;
        }

        public override void Draw()
        {
            if (currentSprite != null)
            {
                Raylib.DrawTextureV(currentSprite.texture, parent.transform.LocalPosition, currentSprite.color);
            }
            else
            {
                Rectangle rec = new Rectangle(0, 0, 25, 25);
                //Raylib.DrawRectanglePro(rec, parent.position, 0, Color.MAGENTA);
                


                Raylib.DrawRectangleV(parent.position, new System.Numerics.Vector2(25, 25), Color.MAGENTA);
            }
                
        }

    }
}
