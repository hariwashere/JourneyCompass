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
    DateTime when;

    public PainScale()
    {
    }

    public PainScale(int pain)
    {
        painThreshold = pain;
        when = DateTime.Now;
    }
    public int PainThreshold
    {
        get { return painThreshold; }
    }

    public DateTime When
    {
        get { return when; }
    }

    public override void WriteXml(XmlWriter writer)
    {
        writer.WriteStartElement("PainScale");
        writer.WriteAttributeString("painThreshold", painThreshold.ToString());
        writer.WriteAttributeString("when", when.ToString());
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
            for (; ; )
            {
                switch (painScale.LocalName)
                {
                    case "painThreshold":
                        painThreshold = painScale.ValueAsInt;
                        break;

                    case "when":
                        String dateTime =  painScale.Value;
                        when = DateTime.Parse(dateTime);
                        break;
                }

                if (!painScale.MoveToNextAttribute())
                {
                    break;
                }
            }
        }
    }
}