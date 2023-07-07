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
			public bool passable;
		}


		List<NavagationNode> navagationMesh = new List<NavagationNode>();

		float nodeDistance;
		Vector2 navagationAreaMax, navagationAreaMin;
		public bool debugging = false;

		public AIManager(Vector2 navagationAreaMax, Vector2 navagationAreaMin, float nodeDistance, bool debugging = false)
		{
			this.navagationAreaMax = navagationAreaMax;
			this.navagationAreaMin = navagationAreaMin;
			this.nodeDistance = nodeDistance;

			this.debugging = debugging;

			for (float x = navagationAreaMin.X; x < navagationAreaMax.X; x += nodeDistance)
			{
				for (float y = navagationAreaMin.Y; y < navagationAreaMax.Y; y += nodeDistance)
				{
					navagationMesh.Add(new NavagationNode(new Vector2(x, y)));
				}
			}

			this.debugging = debugging;
		}

		public void Update()
		{

		}

		public void Draw()
		{
			if (debugging)
			{
				for (int i = 0; i < navagationMesh.Count; i++)
				{
					if (navagationMesh[i].passable)
						Raylib_cs.Raylib.DrawCircle((int)navagationMesh[i].transform.LocalPosition.X, (int)navagationMesh[i].transform.LocalPosition.Y, 2, Raylib_cs.Color.GREEN);
					else
						Raylib_cs.Raylib.DrawCircle((int)navagationMesh[i].transform.LocalPosition.X, (int)navagationMesh[i].transform.LocalPosition.Y, 2, Raylib_cs.Color.RED);
				}
			}
		}

		public Vector2[] GetNavagationPath(Vector2 from, Vector2 to)
		{
			List<Vector2> path = new List<Vector2>();




			return path.ToArray();
		}

	}
}
