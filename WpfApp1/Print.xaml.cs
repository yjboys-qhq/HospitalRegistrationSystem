using System;
using System.Windows;
using System.IO;
using System.Windows.Xps.Packaging;


namespace WpfApp1
{
    /// <summary>
    /// Print.xaml 的交互逻辑
    /// </summary>
    public partial class Print : System.Windows.Window
    {
        //public static event EventHandler<Boolean> CloseEvent;//定义在Monitor中的一个事件，参数是MessageArgs对象
        string startpath = "";
        XpsDocument result;
        private Print()
        {
            InitializeComponent();
        }
        public Print(string path)
        {
            InitializeComponent();
            startpath = path;
        }

        private XpsDocument ConvertWordToXPS(string wordDocName)
        {
            FileInfo fi = new FileInfo(wordDocName);
            try
            {
                if (!File.Exists(wordDocName))
                {
                    result = new XpsDocument(wordDocName, System.IO.FileAccess.Read);
                }

                if (File.Exists(wordDocName))
                {
                    result = new XpsDocument(wordDocName, FileAccess.Read);
                }

            }
            catch (Exception ex)
            {
                string error = ex.Message;
            }
            return result;
        }

        ///// <summary> 
        /////  Convert the word document to xps document 
        ///// </summary> 
        ///// <param name="wordFilename">Word document Path</param> 
        ///// <param name="xpsFilename">Xps document Path</param> 
        ///// <returns></returns> 
        //private XpsDocument ConvertWordToXps(string wordFilename, string xpsFilename)
        //{
        //    // Create a WordApplication and host word document 
        //    Word.Application wordApp = new Microsoft.Office.Interop.Word.Application();
        //    try
        //    {
        //        wordApp.Documents.Open(wordFilename);
        //        // To Invisible the word document 
        //        wordApp.Application.Visible = false;
        //        // Minimize the opened word document 
        //        wordApp.WindowState = WdWindowState.wdWindowStateMinimize;
        //        Document doc = wordApp.ActiveDocument;
        //        doc.SaveAs(xpsFilename, WdSaveFormat.wdFormatXPS);
        //        XpsDocument xpsDocument = new XpsDocument(xpsFilename, FileAccess.Read);
        //        return xpsDocument;
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Error occurs, The error message is  " + ex.ToString());
        //        return null;
        //    }
        //    finally
        //    {
        //        wordApp.Documents.Close();
        //        ((_Application)wordApp).Quit(WdSaveOptions.wdDoNotSaveChanges);
        //    }
        //}


        /// <summary> 
        ///  View Word Document in WPF DocumentView Control 
        /// </summary> 
        /// <param name="sender"></param> 
        /// <param name="e"></param> 
        private void init(string path)
        {
           
            if (string.IsNullOrEmpty(path) || !File.Exists(path))
            {
                MessageBox.Show("The file is invalid. Please select an existing file again.");
            }
            else
            {
                XpsDocument xpsDocument = ConvertWordToXPS(path);
                if (xpsDocument == null)
                {
                    return;
                }
                documentviewWord.Document = xpsDocument.GetFixedDocumentSequence();
            }
        }

        private void documentviewWord_Loaded(object sender, RoutedEventArgs e)
        {
            init(startpath);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            result.Close();
            DeleteFile(this.startpath);
            // CloseEvent(sender,true);//定义在Monitor中的一个事件，参数是MessageArgs对象

        }
        public void DeleteFile(string path)
        {
            FileAttributes attr = File.GetAttributes(path);
            if (attr == FileAttributes.Directory)
            {
                Directory.Delete(path, true);
            }
            else
            {
                File.Delete(path);
            }
        }
    }

}
