using System;
using System.Collections.Generic;
using System.Timers;
using System.Windows.Controls;
using MaterialDesignThemes.Wpf;
using JameNegar.Constants;
using JameNegar.Interfaces;
using JameNegar.Templates;

namespace JameNegar.Forms.JameNegarManager.CreateDocument
{
    public partial class CreateDocumentSlide5 : UserControl, ITransitionCommand
    {
        public Action TransitionMovePreviousCommand { get; set; }
        public Action TransitionMoveNextCommand { get; set; }

        System.Timers.Timer timerGoNextPage;

        internal string DocumentPath { get; private set; }

        //Slide2
        private DocumentTypes? documentType;
        private TemplateTypes? templateType;
        string documentTypePersian;
        string documentTypeEnglish;
        string templateTypePersian;
        string templateTypeEnglish;

        //Slide3
        private string universityFa;
        private string universityEn;

        private string branchFa;
        private string branchEn;

        private string departmentFa;
        private string departmentEn;

        private string groupFa;
        private string groupEn;

        private string fieldOfStudyFa;
        private string fieldOfStudyEn;

        private string areaOfStudyFa;
        private string areaOfStudyEn;

        private string academicDegreeFa;
        private string academicDegreeEn;



        //Slide4
        private string nameOfCourseFa;

        private string titleFa;
        private string titleEn;

        private string authorFa;
        private string authorEn;

        private string advisorFa;
        private string advisorEn;

        private string supervisorFa;
        private string supervisorEn;

        private string defenseDateFa;
        private string defenseDateEn;

        private string locationFa;


        private string sourcePath;

        public CreateDocumentSlide5()
        {
            InitializeComponent();

            Random randomInterval = new Random();
            timerGoNextPage = new System.Timers.Timer();
            timerGoNextPage.Elapsed += TimerGoNextPage_Elapsed;
            timerGoNextPage.Interval = randomInterval.Next(1000, 3500);

            btnConfirmCreateDocument.Click += BtnConfirmCreateDocument_Click;
            chkBoxAgreement.Click += ChkBoxAgreement_Click;

            lblHyperLinkSource.Click += LblHyperLinkSource_Click; ;
        }

        private void LblHyperLinkSource_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start(sourcePath);
        }

        private void TimerGoNextPage_Elapsed(object sender, ElapsedEventArgs e)
        {
            Dispatcher?.Invoke(() =>
            {
                TransitionMoveNextCommand?.Invoke();
                timerGoNextPage.Stop();
            });
        }

        bool selectedConfirm = false;
        private void BtnConfirmCreateDocument_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (!selectedConfirm && chkBoxAgreement.IsChecked == true)
            {
                timerGoNextPage.Start();
                ButtonProgressAssist.SetIsIndeterminate(btnConfirmCreateDocument, true);
                //btnConfirmCreateDocument.IsEnabled = false;
                btnBackward.IsEnabled = false;
                chkBoxAgreement.IsEnabled = false;
                selectedConfirm = true;
            }
        }
        private void ChkBoxAgreement_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            btnConfirmCreateDocument.IsEnabled = (bool)chkBoxAgreement.IsChecked;
        }

        #region Functions
        public void resetControls()
        {
            documentType = null;
            templateType = null;
            documentTypePersian = null;
            documentTypeEnglish = null;
            templateTypePersian = null;
            templateTypeEnglish = null;

            universityFa = null;
            universityEn = null;
            branchFa = null;
            branchEn = null;
            departmentFa = null;
            departmentEn = null;
            groupFa = null;
            groupEn = null;
            fieldOfStudyFa = null;
            fieldOfStudyEn = null;
            areaOfStudyFa = null;
            areaOfStudyEn = null;
            academicDegreeFa = null;
            academicDegreeEn = null;

            nameOfCourseFa = null;
            titleFa = null;
            titleEn = null;
            authorFa = null;
            authorEn = null;
            supervisorFa = null;
            supervisorEn = null;
            advisorFa = null;
            advisorEn = null;
            defenseDateFa = null;
            defenseDateEn = null;

            locationFa = null;

            selectedConfirm = false;

            Dispatcher.Invoke(() =>
            {
                dataGridType.ItemsSource = null;
                dataGridUniversity.ItemsSource = null;
                dataGridDocumentData.ItemsSource = null;

                btnConfirmCreateDocument.IsEnabled = false;
                btnBackward.IsEnabled = true;
                ButtonProgressAssist.SetIsIndeterminate(btnConfirmCreateDocument, false);

                chkBoxAgreement.IsEnabled = true;
                chkBoxAgreement.IsChecked = false;

                timerGoNextPage.Stop();

            });
        }
        public void initializeVariables(CreateDocumentSlide2 slide2, CreateDocumentSlide3 slide3, CreateDocumentSlide4 slide4)
        {
            chkBoxAgreement.IsEnabled = true;
            chkBoxAgreement.IsChecked = false;

            btnConfirmCreateDocument.IsEnabled = false;
            btnBackward.IsEnabled = true;

            DocumentPath = Properties.Settings.Default.WorkSpaceDirectory.TrimEnd('\\') + "\\" + slide2.DocumentName + ".docx";

            //Slide2
            documentType = slide2.DocumentType;
            templateType = slide2.TemplateType;
            documentTypePersian = DedicatedFunctions.getDocumentTypePersianName((DocumentTypes)documentType);
            documentTypeEnglish = DedicatedFunctions.getDocumentTypeEnglishName((DocumentTypes)documentType);
            templateTypePersian = DedicatedFunctions.getTemplateTypePersianName((TemplateTypes)templateType);
            templateTypeEnglish = DedicatedFunctions.getTemplateTypeEnglishName((TemplateTypes)templateType);

            dataGridType.ItemsSource = new List<DataGridModel>()
    {
        new DataGridModel("نوع سند" , documentTypePersian, documentTypeEnglish),
        new DataGridModel("نوع قالب" , templateTypePersian, templateTypeEnglish),
    };
            lblPath.Text = DocumentPath;

            //Slide3 (اطلاعات دانشگاهی)
            universityFa = slide3.UniversityFa;
            universityEn = slide3.UniversityEn;
            branchFa = slide3.BranchFa;
            branchEn = slide3.BranchEn;
            departmentFa = slide3.DepartmentFa;
            departmentEn = slide3.DepartmentEn;
            groupFa = slide3.GroupFa;
            groupEn = slide3.GroupEn;
            fieldOfStudyFa = slide3.FieldOfStudyFa;
            fieldOfStudyEn = slide3.FieldOfStudyEn;
            areaOfStudyFa = slide3.AreaOfStudyFa;
            areaOfStudyEn = slide3.AreaOfStudyEn;
            academicDegreeFa = slide3.AcademicDegreeFa;
            academicDegreeEn = slide3.AcademicDegreeEn;

            List<DataGridModel> educationDataList = new List<DataGridModel>();
            educationDataList.Add(new DataGridModel("دانشگاه", universityFa, universityFa));
            lblHyperUniversity.Text = universityFa;

            if (!string.IsNullOrEmpty(branchFa) && !string.IsNullOrEmpty(branchFa))
                educationDataList.Add(new DataGridModel("واحد", branchFa, branchFa));

            educationDataList.Add(new DataGridModel("واحد", departmentFa, departmentEn));

            if (!string.IsNullOrEmpty(groupFa))
                educationDataList.Add(new DataGridModel("مرکز آموزشی", groupFa, groupFa));

            educationDataList.Add(new DataGridModel("رشته تحصیلی", fieldOfStudyFa, fieldOfStudyFa));

            if (!string.IsNullOrEmpty(areaOfStudyFa))
                educationDataList.Add(new DataGridModel("گرایش تحصیلی", areaOfStudyFa, areaOfStudyFa));

            educationDataList.Add(new DataGridModel("مقطع تحصیلی", academicDegreeFa, academicDegreeFa));
            dataGridUniversity.ItemsSource = educationDataList;

            //Slide4 (اطلاعات سند - بر اساس نوع سند)
            titleFa = slide4.TitleFa;
            titleEn = slide4.TitleEn;
            authorFa = slide4.AuthorFa;
            authorEn = slide4.AuthorEn;
            supervisorFa = slide4.SupervisorFa;
            supervisorEn = slide4.SupervisorEn;
            advisorFa = slide4.AdvisorFa;
            advisorEn = slide4.AdvisorEn;
            defenseDateFa = slide4.DefenseDateFa;
            defenseDateEn = slide4.DefenseDateEn;

            sourcePath = TemplateAccess.getTemplateSourcePath(slide3.University, slide2.TemplateType);

            lblDocumentInformation.Text = "مشخصات " + documentTypePersian;

            var listDocumentData2 = new List<DataGridModel>();

            // ========== بر اساس نوع سند ==========
            switch (documentType)
            {
                case DocumentTypes.SchoolResearch:
                    // تحقیق درسی
                    listDocumentData2.Add(new DataGridModel("نام درس", slide4.NameOFCourseFa, null));
                    listDocumentData2.Add(new DataGridModel("عنوان تحقیق", titleFa, titleEn));
                    listDocumentData2.Add(new DataGridModel("نام دانشجو", authorFa, authorEn));
                    listDocumentData2.Add(new DataGridModel("استاد درس", supervisorFa, supervisorEn));
                    if (!string.IsNullOrEmpty(defenseDateFa))
                        listDocumentData2.Add(new DataGridModel("تاریخ انجام تحقیق", defenseDateFa, defenseDateEn));
                    break;

                case DocumentTypes.Thesis:
                    // گزارش کارورزی
                    listDocumentData2.Add(new DataGridModel("عنوان کارورزی", slide4.NameOFCourseFa, null));
                    listDocumentData2.Add(new DataGridModel("موضوع کارورزی", titleFa, titleEn));
                    listDocumentData2.Add(new DataGridModel("محل کارورزی", slide4.LocationFa, null));
                    listDocumentData2.Add(new DataGridModel("مدرس", supervisorFa, supervisorEn));
                    listDocumentData2.Add(new DataGridModel("مربی", advisorFa, advisorEn));
                    listDocumentData2.Add(new DataGridModel("کارورز", authorFa, authorEn));
                    if (!string.IsNullOrEmpty(defenseDateFa))
                        listDocumentData2.Add(new DataGridModel("زمان انجام کارورزی", defenseDateFa, defenseDateEn));
                    break;

                case DocumentTypes.Project:
                    // پروژه
                    listDocumentData2.Add(new DataGridModel("عنوان پروژه", titleFa, titleEn));
                    listDocumentData2.Add(new DataGridModel("مدرس راهنما", advisorFa, advisorEn));
                    listDocumentData2.Add(new DataGridModel("نام دانشجو", authorFa, authorEn));
                    if (!string.IsNullOrEmpty(defenseDateFa))
                        listDocumentData2.Add(new DataGridModel("تاریخ ارائه", defenseDateFa, defenseDateEn));
                    break;

                default:
                    // پایان‌نامه، رساله و غیره
                    listDocumentData2.Add(new DataGridModel("عنوان", titleFa, titleEn));
                    listDocumentData2.Add(new DataGridModel("نگارنده", authorFa, authorEn));
                    listDocumentData2.Add(new DataGridModel("استاد راهنما", supervisorFa, supervisorEn));
                    if (!string.IsNullOrEmpty(advisorFa))
                        listDocumentData2.Add(new DataGridModel("استاد مشاور", advisorFa, advisorEn));
                    if (!string.IsNullOrEmpty(defenseDateFa))
                        listDocumentData2.Add(new DataGridModel("تاریخ دفاع", defenseDateFa, defenseDateEn));
                    break;
            }

            dataGridDocumentData.ItemsSource = listDocumentData2;
        }
        #endregion
    }

    public class DataGridModel
    {
        public DataGridModel(string name, string persianValue, string englishValue)
        {
            Name = name;
            PersianValue = persianValue;
            EnglishValue = englishValue;
        }

        public string Name { get; set; }
        public string PersianValue { get; set; }
        public string EnglishValue { get; set; }
    }

}