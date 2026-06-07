using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using JameNegar.Constants;
using JameNegar.Forms.JameNegarManager.CreateDocument.Models;
using MaterialDesignThemes.Wpf;
using static JameNegar.Forms.JameNegarManager.CreateDocument.Models.CreateDocumentControlModel;

namespace JameNegar.Forms.JameNegarManager.CreateDocument
{
    public partial class CreateDocumentSlide4 : UserControl
    {
        private List<CreateDocumentControlModel> controlModels;

        //Properties
        public string NameOFCourseFa { get; private set; }
        public string TitleFa { get; private set; }
        public string TitleEn { get; private set; }
        public string AuthorFa { get; private set; }
        public string AuthorEn { get; private set; }
        public string AdvisorFa { get; private set; }
        public string AdvisorEn { get; private set; }
        public string SupervisorFa { get; private set; }
        public string SupervisorEn { get; private set; }
        public string DefenseDateFa { get; private set; }
        public string DefenseDateEn { get; private set; }
        public string LocationFa { get; private set; }

        public CreateDocumentSlide4()
        {
            InitializeComponent();

            btnForward.IsEnabled = false;

            controlModels = new List<CreateDocumentControlModel>()
            {
                new CreateDocumentControlModel(txtBoxNameOfCourseFa,CreateDocumentControlModel.ControlLevels.Essential),
                new CreateDocumentControlModel(txtBoxTitleFa,CreateDocumentControlModel.ControlLevels.Essential),
                new CreateDocumentControlModel(txtBoxAuthorFa,CreateDocumentControlModel.ControlLevels.Essential),
                new CreateDocumentControlModel(txtBoxSupervisorFa,CreateDocumentControlModel.ControlLevels.Essential),
                new CreateDocumentControlModel(txtBoxAdvisorFa,CreateDocumentControlModel.ControlLevels.Essential),
                new CreateDocumentControlModel(txtBoxDefenseDateFa,CreateDocumentControlModel.ControlLevels.Optional),
                new CreateDocumentControlModel(txtBoxLocationFa,CreateDocumentControlModel.ControlLevels.Essential),
            };

            txtBoxSupervisorFa.PreviewKeyDown += TextBoxMultiLine_PreviewKeyDown;
            txtBoxAdvisorFa.PreviewKeyDown += TextBoxMultiLine_PreviewKeyDown;

            foreach (CreateDocumentControlModel cdcm in controlModels)
            {
                ((TextBox)cdcm.Control).TextChanged += TextBox_TextChanged;
                ((TextBox)cdcm.Control).GotFocus += TextBox_GotFocus;
                ((TextBox)cdcm.Control).LostFocus += TextBox_LostFocus;
            }

           
        }

       

       


        #region Events

        #region TextBox
        private void TextBoxMultiLine_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (e.Key == Key.Enter)
            {
                int lineCount = textBox.Text.Split(new[] { '\n' }, StringSplitOptions.None).Length - 1;
                if (lineCount >= 2)
                {
                    e.Handled = true;
                }
            }
        }
        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;

            for (int i = 0; i < controlModels.Count; i++)
            {
                if (textBox == (TextBox)controlModels[i].Control)
                {
                    controlModels[i].Validate = validateTextBox(textBox, controlModels[i].ControlLevel, false);
                    validateControls();
                    return;
                }
            }
        }
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;

            

            for (int i = 0; i < controlModels.Count; i++)
            {
                if (textBox == (TextBox)controlModels[i].Control)
                {
                    controlModels[i].Validate = validateTextBox(textBox, controlModels[i].ControlLevel, true);

                    if (controlModels[i].Validate)
                        validateControls();

                    return;
                }
            }

        }
        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;

            string tag = textBox.Tag.ToString();

            if (!string.IsNullOrEmpty(tag.Trim()))
            {
                if (tag == "Persian")
                {
                    DedicatedFunctions.changeKeyboardLanguage(KeyboardLanguage.Persian);
                }
                else if (tag == "English")
                {
                    DedicatedFunctions.changeKeyboardLanguage(KeyboardLanguage.English);
                }
            }
        }
        #endregion

        #endregion

        #region Validators
        private bool validateControls()
        {
           

            bool isValid = true;

            foreach (CreateDocumentControlModel controlModel in controlModels)
            {
                var textBox = (TextBox)controlModel.Control;

                // فقط فیلدهایی که Visible هستند و Optional نیستند را بررسی کن
                if (textBox.Visibility == Visibility.Visible &&
                    controlModel.ControlLevel != CreateDocumentControlModel.ControlLevels.Optional)
                {
                    if (string.IsNullOrWhiteSpace(textBox.Text) || textBox.Text.Length < 5)
                    {
                        isValid = false;
                        break;
                    }
                }
            }

            if (isValid)
            {
                // ذخیره مقادیر (فقط فیلدهای Visible)
                NameOFCourseFa = txtBoxNameOfCourseFa.Visibility == Visibility.Visible ? txtBoxNameOfCourseFa.Text : "";
                TitleFa = txtBoxTitleFa.Visibility == Visibility.Visible ? txtBoxTitleFa.Text : "";
                AuthorFa = txtBoxAuthorFa.Visibility == Visibility.Visible ? txtBoxAuthorFa.Text : "";
                SupervisorFa = txtBoxSupervisorFa.Visibility == Visibility.Visible ? txtBoxSupervisorFa.Text : "";
                AdvisorFa = txtBoxAdvisorFa.Visibility == Visibility.Visible ? txtBoxAdvisorFa.Text : "";
                DefenseDateFa = txtBoxDefenseDateFa.Visibility == Visibility.Visible ? txtBoxDefenseDateFa.Text : "";
                LocationFa = txtBoxLocationFa.Visibility == Visibility.Visible ? txtBoxLocationFa.Text : "";

                btnForward.IsEnabled = true;
                return true;
            }
            else
            {
                btnForward.IsEnabled = false;
                return false;
            }
        }
        private bool validateTextBox(TextBox textBox, ControlLevels controlLevel, bool onlyReturn)
        {
            // اگر فیلد مخفی است، بدون اعتبارسنجی true برگردان
            if (textBox.Visibility != Visibility.Visible)
            {
                return true;
            }

            if (controlLevel == ControlLevels.Optional)
            {
                normalControl(textBox);
                return true;
            }

            if (!string.IsNullOrEmpty(textBox.Text) && !string.IsNullOrWhiteSpace(textBox.Text) && textBox.Text.Length > 4)
            {
                normalControl(textBox);
                return true;
            }
            else if (string.IsNullOrEmpty(textBox.Text) || string.IsNullOrWhiteSpace(textBox.Text))
            {
                if (!onlyReturn)
                    errorControl(textBox, "فیلد نباید خالی باشد");
                return false;
            }
            else
            {
                if (!onlyReturn)
                    errorControl(textBox, "حروف بیشتر از 4 حرف میبایست باشد");
                return false;
            }
        }
        #endregion

        #region Functions
        internal void resetControls()
        {
            #region variables

            foreach (CreateDocumentControlModel controlModel in controlModels)
            {
                controlModel.Validate = false;
            }

            NameOFCourseFa = "";

            TitleFa = "";
            TitleEn = "";

            AuthorFa = "";
            AuthorEn = "";

            SupervisorFa = "";
            SupervisorEn = "";

            AdvisorFa = "";
            AdvisorEn = "";

            DefenseDateFa = "";
            DefenseDateEn = "";

            LocationFa = "";
            #endregion


            #region controls
            Dispatcher.Invoke(() =>
            {

                btnForward.IsEnabled = false;
                txtBoxNameOfCourseFa.Visibility = Visibility.Collapsed;

                foreach (CreateDocumentControlModel controlModel in controlModels)
                {
                    TextBox textBox = (TextBox)controlModel.Control;

                    textBox.Text = "";
                    normalControl(textBox);
                }
            });
            #endregion
        }

        private void errorControl(Control control, string hintText)
        {
            HintAssist.SetHelperText(control, hintText);
            control.Foreground = System.Windows.Media.Brushes.Red;

            Thickness margin = new Thickness(0, 0, 0, 20);
            if (control == txtBoxNameOfCourseFa)
                gridNameOfCourse.Margin = margin;
            else if (control == txtBoxTitleFa)
                gridTitle.Margin = margin;
            else if (control == txtBoxAuthorFa)
                gridAuthor.Margin = margin;
            else if (control == txtBoxSupervisorFa)
                gridSupervisor.Margin = margin;
            else if (control == txtBoxAdvisorFa)
                gridAdvisor.Margin = margin;
            else if (control == txtBoxDefenseDateFa)
                gridDefenseDate.Margin = margin;
            else if (control == txtBoxLocationFa)
                gridLocation.Margin = margin;
        }

        private void DateControl(Control control, string hintText)
        {
            HintAssist.SetHelperText(control, hintText);

            Thickness margin = new Thickness(0, 0, 0, 20);
            if (control == txtBoxNameOfCourseFa)
                gridNameOfCourse.Margin = margin;
            else if (control == txtBoxTitleFa)
                gridTitle.Margin = margin;
            else if (control == txtBoxAuthorFa)
                gridAuthor.Margin = margin;
            else if (control == txtBoxSupervisorFa)
                gridSupervisor.Margin = margin;
            else if (control == txtBoxAdvisorFa)
                gridAdvisor.Margin = margin;
            else if (control == txtBoxDefenseDateFa)
                gridDefenseDate.Margin = margin;
            else if (control == txtBoxLocationFa)
                gridLocation.Margin = margin;
        }
        private void normalControl(Control control)
        {
            HintAssist.SetHelperText(control, "");
            control.Foreground = System.Windows.Media.Brushes.Black;

            Thickness margin = new Thickness(0);
            if (control == txtBoxNameOfCourseFa)
                gridNameOfCourse.Margin = margin;
            else if (control == txtBoxTitleFa && txtBoxTitleFa.Foreground != System.Windows.Media.Brushes.Red)
                gridTitle.Margin = margin;
            else if (control == txtBoxTitleFa && txtBoxTitleFa.Foreground != System.Windows.Media.Brushes.Red)
                gridTitle.Margin = margin;
            else if (control == txtBoxAuthorFa && txtBoxAuthorFa.Foreground != System.Windows.Media.Brushes.Red)
                gridAuthor.Margin = margin;
            else if (control == txtBoxAuthorFa && txtBoxAuthorFa.Foreground != System.Windows.Media.Brushes.Red)
                gridAuthor.Margin = margin;
            else if (control == txtBoxSupervisorFa && txtBoxSupervisorFa.Foreground != System.Windows.Media.Brushes.Red)
                gridSupervisor.Margin = margin;
            else if (control == txtBoxSupervisorFa && txtBoxSupervisorFa.Foreground != System.Windows.Media.Brushes.Red)
                gridSupervisor.Margin = margin;
            else if (control == txtBoxAdvisorFa && txtBoxAdvisorFa.Foreground != System.Windows.Media.Brushes.Red)
                gridAdvisor.Margin = margin;
            else if (control == txtBoxAdvisorFa && txtBoxAdvisorFa.Foreground != System.Windows.Media.Brushes.Red)
                gridAdvisor.Margin = margin;
            else if (control == txtBoxDefenseDateFa && txtBoxDefenseDateFa.Foreground != System.Windows.Media.Brushes.Red)
                gridDefenseDate.Margin = margin;
            else if (control == txtBoxDefenseDateFa && txtBoxDefenseDateFa.Foreground != System.Windows.Media.Brushes.Red)
                gridDefenseDate.Margin = margin;
            else if (control == txtBoxLocationFa && txtBoxLocationFa.Foreground != System.Windows.Media.Brushes.Red)
                gridLocation.Margin = margin;
        }

        public void initializeVariables(DocumentTypes documentType)
        {
            if (documentType == DocumentTypes.SchoolResearch)
                txtBoxNameOfCourseFa.Visibility = Visibility.Visible;
            else
                txtBoxNameOfCourseFa.Visibility = Visibility.Collapsed;

            SetAllControlLevelsToEssential();

            SetControlLevelToOptional(txtBoxDefenseDateFa);

            if (documentType == DocumentTypes.SchoolResearch)
            {
                // تحقیق درسی
                txtBoxNameOfCourseFa.Visibility = Visibility.Visible;
                HintAssist.SetHint(txtBoxNameOfCourseFa, "نام درس");
                HintAssist.SetHint(txtBoxTitleFa, "عنوان تحقیق");
                HintAssist.SetHint(txtBoxAuthorFa, "نام دانشجو");
                txtBoxAuthorFa.IsEnabled = false;
                txtBoxAuthorFa.Text = GetStudentName();
                HintAssist.SetHint(txtBoxSupervisorFa, "استاد درس");
                HintAssist.SetHint(txtBoxDefenseDateFa, "تاریخ انجام تحقیق");
                DateControl(txtBoxDefenseDateFa, "مثال: بهار 1405، اردیبهشت 1405، 1405/02/01");


                // مخفی کردن فیلدهای اضافی و Optional کردن آنها
                gridLocation.Visibility = Visibility.Collapsed;
                SetControlLevelToOptional(txtBoxLocationFa);

                gridAdvisor.Visibility = Visibility.Collapsed;
                SetControlLevelToOptional(txtBoxAdvisorFa);
            }
            else if (documentType == DocumentTypes.Thesis)
            {
                // گزارش کارورزی
                txtBoxNameOfCourseFa.Visibility = Visibility.Visible;
                HintAssist.SetHint(txtBoxNameOfCourseFa, "عنوان کارورزی");
                gridTitle.Visibility = Visibility.Visible;
                HintAssist.SetHint(txtBoxTitleFa, "موضوع کارورزی");
                gridLocation.Visibility = Visibility.Visible;
                HintAssist.SetHint(txtBoxLocationFa, "محل کارورزی");
                gridSupervisor.Visibility = Visibility.Visible;
                HintAssist.SetHint(txtBoxSupervisorFa, "مدرس");
                gridAdvisor.Visibility = Visibility.Visible;
                HintAssist.SetHint(txtBoxAdvisorFa, "مربی");
                gridAuthor.Visibility = Visibility.Visible;
                HintAssist.SetHint(txtBoxAuthorFa, "کارورز");
                txtBoxAuthorFa.IsEnabled = false;
                txtBoxAuthorFa.Text = GetStudentName();
                gridDefenseDate.Visibility = Visibility.Visible;
                HintAssist.SetHint(txtBoxDefenseDateFa, "زمان انجام کارورزی");
                DateControl(txtBoxDefenseDateFa, "مثال: بهار 1405، اردیبهشت 1405، 1405/02/01");

            }
            else if (documentType == DocumentTypes.Project)
            {
                // پروژه
                txtBoxNameOfCourseFa.Visibility = Visibility.Collapsed;
                SetControlLevelToOptional(txtBoxNameOfCourseFa);

                gridTitle.Visibility = Visibility.Visible;
                HintAssist.SetHint(txtBoxTitleFa, "عنوان پروژه");
                gridAdvisor.Visibility = Visibility.Visible;
                HintAssist.SetHint(txtBoxAdvisorFa, "مدرس راهنما");
                gridAuthor.Visibility = Visibility.Visible;
                HintAssist.SetHint(txtBoxAuthorFa, "نام دانشجو");
                txtBoxAuthorFa.IsEnabled = false;
                txtBoxAuthorFa.Text = GetStudentName();

                gridLocation.Visibility = Visibility.Collapsed;
                SetControlLevelToOptional(txtBoxLocationFa);

                gridSupervisor.Visibility = Visibility.Collapsed;
                SetControlLevelToOptional(txtBoxSupervisorFa);

                gridDefenseDate.Visibility = Visibility.Visible;
                HintAssist.SetHint(txtBoxDefenseDateFa, "تاریخ ارائه");
                DateControl(txtBoxDefenseDateFa, "مثال: بهار 1405، اردیبهشت 1405، 1405/02/01");

            }



            validateControls();

            //string initialTitle = "مشخصات ";
            //if(documentType == DocumentTypes.Project)
            //	lblTitle.Text = initialTitle + DocumentTypeValues.DocumentType_ProjectFa;
            //else if(documentType == DocumentTypes.Thesis)
            //	lblTitle.Text = initialTitle + DocumentTypeValues.DocumentType_ThesisFa;
            //else if(documentType == DocumentTypes.Dissertation)
            //	lblTitle.Text = initialTitle + DocumentTypeValues.DocumentType_DissertationFa;
            //else if(documentType == DocumentTypes.SchoolResearch)
            //	lblTitle.Text = initialTitle + "تحقیق درسی";
        }
        #endregion

        private string GetStudentName()
        {
            var name = Properties.Settings.Default.UserName;
            return string.IsNullOrEmpty(name) ? "نام کاربری" : name;
        }

        private void SetAllControlLevelsToEssential()
        {
            foreach (var model in controlModels)
            {
                model.ControlLevel = CreateDocumentControlModel.ControlLevels.Essential;
            }
        }

        private void SetControlLevelToOptional(TextBox textBox)
        {
            var model = controlModels.FirstOrDefault(m => m.Control == textBox);
            if (model != null)
            {
                model.ControlLevel = CreateDocumentControlModel.ControlLevels.Optional;
            }
        }
    }


}