using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.swpublished;
using SolidWorks.Interop.swconst;
using SolidWorksTools.File;
using SolidWorksTools;
using UnityEngine;

using qwe;

namespace SwCSharpAddin1
{
    public class SwLine
    {
        private Vector3 _p1;
        private Vector3 _p2;

        public SwLine(ISketchLine _sketchLine, int _lineInCode)
        {
            ThisSketchLine = _sketchLine;
            LineInCode = _lineInCode;            
        }

        public SwLine(Vector3 __p1, Vector3 __p2, int _lineInCode)
        {
            p1 = __p1;
            p2 = __p2;
            LineInCode = _lineInCode;
        }

        public ISketchLine ThisSketchLine { get; set; }
        public System.Drawing.Color SkecthSegmentColor { get; set; }
        public int LineInCode { get; set; }
        public Vector3 p1 
        {
            get 
            {
                if (ThisSketchLine != null)
                {
                    _p1 = new Vector3();
                    _p1.x = (float)ThisSketchLine.IGetStartPoint2().X;
                    _p1.y = (float)ThisSketchLine.IGetStartPoint2().Y;
                    _p1.z = (float)ThisSketchLine.IGetStartPoint2().Z;
                }
                return _p1; 
            }
            set
            {
                _p1 = value;
            }

        }
        public Vector3 p2
        {
            get
            {
                if (ThisSketchLine != null)
                {
                    _p2 = new Vector3();
                    _p2.x = (float)ThisSketchLine.IGetEndPoint2().X;
                    _p2.y = (float)ThisSketchLine.IGetEndPoint2().Y;
                    _p2.z = (float)ThisSketchLine.IGetEndPoint2().Z;
                }
                return _p2;
            }
            set
            {
                _p2 = value;
            }
        }
        public void UpdateLine(Vector3 _p1, Vector3 _p2, Color _col)
        {
            
        }
    }
}
