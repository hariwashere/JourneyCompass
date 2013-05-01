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
        if (IsPostBack)
        {
        }
        try
        {
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
    }
    protected void addSymptoms(object sender, EventArgs e)
    {
        Response.Redirect("SymptomInput.aspx");
    }

    protected void viewSymptomSummary(object sender, EventArgs e)
    {
        Response.Redirect("SymptomSummary.aspx");
    }
}
