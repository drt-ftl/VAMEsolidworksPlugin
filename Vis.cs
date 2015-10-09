using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.swpublished;
using SolidWorks.Interop.swconst;
using SolidWorksTools.File;
using SolidWorksTools;
using UnityEngine;
using qwe;
using System.Windows.Forms;

namespace SwCSharpAddin1
{
    public class Vis
    {
        IModelDoc2 myModel;
        PartDoc myPart;
        ISldWorks swApp;
        MaterialVisualPropertiesData myMatVisProps = default(MaterialVisualPropertiesData);
        string configName = null;
        string databaseName = null;
        string newPropName = null;
        bool orgBlend = false;
        bool orgApply = false;
        double orgAngle = 0;
        double orgScale = 0;
        long longstatus = 0;

        public Vis(ISldWorks _swApp, IModelDoc2 _myModel)
        {
            myModel = _myModel;
            swApp = _swApp;
            myModel = (ModelDoc2)swApp.ActiveDoc;
            myPart = (PartDoc)myModel;
            myMatVisProps = myPart.GetMaterialVisualProperties();

        }

        public void UpdateVis()
        {
            if ((myMatVisProps != null))
            {
                //    dump_material_visual_properties(myMatVisProps, myPart);

                //    // Set the material to something else, so that the display changes
                //    configName = "default";
                //    databaseName = "SolidWorks Materials";
                //    newPropName = "Beech";
                //    myPart.SetMaterialPropertyName2(configName, databaseName, newPropName);
                //    dump_material_visual_properties(myMatVisProps, myPart);
                //    // Set the material visual properties to be just color, no advanced graphics
                //    myMatVisProps = myPart.GetMaterialVisualProperties();

                //    if ((myMatVisProps != null))
                //    {
                //        longstatus = myPart.SetMaterialVisualProperties(myMatVisProps, (int)swInConfigurationOpts_e.swThisConfiguration, null);
                //        dump_material_visual_properties(myMatVisProps, myPart);

                //        // Set the material visual properties to be RealView
                //        myMatVisProps.RealView = true;
                //        longstatus = myPart.SetMaterialVisualProperties(myMatVisProps, (int)swInConfigurationOpts_e.swThisConfiguration, null);
                //        dump_material_visual_properties(myMatVisProps, myPart);

                //        // Set the material visual properties to be SolidWorks standard textures
                //        myMatVisProps.RealView = false;
                //        longstatus = myPart.SetMaterialVisualProperties(myMatVisProps, (int)swInConfigurationOpts_e.swThisConfiguration, null);
                //        dump_material_visual_properties(myMatVisProps, myPart);
                //    }

                //    myMatVisProps = myPart.GetMaterialVisualProperties();


                //        // Toggle the apply material color to part flag
                if (myMatVisProps.ApplyMaterialColorToPart == false)
                {
                    orgApply = false;
                }
                else
                {
                    orgApply = true;
                }

                myMatVisProps.ApplyMaterialColorToPart = !orgApply;
                longstatus = myPart.SetMaterialVisualProperties(myMatVisProps, (int)swInConfigurationOpts_e.swThisConfiguration, null);
                dump_material_visual_properties(myMatVisProps, myPart);
                myMatVisProps.ApplyMaterialColorToPart = orgApply;
                longstatus = myPart.SetMaterialVisualProperties(myMatVisProps, (int)swInConfigurationOpts_e.swThisConfiguration, null);
                dump_material_visual_properties(myMatVisProps, myPart);

                //        // Toggle the apply material hatch to drawing section view flag
                //        if (myMatVisProps.ApplyMaterialHatchToSection == false)
                //        {
                //            orgApply = false;
                //        }
                //        else
                //        {
                //            orgApply = true;
                //        }

                //        myMatVisProps.ApplyAppearance = !orgApply;
                //        longstatus = myPart.SetMaterialVisualProperties(myMatVisProps, (int)swInConfigurationOpts_e.swThisConfiguration, null);
                //        dump_material_visual_properties(myMatVisProps, myPart);
                //        myMatVisProps.ApplyAppearance = orgApply;
                //        longstatus = myPart.SetMaterialVisualProperties(myMatVisProps, (int)swInConfigurationOpts_e.swThisConfiguration, null);
                //        dump_material_visual_properties(myMatVisProps, myPart);
                //    }
            }
        }

        private void dump_material_visual_properties(MaterialVisualPropertiesData myMatVisProps, PartDoc myPart)
        {
            string configName = null;
            string databaseName = null;
            string propName = null;
            //bool bBlendColor = false;
            bool bApplyColor = false;
            bool bApplyAppearance = false;
            configName = "default";
            databaseName = null;
            propName = myPart.GetMaterialPropertyName2(configName, out databaseName);

            if ((myMatVisProps != null))
            {
                //bBlendColor = myMatVisProps.BlendColor;
                bApplyColor = myMatVisProps.ApplyMaterialColorToPart;
                bApplyAppearance = myMatVisProps.ApplyAppearance;


                ////MessageBox.Show("   SolidWorks standard texture scale = " + dScale + ", Angle = " + dAngle);
                //if (bBlendColor == false)
                //{
                //    //MessageBox.Show("   Do not blend part color with SolidWorks standard texture.");
                //}
                //else
                //{
                //    //MessageBox.Show("   Blend part color with SolidWorks standard texture.");
                //}
                
                if (bApplyColor == false)
                {
                    //MessageBox.Show("Do not apply material color to part.");
                }
                else
                {
                    //MessageBox.Show("Apply material color to part.");
                }

                if (bApplyAppearance == false)
                {
                    //MessageBox.Show("Do not apply appearance.");
                }
                else
                {
                    //MessageBox.Show("Apply appearance.");
                }

            }
        }
    }
}
