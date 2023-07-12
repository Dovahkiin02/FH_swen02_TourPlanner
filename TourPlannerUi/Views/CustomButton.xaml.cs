using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TourPlannerUi.Views {
    /// <summary>
    /// Interaction logic for CustomButton.xaml
    /// </summary>
    public partial class CustomButton : UserControl {
        public CustomButton() {
            InitializeComponent();
        }

        public static readonly DependencyProperty CommandProperty =
            DependencyProperty.Register("Command", typeof(ICommand), typeof(CustomButton));

        public ICommand Command {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        public static readonly DependencyProperty CommandParameterProperty =
            DependencyProperty.Register("CommandParameter", typeof(object), typeof(CustomButton));

        public object CommandParameter {
            get { return GetValue(CommandParameterProperty); }
            set { SetValue(CommandParameterProperty, value); }
        }

        public object Content {
            get { return GetValue(ContentProperty); }
            set { SetValue(ContentProperty, value); }
        }

        public static readonly DependencyProperty ContentProperty =
            DependencyProperty.Register("Content", typeof(object), typeof(CustomButton));

        public static readonly DependencyProperty ToolTipProperty =
            DependencyProperty.Register("ToolTip", typeof(string), typeof(CustomButton));

        public string ToolTip {
            get { return (string)GetValue(ToolTipProperty); }
            set { SetValue(ToolTipProperty, value); }
        }
    }
}
