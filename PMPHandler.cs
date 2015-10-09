using System;
using System.Windows.Forms;
using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.swpublished;
using SolidWorks.Interop.swconst;
namespace qwe
{

    public class PMPHandler : IPropertyManagerPage2Handler2
    {
        ISldWorks iSwApp;
        SwAddin userAddin;

        public PMPHandler(SwAddin addin)
        {
            userAddin = addin;
            iSwApp = (ISldWorks)userAddin.SwApp;
        }

        //Implement these methods from the interface
        public void AfterClose()
        {
            //This function must contain code, even if it does nothing, to prevent the
            //.NET runtime environment from doing garbage collection at the wrong time.
            int IndentSize;
            IndentSize = System.Diagnostics.Debug.IndentSize;
            System.Diagnostics.Debug.WriteLine(IndentSize);
        }

        public void OnCheckboxCheck(int id, bool status)
        {

        }

        public void OnClose(int reason)
        {
            //This function must contain code, even if it does nothing, to prevent the
            //.NET runtime environment from doing garbage collection at the wrong time.
            int IndentSize;
            IndentSize = System.Diagnostics.Debug.IndentSize;
            System.Diagnostics.Debug.WriteLine(IndentSize);
        }

        public void OnComboboxEditChanged(int id, string text)
        {

        }

        public int OnActiveXControlCreated(int id, bool status)
        {
            return -1;
        }

        public void OnButtonPress(int id)
        {
            if (id == 2)
                userAddin.loadFile();
            else if (id == 3)
                MessageBox.Show("Button 2 Pressed!");
        }

        public void OnComboboxSelectionChanged(int id, int item)
        {

        }

        public void OnGroupCheck(int id, bool status)
        {

        }

        public void OnGroupExpand(int id, bool status)
        {

        }

        public bool OnHelp()
        {
            return true;
        }

        public void OnListboxSelectionChanged(int id, int item)
        {

        }

        public bool OnNextPage()
        {
            return true;
        }

        public void OnNumberboxChanged(int id, double val)
        {

        }

        public void OnOptionCheck(int id)
        {

        }

        public bool OnPreviousPage()
        {
            return true;
        }

        public void OnSelectionboxCalloutCreated(int id)
        {

        }

        public void OnSelectionboxCalloutDestroyed(int id)
        {

        }

        public void OnSelectionboxFocusChanged(int id)
        {

        }

        public void OnSelectionboxListChanged(int id, int item)
        {

        }

        public void OnTextboxChanged(int id, string text)
        {

        }
    }
}
