using System;
using System.ComponentModel;

namespace Sandbox.Domain
{
	public static class StringComparer
	{
		public static bool Compare(string s1, string s2)
		{
			return Compare(s1, s2, StringRelation.Equals, StringComparison.InvariantCultureIgnoreCase);
		}

		public static bool Compare(string s1, string s2, StringRelation relation)
		{
			return Compare(s1, s2, relation, StringComparison.InvariantCultureIgnoreCase);
		}
		
		public static bool Compare(string s1, string s2, StringRelation relation, StringComparison comparison)
		{
			switch (relation)
			{
				case StringRelation.Equals:
					return string.Equals(s1, s2, comparison);
				case StringRelation.StartsWith:
					return s1.StartsWith(s2, comparison);
				case StringRelation.EndsWith:
					return s1.EndsWith(s2, comparison);
				case StringRelation.Contains:
					return s1.IndexOf(s2, comparison) >= 0;
				default:
					throw new ArgumentOutOfRangeException(nameof(relation));
			}
		}
	}
}