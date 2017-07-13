using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Composition;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Hosting;

namespace Banhuizhushou.B_CLASS
{
    class UiBlur
    {
        public static void getblur(UIElement ui)
        {
            Visual hostvisual = ElementCompositionPreview.GetElementVisual(ui);
            Compositor compositor = hostvisual.Compositor;
            var backdropbrush = compositor.CreateHostBackdropBrush();
            var glassvisual = compositor.CreateSpriteVisual();
            glassvisual.Brush = backdropbrush;
            ElementCompositionPreview.SetElementChildVisual(ui, glassvisual);
            var bindSizeAnimation = compositor.CreateExpressionAnimation("hostvisual.size");
            bindSizeAnimation.SetReferenceParameter("hostVisual", hostvisual);
            glassvisual.StartAnimation("size", bindSizeAnimation);
        }
    }
}
