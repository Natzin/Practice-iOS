using Foundation;
using System;
using UIKit;
using System.Collections.Generic;

namespace Practice1
{
    public partial class CallHistoryController : UITableViewController
    {
        public List<String> lstPhoneNumbers_z;
        private List<String> lstPhoneNumbers { get { return lstPhoneNumbers_z; } }

        private static NSString CallHistoryCellId = new NSString("CallHistoryCell");

        public CallHistoryController(IntPtr handle) : base(handle)
        {
            TableView.RegisterClassForCellReuse(typeof(UITableViewCell), CallHistoryCellId);
            TableView.Source = new CallHistoryDataSource(this);
            lstPhoneNumbers_z = new List<string>();
        }

        class CallHistoryDataSource : UITableViewSource
        {
            CallHistoryController controller;

            public CallHistoryDataSource(CallHistoryController controller)
            {
                this.controller = controller;
            }

            public override nint RowsInSection(UITableView tableview, nint section)
            {
                return controller.lstPhoneNumbers.Count;
            }

			public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
			{
                var cell = tableView.DequeueReusableCell(CallHistoryController.CallHistoryCellId);

                int row = indexPath.Row;
                cell.TextLabel.Text = controller.lstPhoneNumbers[row];
                return cell;
			}
		}
    }
}