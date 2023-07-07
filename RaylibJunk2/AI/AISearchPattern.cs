using System.Numerics;
using System.Threading.Tasks;

namespace RaylibJunk2.AI
{
	internal abstract class AISearchPattern
	{
		List<RaylibJunk2.AI.AIManager.NavagationNode> navMesh = new List<AIManager.NavagationNode>();
		public abstract Vector2[] GetPath(Vector2 start, Vector2 end);

		public void RegisterNavMesh(List<RaylibJunk2.AI.AIManager.NavagationNode> navMesh)
		{
			this.navMesh = navMesh;
		}
		protected int GetIndexBasedOnVector(Vector2 point)
		{
			int currentIndex = 0;
			float currentDistance = float.MaxValue;

			Parallel.For(0, navMesh.Count, i =>
			{
				float tempDistnace = Vector2.Distance(point, navMesh[i].transform.LocalPosition);
				if (tempDistnace < currentDistance)
				{
					currentIndex = i;
					currentDistance = tempDistnace;
				}
			});

			return currentIndex;
		}
	}
}
