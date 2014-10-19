using System;
using System.Collections.Generic;
using System.Linq;

namespace TOUJOU.Kusa
{
	public class TRXComparer
	{
		public static ComparisonSummary Compare(string testResultFileName1, string testResultFileName2)
		{
			return Compare(new TRX(testResultFileName1), new TRX(testResultFileName2));
		}

		public static ComparisonSummary Compare(TRX testResult1, TRX testResult2)
		{
			if (testResult1 == null)
			{
				throw new ArgumentNullException("testResult1");
			}

			if (testResult2 == null)
			{
				throw new ArgumentNullException("testResult2");
			}

			if (testResult1.Creation > testResult2.Creation)
			{
				TRX tmpTestResultXumary = testResult1;
				testResult1 = testResult2;
				testResult2 = tmpTestResultXumary;
			}

			IEnumerable<TestKey> keyIndex;
			{
				keyIndex = testResult1.TestDefinitions.Concat(testResult2.TestDefinitions).Distinct(TestDefinitionComparer.Default).OrderBy(testDefinition => testDefinition, TestDefinitionComparer.Default).Select(testDefinition => testDefinition.Key);
			}

			ComparisonSummary comparisonSummary = new ComparisonSummary();

			foreach (TestKey key in keyIndex)
			{
				IEnumerable<TestResult> beforeList = testResult1.TestDefinitions.Where(testDefinition => testDefinition.Key.Equals(key)).Select(testDefinition => testDefinition.Result);
				IEnumerable<TestResult> afterList = testResult2.TestDefinitions.Where(testDefinition => testDefinition.Key.Equals(key)).Select(testDefinition => testDefinition.Result);

				comparisonSummary.AddDetail(key, beforeList, afterList);
			}

			return comparisonSummary;
		}
	}
}
