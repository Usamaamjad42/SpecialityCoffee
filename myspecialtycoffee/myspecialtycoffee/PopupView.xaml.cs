using myspecialtycoffee.Model;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace myspecialtycoffee
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PopupView : PopupPage
	{
        TaskCompletionSource<bool> _tcs = null;
        public PopupView ()
		{
			InitializeComponent ();
		}

        private void btnHandel_Clicked(object sender, EventArgs e)
        {       
            try
            {

                PopupNavigation.Instance.PopAsync(true);
                _tcs?.SetResult(true);

                var Fdate = txt_fromDate.Date;
                var Tdate = txt_toDate.Date;

                MessagingCenter.Send(new DateDetailsModel() { fdate = Fdate, tdate = Tdate }, "PopUpDateData");
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            //var fdate = txt_fromDate.Date;
            //var tdate = txt_toDate.Date;

            //DateTime StartingDate = DateTime.Parse(fdate.ToShortDateString());
            //DateTime EndingDate = DateTime.Parse(tdate.ToShortDateString());

            //GetDateRange(StartingDate, EndingDate);

        }

        private void GetDateRange(DateTime StartingDate, DateTime EndingDate)
        {
            if (StartingDate > EndingDate)
            {
                return;
            }

            EndingDate = EndingDate.AddDays(1);

            Sales sales = new Sales();

            sales.LoadDataDateWise(StartingDate.ToShortDateString(), EndingDate.ToShortDateString());

        }

        public async Task<bool> Show()
        {
            _tcs = new TaskCompletionSource<bool>();
            await Navigation.PushPopupAsync(this);
      
            return await _tcs.Task;
        }

    }
}