using System;
using System.Linq;
using System.Xml;
using Verse;

namespace MaterialPatchingUtility
{
    public class PatchOperationBalanceMaterial : PatchOperationPathed
    {
        protected double percentage;
		protected string material;

		protected override bool ApplyWorker(XmlDocument xml)
		{
			bool result = false;
			XmlNode[] array = xml.SelectNodes(xpath).Cast<XmlNode>().ToArray();
			foreach (XmlNode xmlNode in array)
			{
				result = true;
				double baseAmount = Convert.ToDouble(xmlNode.InnerText);
				//Log.Message("The inner text of the node is " + baseAmount.ToString());
				double extractionAmount = baseAmount / 100 * percentage;
				//Log.Message("We have to extract " + extractionAmount.ToString() + " from it");
				XmlNode materialXmlNode = xmlNode.OwnerDocument.CreateElement(material);
				materialXmlNode.InnerXml = xmlNode.InnerXml;
				materialXmlNode.InnerText = Convert.ToInt32(extractionAmount).ToString();
				xmlNode.InnerText = (Convert.ToInt32(baseAmount) - Convert.ToInt32(extractionAmount)).ToString();
				xmlNode.ParentNode.InsertBefore(materialXmlNode, xmlNode);
			}
			return result;
		}
	}
}
