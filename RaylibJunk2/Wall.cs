using System.Numerics;

namespace RaylibJunk2
{
    internal class Wall : GameObject
    {
        protected Vector2 scale;

        public Wall()
        {
            scale.X = 100;
            scale.Y = 100;
        }
        public Wall(Vector2 scale)
        {
            this.scale = scale;
        }


    }
}
