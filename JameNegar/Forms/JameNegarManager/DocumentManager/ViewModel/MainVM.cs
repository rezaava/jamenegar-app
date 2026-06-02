using System;
using JameNegar.Forms.JameNegarManager.DocumentManager.Utilities;

namespace JameNegar.Forms.JameNegarManager.DocumentManager.ViewModel
{
    public class MainVM : ViewModelBase, IGoToDocumentManager
    {
        public Action GoToMain { get; set; }
        public Action GoToDocuments { get; set; }
        public Action GoToLogin { get; set; }

        public MainVM()
        {
        }
    }
}
