using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace CrispyLo.Modules
{
    internal class VisuelCSS
    {
        public static ControlTemplate CreateButtonStyleTemplate()
        {
            ControlTemplate template = new ControlTemplate(typeof(RadioButton));

            // Créer le Border
            FrameworkElementFactory border = new FrameworkElementFactory(typeof(Border));
            border.Name = "border";
            border.SetBinding(Border.BackgroundProperty, new Binding("Background") { RelativeSource = RelativeSource.TemplatedParent });
            border.SetValue(Border.BorderBrushProperty, Brushes.Gray);
            border.SetValue(Border.BorderThicknessProperty, new Thickness(1));
            border.SetBinding(Border.PaddingProperty, new Binding("Padding") { RelativeSource = RelativeSource.TemplatedParent });
            border.SetValue(Border.CornerRadiusProperty, new CornerRadius(3));

            // Créer le ContentPresenter
            FrameworkElementFactory contentPresenter = new FrameworkElementFactory(typeof(ContentPresenter));
            contentPresenter.SetValue(FrameworkElement.HorizontalAlignmentProperty, HorizontalAlignment.Center);
            contentPresenter.SetValue(FrameworkElement.VerticalAlignmentProperty, VerticalAlignment.Center);

            border.AppendChild(contentPresenter);
            template.VisualTree = border;

            // Trigger IsChecked
            Trigger checkedTrigger = new Trigger();
            checkedTrigger.Property = System.Windows.Controls.Primitives.ToggleButton.IsCheckedProperty;
            checkedTrigger.Value = true;
            checkedTrigger.Setters.Add(new Setter(Border.BackgroundProperty, Brushes.Yellow, "border"));
            template.Triggers.Add(checkedTrigger);

            // Trigger IsMouseOver
            Trigger mouseOverTrigger = new Trigger();
            mouseOverTrigger.Property = UIElement.IsMouseOverProperty;
            mouseOverTrigger.Value = true;
            mouseOverTrigger.Setters.Add(new Setter(Border.BackgroundProperty, Brushes.LightBlue, "border"));
            template.Triggers.Add(mouseOverTrigger);

            return template;
        }

        public static ControlTemplate CreatePanierRadioTemplate()
        {
            ControlTemplate template = new ControlTemplate(typeof(RadioButton));

            FrameworkElementFactory border = new FrameworkElementFactory(typeof(Border));
            border.Name = "border";
            border.SetValue(Border.BackgroundProperty, Brushes.Transparent);
            border.SetValue(Border.BorderBrushProperty, Brushes.Transparent);
            border.SetValue(Border.BorderThicknessProperty, new Thickness(1));

            FrameworkElementFactory content = new FrameworkElementFactory(typeof(ContentPresenter));
            content.SetValue(ContentPresenter.HorizontalAlignmentProperty, HorizontalAlignment.Stretch);
            content.SetValue(ContentPresenter.VerticalAlignmentProperty, VerticalAlignment.Center);
            border.AppendChild(content);

            template.VisualTree = border;

            Trigger hoverTrigger = new Trigger();
            hoverTrigger.Property = RadioButton.IsMouseOverProperty;
            hoverTrigger.Value = true;
            hoverTrigger.Setters.Add(new Setter(Border.BorderBrushProperty, Brushes.Black, "border"));
            template.Triggers.Add(hoverTrigger);

            Trigger checkedTrigger = new Trigger();
            checkedTrigger.Property = RadioButton.IsCheckedProperty;
            checkedTrigger.Value = true;
            checkedTrigger.Setters.Add(new Setter(Border.BackgroundProperty, Brushes.Silver, "border"));
            template.Triggers.Add(checkedTrigger);

            return template;
        }
    }
}
