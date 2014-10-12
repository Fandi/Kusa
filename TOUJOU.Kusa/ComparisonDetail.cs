using System;
using System.Collections.Generic;
using System.Linq;

namespace TOUJOU.Kusa
{
	public class ComparisonDetail
	{
		public TestKey Key { get; protected set; }
		public IEnumerable<TestResult> BeforeList { get; protected set; }
		public IEnumerable<TestResult> AfterList { get; protected set; }

		public TestResult Before
		{
			get
			{
				return BeforeList.FirstOrDefault();
			}
		}

		public TestResult After
		{
			get
			{
				return AfterList.FirstOrDefault();
			}
		}

		public bool IsMultiResult
		{
			get
			{
				return BeforeList.Count() > 1 ||
					AfterList.Count() > 1;
			}
		}

		public Difference Difference
		{
			get
			{
				if (Before != null &&
					After != null &&
					!Before.OutcomeEquals(After))
				{
					return Difference.Outcome;
				}
				else if (Before == null)
				{
					return Difference.Addition;
				}
				else if (After == null)
				{
					return Difference.Removal;
				}

				return Difference.None;
			}
		}

		public ComparisonDetail(TestKey key, IEnumerable<TestResult> beforeList, IEnumerable<TestResult> afterList)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}

			if ((beforeList == null ||
				beforeList.Count() == 0) &&
				(afterList == null ||
				afterList.Count() == 0))
			{
				throw new ArgumentException("At least one TestResult must be provided on either beforeList or afterList.");
			}

			Key = key;
			BeforeList = (beforeList ?? new TestResult[] { }).OrderByDescending(testResult => testResult.StartTime);
			AfterList = (afterList ?? new TestResult[] { }).OrderByDescending(testResult => testResult.StartTime);
		}
	}
}
