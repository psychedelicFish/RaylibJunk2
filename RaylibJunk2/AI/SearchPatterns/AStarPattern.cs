using System.Numerics;
using System.Threading.Tasks;

namespace RaylibJunk2.AI.SearchPatterns
{
	internal class AStarPattern : AISearchPattern
	{
		public override Vector2[] GetPath(Vector2 start, Vector2 end)
		{
			List<Vector2> path = new List<Vector2>();

			// locate the closest node to the start and end

			Console.WriteLine("Start: " + GetIndexBasedOnVector(start));
			//Console.WriteLine("End: " + GetIndexBasedOnVector(end));

			return path.ToArray();
		}

		
	}
}
