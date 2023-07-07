using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaylibJunk2
{
    internal class SceneManager
    {
        public SceneManager()
        {

        }

        public List<Scene> sceneList = new List<Scene>();

        Scene currentScene;

        public void Update(float deltaTime)
        {
            currentScene.Update(deltaTime);
        }

        public void Draw()
        {
            currentScene.Draw();
        }

        public void AddToScenes(Scene scene, bool current)
        {
            sceneList.Add(scene);
            if (current)
            {
                currentScene = scene;
            }
        }

    }
}
