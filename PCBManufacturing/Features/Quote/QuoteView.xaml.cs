using System.Windows.Controls;
using System.Windows.Input;

namespace PCBManufacturing.Features.Quote
{
    /// <summary>
    /// Interaction logic for QuoteView.xaml
    /// </summary>
    public partial class QuoteView : UserControl
    {
        public QuoteView()
        {
            InitializeComponent();
        }

        private void BoardValueTextBox_OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Enter || sender is not TextBox textBox)
            {
                return;
            }

            textBox.GetBindingExpression(TextBox.TextProperty)?.UpdateSource();

            if (!Validation.GetHasError(textBox))
            {
                Keyboard.ClearFocus();
            }

            e.Handled = true;
        }
    }
}
