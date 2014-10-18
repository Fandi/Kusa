using System;
using System.Collections.Generic;

namespace TOUJOU.Kusa
{
	public class TestDefinitionComparer : IEqualityComparer<TestDefinition>, IComparer<TestDefinition>
	{
		public static readonly TestDefinitionComparer Default = new TestDefinitionComparer();

		public bool Equals(TestDefinition x, TestDefinition y)
		{
			if (x == null &&
				y == null)
			{
				return true;
			}

			if (x == null ||
				y == null)
			{
				return false;
			}

			return x.Key.Equals(y.Key);
		}

		public int GetHashCode(TestDefinition obj)
		{
			if (obj == null)
			{
				throw new ArgumentNullException("obj");
			}

			return obj.Key.GetHashCode();
		}

		public int Compare(TestDefinition x, TestDefinition y)
		{
			if (x == null &&
				y == null)
			{
				return 0;
			}
			else if (x == null)
			{
				throw new ArgumentNullException("x");
			}
			else if (y == null)
			{
				throw new ArgumentNullException("y");
			}

			return x.Key.CompareTo(y.Key);
		}
	}
}
