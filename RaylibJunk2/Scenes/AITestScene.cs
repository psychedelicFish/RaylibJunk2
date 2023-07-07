using RaylibJunk2.Components.Scenes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaylibJunk2.Scenes
{
	internal class AITestScene : Scene
	{
		AI.AIManager aiManager;
		public AITestScene(int index, bool current = false) : base(index, current)
		{
			aiManager = new AI.AIManager(new System.Numerics.Vector2(900, 900), new System.Numerics.Vector2(0, 0), 10, true);
		}

		public override void Update(float deltaTime)
		{
			base.Update(deltaTime);
			aiManager.Update();
		}

		public override void Draw()
		{
			base.Draw();
			aiManager.Draw();
		}
	}
}
