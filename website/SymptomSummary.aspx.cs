using System;
using System.Data;
using System.Configuration;
using System.Collections.ObjectModel;
using System.Collections.Generic;

using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using Microsoft.Health;
using Microsoft.Health.Web;
using Microsoft.Health.ItemTypes;

public partial class SymptomSummary : HealthServicePage
{
    List<Symptom> symptoms = new List<Symptom>();
    Dictionary<String, Table> symptomNameTable = new Dictionary<string, Table>();

    protected void Page_Load(object sender, EventArgs e)
    {
        initializeDictionary();
        getPatientData();
        getBasicInfo();
        getSymptomSummary();
        PopulateSymptomTable();
    }

    protected void initializeDictionary()
    {
        //symptomNameTable.Add("Pain", pain_summary_table);
        //symptomNameTable.Add("Nausea", nausea_summary_table);
        //symptomNameTable.Add("Fatigue", fatigue_summary_table);
        //symptomNameTable.Add("Constipation", constipation_summary_table);
        //symptomNameTable.Add("Sleep", sleep_summary_table);
    }

    protected void getPatientData()
    {
        patient_name.Text = PersonInfo.Name;
    }

    protected void getBasicInfo()
    {
        HealthRecordSearcher searcher = PersonInfo.SelectedRecord.CreateSearcher();
        HealthRecordFilter filter = new HealthRecordFilter(Basic.TypeId);
        searcher.Filters.Add(filter);
        HealthRecordItemCollection items = searcher.GetMatchingItems()[0];
        Basic basicInfo = (Basic)items[0];
        dob.Text = basicInfo.BirthYear.ToString();
        city.Text = basicInfo.City;
        state.Text = basicInfo.StateOrProvince;
    }

    protected void getSymptomSummary()
    {
        DateTime from;
        DateTime to = DateTime.Now;
        String from_date_string = from_date.Text;
        if (String.IsNullOrEmpty(from_date_string))
            from = DateTime.Now.AddDays(-7);
        else
            from = DateTime.Parse(from_date_string + " 00:00:01 AM");

        HealthRecordSearcher searcher = PersonInfo.SelectedRecord.CreateSearcher();

        HealthRecordFilter filter = new HealthRecordFilter(Condition.TypeId);
        filter.CreatedDateMax = to;
        filter.CreatedDateMin = from;
        searcher.Filters.Add(filter);

        HealthRecordItemCollection items = searcher.GetMatchingItems()[0];
        foreach (Condition condition in items)
        {
            if (isSymptom(condition))
            {
                Symptom symptom = new Symptom();
                symptom.SymptomName = condition.Name.Text;
                symptom.SymptomValue = Convert.ToInt32(condition.Status.Text);
                symptom.When = DateTime.Parse(condition.OnsetDate.ToString());
                if (symptom.When >= from)
                    symptoms.Add(symptom);
            }
        }
        symptoms.Sort(delegate(Symptom p1, Symptom p2) { return p1.When.CompareTo(p2.When); });
    }

    protected void PopulateSymptomTable()
    {
        clearExistingRows();
        initializeHeaderRows();
        populateSymptomValues();
    }

    protected void clearExistingRows()
    {
        foreach (KeyValuePair<string, Table> pair in symptomNameTable)
        {
            pair.Value.Rows.Clear();
        }
    }

    protected void initializeHeaderRows()
    {
        foreach (KeyValuePair<string, Table> pair in symptomNameTable)
        {
            Table symptomTable = pair.Value;
            String symptomName = pair.Key;
            TableHeaderRow headerRow = new TableHeaderRow();

            TableHeaderCell headerWhenCell = new TableHeaderCell();
            headerWhenCell.Text = "When";
            headerRow.Cells.Add(headerWhenCell);

            TableHeaderCell headerNameCell = new TableHeaderCell();
            headerNameCell.Text = symptomName + " Scale";
            headerRow.Cells.Add(headerNameCell);
            symptomTable.Rows.Add(headerRow);
        }
    }

    protected void populateSymptomValues()
    {
     // Populate Series...
        Dictionary<string, List<Symptom>> mySymptomDict = new Dictionary<string, List<Symptom>>();
        foreach (Symptom symptom in symptoms)
        {
            if (mySymptomDict.ContainsKey(symptom.SymptomName) == true)
            {
                List<Symptom> mySymptomList = mySymptomDict[symptom.SymptomName];
                mySymptomList.Add(symptom);
            }
            else
            {
                List<Symptom> mySymptomList = new List<Symptom>();
                mySymptomList.Add(symptom);
                mySymptomDict.Add(symptom.SymptomName, mySymptomList);
            }
        }

        chartScript.Text = "<script type='text/javascript'>";
        string collapse_series = "     series: [";
        int totItem = mySymptomDict.Count;
        string chartSetting ="     chart: {type: 'spline' },";
        chartSetting += "     xAxis: {";
        chartSetting += "        type: 'datetime',";
        chartSetting += "        dateTimeLabelFormats: {";
        chartSetting += "           month: '%e. %b',";
        chartSetting += "           year: '%b'";
        chartSetting += "        }";
        chartSetting += "     },";
        chartSetting += "     yAxis: {";
        chartSetting += "        title: { text: 'Scale' },";
        chartSetting += "        min: 0,";
        chartSetting += "        max: 10,";
        chartSetting += "        tickPositions: [0,1,2,3,4,5,6,7,8,9,10]";
        chartSetting += "     },";

        foreach (string key in mySymptomDict.Keys)
        {
            totItem--;
            chartScript.Text += "$(function () {";
            chartScript.Text += "  $('#"+key+"_graph').highcharts({";
            chartScript.Text += "     title: {text: '"+key+" Summary' },";
            chartScript.Text += chartSetting;
            chartScript.Text += "     series: [{";
            chartScript.Text += "       name: '"+key+" Symptom',";
            chartScript.Text += "       data: [";
            string series_data = "";
            foreach (Symptom symptom in mySymptomDict[key])
            {
                if (mySymptomDict[key].IndexOf(symptom) == mySymptomDict[key].Count - 1)
                {
                    series_data += "         [Date.UTC(" + symptom.When.Year + ", " + (symptom.When.Month-1) + ", " + symptom.When.Day + ", " + symptom.When.Hour + ", " + symptom.When.Minute + ", " + symptom.When.Second + "), " + symptom.SymptomValue + "]";
                }
                else
                {
                    series_data += "         [Date.UTC(" + symptom.When.Year + ", " + (symptom.When.Month-1) + ", " + symptom.When.Day + ", " + symptom.When.Hour + ", " + symptom.When.Minute + ", " + symptom.When.Second + "), " + symptom.SymptomValue + "],";
                }
            }
            chartScript.Text += series_data;
            chartScript.Text += "       ]";
            chartScript.Text += "     }]";
            chartScript.Text += "  });";
            chartScript.Text += "});";

            // For collapsed series..
            if (totItem == 0)
            {
                collapse_series += "{name: '" + key + " Symptom',";
                collapse_series += " data: [" + series_data + "]}";
            }
            else
            {
                collapse_series += "{name: '" + key + " Symptom',";
                collapse_series += " data: [" + series_data + "]},";
            }
        }
        chartScript.Text += "</script>";

        collapse_chartScript.Text = "<script type='text/javascript'> $(function () {";
        collapse_chartScript.Text += "  $('#CollapseContainer').highcharts({";
        collapse_chartScript.Text += "     title: {text: 'All Symptom Summary' },";
        collapse_chartScript.Text += chartSetting;
        collapse_chartScript.Text += collapse_series + "]";
        collapse_chartScript.Text += "  });";
        collapse_chartScript.Text += "});";
        collapse_chartScript.Text += "</script>";

        //Console.WriteLine(chartScript.Text);

        //foreach (Symptom symptom in symptoms)
        //{
        //    Table table = symptomNameTable[symptom.SymptomName];
        //    TableRow row = new TableRow();
        //    c_PainSummaryTable.Rows.Add(row);

        //    TableHeaderCell headerDateCell = new TableHeaderCell();
        //    headerDateCell.Text = symptom.When.ToString();
        //    //headerDateCell.Text = symptom.When.ToUniversalTime().ToString();
        //    row.Cells.Add(headerDateCell);

        //    TableCell symptomValueCell = new TableCell();
        //    symptomValueCell.Text = String.Format("{0:F2}", symptom.SymptomValue);
        //    row.Cells.Add(symptomValueCell);
        //    table.Rows.Add(row);
        //}
    }

    private bool isSymptom(Condition condition)
    {
        foreach (String symptom in Symptom.symptomNames)
        {
            if (symptom.Equals(condition.Name.Text))
                return true;
        }
        return false;
    }

    protected void Page_Error(object sender, EventArgs e)
    {
        Exception ex = Server.GetLastError();

        if (ex is HealthServiceCredentialTokenExpiredException)
        {
            WebApplicationUtilities.RedirectToLogOn(HttpContext.Current);
        }

        // ShowError(ex);
        throw ex;
    }

}