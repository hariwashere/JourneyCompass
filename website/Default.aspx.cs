/********************************************************************++

Copyright (c) Microsoft Corporation. All rights reserved.

************************************************************************/

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


public partial class _Default : HealthServicePage
{
    
    protected void Page_Load(object sender, EventArgs e)
    {
        //CreateChildApplication.CreateApplication();
        if (IsPostBack)
        {
            //AddHeightEntry();
            //AddPeakFlowZone();
        }
        try
        {
            //ItemTypeManager.RegisterTypeHandler(customTypeId, typeof(PeakFlowZone), true);
            CustomHealthTypeWrapper.RegisterCustomDataType();
            ApplicationInfo info = ApplicationConnection.GetApplicationInfo();
            AppName.Text += info.Name;
            c_UserName.Text = PersonInfo.Name;

            StartupData.SetActiveView(StartupData.Views[0]);
        }
        catch (HealthServiceException ex)
        {
            Error.Text += ex.ToString();
            StartupData.SetActiveView(StartupData.Views[1]);
        }
        //getHeight();
        //getROB();
    }
    protected void addPainScale(object sender, EventArgs e)
    {
        Response.Redirect("PainScaleInput.aspx");
    }

    protected void viewSymptomSummary(object sender, EventArgs e)
    {
        Response.Redirect("SymptomSummary.aspx");
    }

    protected void AddHeightEntry()
    {
        String heightText = "22";//c_Height.Text;
        Length value = new Length(Convert.ToDouble(heightText));
        Height height = new Height(new HealthServiceDateTime(DateTime.Now), value);
        PersonInfo.SelectedRecord.NewItem(height);
    }

    //protected void AddPeakFlowZone()
    //{
    //    PainScale painScale = new PainScale(5);
    //    CustomHealthTypeWrapper wrapper = new CustomHealthTypeWrapper(painScale);
    //    //PeakFlowZone pfz = new PeakFlowZone(1.1, 2.2, 3.3, new HealthServiceDateTime());
    //    PersonInfo.SelectedRecord.NewItem(wrapper);
    //    //String heightText = c_Height.Text;
    //    //Length value = new Length(Convert.ToDouble(heightText));
    //    //Height height = new Height(new HealthServiceDateTime(DateTime.Now), value);
    //    //PersonInfo.SelectedRecord.NewItem(height);
    //}

    //protected void getHeight()
    //{
    //    HealthRecordSearcher searcher = PersonInfo.SelectedRecord.CreateSearcher();

    //    HealthRecordFilter filter = new HealthRecordFilter(Height.TypeId);
    //    searcher.Filters.Add(filter);

    //    HealthRecordItemCollection items = searcher.GetMatchingItems()[0];
    //    double height = 0.0;
    //    if (items != null && items.Count > 0)
    //        height = ((Height)items[0]).Value.Meters;
    //    c_height_text.Text = String.Format("{0:F2}", height);
    //}

    //protected void getROB()
    //{
    //    HealthRecordSearcher searcher = PersonInfo.SelectedRecord.CreateSearcher();

    //    HealthRecordFilter filter = new HealthRecordFilter(customTypeId);
    //    searcher.Filters.Add(filter);

    //    HealthRecordItemCollection items = searcher.GetMatchingItems()[0];
    //    int pain = 999;
    //    if (items != null && items.Count > 0)
    //    {
    //        CustomHealthTypeWrapper wrapper = (CustomHealthTypeWrapper)items[0];
    //        PainScale painScale = wrapper.WrappedObject as PainScale;
    //        if (painScale != null)

    //            pain = painScale.PainThreshold;
    //    }

    //    c_rob_text.Text = String.Format("{0:F2}", pain);
    //}
}
