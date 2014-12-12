using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml;

namespace CustomControls
{
    public class StateButton : Button
    {
        public StateButton()
		{
            DefaultStyleKey = typeof(StateButton);
		}

        #region Properties

        public static readonly DependencyProperty PressedContentProperty = DependencyProperty.Register("PressedContent", typeof(FrameworkElement), typeof(StateButton), null);

        public FrameworkElement PressedContent
        {
            get { return GetValue(PressedContentProperty) as FrameworkElement; }
            set { SetValue(PressedContentProperty, value); }
        }

        #endregion
    }
}
