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
    public class Triangle
    {
        private IModelDoc2 swModel;
        private Vector3 p1;
        private Vector3 p2;
        private Vector3 p3;
        private UserControl1 uc;

        public Triangle(IModelDoc2 _mod, UserControl1 _uc, int _id)
        {
            swModel = _mod;
            uc = _uc;
            id = _id;
            name = "Surface-Plane" + (id + 1).ToString();
            isShowing = true;
            //swModel.Extension.SelectByID(name, "SURFACEBODY", 0, 0, 0, false, 0, null);
            //Body2 body = swModel.SelectionManager.GetSelectedObject3(1);
            //var face = body.GetFirstFace();
            //face.Select(0);
            //var bRet = swModel.SelectedFaceProperties(0x00FF0000, 1, 1, 1, 1, 0, 1, false, "");
        }
        public SketchLine l1 { get; set; }
        public SketchLine l2 { get; set; }
        public SketchLine l3 { get; set; }
        public Surface s { get; set; }
        public Body2 b { get; set; }
        public int id { get; set;}

        public string name { get; internal set; }
        public bool isShowing { get; set; }
        public void ChangeVis(double vis)
        {
            //swModel.Extension.SelectByID(name, "SURFACEBODY", 0, 0, 0, false, 0, null);
            //Body2 body = swModel.SelectionManager.GetSelectedObject3(1);
            //var face = body.GetFirstFace();            
            //face.Select(0);
            //var bRet = swModel.SelectedFaceProperties(0x00FF0000, 1, 1, 1, 1, vis, 1, false, "");

            //swModel.Extension.SelectByID("3DSketch1", "3DSKETCH", 1, 1, 1, false, 0, null);
            //swModel.SelectedFeatureProperties(0x000000FF, 1, 1, 1, 1, 0, 1, false, false, "");
        }
        public void Change(int sliderMin, int sliderMax)
        {
            if (isShowing)
            {
                if (sliderMin > id || sliderMax < id)
                {
                    swModel.Extension.SelectByID2(name, "SURFACEBODY", 0, 0, 0, false, 0, null, 0);
                    swModel.FeatureManager.HideBodies();
                    isShowing = false;
                }
            }
            else
            {
                if (sliderMin < id && sliderMax > id)
                {
                    swModel.Extension.SelectByID(name, "SURFACEBODY", 0, 0, 0, false, 0, null);
                    swModel.FeatureManager.ShowBodies();
                    swModel.Extension.SelectByID(name, "SURFACEBODY", 0, 0, 0, false, 0, null);
                    Body2 body = swModel.SelectionManager.GetSelectedObject3(1);
                    var face = body.GetFirstFace();
                    face.Select(0);
                    var bRet = swModel.SelectedFaceProperties(0x0074AABB, 1, 1, 1, 1, uc.transparency, 1, false, "");
                    isShowing = true;
                }
            }
            if (sliderMax == id)
            {
                swModel.Extension.SelectByID(name, "SURFACEBODY", 0, 0, 0, false, 0, null);
                swModel.FeatureManager.ShowBodies();
                swModel.Extension.SelectByID(name, "SURFACEBODY", 0, 0, 0, false, 0, null);
                Body2 body = swModel.SelectionManager.GetSelectedObject3(1);
                var face = body.GetFirstFace();
                face.Select(0);
                var bRet = swModel.SelectedFaceProperties(0x00FF0000, 1, 1, 1, 1, uc.transparency, 1, false, "");
                isShowing = true;
            }
            ChangeVis(1);
        }
    }
}
