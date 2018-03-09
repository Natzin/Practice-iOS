// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;

namespace Practice2
{
    [Register ("ViewController")]
    partial class ViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton btnCapture { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton btnUpload { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIImageView imgPicture { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UISlider sldBrightness { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UISlider sldTemperature { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UISlider sldTint { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (btnCapture != null) {
                btnCapture.Dispose ();
                btnCapture = null;
            }

            if (btnUpload != null) {
                btnUpload.Dispose ();
                btnUpload = null;
            }

            if (imgPicture != null) {
                imgPicture.Dispose ();
                imgPicture = null;
            }

            if (sldBrightness != null) {
                sldBrightness.Dispose ();
                sldBrightness = null;
            }

            if (sldTemperature != null) {
                sldTemperature.Dispose ();
                sldTemperature = null;
            }

            if (sldTint != null) {
                sldTint.Dispose ();
                sldTint = null;
            }
        }
    }
}