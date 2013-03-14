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
        HealthRecordSearcher searcher = PersonInfo.SelectedRecord.CreateSearcher();

        HealthRecordFilter filter = new HealthRecordFilter(customTypeId);
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
        TableHeaderCell headerNumberCell = new TableHeaderCell();
        headerNumberCell.Text = "Number";
        headerRow.Cells.Add(headerNumberCell);

        TableHeaderCell headerPainCell = new TableHeaderCell();
        headerPainCell.Text = "Pain Scale";
        headerRow.Cells.Add(headerPainCell);

        c_PainSummaryTable.Rows.Add(headerRow);

        //foreach (PainScale painScale in painScaleSummary)
        for (int i = 0; i < painScaleSummary.Count; i++ )
        {
            PainScale painScale = painScaleSummary[i];
            TableRow row = new TableRow();
            c_PainSummaryTable.Rows.Add(row);

            TableHeaderCell headerRowNumberCell = new TableHeaderCell();
            headerRowNumberCell.Text = i.ToString();
            row.Cells.Add(headerRowNumberCell);

            TableCell painCell = new TableCell();
            painCell.Text = String.Format("{0:F2}", painScale.PainThreshold);
            row.Cells.Add(painCell);
        }
    }
}