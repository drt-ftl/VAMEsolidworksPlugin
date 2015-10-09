using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using qwe;

using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.swpublished;
using SolidWorks.Interop.swconst;
using SolidWorksTools.File;
using SolidWorksTools;

namespace SwCSharpAddin1
{
    [ComVisible(true)]
    [ProgId("TestPane")]
    public partial class UserControl1 : UserControl
    {
        ISldWorks swApp;
        public int dmcTimeSlider = 1;
        public int dmcTimeSliderPrev = 1;

        public int stlTimeSlider = 1;
        public int stlTimeSliderPrev = 1;

        private int dmcCodeOffset = 0;
        private int stlCodeOffset = 0;
        private int max = 500;
        private qwe.SwAddin swAddin;
        public bool toggle = false;
        public double visibility = 1.0d;
        public int[] color = new int[3];
        public double transparency = 0.0d;
        public double emission = 0.0d;
        private Vis vis;

        public UserControl1()
        {
            InitializeComponent();
            color[0] = 0xFF;
            color[1] = 0xFF;
            color[2] = 0xFF;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!swAddin.dmcLoaded || !swAddin.stlLoaded)
                swAddin.loadFile();
        }

        public void getSwApp(ISldWorks swAppIn)
        {
            swApp = swAppIn;
        }

        public void getSwAddin(qwe.SwAddin _swAddIn)
        {
            swAddin = _swAddIn;
            UpdateCode();
        }

        private void TimeMax_Scroll(object sender, EventArgs e)
        {
            ScrollCode.Value = 0;
            switch (swAddin.LoadedType)
            {
                case qwe.SwAddin.fileType.DMC:
                    dmcCodeOffset = 0;
                    break;
                case qwe.SwAddin.fileType.STL:
                    stlCodeOffset = 0;
                    break;
                default:
                    break;
            }
            UpdateCode();
        }

        private void ScrollCode_Scroll(object sender, ScrollEventArgs e)
        {
            switch (swAddin.LoadedType)
            {
                case qwe.SwAddin.fileType.DMC:
                    dmcCodeOffset += ScrollCode.Value;
                    break;
                case qwe.SwAddin.fileType.STL:
                    stlCodeOffset += ScrollCode.Value;
                    break;
                default:
                    break;
            }
            UpdateCode();
        }

        public void UpdateCode()
        {
            if (!swAddin.dmcLoaded && !swAddin.stlLoaded) return;

            switch (swAddin.LoadedType)
            {
                case qwe.SwAddin.fileType.DMC:
                    dmcTimeSlider = TimeMax.Value;
                    TimeMaxLabel.Text = "TimeMax: " + dmcTimeSlider.ToString();
                    OnDMC();
                    break;
                case qwe.SwAddin.fileType.STL:
                    visibility = VisSlider.Value;
                    stlTimeSlider = TimeMax.Value;
                    TimeMaxLabel.Text = "TimeMax: " + stlTimeSlider.ToString();
                    OnSTL();
                    foreach (var surf in swAddin.stlSurfaces)
                    {
                        surf.Change(0, stlTimeSlider);
                    }
                    break;
                default:
                    break;
            }
        }

        public void BeginCode(int count)
        {
            switch (swAddin.LoadedType)
            {
                case qwe.SwAddin.fileType.DMC:
                    dmcTimeSlider = count;
                    dmcTimeSliderPrev = count;
                    break;
                case qwe.SwAddin.fileType.STL:
                    stlTimeSlider = count;
                    stlTimeSliderPrev = count;
                    break;
                default:
                    break;
            }
            TimeMax.Maximum = count;
            TimeMax.Value = count;
            TimeMax.TickFrequency = (int)(count / 10);
            UpdateCode();
        }

        #region DMC
        public void OnDMC()
        {
            var str = "";
            if (dmcTimeSlider >= swAddin.firstDmcLineInCode)
            {
                FirstLine.BringToFront();
                ScrollCode.BringToFront();
                CodeWindow.Padding = new System.Windows.Forms.Padding(0, 12, 0, 0);
                var firstLineIndex = swAddin.model_code_xrefDMC[TimeMax.Value];
                var thisIndex = firstLineIndex + dmcCodeOffset;
                if (swAddin.dmcCode.Count - 1 >= thisIndex && thisIndex > 0)
                    FirstLine.Text = swAddin.dmcCode[thisIndex];
                for (int i = thisIndex + 1; i < thisIndex + max; i++)
                {
                    if (swAddin.dmcCode.Count > i)
                        str += swAddin.dmcCode[i];
                }
                CodeWindow.Text = str;
                var _swLine = swAddin.dmcLines[dmcTimeSlider];
                var _p1 = _swLine.p1;
                var _p2 = _swLine.p2;
                p1xDisplay.Text = _p1.x.ToString("f5");
                p1yDisplay.Text = _p1.y.ToString("f5");
                p1zDisplay.Text = _p1.z.ToString("f5");
                p2xDisplay.Text = _p2.x.ToString("f5");
                p2yDisplay.Text = _p2.y.ToString("f5");
                p2zDisplay.Text = _p2.z.ToString("f5");
                //var c = _swLine.ThisSketchLine.MakeInfinite();

            }
            else
            {
                for (int i = dmcTimeSlider + dmcCodeOffset; i < dmcTimeSlider + dmcCodeOffset + max; i++)
                {
                    if (swAddin.dmcCode.Count > i)
                        str += swAddin.dmcCode[i];
                }
                FirstLine.SendToBack();
                CodeWindow.Padding = new System.Windows.Forms.Padding(0, 0, 0, 0);
                CodeWindow.Text = str;
            }
        }
        #endregion

        #region STL
        public void OnSTL()
        {
            var str = "";
            if (stlTimeSlider >= swAddin.firstStlLineInCode)
            {
                FirstLine.BringToFront();
                ScrollCode.BringToFront();
                CodeWindow.Padding = new System.Windows.Forms.Padding(0, 12, 0, 0);
                var firstLineIndex = swAddin.model_code_xrefSTL[TimeMax.Value];
                var thisIndex = firstLineIndex + stlCodeOffset;
                if (swAddin.stlCode.Count - 1 >= thisIndex && thisIndex > 0)
                    FirstLine.Text = swAddin.stlCode[thisIndex];
                for (int i = thisIndex + 1; i < thisIndex + max; i++)
                {
                    if (swAddin.stlCode.Count > i)
                        str += swAddin.stlCode[i];
                }
                CodeWindow.Text = str;
                var _swSurface = swAddin.stlSurfaces[stlTimeSlider];
                //var _p1 = _swLine.p1;
                //var _p2 = _swLine.p2;
                //p1xDisplay.Text = _p1.x.ToString("f5");
                //p1yDisplay.Text = _p1.y.ToString("f5");
                //p1zDisplay.Text = _p1.z.ToString("f5");
                //p2xDisplay.Text = _p2.x.ToString("f5");
                //p2yDisplay.Text = _p2.y.ToString("f5");
                //p2zDisplay.Text = _p2.z.ToString("f5");
                //var c = _swLine.ThisSketchLine.MakeInfinite();

            }
            else
            {
                for (int i = stlTimeSlider + stlCodeOffset; i < stlTimeSlider + stlCodeOffset + max; i++)
                {
                    if (swAddin.stlCode.Count > i)
                        str += swAddin.stlCode[i];
                }
                FirstLine.SendToBack();
                CodeWindow.Padding = new System.Windows.Forms.Padding(0, 0, 0, 0);
                CodeWindow.Text = str;
            }
        }
        #endregion

        private void VisSlider_Scroll(object sender, EventArgs e)
        {
            switch (swAddin.LoadedType)
            {
                case qwe.SwAddin.fileType.DMC:                    
                    break;
                case qwe.SwAddin.fileType.STL:
                    break;
                default:
                    break;
            }
            UpdateCode();
        }

        private void SwitchType_Click(object sender, EventArgs e)
        {
            switch (swAddin.LoadedType)
            {
                case qwe.SwAddin.fileType.DMC:
                    if (swAddin.stlLoaded)
                    {
                        SwitchType.Text = "Switch To DMC";
                        swAddin.LoadedType = qwe.SwAddin.fileType.STL;
                        swAddin.SwitchTaskPane();
                    }
                    break;
                case qwe.SwAddin.fileType.STL:
                    if (swAddin.dmcLoaded)
                    {
                        SwitchType.Text = "Switch To STL";
                        swAddin.LoadedType = qwe.SwAddin.fileType.DMC;
                        swAddin.SwitchTaskPane();
                    }
                    break;
                default:
                    break;
            }
            swAddin.SwitchTaskPane();
        }
    }
}
