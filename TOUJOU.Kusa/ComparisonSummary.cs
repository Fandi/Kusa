using System.Collections.Generic;

namespace TOUJOU.Kusa
{
	public class ComparisonSummary
	{
		protected List<ComparisonDetail> details;

		public IEnumerable<ComparisonDetail> Details
		{
			get
			{
				foreach (ComparisonDetail comparisonDetail in details)
				{
					yield return comparisonDetail;
				}
			}
		}

		internal protected ComparisonSummary()
		{
			details = new List<ComparisonDetail>();
		}

		internal protected void AddDetail(ComparisonDetail detail)
		{
			details.Add(detail);
		}
	}
}
