using System;
using System.Collections.Generic;
using System.Xml;

namespace TOUJOU.Kusa
{
	public class TRX
	{
		private string testResultFileName;

		public DateTime Creation { get; protected set; }

		public bool IsTimeSummaryLoaded { get; protected set; }
		public DateTime? Queuing { get; protected set; }
		public DateTime? Start { get; protected set; }
		public DateTime? Finished { get; protected set; }

		public bool IsCounterSummaryLoaded { get; protected set; }
		public int? Total { get; protected set; }
		public int? Error { get; protected set; }
		public int? Failed { get; protected set; }
		public int? Timeout { get; protected set; }
		public int? Aborted { get; protected set; }
		public int? Inconclusive { get; protected set; }
		public int? PassedButRunAborted { get; protected set; }
		public int? NotRunnable { get; protected set; }
		public int? NotExecuted { get; protected set; }
		public int? Executed { get; protected set; }
		public int? Disconnected { get; protected set; }
		public int? Warning { get; protected set; }
		public int? Passed { get; protected set; }
		public int? Completed { get; protected set; }
		public int? InProgress { get; protected set; }
		public int? Pending { get; protected set; }

		public bool AreTestDefinitionsLoaded { get; protected set; }
		private List<TestDefinition> testDefinitions;

		public IEnumerable<TestDefinition> TestDefinitions
		{
			get
			{
				foreach (TestDefinition testDefinition in testDefinitions)
				{
					yield return testDefinition;
				}
			}
		}

		public TRX(string testResultFileName, bool timeSummary = true, bool counterSummary = true, bool testDefinitions = true)
		{
			if (testResultFileName == null)
			{
				throw new ArgumentNullException("testResultFileName");
			}

			this.testResultFileName = testResultFileName;

			XmlDocument document = new XmlDocument();
			document.Load(testResultFileName);

			XmlNamespaceManager nsmgr = new XmlNamespaceManager(document.NameTable);
			nsmgr.AddNamespace("ns", "http://microsoft.com/schemas/VisualStudio/TeamTest/2010");

			#region Creation

			XmlAttribute creationAttribute = document.DocumentElement.SelectSingleNode("/ns:TestRun/ns:Times/@creation", nsmgr) as XmlAttribute;

			if (creationAttribute == null)
			{
				throw new XmlException("Missing attribute 'creation' on 'Times' element.");
			};

			Creation = DateTime.Parse(creationAttribute.Value);

			#endregion

			#region TimeSummary

			if (timeSummary)
			{
				LoadTimeSummary(document.DocumentElement, nsmgr);
			}

			#endregion

			#region CounterSummary

			if (counterSummary)
			{
				LoadCounterSummary(document.DocumentElement, nsmgr);
			}

			#endregion

			#region TestDefinitions

			if (testDefinitions)
			{
				LoadTestDefinitions(document.DocumentElement, nsmgr);
			}

			#endregion
		}

		private void LoadTimeSummary(XmlElement documentElement, XmlNamespaceManager nsmgr)
		{
			if (IsTimeSummaryLoaded)
			{
				return;
			}

			#region Start

			DateTime start;
			XmlAttribute startAttribute = documentElement.SelectSingleNode("/ns:TestRun/ns:Times/@start", nsmgr) as XmlAttribute;

			if (startAttribute != null &&
				DateTime.TryParse(startAttribute.Value, out start))
			{
				Start = start;
			};

			#endregion

			#region Finished

			DateTime finished;
			XmlAttribute finishedAttribute = documentElement.SelectSingleNode("/ns:TestRun/ns:Times/@finish", nsmgr) as XmlAttribute;

			if (finishedAttribute != null &&
				DateTime.TryParse(finishedAttribute.Value, out finished))
			{
				Finished = finished;
			};

			#endregion

			#region Queuing

			DateTime queuing;
			XmlAttribute queuingAttribute = documentElement.SelectSingleNode("/ns:TestRun/ns:Times/@queuing", nsmgr) as XmlAttribute;

			if (queuingAttribute != null &&
				DateTime.TryParse(queuingAttribute.Value, out queuing))
			{
				Queuing = queuing;
			};

			#endregion

			IsTimeSummaryLoaded = true;
		}

		private void LoadCounterSummary(XmlElement documentElement, XmlNamespaceManager nsmgr)
		{
			if (IsCounterSummaryLoaded)
			{
				return;
			}

			#region Total

			int total;
			XmlAttribute totalAttribute = documentElement.SelectSingleNode("/ns:TestRun/ns:ResultSummary/ns:Counters/@total", nsmgr) as XmlAttribute;

			if (totalAttribute != null &&
				int.TryParse(totalAttribute.Value, out total))
			{
				Total = total;
			};

			#endregion

			#region Error

			int error;
			XmlAttribute errorAttribute = documentElement.SelectSingleNode("/ns:TestRun/ns:ResultSummary/ns:Counters/@error", nsmgr) as XmlAttribute;

			if (errorAttribute != null &&
				int.TryParse(errorAttribute.Value, out error))
			{
				Error = error;
			};

			#endregion

			#region Failed

			int failed;
			XmlAttribute failedAttribute = documentElement.SelectSingleNode("/ns:TestRun/ns:ResultSummary/ns:Counters/@failed", nsmgr) as XmlAttribute;

			if (failedAttribute != null &&
				int.TryParse(failedAttribute.Value, out failed))
			{
				Failed = failed;
			};

			#endregion

			#region Timeout

			int timeout;
			XmlAttribute timeoutAttribute = documentElement.SelectSingleNode("/ns:TestRun/ns:ResultSummary/ns:Counters/@timeout", nsmgr) as XmlAttribute;

			if (timeoutAttribute != null &&
				int.TryParse(timeoutAttribute.Value, out timeout))
			{
				Timeout = timeout;
			};

			#endregion

			#region Aborted

			int aborted;
			XmlAttribute abortedAttribute = documentElement.SelectSingleNode("/ns:TestRun/ns:ResultSummary/ns:Counters/@aborted", nsmgr) as XmlAttribute;

			if (abortedAttribute != null &&
				int.TryParse(abortedAttribute.Value, out aborted))
			{
				Aborted = aborted;
			};

			#endregion

			#region Inconclusive

			int inconclusive;
			XmlAttribute inconclusiveAttribute = documentElement.SelectSingleNode("/ns:TestRun/ns:ResultSummary/ns:Counters/@inconclusive", nsmgr) as XmlAttribute;

			if (inconclusiveAttribute != null &&
				int.TryParse(inconclusiveAttribute.Value, out inconclusive))
			{
				Inconclusive = inconclusive;
			};

			#endregion

			#region PassedButRunAborted

			int passedButRunAborted;
			XmlAttribute passedButRunAbortedAttribute = documentElement.SelectSingleNode("/ns:TestRun/ns:ResultSummary/ns:Counters/@passedButRunAborted", nsmgr) as XmlAttribute;

			if (passedButRunAbortedAttribute != null &&
				int.TryParse(passedButRunAbortedAttribute.Value, out passedButRunAborted))
			{
				PassedButRunAborted = passedButRunAborted;
			};

			#endregion

			#region NotRunnable

			int notRunnable;
			XmlAttribute notRunnableAttribute = documentElement.SelectSingleNode("/ns:TestRun/ns:ResultSummary/ns:Counters/@notRunnable", nsmgr) as XmlAttribute;

			if (notRunnableAttribute != null &&
				int.TryParse(notRunnableAttribute.Value, out notRunnable))
			{
				NotRunnable = notRunnable;
			};

			#endregion

			#region NotExecuted

			int notExecuted;
			XmlAttribute notExecutedAttribute = documentElement.SelectSingleNode("/ns:TestRun/ns:ResultSummary/ns:Counters/@notExecuted", nsmgr) as XmlAttribute;

			if (notExecutedAttribute != null &&
				int.TryParse(notExecutedAttribute.Value, out notExecuted))
			{
				NotExecuted = notExecuted;
			};

			#endregion

			#region Executed

			int executed;
			XmlAttribute executedAttribute = documentElement.SelectSingleNode("/ns:TestRun/ns:ResultSummary/ns:Counters/@executed", nsmgr) as XmlAttribute;

			if (executedAttribute != null &&
				int.TryParse(executedAttribute.Value, out executed))
			{
				Executed = executed;
			};

			#endregion

			#region Disconnected

			int disconnected;
			XmlAttribute disconnectedAttribute = documentElement.SelectSingleNode("/ns:TestRun/ns:ResultSummary/ns:Counters/@disconnected", nsmgr) as XmlAttribute;

			if (disconnectedAttribute != null &&
				int.TryParse(disconnectedAttribute.Value, out disconnected))
			{
				Disconnected = disconnected;
			};

			#endregion

			#region Warning

			int warning;
			XmlAttribute warningAttribute = documentElement.SelectSingleNode("/ns:TestRun/ns:ResultSummary/ns:Counters/@warning", nsmgr) as XmlAttribute;

			if (warningAttribute != null &&
				int.TryParse(warningAttribute.Value, out warning))
			{
				Warning = warning;
			};

			#endregion

			#region Passed

			int passed;
			XmlAttribute passedAttribute = documentElement.SelectSingleNode("/ns:TestRun/ns:ResultSummary/ns:Counters/@passed", nsmgr) as XmlAttribute;

			if (passedAttribute != null &&
				int.TryParse(passedAttribute.Value, out passed))
			{
				Passed = passed;
			};

			#endregion

			#region Completed

			int completed;
			XmlAttribute completedAttribute = documentElement.SelectSingleNode("/ns:TestRun/ns:ResultSummary/ns:Counters/@completed", nsmgr) as XmlAttribute;

			if (completedAttribute != null &&
				int.TryParse(completedAttribute.Value, out completed))
			{
				Completed = completed;
			};

			#endregion

			#region InProgress

			int inProgress;
			XmlAttribute inProgressAttribute = documentElement.SelectSingleNode("/ns:TestRun/ns:ResultSummary/ns:Counters/@inProgress", nsmgr) as XmlAttribute;

			if (inProgressAttribute != null &&
				int.TryParse(inProgressAttribute.Value, out inProgress))
			{
				InProgress = inProgress;
			};

			#endregion

			#region Pending

			int pending;
			XmlAttribute pendingAttribute = documentElement.SelectSingleNode("/ns:TestRun/ns:ResultSummary/ns:Counters/@pending", nsmgr) as XmlAttribute;

			if (pendingAttribute != null &&
				int.TryParse(pendingAttribute.Value, out pending))
			{
				Pending = pending;
			};

			#endregion

			IsCounterSummaryLoaded = true;
		}

		private void LoadTestDefinitions(XmlElement xmlElement, XmlNamespaceManager nsmgr)
		{
			if (AreTestDefinitionsLoaded)
			{
				return;
			}

			testDefinitions = new List<TestDefinition>();

			foreach (XmlNode testDefinitionElement in xmlElement.SelectNodes("/ns:TestRun/ns:TestDefinitions/ns:UnitTest", nsmgr))
			{
				testDefinitions.Add(new TestDefinition(testDefinitionElement as XmlElement));
			}

			AreTestDefinitionsLoaded = true;
		}

		public void LoadTimeSummary()
		{
			if (testResultFileName == null)
			{
				return;
			}

			XmlDocument document = new XmlDocument();
			document.Load(testResultFileName);

			XmlNamespaceManager nsmgr = new XmlNamespaceManager(document.NameTable);
			nsmgr.AddNamespace("ns", "http://microsoft.com/schemas/VisualStudio/TeamTest/2010");

			LoadTimeSummary(document.DocumentElement, nsmgr);
		}

		public void LoadCounterSummary()
		{
			if (testResultFileName == null)
			{
				return;
			}

			XmlDocument document = new XmlDocument();
			document.Load(testResultFileName);

			XmlNamespaceManager nsmgr = new XmlNamespaceManager(document.NameTable);
			nsmgr.AddNamespace("ns", "http://microsoft.com/schemas/VisualStudio/TeamTest/2010");

			LoadCounterSummary(document.DocumentElement, nsmgr);
		}

		public void LoadTestDefinitions()
		{
			if (testResultFileName == null)
			{
				return;
			}

			XmlDocument document = new XmlDocument();
			document.Load(testResultFileName);

			XmlNamespaceManager nsmgr = new XmlNamespaceManager(document.NameTable);
			nsmgr.AddNamespace("ns", "http://microsoft.com/schemas/VisualStudio/TeamTest/2010");

			LoadTestDefinitions(document.DocumentElement, nsmgr);
		}

		public static TRX GetSnapshot(string testResultFileName)
		{
			return new TRX(testResultFileName, true, true, false);
		}
	}
}
