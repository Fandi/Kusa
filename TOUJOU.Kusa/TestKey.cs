using System;
using System.Collections.Generic;

namespace TOUJOU.Kusa
{
	public class TestKey : IEquatable<TestKey>, IComparable<TestKey>
	{
		private int context;
		private int key;

		public string ClassName
		{
			get;
			protected set;
		}

		public string MethodName
		{
			get;
			protected set;
		}

		public TestKey(string className, string methodName)
		{
			if (string.IsNullOrWhiteSpace(className))
			{
				throw new ArgumentNullException("className");
			}

			if (string.IsNullOrWhiteSpace(methodName))
			{
				throw new ArgumentNullException("methodName");
			}

			ClassName = className.Split(",".ToCharArray())[0].Trim();
			MethodName = methodName.Trim();
			context = ClassName.GetHashCode();
			key = MethodName.GetHashCode();
		}

		public bool Equals(TestKey other)
		{
			if (other == null)
			{
				return false;
			}

			return this.key == other.key &&
				this.context == other.context;
		}

		public int CompareTo(TestKey other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}

			int result = Comparer<int>.Default.Compare(this.context, other.context);

			if (result == 0)
			{
				result = Comparer<int>.Default.Compare(this.key, other.key);
			}

			return result;
		}

		public override int GetHashCode()
		{
			return context ^ key;
		}
	}
}
