using System;
using Practice1;
using UIKit;
using Foundation;
using System.Collections.Generic;

namespace Practice1
{
    public partial class ViewController : UIViewController
    {
        private String strTranslateNumber_z;
        private String strTranslateNumber { get { return strTranslateNumber_z; } }

        private List<String> lstPhoneNumbers_z;
        private List<String> lstPhoneNumbers { get { return lstPhoneNumbers_z; } }

        protected ViewController(IntPtr handle) : base(handle)
        {
            lstPhoneNumbers_z = new List<string>();
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            btnTanslate.TouchUpInside += btnTanslate_TouchUpInside;

            btnCall.TouchUpInside += btnCall_TouchUpInside;

            btnHistoryCall.TouchUpInside += btnHistoryCall_TouchUpInside;
        }

        private void btnHistoryCall_TouchUpInside(object sender, EventArgs e)
        {
            CallHistoryController chcHistory = this.Storyboard.InstantiateViewController("CallHistoryController") as CallHistoryController;

            if (
                chcHistory != null
            )
            {
                chcHistory.lstPhoneNumbers_z = this.lstPhoneNumbers;
                this.NavigationController.PushViewController(chcHistory, true);
            }
        }


        //public override void PrepareForSegue(UIStoryboardSegue segue, NSObject sender)
        //{
        //          base.PrepareForSegue(segue, sender);

        //          CallHistoryController chcHistory = segue.DestinationViewController as CallHistoryController;

        //          if (
        //              chcHistory != null
        //          )
        //          {
        //              chcHistory.lstPhoneNumbers_z = this.lstPhoneNumbers;
        //          }
        //}

        private void btnTanslate_TouchUpInside(object sender, EventArgs e)
        {
            strTranslateNumber_z = PhoneTranslator.ToNumber(txtPhoneNumber.Text);

            txtPhoneNumber.ResignFirstResponder();

            if (
                strTranslateNumber == ""
            )
            {
                btnCall.SetTitle("Call ", UIControlState.Normal);
                btnCall.Enabled = false;
            }
            else
            {
                btnCall.SetTitle("Call " + strTranslateNumber, UIControlState.Normal);
                btnCall.Enabled = true;
            }
        }

        private void btnCall_TouchUpInside(object sender, EventArgs e)
        {
            this.lstPhoneNumbers_z.Add(strTranslateNumber);

            NSUrl nsrURL = new NSUrl("tel:" + strTranslateNumber);
            if (
                !UIApplication.SharedApplication.OpenUrl(nsrURL)
            )
            {
                UIAlertController alcAlert = UIAlertController.Create("Not supported", "Scheme 'tel:' is not supported on this device", UIAlertControllerStyle.Alert);
                alcAlert.AddAction(UIAlertAction.Create("OK", UIAlertActionStyle.Default, null));
                PresentViewController(alcAlert, true, null);
            }
        }


        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
        }
    }
}
