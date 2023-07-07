using RaylibJunk2.Components.Scenes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaylibJunk2.Managers
{
    internal class SceneManager
    {
        public SceneManager()
        {

        }

        public List<Scene> sceneList = new List<Scene>();

        Scene? currentScene;

        public Scene CurrentScene { get => currentScene; }

        public void Update(float deltaTime)
        {
            if (currentScene != null)
            {
                currentScene.Update(deltaTime);
            }
        }

        public void Draw()
        {
            if(currentScene != null)
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
