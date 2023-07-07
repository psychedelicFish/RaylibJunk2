using System;
using RaylibJunk2.Components;
using RaylibJunk2.Managers;

namespace RaylibJunk2.GameObjects
{
	class TestAIObject : GameObject
	{
        AIAgent cAgent;

        public TestAIObject() : base()
		{
            cAgent = new AIAgent(this);
            GameManager.AddGameObjectToCurrentScene(this);
		}

        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);
        }

        public override void Draw()
        {
            base.Draw();
        }
    }
}

