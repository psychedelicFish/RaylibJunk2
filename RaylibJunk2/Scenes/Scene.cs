using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaylibJunk2.Components.Scenes
{
    internal class Scene
    {
        List<GameObject> objects = new List<GameObject>();
        int index;
        public Scene(int index, bool current = false)
        {
            this.index = index;
            GameManager.sceneManager.AddToScenes(this, current);
        }

        public virtual void Update(float deltaTime)
        {
            for (int i = 0; i < objects.Count; i++)
            {
                objects[i].Update(deltaTime);
            }
        }

        public virtual void Draw()
        {
            for (int i = 0; i < objects.Count; i++)
            {
                objects[i].Draw();
            }
        }

    }
}
