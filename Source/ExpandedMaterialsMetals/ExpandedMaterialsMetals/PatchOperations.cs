using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Text;
using System.Threading.Tasks;
using Verse;
using RimWorld;

namespace ExpandedMaterialsMetals
{
    public static class SplitUtility
    {
        public static int Split(string splitMode, int amount, out int extracted)
        {
            if (splitMode == "small")
            {
                switch (amount)
                {
                    case 15:
                        extracted = 5;
                        return 10;
                    case 20:
                        extracted = 5;
                        return 15;
                    case 25:
                        extracted = 10;
                        return 15;
                    case 30:
                        extracted = 10;
                        return 20;
                    case 35:
                        extracted = 15;
                        return 20;
                    case 40:
                        extracted = 15;
                        return 25;
                    case 60:
                        extracted = 20;
                        return 40;
                    case 80:
                        extracted = 30;
                        return 50;
                    default:
                        extracted = amount / 2;
                        return amount - extracted;
                }
            }
            if (splitMode == "big")
            {
                switch (amount)
                {
                    case 15:
                        extracted = 10;
                        return 5;
                    case 20:
                        extracted = 15;
                        return 5;
                    case 25:
                        extracted = 15;
                        return 10;
                    case 30:
                        extracted = 20;
                        return 10;
                    case 35:
                        extracted = 20;
                        return 15;
                    case 40:
                        extracted = 25;
                        return 15;
                    case 60:
                        extracted = 40;
                        return 20;
                    case 80:
                        extracted = 50;
                        return 30;
                    default:
                        extracted = amount / 2;
                        return amount - extracted;
                }
            }
            extracted = 0;
            return amount;
        }
    }

    public class PatchOperationDistributeCost : PatchOperationPathed
    {
        protected string newMaterial;
        protected string splitMode;
        protected int minimum = 1;

        protected override bool ApplyWorker(XmlDocument xml)
        {
            bool result = false;

            XmlNode[] selectedNodes = xml.SelectNodes(xpath).Cast<XmlNode>().ToArray();

            foreach (XmlNode node in selectedNodes)
            {
                if (Convert.ToInt32(node.InnerText) > minimum)
                {
                    result = true;
                    int splitAmount;
                    int originalAmount = Convert.ToInt32(node.InnerText);
                    originalAmount = SplitUtility.Split(splitMode, originalAmount, out splitAmount);
                    XmlNode newMaterialNode = node.OwnerDocument.CreateElement(newMaterial);
                    newMaterialNode.InnerXml = node.InnerXml;
                    newMaterialNode.InnerText = splitAmount.ToString();
                    node.InnerText = originalAmount.ToString();
                    node.ParentNode.InsertBefore(newMaterialNode, node);
                }
            }
            return result;
        }
    }
}
