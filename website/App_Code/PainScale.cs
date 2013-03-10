using System;
using System.Collections.Generic;
using System.Web;
using System.Xml.XPath;
using System.Xml;

/// <summary>
/// Summary description for PainScale
/// </summary>
public class PainScale : HealthRecordItemCustomBase
{
    int painThreshold;
    public PainScale()
    {
    }
    public PainScale(int pain)
    {
        painThreshold = pain;
    }
    public int PainThreshold
    {
        get { return painThreshold; }
    }

    public override void WriteXml(XmlWriter writer)
    {
        writer.WriteStartElement("PainScale");
        writer.WriteAttributeString("painThreshold", painThreshold.ToString());
        writer.WriteEndElement();
    }
    public override void ParseXml(IXPathNavigable typeSpecificXml)
    {
        XPathNavigator navigator = typeSpecificXml.CreateNavigator();
        XPathNavigator painScale = navigator.SelectSingleNode(
            "PainScale");
        if (painScale != null)
        {

            painScale.MoveToFirstAttribute();
            painThreshold = painScale.ValueAsInt;
        }
        else
            painThreshold = 0;
    }
}