using RaylibJunk2.Components;
using System.Numerics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
//using Raylib_cs;

namespace RaylibJunk2.AI
{
	class AIManager
	{
		class NavagationNode
		{

			public NavagationNode(Vector2 position)
			{
				transform.LocalPosition = position;
				passable = true;
			}

			public Transform transform = new Transform();
			bool passable;
		}


		List<NavagationNode> navagationMesh = new List<NavagationNode>();

		float nodeDistance;
		Vector2 navagationAreaMax, navagationAreaMin;

		public AIManager(Vector2 navagationAreaMax, Vector2 navagationAreaMin, float nodeDistance)
		{
			this.navagationAreaMax = navagationAreaMax;
			this.navagationAreaMin = navagationAreaMin;
			this.nodeDistance = nodeDistance;

			for(float x = navagationAreaMin.X; x < navagationAreaMax.X; x += nodeDistance)
			{
				for(float y = navagationAreaMin.Y; y < navagationAreaMax.Y; y += nodeDistance)
				{
					navagationMesh.Add(new NavagationNode(new Vector2(x, y)));
				}
			}
		}

		public void Update()
		{

		}

		public void Draw()
		{
			Parallel.For(0, navagationMesh.Count, i =>
			{
				Raylib_cs.Raylib.DrawCircle((int)navagationMesh[i].transform.LocalPosition.X, (int)navagationMesh[i].transform.LocalPosition.Y, 2, Raylib_cs.Color.LIME);
			});
		}

	}
}
