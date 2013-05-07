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
     List<SymptomScale> symptomScaleSummary = new List<SymptomScale>();

    protected void Page_Load(object sender, EventArgs e)
    {
        populatePatientData();
        getSymptomSummary();
        getBasicInfo();
        PopulateSymptomTable();
    }

    protected void populatePatientData()
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
        for (int i = 0; i < items.Count; )
        {
            Condition condition = (Condition)items[i];
            if (isSymptom(condition))
            {
                SymptomScale symptomScale = new SymptomScale();
                symptomScale.ConstipationThreshold = Convert.ToInt32(((Condition)items[i]).Status.Text);
                symptomScale.FatigueThreshold = Convert.ToInt32(((Condition)items[i + 1]).Status.Text);
                symptomScale.SleepThreshold = Convert.ToInt32(((Condition)items[i + 2]).Status.Text);
                symptomScale.NauseaThreshold = Convert.ToInt32(((Condition)items[i + 3]).Status.Text);
                symptomScale.PainThreshold = Convert.ToInt32(((Condition)items[i + 4]).Status.Text);
                symptomScale.When = DateTime.Parse(condition.OnsetDate.ToString());
                i += 5;
                symptomScaleSummary.Add(symptomScale);
            }
            else
                i += 1;
        }
    }

    private bool isSymptom(Condition condition)
    {
        foreach(String symptom in SymptomScale.symptomNames){
            if(symptom.Equals(condition.Name.Text))
                return true;
        }
        return false;
    }
    void PopulateSymptomTable()
    {
        c_PainSummaryTable.Rows.Clear();
        nausea_summary_table.Rows.Clear();
        TableHeaderRow nauseaHeaderRow = new TableHeaderRow();
        TableHeaderCell nauseaWhenCell = new TableHeaderCell();
        nauseaWhenCell.Text = "When";
        nauseaHeaderRow.Cells.Add(nauseaWhenCell);

        TableHeaderRow headerRow = new TableHeaderRow();
        TableHeaderCell headerWhenCell = new TableHeaderCell();
        headerWhenCell.Text = "When";
        headerRow.Cells.Add(headerWhenCell);

        TableHeaderCell headerPainCell = new TableHeaderCell();
        headerPainCell.Text = "Pain Scale";
        headerRow.Cells.Add(headerPainCell);

        TableHeaderCell headerNauseaCell = new TableHeaderCell();
        headerNauseaCell.Text = "Nausea Scale";
        nauseaHeaderRow.Cells.Add(headerNauseaCell);

        //TableHeaderCell headerNauseaCell = new TableHeaderCell();
        //headerNauseaCell.Text = "Nausea Scale";
        //headerRow.Cells.Add(headerNauseaCell);

        TableHeaderCell headerFatigueCell = new TableHeaderCell();
        headerFatigueCell.Text = "Fatigue Scale";
        headerRow.Cells.Add(headerFatigueCell);

        TableHeaderCell headerSleepCell = new TableHeaderCell();
        headerSleepCell.Text = "Sleep Scale";
        headerRow.Cells.Add(headerSleepCell);

        TableHeaderCell headerConstipationCell = new TableHeaderCell();
        headerConstipationCell.Text = "Constipation Scale";
        headerRow.Cells.Add(headerConstipationCell);

        c_PainSummaryTable.Rows.Add(headerRow);
        nausea_summary_table.Rows.Add(nauseaHeaderRow);
        symptomScaleSummary.Sort(delegate(SymptomScale p1, SymptomScale p2) { return p1.When.CompareTo(p2.When); });

        for (int i = 0; i < symptomScaleSummary.Count; i++)
        {
            SymptomScale painScale = symptomScaleSummary[i];
            TableRow row = new TableRow();
            TableRow nauseaRow = new TableRow();
            c_PainSummaryTable.Rows.Add(row);
            nausea_summary_table.Rows.Add(nauseaRow);

            TableHeaderCell headerDateCell = new TableHeaderCell();
            headerDateCell.Text = painScale.When.ToString();
            TableHeaderCell headerNauseaDateCell = new TableHeaderCell();
            headerNauseaDateCell.Text = painScale.When.ToString();
            row.Cells.Add(headerDateCell);
            nauseaRow.Cells.Add(headerNauseaDateCell);

            TableCell painCell = new TableCell();
            painCell.Text = String.Format("{0:F2}", painScale.PainThreshold);
            row.Cells.Add(painCell);

            TableCell nauseaCell = new TableCell();
            nauseaCell.Text = String.Format("{0:F2}", painScale.NauseaThreshold);
            nauseaRow.Cells.Add(nauseaCell);

            TableCell fatigueCell = new TableCell();
            fatigueCell.Text = String.Format("{0:F2}", painScale.FatigueThreshold);
            row.Cells.Add(fatigueCell);


            TableCell sleepCell = new TableCell();
            sleepCell.Text = String.Format("{0:F2}", painScale.SleepThreshold);
            row.Cells.Add(sleepCell);

            TableCell constipationCell = new TableCell();
            constipationCell.Text = String.Format("{0:F2}", painScale.ConstipationThreshold);
            row.Cells.Add(constipationCell);
        }
    }
}