using RaylibJunk2.AI;
using RaylibJunk2.AI.SearchPatterns;
using RaylibJunk2.Components.Scenes;
using RaylibJunk2.GameObjects;
using RaylibJunk2.Managers;
using System.Numerics;

namespace RaylibJunk2.Scenes
{
	internal class AITestScene : Scene
	{
		AI.AIManager aiManager;

        GameObject agent;

		public AITestScene(int index, bool current = false) : base(index, current)
		{
			aiManager = new AI.AIManager(new System.Numerics.Vector2(900, 900), new System.Numerics.Vector2(0, 0), 10, new AStarPattern(), true);

			// Setup the test AI agent

			agent = new TestAIObject();
			AddGameObjectToScene(agent);

		}

		public override void Update(float deltaTime)
		{
			base.Update(deltaTime);
			aiManager.Update();

			if(Raylib_cs.Raylib.IsMouseButtonPressed(Raylib_cs.MouseButton.MOUSE_BUTTON_LEFT))
			{
				Vector2 mousePos = new Vector2();
                mousePos.X = Raylib_cs.Raylib.GetMouseX();
				mousePos.Y = Raylib_cs.Raylib.GetMouseY();
				//aiManager.GetNavagationPath(agent.transform.LocalPosition, mousePos);
				aiManager.GetNavagationPath(mousePos, mousePos);
				//GetNavagationPath
				// send to test object to get a new path to walk
			}
		}

		public override void Draw()
		{
			base.Draw();
			aiManager.Draw();
		}
	}
}
