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

    //protected void addPainScale(object sender, EventArgs e)
    //{
    //    PainScale painScale = new PainScale();
    //    painScale.PainThreshold = Convert.ToInt32(c_pain.Text);
    //    painScale.NauseaThreshold = Convert.ToInt32(c_nausea.Text);
    //    painScale.SleepThreshold = Convert.ToInt32(c_sleep.Text);
    //    painScale.FatigueThreshold = Convert.ToInt32(c_fatigue.Text);
    //    painScale.ConstipationThreshold = Convert.ToInt32(c_constipation.Text);

    //    CustomHealthTypeWrapper wrapper = new CustomHealthTypeWrapper(painScale);
    //    PersonInfo.SelectedRecord.NewItem(wrapper);
    //}


    protected void addPainScale(object sender, EventArgs e)
    {
        String[] symptomNames = new String[] {"Pain", "Nausea", "Sleep", "Faigue", "Consptipation"};
        String[] symptomValues = new String[] { c_pain.Text, c_nausea.Text, c_sleep.Text, c_fatigue.Text, c_constipation.Text };

        for (int i = 0; i < 5; i++)
        {
            Condition condition = new Condition();
            CodableValue symptomName = new CodableValue(symptomNames[i]);
            condition.Name = symptomName;
            ApproximateDateTime now = new ApproximateDateTime(DateTime.Now);
            condition.OnsetDate = now;
            CodableValue symptomValue = new CodableValue(symptomValues[i]);
            condition.Status = symptomValue;
            PersonInfo.SelectedRecord.NewItem(condition);
        }
    }
}