using System;
using System.Collections.Generic;
using System.Web;
using System.Xml.XPath;
using System.Xml;

/// <summary>
/// Summary description for PainScale
/// </summary>
public class SymptomScale : HealthRecordItemCustomBase
{
    public static String[] symptomNames = new String[] { "Pain", "Nausea", "Sleep", "Faigue", "Constipation" };
    int painThreshold;
    int fatigueThreshold;
    int sleepThreshold;
    int constipationThreshold;
    int nauseaThreshold;
    DateTime when;

    public SymptomScale()
    {
        when = DateTime.Now;
    }

    public int PainThreshold
    {
        get { return painThreshold; }
        set { painThreshold = value; }
    }

    public int FatigueThreshold
    {
        get { return fatigueThreshold; }
        set { fatigueThreshold = value; }
    }

    public int SleepThreshold
    {
        get { return sleepThreshold; }
        set { sleepThreshold = value; }
    }

    public int ConstipationThreshold
    {
        get { return constipationThreshold; }
        set { constipationThreshold = value; }
    }

    public int NauseaThreshold
    {
        get { return nauseaThreshold; }
        set { nauseaThreshold = value; }
    }

    public DateTime When
    {
        get { return when; }
        set { when = value; }
    }

    public override void WriteXml(XmlWriter writer)
    {
        writer.WriteStartElement("PainScale");
        writer.WriteAttributeString("painThreshold", painThreshold.ToString());
        writer.WriteAttributeString("nauseaThreshold", nauseaThreshold.ToString());
        writer.WriteAttributeString("sleepThreshold", sleepThreshold.ToString());
        writer.WriteAttributeString("constipationThreshold", constipationThreshold.ToString());
        writer.WriteAttributeString("fatigueThreshold", fatigueThreshold.ToString());
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

                    case "nauseaThreshold":
                        nauseaThreshold = painScale.ValueAsInt;
                        break;

                    case "sleepThreshold":
                        sleepThreshold = painScale.ValueAsInt;
                        break;

                    case "fatigueThreshold":
                        fatigueThreshold = painScale.ValueAsInt;
                        break;

                    case "constipationThreshold":
                        constipationThreshold = painScale.ValueAsInt;
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