using RaylibJunk2.GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaylibJunk2.Components
{
	internal class AIAgent : Component
	{
		public AIAgent(GameObject parent) : base(parent)
		{

		}

        public override void Draw()
        {
            base.Draw();

            // ToDo: add draw path here
        }

        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);
        }

    }
}
