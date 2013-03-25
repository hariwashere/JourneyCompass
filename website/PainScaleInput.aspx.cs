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



public partial class PainScaleInput : HealthServicePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
    }

    protected void addPainScale(object sender, EventArgs e)
    {
        PainScale painScale = new PainScale();
        painScale.PainThreshold = Convert.ToInt32(c_pain.Text);
        painScale.NauseaThreshold = Convert.ToInt32(c_nausea.Text);
        painScale.SleepThreshold = Convert.ToInt32(c_sleep.Text);
        painScale.FatigueThreshold = Convert.ToInt32(c_fatigue.Text);
        painScale.ConstipationThreshold = Convert.ToInt32(c_constipation.Text);

        CustomHealthTypeWrapper wrapper = new CustomHealthTypeWrapper(painScale);
        PersonInfo.SelectedRecord.NewItem(wrapper);
    }
}