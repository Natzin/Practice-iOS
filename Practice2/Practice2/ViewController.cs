using System;

using UIKit;
using System.Drawing;
using System.Threading.Tasks;
using AVFoundation;
using Foundation;
using System.IO;
using System.Linq;
using System.Diagnostics.Contracts;
using CoreLocation;
using MapKit;
using LocalAuthentication;
using ObjCRuntime;

namespace Practice2
{
    public partial class ViewController : UIViewController
    {
        //                                                  //Variables
        //*
        String strLocalFile;
        AVCaptureDevice acdDevice;
        AVCaptureSession acsSession;
        AVCaptureDeviceInput deviceInput;
        AVCaptureStillImageOutput imageOutput;
        AVCaptureVideoPreviewLayer previewLayer;
        String strLocator;
        String strCountry;
        String strCity;
        Double dblLatitude;
        Double dblLongitude;
        Byte[] arrJpg;
        CLLocationManager cLLocation;
        NSError nsError;
        //*/

        //                                                  //Method to create alertView and reuse it
        public static void MessageBox(String strTitle_I, String strMessage_I)
        {
            UIAlertView alertView = new UIAlertView();
            alertView.Title = strTitle_I;
            alertView.Message = strMessage_I;
            alertView.AddButton("OK");
            alertView.Show();
        }

        protected ViewController(IntPtr handle) : base(handle)
        {
        }

        //                                                  //We convert the ViewLoad method into async by the biometric
        //                                                  //  authorization of the camera
        public override async void ViewDidLoad()
        {
            base.ViewDidLoad();

            try
            {
                //                                          //Biometric Authorizacion (TouchID or FaceID Authorization)
                var Verify = new LocalAuthentication.LAContext();
                //                                          //Use the Policy of Biometrics Authorization
                var auth = await Verify.EvaluatePolicyAsync
                                       (LAPolicy.DeviceOwnerAuthenticationWithBiometrics,
                                            "authentication to use the application");
                //                                          //Camera Authorization after Biometrics Authorization
                if (
                    auth.Item1
                )
                {
                    await CameraAuterization();
                    ConfigurationCamera();
                }
                else
                {
                    //                                      //Close de App
                    Selector selector = new Selector("terminateWithSuccess");
                    UIApplication.SharedApplication.PerformSelector(selector, UIApplication.SharedApplication, 0);
                }
            }
            //                                              //Access fail
            catch (NSErrorException ex)
            {
                var reason = Convert.ToInt16(ex.Code);
                var status = (LAStatus)reason;

                MessageBox("Status", status.ToString());
            }

            this.btnCapture.TouchUpInside += BtnCapture_TouchUpInside;

            this.btnUpload.TouchUpInside += BtnUpload_TouchUpInside;

            //                                              //Camera properties
            this.sldTint.MinValue = 1000f;
            this.sldTint.MaxValue = 1000f;
            this.sldTint.ValueChanged += ValueChanged;

            this.sldTemperature.MinValue = -150f;
            this.sldTemperature.MaxValue = 150f;
            this.sldTemperature.ValueChanged += ValueChanged;

            this.sldBrightness.MinValue = acdDevice.ActiveFormat.MinISO;
            this.sldBrightness.MaxValue = acdDevice.ActiveFormat.MaxISO;
            this.sldBrightness.ValueChanged += SldBrightness_ValueChanged;
        }

        //                                                  //Camera Dye and Temperature
        protected void ValueChanged(object sender, EventArgs e)
        {
            var TempAndTint = new AVCaptureWhiteBalanceTemperatureAndTintValues
                                (this.sldTemperature.Value, this.sldTint.Value);
            var StatusDev = acdDevice.GetDeviceWhiteBalanceGains(TempAndTint);

            if (
                acdDevice.LockForConfiguration(out nsError)
            )
            {
                StatusDev = Settings(StatusDev);
                acdDevice.SetWhiteBalanceModeLockedWithDeviceWhiteBalanceGains(StatusDev, null);
                acdDevice.UnlockForConfiguration();
            }
            sldTint.BeginInvokeOnMainThread(() =>
            {
                sldTint.Value = TempAndTint.Tint;
            });
            sldTemperature.BeginInvokeOnMainThread(() =>
            {
                sldTemperature.Value = 
            });
        }

        AVCaptureWhiteBalanceGains Settings(AVCaptureWhiteBalanceGains statusDev)
        {
            statusDev.RedGain = Math.Max(1, statusDev.RedGain);
            statusDev.BlueGain = Math.Max(1, statusDev.BlueGain);
            statusDev.GreenGain = Math.Max(1, statusDev.GreenGain);


        }


        //                                                  //Camera Brigthness
        void SldBrightness_ValueChanged(object sender, EventArgs e)
        {
            if (
                acdDevice.LockForConfiguration(out nsError)
            )
            {
                //                                          //Camera Values update
                acdDevice.LockExposure(acdDevice.ExposureDuration, sldBrightness.Value, null);
                //                                          //Release Resourse
                acdDevice.UnlockForConfiguration();
            }
        }


        //                                                  //Camera access Authozation
        //*
        async Task CameraAuterization()
        {
            AVAuthorizationStatus acvStatus = AVCaptureDevice.GetAuthorizationStatus(AVMediaType.Video);
            if (
                acvStatus != AVAuthorizationStatus.Authorized
            )
            {
                await AVCaptureDevice.RequestAccessForMediaTypeAsync(AVMediaType.Video);
            }
        }//*/

        //                                                  //Camera can't use in simulator
        //*
        public void ConfigurationCamera()
        {
            acsSession = new AVCaptureSession();
            previewLayer = new AVCaptureVideoPreviewLayer(acsSession)
            {
                Frame = new RectangleF(30, 40, 300, 350)
            };
            View.Layer.AddSublayer(previewLayer);
            acdDevice = AVCaptureDevice.GetDefaultDevice(AVMediaType.Video);
            deviceInput = AVCaptureDeviceInput.FromDevice(acdDevice);
            acsSession.AddInput(deviceInput);
            imageOutput = new AVCaptureStillImageOutput()
            {
                OutputSettings = new NSDictionary()
            };
            acsSession.AddOutput(imageOutput);
            acsSession.StartRunning();
        }//*/

        protected void BtnUpload_TouchUpInside(object sender, EventArgs e)
        {
            MessageBox("Complete", "Update date file complete");
        }

        //*
        protected async void BtnCapture_TouchUpInside(object sender, EventArgs e)
        {
            var VideoOut = imageOutput.ConnectionFromMediaType(AVMediaType.Video);
            var VideoBuffer = await imageOutput.CaptureStillImageTaskAsync(VideoOut);
            var ImageData = AVCaptureStillImageOutput.JpegStillToNSData(VideoBuffer);

            arrJpg = ImageData.ToArray();
            String strFile = Environment.GetFolderPath(Environment.SpecialFolder.Personal);

            String strResult = "Image";
            strLocalFile = strResult + ".jpg";
            strLocator = Path.Combine(strFile, strLocalFile);
            File.WriteAllBytes(strLocator, arrJpg);
            imgPicture.Image = UIImage.FromFile(strLocator);
        }//*/


        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
        }


    }
}
