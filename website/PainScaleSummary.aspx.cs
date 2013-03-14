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
    protected void Page_Load(object sender, EventArgs e)
    {
        getROB();
    }
    protected void getROB()
    {
        HealthRecordSearcher searcher = PersonInfo.SelectedRecord.CreateSearcher();

        HealthRecordFilter filter = new HealthRecordFilter(customTypeId);
        searcher.Filters.Add(filter);

        HealthRecordItemCollection items = searcher.GetMatchingItems()[0];
        int pain = 999;
        if (items != null && items.Count > 0)
        {
            CustomHealthTypeWrapper wrapper = (CustomHealthTypeWrapper)items[0];
            PainScale painScale = wrapper.WrappedObject as PainScale;
            if (painScale != null)

                pain = painScale.PainThreshold;
        }

        c_pscale_text.Text = String.Format("{0:F2}", pain);
    }
}