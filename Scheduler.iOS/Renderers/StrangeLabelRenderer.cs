using Scheduler.CustomControlls;
using Scheduler.iOS.Renderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(BackgroundLabel), typeof(StrangeLabelRenderer))]
namespace Scheduler.iOS.Renderers
{
    public class StrangeLabelRenderer : LabelRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Label> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                Control.BackgroundColor = UIColor.FromRGB(204, 153, 255);
            }
        }
    }
}
