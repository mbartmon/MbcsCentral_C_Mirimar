using System;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using System.Collections.Generic;

    public static class VisualExtensions
    {
        public static FrameworkElement Clone(this FrameworkElement referenceElement, bool shareRenderingData)
        {
            return new InternalElement(referenceElement, shareRenderingData);
        }

        internal class InternalElement : FrameworkElement
        {
            private Visual rootVisual;
            private bool shareRenderingData;

            public InternalElement(Visual rootVisual, bool shareRenderingData)
            {
                this.rootVisual = rootVisual;
                this.shareRenderingData = shareRenderingData;
            }

            protected override Size MeasureOverride(Size availableSize)
            {
                var fe = rootVisual as FrameworkElement;
                if (fe != null && !fe.IsLoaded)
                {
                    fe.ApplyTemplate();
                }

                UIElement element = rootVisual as UIElement;
                if (element != null && element.IsMeasureValid)
                {
                    return element.DesiredSize;
                }

                return base.MeasureOverride(availableSize);
            }

            protected override Size ArrangeOverride(Size finalSize)
            {
                var fe = rootVisual as FrameworkElement;
                if (fe != null && fe.IsArrangeValid)
                {
                    return new Size(fe.ActualWidth, fe.ActualHeight);
                }

                UIElement element = rootVisual as UIElement;
                if (element != null && element.IsArrangeValid)
                {
                    return element.RenderSize;
                }

                return base.ArrangeOverride(finalSize);
            }

            protected override void OnRender(DrawingContext drawingContext)
            {
                if (rootVisual != null)
                {
                    SendToContext(rootVisual, rootVisual, drawingContext, shareRenderingData);
                }
            }

            private static T IfShareFreezable<T>(T freezable, bool shareRenderingData) where T : Freezable
            {
                T result = freezable;
                if (result != null && !shareRenderingData)
                {
                    result = (T)freezable.CloneCurrentValue();
                    if (freezable.CanFreeze)
                    {
                        result.Freeze();
                    }
                }
                return result;
            }

            private static void SendToContext(Visual parent, Visual child, DrawingContext drawingContext, bool shareRenderingData)
            {
                var drawing = IfShareFreezable(VisualTreeHelper.GetDrawing(child), shareRenderingData);
                var transform = child.TransformToAncestor(parent) as Transform;
                var effect = IfShareFreezable(VisualTreeHelper.GetBitmapEffect(child), shareRenderingData);
                var clip = IfShareFreezable(VisualTreeHelper.GetClip(child), shareRenderingData);
                var opacity = VisualTreeHelper.GetOpacity(child);
                var opacityMask = IfShareFreezable(VisualTreeHelper.GetOpacityMask(child), shareRenderingData);
                var guidelinesX = IfShareFreezable(VisualTreeHelper.GetXSnappingGuidelines(child), shareRenderingData);
                var guidelinesY = IfShareFreezable(VisualTreeHelper.GetYSnappingGuidelines(child), shareRenderingData);
                var pushCounter = 0;

                if (transform != null && transform != Transform.Identity)
                {
                    drawingContext.PushTransform(transform);
                    pushCounter++;
                }

                if (effect != null)
                {
                    drawingContext.PushEffect(effect, null);
                    pushCounter++;
                }

                if (clip != null)
                {
                    drawingContext.PushClip(clip);
                    pushCounter++;
                }

                if (opacity >= 0 && (int)opacity < 1)
                {
                    drawingContext.PushOpacity(opacity);
                    pushCounter++;
                }

                if (opacityMask != null)
                {
                    drawingContext.PushOpacityMask(opacityMask);
                    pushCounter++;
                }

                if (guidelinesX != null && guidelinesY != null)
                {
                    drawingContext.PushGuidelineSet(new GuidelineSet()
                    {
                        GuidelinesX = guidelinesX,
                        GuidelinesY = guidelinesY
                    });
                    pushCounter++;
                }

                if (drawing != null) drawingContext.DrawDrawing(drawing);

                var count = VisualTreeHelper.GetChildrenCount(child);
                for (int i = 0; i < count; i++)
                {
                    Visual visual = VisualTreeHelper.GetChild(child, i) as Visual;
                    if (visual != null)
                    {
                        SendToContext(child, visual, drawingContext, shareRenderingData);
                    }
                }

                while (pushCounter-- > 0)
                {
                    drawingContext.Pop();
                }
            }
        }
    }
