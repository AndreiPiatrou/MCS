using System.Diagnostics.Contracts;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

using MahApps.Metro.Controls;

namespace MCS.Desktop.Behaviors
{
    public class InteractionLayout
    {
        public static readonly DependencyProperty ActionInProgressProperty = DependencyProperty.RegisterAttached(
            "ActionInProgress",
            typeof(bool),
            typeof(InteractionLayout),
            new PropertyMetadata(default(bool), PropertyChangedCallback));

        private static void PropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Contract.Requires(d is Grid);

            var grid = (Grid)d;

            if (Equals(e.NewValue, true))
            {
                var behavior = new InteractionLayout
                {
                    layout = new Grid
                    {
                        Background = Brushes.White,
                        Opacity = 0.5
                    },
                    progressLayout = new Grid
                    {
                        Background = Brushes.Transparent
                    }
                };

                Grid.SetColumnSpan(behavior.layout, grid.ColumnDefinitions.Count == 0 ? 1 : grid.ColumnDefinitions.Count);
                Grid.SetRowSpan(behavior.layout, grid.RowDefinitions.Count == 0 ? 1 : grid.RowDefinitions.Count);

                Grid.SetColumnSpan(behavior.progressLayout, grid.ColumnDefinitions.Count == 0 ? 1 : grid.ColumnDefinitions.Count);
                Grid.SetRowSpan(behavior.progressLayout, grid.RowDefinitions.Count == 0 ? 1 : grid.RowDefinitions.Count);
                behavior.progressLayout.Children.Add(new ProgressRing { IsActive = true });

                grid.Children.Add(behavior.layout);
                grid.Children.Add(behavior.progressLayout);
                SetBehavior(grid, behavior);
            }
            else
            {
                var behavior = GetBehavior(grid);
                if (behavior == null)
                {
                    return;
                }

                grid.Children.Remove(behavior.layout);
                grid.Children.Remove(behavior.progressLayout);
                SetBehavior(grid, null);
            }
        }

        public static void SetActionInProgress(DependencyObject element, bool value)
        {
            element.SetValue(ActionInProgressProperty, value);
        }

        public static bool GetActionInProgress(DependencyObject element)
        {
            return (bool)element.GetValue(ActionInProgressProperty);
        }

        private static readonly DependencyProperty BehaviorProperty = DependencyProperty.RegisterAttached(
            "Behavior",
            typeof(InteractionLayout),
            typeof(InteractionLayout),
            new PropertyMetadata(default(InteractionLayout)));

        private static void SetBehavior(DependencyObject element, InteractionLayout value)
        {
            element.SetValue(BehaviorProperty, value);
        }

        private static InteractionLayout GetBehavior(DependencyObject element)
        {
            return (InteractionLayout)element.GetValue(BehaviorProperty);
        }

        private Grid layout;
        private Grid progressLayout;
    }
}
