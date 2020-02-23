using Syncfusion.SfDataGrid.XForms;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace myspecialtycoffee
{
    class CustomStyleSyncfusion : DataGridStyle
    {
        public override Color GetHeaderBackgroundColor()
        {
            return Color.FromHex("#B97B24");
        }

        public override Color GetHeaderForegroundColor()
        {
            return Color.White;
        }

        public override Color GetAlternatingRowBackgroundColor()
        {
            return Color.FromHex("#f1e4d3");
        }

        public override Color GetCaptionSummaryRowBackgroundColor()
        {
            return Color.FromHex("#d5af7b");
        }

        public override Color GetCaptionSummaryRowForegroundColor()
        {
            return Color.White;
        }
    }
}
