using System;
using System.Xml;

namespace TOUJOU.Kusa
{
	public class TestDefinition
	{
		public TestKey Key { get; protected set; }
		public TestResult Result { get; protected set; }

		public TestDefinition(XmlElement testDefinitionElement)
		{
			if (testDefinitionElement == null)
			{
				throw new ArgumentNullException("testDefinitionNode");
			}

			if (testDefinitionElement.LocalName != "UnitTest")
			{
				throw new ArgumentException("Expected 'UnitTest' instead of '" + testDefinitionElement.LocalName + "'");
			}

			XmlNamespaceManager nsmgr = new XmlNamespaceManager(testDefinitionElement.OwnerDocument.NameTable);
			nsmgr.AddNamespace("ns", "http://microsoft.com/schemas/VisualStudio/TeamTest/2010");

			GenerateKey(testDefinitionElement, nsmgr);
			GenerateResult(testDefinitionElement, nsmgr);
		}

		private void GenerateKey(XmlElement testDefinitionElement, XmlNamespaceManager nsmgr)
		{
			XmlAttribute classNameAttribute = testDefinitionElement.SelectSingleNode("./ns:TestMethod/@className", nsmgr) as XmlAttribute;

			if (classNameAttribute == null)
			{
				throw new XmlException("Missing attribute 'className' on 'TestMethod' element.");
			}

			XmlAttribute methodNameAttribute = testDefinitionElement.SelectSingleNode("./ns:TestMethod/@name", nsmgr) as XmlAttribute;

			if (methodNameAttribute == null)
			{
				throw new XmlException("Missing attribute 'name' on 'TestMethod' element.");
			}

			Key = new TestKey(classNameAttribute.Value, methodNameAttribute.Value);
		}

		private void GenerateResult(XmlElement testDefinitionElement, XmlNamespaceManager nsmgr)
		{
			XmlAttribute idAttribute = testDefinitionElement.SelectSingleNode("./@id", nsmgr) as XmlAttribute;

			if (idAttribute == null)
			{
				throw new XmlException("Missing attribute 'id' on 'UnitTest' element.");
			}

			XmlElement testResultElement = testDefinitionElement.SelectSingleNode("/ns:TestRun/ns:Results/ns:UnitTestResult[@testId='" + idAttribute.Value + "']", nsmgr) as XmlElement;

			if (testResultElement == null)
			{
				throw new XmlException("Missing element 'UnitTestResult' with id of: '" + idAttribute.Value + "'.");
			}

			Result = new TestResult(testResultElement);
		}
	}
}
