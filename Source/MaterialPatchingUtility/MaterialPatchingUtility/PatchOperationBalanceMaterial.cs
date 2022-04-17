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
		protected bool changePrevMaterial;
		protected string newMaterial;
		protected int minimum;

		protected override bool ApplyWorker(XmlDocument xml)
		{
			bool result = false;
			XmlNode[] array = xml.SelectNodes(xpath).Cast<XmlNode>().ToArray();
			foreach (XmlNode xmlNode in array)
			{
				if (Convert.ToInt32(xmlNode.InnerText) >= minimum)
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
					if (changePrevMaterial)
					{
						XmlNode newMaterialNode = xmlNode.OwnerDocument.CreateElement(newMaterial);
						newMaterialNode.InnerXml = xmlNode.InnerXml;
						newMaterialNode.InnerText = xmlNode.InnerText;
						xmlNode.ParentNode.InsertBefore(newMaterialNode, xmlNode);
						xmlNode.ParentNode.RemoveChild(xmlNode);
					}
				}
			}
			return result;
		}
	}
}
