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

public partial class PainScaleSummary : HealthServicePage
{
    Guid customTypeId = new Guid("a5033c9d-08cf-4204-9bd3-cb412ce39fc0");
    List<PainScale> painScaleSummary = new List<PainScale>();

    protected void Page_Load(object sender, EventArgs e)
    {
        getPainScaleSummary();
        PopulateHeightTable();
    }

    protected void getPainScaleSummary()
    {
        DateTime from;
        DateTime to;
        String from_date_string = from_date.Text;
        String to_date_string = to_date.Text;
        if (String.IsNullOrEmpty(from_date_string) || String.IsNullOrEmpty(to_date_string))
        {
            to = DateTime.Now;
            from = DateTime.Now.AddDays(-7);
        }
        else
        {
            to = DateTime.Parse(to_date_string + " 11:59:59 PM");
            from = DateTime.Parse(from_date_string + " 00:00:01 AM");
        }
        HealthRecordSearcher searcher = PersonInfo.SelectedRecord.CreateSearcher();

        HealthRecordFilter filter = new HealthRecordFilter(customTypeId);
        filter.CreatedDateMax= to;
        filter.CreatedDateMin = from;
        searcher.Filters.Add(filter);

        HealthRecordItemCollection items = searcher.GetMatchingItems()[0];
        foreach (HealthRecordItem item in items)
        {
            CustomHealthTypeWrapper wrapper = (CustomHealthTypeWrapper)item;
            PainScale painScale = wrapper.WrappedObject as PainScale;
            if (painScale != null)
                painScaleSummary.Add(painScale);
        }
    }

    void PopulateHeightTable()
    {
        c_PainSummaryTable.Rows.Clear();

        TableHeaderRow headerRow = new TableHeaderRow();
        TableHeaderCell headerWhenCell = new TableHeaderCell();
        headerWhenCell.Text = "When";
        headerRow.Cells.Add(headerWhenCell);

        TableHeaderCell headerPainCell = new TableHeaderCell();
        headerPainCell.Text = "Pain Scale";
        headerRow.Cells.Add(headerPainCell);

        TableHeaderCell headerNauseaCell = new TableHeaderCell();
        headerNauseaCell.Text = "Nausea Scale";
        headerRow.Cells.Add(headerNauseaCell);

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
        painScaleSummary.Sort(delegate(PainScale p1, PainScale p2) { return p1.When.CompareTo(p2.When); });

        //foreach (PainScale painScale in painScaleSummary)
        for (int i = 0; i < painScaleSummary.Count; i++)
        {
            PainScale painScale = painScaleSummary[i];
            TableRow row = new TableRow();
            c_PainSummaryTable.Rows.Add(row);

            TableHeaderCell headerDateCell = new TableHeaderCell();
            headerDateCell.Text = painScale.When.ToString();
            row.Cells.Add(headerDateCell);

            TableCell painCell = new TableCell();
            painCell.Text = String.Format("{0:F2}", painScale.PainThreshold);
            row.Cells.Add(painCell);

            TableCell nauseaCell = new TableCell();
            nauseaCell.Text = String.Format("{0:F2}", painScale.NauseaThreshold);
            row.Cells.Add(nauseaCell);

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