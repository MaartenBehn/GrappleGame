using System;
using System.Linq;
using UnityEngine;

namespace Utility
{
	public static class GrappleMath
	{
		public static Vector3[] GetSphereDirections(int numDirections)
		{
			Vector3[] pts = new Vector3[numDirections];
			double inc = Math.PI * (3 - Math.Sqrt(5));
			float off = 2f / numDirections;
 
			foreach (int k in Enumerable.Range(0, numDirections))
			{
				float y = k * off - 1 + (off / 2);
				double r = Math.Sqrt(1 - y * y);
				double phi = k * inc;
				float x = (float)(Math.Cos(phi) * r);
				float z = (float)(Math.Sin(phi) * r);
				pts[k] = new Vector3(x, y, z);
			}
			return pts;
		}
	}
}