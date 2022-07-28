using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using Syncfusion.XForms.iOS.EffectsView;
using Syncfusion.XForms.iOS.TextInputLayout;
using UIKit;

namespace InvernaderoApp.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            global::Xamarin.Forms.Forms.Init();
            this.OnInit();
            LoadApplication(new App());

            return base.FinishedLaunching(app, options);
        }

        private void OnInit()
        {
            Rg.Plugins.Popup.Popup.Init();
            XF.Material.iOS.Material.Init();
            SfTextInputLayoutRenderer.Init();
            FFImageLoading.Forms.Platform.CachedImageRenderer.Init();
            Syncfusion.XForms.iOS.Core.SfAvatarViewRenderer.Init();
            SfEffectsViewRenderer.Init();
            Syncfusion.XForms.iOS.Buttons.SfButtonRenderer.Init();
            Syncfusion.XForms.iOS.Cards.SfCardViewRenderer.Init();
            Syncfusion.SfGauge.XForms.iOS.SfGaugeRenderer.Init();
            Syncfusion.XForms.iOS.Buttons.SfSwitchRenderer.Init();
        }
    }
}
