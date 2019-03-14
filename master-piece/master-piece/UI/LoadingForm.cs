using System.Threading;
using System.Windows.Forms;

namespace master_piece
{
    public partial class LoadingForm : Form
    {
        private delegate void CloseDelegate();

        private static LoadingForm loadingForm;

        public LoadingForm()
        {
            InitializeComponent();
        }

        static public void ShowSplashScreen()
        {
            if (loadingForm != null)
                return;
            Thread thread = new Thread(new ThreadStart(ShowForm));
            thread.IsBackground = true;
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
        }

        static private void ShowForm()
        {
            loadingForm = new LoadingForm();
            Application.Run(loadingForm);
        }

        static public void CloseForm()
        {
            loadingForm.Invoke(new CloseDelegate(CloseFormInternal));
        }

        static private void CloseFormInternal()
        {
            loadingForm.Close();
            loadingForm = null;
        }
    }
}
