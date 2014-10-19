using System;
using System.Xml;

namespace TOUJOU.Kusa
{
	public class TestResult
	{
		public Outcome Outcome { get; protected set; }
		public Output Output { get; protected set; }
		public DateTime? StartTime { get; protected set; }
		public DateTime? EndTime { get; protected set; }
		public TimeSpan? Duration { get; protected set; }

		public TestResult()
		{
		}

		public TestResult(XmlElement testResultElement)
		{
			if (testResultElement == null)
			{
				throw new ArgumentNullException("testResultElement");
			}

			if (testResultElement.LocalName != "UnitTestResult")
			{
				throw new ArgumentException("Expected 'UnitTestResult' instead of '" + testResultElement.LocalName + "'");
			}

			XmlNamespaceManager nsmgr = new XmlNamespaceManager(testResultElement.OwnerDocument.NameTable);
			nsmgr.AddNamespace("ns", "http://microsoft.com/schemas/VisualStudio/TeamTest/2010");

			#region Outcome

			XmlAttribute outcomeAttribute = testResultElement.SelectSingleNode("./@outcome", nsmgr) as XmlAttribute;

			if (outcomeAttribute == null)
			{
				throw new XmlException("Missing attribute 'outcome' on 'UnitTestResult' element.");
			};

			Outcome = (Outcome)Enum.Parse(typeof(Outcome), outcomeAttribute.Value);

			#endregion

			#region Output

			XmlElement messageElement = testResultElement.SelectSingleNode("./ns:Output/ns:ErrorInfo/ns:Message", nsmgr) as XmlElement;
			XmlElement stackTraceElement = testResultElement.SelectSingleNode("./ns:Output/ns:ErrorInfo/ns:StackTrace", nsmgr) as XmlElement;

			string message = null;
			string stackTrace = null;

			if (messageElement != null)
			{
				message = messageElement.InnerText;
			}

			if (stackTraceElement != null)
			{
				stackTrace = stackTraceElement.InnerText;
			}

			Output = new Output(message, stackTrace);

			#endregion

			#region StartTime

			XmlAttribute startTimeAttribute = testResultElement.SelectSingleNode("./@startTime", nsmgr) as XmlAttribute;

			if (startTimeAttribute == null)
			{
				throw new XmlException("Missing attribute 'startTime' on 'UnitTestResult' element.");
			};

			StartTime = DateTime.Parse(startTimeAttribute.Value);

			#endregion

			#region EndTime

			XmlAttribute endTimeAttribute = testResultElement.SelectSingleNode("./@endTime", nsmgr) as XmlAttribute;

			if (endTimeAttribute == null)
			{
				throw new XmlException("Missing attribute 'endTime' on 'UnitTestResult' element.");
			};

			EndTime = DateTime.Parse(endTimeAttribute.Value);

			#endregion

			#region Duration

			XmlAttribute durationAttribute = testResultElement.SelectSingleNode("./@duration", nsmgr) as XmlAttribute;

			if (durationAttribute == null)
			{
				throw new XmlException("Missing attribute 'duration' on 'UnitTestResult' element.");
			};

			Duration = TimeSpan.Parse(durationAttribute.Value);

			#endregion
		}
	}
}
