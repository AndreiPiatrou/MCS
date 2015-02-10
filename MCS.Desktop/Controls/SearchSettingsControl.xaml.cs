using System.Windows;

using MCS.Desktop.ViewModels;

namespace MCS.Desktop.Controls
{
    public partial class SearchSettingsControl
    {
        public static readonly DependencyProperty SettingsProperty = DependencyProperty.Register("Settings", typeof(CriteriaSearchSettings), typeof(SearchSettingsControl), new PropertyMetadata(default(CriteriaSearchSettings)));

        public SearchSettingsControl()
        {
            InitializeComponent();
        }

        public CriteriaSearchSettings Settings
        {
            get
            {
                return (CriteriaSearchSettings)GetValue(SettingsProperty);
            }
            set
            {
                SetValue(SettingsProperty, value);
            }
        }
    }
}
