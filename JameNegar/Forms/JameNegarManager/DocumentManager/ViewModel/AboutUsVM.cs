using System;
using JameNegar.Forms.JameNegarManager.DocumentManager.Utilities;

namespace JameNegar.Forms.JameNegarManager.DocumentManager.ViewModel
{
    public class AboutUsVM : ViewModelBase, IGoToDocumentManager
    {
        public Action GoToMain { get; set; }
        public Action GoToDocuments { get; set; }
        public Action GoToLogin { get; set; }

        public AboutUsVM()
        {
        }

        internal void goBack(bool isUserLogined)
        {
            if (isUserLogined)
                GoToMain.Invoke();
            else
                GoToLogin.Invoke();
        }
    }
}
