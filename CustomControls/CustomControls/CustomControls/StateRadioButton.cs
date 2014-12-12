using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace CustomControls
{
    public class StateRadioButton : RadioButton
    {
        public StateRadioButton()
		{
            DefaultStyleKey = typeof(StateRadioButton);
            this.Checked += StateRadioButton_Checked;
            this.Unchecked += StateRadioButton_Unchecked;
		}

        void StateRadioButton_Unchecked(object sender, RoutedEventArgs e)
        {
            
        }

        void StateRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            
        }

        #region Properties

        public static readonly DependencyProperty CheckedContentProperty = DependencyProperty.Register("CheckedContent", typeof(FrameworkElement), typeof(StateRadioButton), null);

        public FrameworkElement CheckedContent
        {
            get { return GetValue(CheckedContentProperty) as FrameworkElement; }
            set { SetValue(CheckedContentProperty, value); }
        }

        #endregion
    }
}
