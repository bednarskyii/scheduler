using System;
using CoreAnimation;
using CoreGraphics;
using Scheduler.CustomControlls;
using Scheduler.iOS.Renderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(StrangeFrame), typeof(StrangeFrameRenderer))]
namespace Scheduler.iOS.Renderers
{
    public class StrangeFrameRenderer : FrameRenderer
    {
        public override void LayoutSublayersOfLayer(CALayer layer)
        {
            base.LayoutSublayersOfLayer(layer);

            //left side
            var path = UIBezierPath.FromRoundedRect(this.Bounds, UIRectCorner.TopLeft | UIRectCorner.BottomLeft, new CGSize(0, 0));
            var mask = new CAShapeLayer();
            mask.Path = path.CGPath;
            this.Layer.Mask = mask;
            //right side
            this.ClipsToBounds = true;
            this.Layer.CornerRadius = 10;
            this.Layer.MaskedCorners = CACornerMask.MaxXMaxYCorner | CACornerMask.MaxXMinYCorner;
        }
    }
}
