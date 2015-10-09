using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;

using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.swpublished;
using SolidWorks.Interop.swconst;
using SolidWorksTools.File;

namespace qwe
{
    public class AddFeature
    {
        ISldWorks swApp;
        IModelDoc2 Doc;
        IFeature feat;
        ISelectionMgr SelMan;

        public AddFeature (ISldWorks _swApp)
        {
            swApp = _swApp;
            Doc = swApp.ActiveDoc;
            BitmapHandler iBmp = new BitmapHandler();
            Assembly thisAssembly;
            thisAssembly = System.Reflection.Assembly.GetAssembly(this.GetType());
            var sbm = iBmp.CreateFileFromResourceBitmap("qwe.ToolbarSmall.bmp", thisAssembly);
            var lbm = iBmp.CreateFileFromResourceBitmap("qwe.ToolbarLarge.bmp", thisAssembly);
            Doc.InsertLibraryFeature("Dave's LibFeatPartNameIn");

            var tpv = swApp.CreateTaskpaneView2(lbm, "DoesIt");
            
            tpv.AddStandardButton(0, "HERE!");
            swApp.ActivateTaskPane(1);
            //tpv.AddStandardButton(1, "tooltip!");

            //var toolbar = swApp.AddToolbar4(

            //var SelMan = Doc.SelectionManager;
            //string[] Methods = new string[9];
            //int Names = 0;
            //int Types = 0;
            //int Values = 0;
            //int vEditBodies = 0;
            //long options = 0;
            //int dimTypes = 0;
            //int dimValue = 0;
            //string[] icons = new string[3];

            //var ThisFile = "C:/Analytics";
            //Methods[0] = ThisFile;
            //Methods[1] = "FeatureModule";
            //Methods[2] = "swmRebuild";
            //Methods[3] = ThisFile;
            //Methods[4] = "FeatureModule";
            //Methods[5] = "swmEditDefinition";
            //Methods[6] = "";
            //Methods[7] = "";
            //Methods[8] = "";  //A security routine is optional;
            //var pathname = ThisFile;
            //icons[0] = pathname + "/FeatureIcon.bmp";
            //icons[1] = pathname + "/FeatureIcon.bmp";
            //icons[2] = pathname + "/FeatureIcon.bmp";
            //options = (long)swMacroFeatureOptions_e.swMacroFeatureByDefault;

            //Feature selFeat = SelMan.GetSelectedObject6(1, -1);
            //IFeatureManager swFeatMgr = Doc.FeatureManager;
            //swFeatMgr.InsertMacroFeature3("EmptyFeature", "", (object)Methods, (object)Names, (object)Types, (object)Values, (object)dimTypes, (object)dimValue, (object)vEditBodies, (object)icons, (int)options);
            //var boolstatus = feat.MakeSubFeature(selFeat);
        }

        void tpv_TaskPaneToolbarButtonClicked()
        {
        }
    }
}
