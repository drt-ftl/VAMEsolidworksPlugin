using System;
using System.Runtime.InteropServices;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Windows.Forms;
using UnityEngine;

using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.swpublished;
using SolidWorks.Interop.swconst;
using SolidWorksTools.File;
using SolidWorksTools;

using System.IO;
using SwCSharpAddin1;
using System.Threading;
using System.Threading.Tasks;


namespace qwe
{
    #region begin
    /// <summary>
    /// Summary description for qwe.
    /// </summary>
    [Guid("7e5e69fc-ce68-47ee-b3ae-eac40a348327"), ComVisible(true)]
    [SwAddin(
        Description = "qwe description",
        Title = "qwe",
        LoadAtStartup = true
        )]
#endregion
    public class SwAddin : ISwAddin
    {
        #region Local Variables
        public static ISldWorks iSwApp;
        ICommandManager iCmdMgr;
        public IModelDoc2 modDoc;
        int addinID;
        public static Vector3 LastPoint = new Vector3(0.12345678f, 8.7654321f, 99999999);
        public List<string> dmcCode = new List<string>();
        public List<SwLine> dmcLines = new List<SwLine>();
        public List<string> stlCode = new List<string>();
        public List<Triangle> stlSurfaces = new List<Triangle>();
        public List<Vector3[]> stlSurfaceVertices = new List<Vector3[]>();
        public List<int> model_code_xrefDMC = new List<int>();
        public List<int> model_code_xrefSTL = new List<int>();
        int stlCounter = 0;
        public int firstDmcLineInCode = 0;
        public int firstStlLineInCode = 0;
        private float dmcScale = 25 / 2540000f;
        private string dxfCode = "";
        private string header = "  0\nSECTION\n2\nHEADER\n9\n$ACADVER\n1\nAC1009\n0\nENDSEC\n0\nSECTION\n2\nTABLES\n0\nTABLE\n2\nLAYER\n70\n1\n0\nLAYER\n2\n_0\n0\nENDTAB\n0\nENDSEC\n0\nSECTION\n2\nBLOCKS\n0\nENDSEC\n0\nSECTION\n2\nENTITIES";
        private string path = "";
        private string fileName = "";

        public SketchManager swSketchManager;
        public SelectionMgr swSelectionMgr;
        public bool dmcLoaded = false;
        public bool stlLoaded = false;
        TaskpaneView tpv;
        UserControl1 tph;
        int topVisibleSurface;
        int bottomVisibleSurface;
        public enum fileType { DMC, JOB, STL,None}
        public fileType LoadedType;
        private Vis vis;
        #endregion

        #region setup

        #region Event Handler Variables
        Hashtable openDocs;
        SldWorks SwEventPtr;
        #endregion

        #region Property Manager Variables
        //UserPMPage ppage;
        DMCimportPMPage dmcPage;
        #endregion


        // Public Properties
        public ISldWorks SwApp
        {
            get { return iSwApp; }
        }
        public ICommandManager CmdMgr
        {
            get { return iCmdMgr; }
        }


        public List<string> DmcCode
        {
            get { return dmcCode; }
        }

        #region SolidWorks Registration
        [ComRegisterFunctionAttribute]
        public static void RegisterFunction(Type t)
        {

            #region Get Custom Attribute: SwAddinAttribute
            SwAddinAttribute SWattr = null;
            Type type = typeof(SwAddin);
            foreach (System.Attribute attr in type.GetCustomAttributes(false))
            {
                if (attr is SwAddinAttribute)
                {
                    SWattr = attr as SwAddinAttribute;
                    break;
                }
            }
            #endregion

            Microsoft.Win32.RegistryKey hklm = Microsoft.Win32.Registry.LocalMachine;
            Microsoft.Win32.RegistryKey hkcu = Microsoft.Win32.Registry.CurrentUser;

            string keyname = "SOFTWARE\\SolidWorks\\Addins\\{" + t.GUID.ToString() + "}";
            Microsoft.Win32.RegistryKey addinkey = hklm.CreateSubKey(keyname);
            addinkey.SetValue(null, 0);

            addinkey.SetValue("Description", SWattr.Description);
            addinkey.SetValue("Title", SWattr.Title);

            keyname = "Software\\SolidWorks\\AddInsStartup\\{" + t.GUID.ToString() + "}";
            addinkey = hkcu.CreateSubKey(keyname);
            addinkey.SetValue(null, Convert.ToInt32(SWattr.LoadAtStartup));
        }

        [ComUnregisterFunctionAttribute]
        public static void UnregisterFunction(Type t)
        {
            Microsoft.Win32.RegistryKey hklm = Microsoft.Win32.Registry.LocalMachine;
            Microsoft.Win32.RegistryKey hkcu = Microsoft.Win32.Registry.CurrentUser;

            string keyname = "SOFTWARE\\SolidWorks\\Addins\\{" + t.GUID.ToString() + "}";
            hklm.DeleteSubKey(keyname);

            keyname = "Software\\SolidWorks\\AddInsStartup\\{" + t.GUID.ToString() + "}";
            hkcu.DeleteSubKey(keyname);
        }

        #endregion

        #region ISwAddin Implementation
        public SwAddin()
        {
        }

        public bool ConnectToSW(object ThisSW, int cookie)
        {
            iSwApp = (ISldWorks)ThisSW;
            addinID = cookie;

            //Setup callbacks
            iSwApp.SetAddinCallbackInfo(0, this, addinID);

            #region Setup the Command Manager
            iCmdMgr = iSwApp.GetCommandManager(cookie);
            AddCommandMgr();
            #endregion

            #region Setup the Event Handlers
            SwEventPtr = (SldWorks)iSwApp;
            openDocs = new Hashtable();
            AttachEventHandlers();
            #endregion

            #region Setup Sample Property Manager
            AddPMP();
            #endregion

            return true;
        }

        public bool DisconnectFromSW()
        {
            RemoveCommandMgr();
            RemovePMP();
            DetachEventHandlers();

            iSwApp = null;
            //The addin _must_ call GC.Collect() here in order to retrieve all managed code pointers 
            GC.Collect();
            return true;
        }
        #endregion

        #region UI Methods
        public void AddCommandMgr()
        {
            AddTaskPane();
            ICommandGroup cmdGroup;
            BitmapHandler iBmp = new BitmapHandler();
            Assembly thisAssembly;

            thisAssembly = System.Reflection.Assembly.GetAssembly(this.GetType());

            cmdGroup = iCmdMgr.CreateCommandGroup(1, "FTL AMF Visualizer", "FTL AMF Visualizer", "", -1);
            cmdGroup.LargeIconList = iBmp.CreateFileFromResourceBitmap("qwe.ToolbarLarge.bmp", thisAssembly);
            cmdGroup.SmallIconList = iBmp.CreateFileFromResourceBitmap("qwe.ToolbarSmall.bmp", thisAssembly);
            cmdGroup.LargeMainIcon = iBmp.CreateFileFromResourceBitmap("qwe.ToolbarLarge.bmp", thisAssembly);
            cmdGroup.SmallMainIcon = iBmp.CreateFileFromResourceBitmap("qwe.ToolbarSmall.bmp", thisAssembly);

            //cmdGroup.AddCommandItem("CreateCube", -1, "Create a cube", "Create cube", 0, "CreateCube", "", 0);
            //cmdGroup.AddCommandItem("Show PMP", -1, "Display sample property manager", "Show PMP", 2, "ShowPMP", "EnablePMP", 2);
            cmdGroup.AddCommandItem("ImportDMC", -1, "Import a DMC file", "Import a DMC file", 0, "importDMC", "", 0);
            //cmdGroup.AddCommandItem("AddTaskPane", -1, "Adds Features and Taskpane and Toolbars", "Adds Features and Taskpane and Toolbars", 2, "AddTaskPane", "", 2);

            cmdGroup.HasToolbar = true;
            cmdGroup.HasMenu = true;
            cmdGroup.Activate();

            thisAssembly = null;

            iBmp.Dispose();
        }


        public void RemoveCommandMgr()
        {
            iCmdMgr.RemoveCommandGroup(1);
        }

        public Boolean AddPMP()
        {
            //ppage = new UserPMPage(this);
            dmcPage = new DMCimportPMPage(this);
            return true;
        }

        public Boolean RemovePMP()
        {
            dmcPage = null;
            //ppage = null;
            return true;
        }

        #endregion

        #region UI Callbacks

        public void importDMC()
        {
            loadFile();
        }

        public void CreateCube()
        {
            //make sure we have a part open
            string partTemplate = iSwApp.GetUserPreferenceStringValue((int)swUserPreferenceStringValue_e.swDefaultTemplatePart);
            IModelDoc2 modDoc = (IModelDoc2)iSwApp.NewDocument(partTemplate, (int)swDwgPaperSizes_e.swDwgPaperA2size, 0.0, 0.0);

            modDoc.InsertSketch2(true);
            modDoc.SketchRectangle(0, 0, 0, .1, .1, .1, false);
            //Extrude the sketch
            IFeatureManager featMan = modDoc.FeatureManager;
            featMan.FeatureExtrusion(true,
                false, false,
                (int)swEndConditions_e.swEndCondBlind, (int)swEndConditions_e.swEndCondBlind,
                0.1, 0.0,
                false, false,
                false, false,
                0.0, 0.0,
                false, false,
                false, false,
                true,
                false, false);
        }


        public void ShowPMP()
        {
            if (dmcPage != null)
                dmcPage.Show();
        }

        public void AddFeat()
        {
            var af = new AddFeature(iSwApp);
        }

        public int EnablePMP()
        {
            if (iSwApp.ActiveDoc != null)
                return 1;
            else
                return 0;
        }
        #endregion

        #region Event Methods
        public bool AttachEventHandlers()
        {
            AttachSwEvents();
            //Listen for events on all currently open docs
            ModelDoc2 modDoc;
            modDoc = (ModelDoc2)iSwApp.GetFirstDocument();
            //iModelDoc = modDoc;
            while (modDoc != null)
            {
                if (!openDocs.Contains(modDoc))
                {
                    AttachModelDocEventHandler(modDoc);
                }
                modDoc = (ModelDoc2)modDoc.GetNext();
            }
            return true;
        }

        private bool AttachSwEvents()
        {
            try
            {
                SwEventPtr.ActiveDocChangeNotify += new DSldWorksEvents_ActiveDocChangeNotifyEventHandler(OnDocChange);
                SwEventPtr.DocumentLoadNotify += new DSldWorksEvents_DocumentLoadNotifyEventHandler(OnDocLoad);
                SwEventPtr.FileNewNotify2 += new DSldWorksEvents_FileNewNotify2EventHandler(OnFileNew);
                SwEventPtr.ActiveModelDocChangeNotify += new DSldWorksEvents_ActiveModelDocChangeNotifyEventHandler(OnModelChange);
                
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }


        private bool DetachSwEvents()
        {
            try
            {
                SwEventPtr.ActiveDocChangeNotify -= new DSldWorksEvents_ActiveDocChangeNotifyEventHandler(OnDocChange);
                SwEventPtr.DocumentLoadNotify -= new DSldWorksEvents_DocumentLoadNotifyEventHandler(OnDocLoad);
                SwEventPtr.FileNewNotify2 -= new DSldWorksEvents_FileNewNotify2EventHandler(OnFileNew);
                SwEventPtr.ActiveModelDocChangeNotify -= new DSldWorksEvents_ActiveModelDocChangeNotifyEventHandler(OnModelChange);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }

        }

        public bool AttachModelDocEventHandler(ModelDoc2 modDoc)
        {
            if (modDoc == null)
                return false;

            DocumentEventHandler docHandler = null;

            if (!openDocs.Contains(modDoc))
            {
                switch (modDoc.GetType())
                {
                    case (int)swDocumentTypes_e.swDocPART:
                        {
                            docHandler = new PartEventHandler(modDoc, this);
                            break;
                        }
                    case (int)swDocumentTypes_e.swDocASSEMBLY:
                        {
                            docHandler = new AssemblyEventHandler(modDoc, this);
                            break;
                        }
                    case (int)swDocumentTypes_e.swDocDRAWING:
                        {
                            docHandler = new DrawingEventHandler(modDoc, this);
                            break;
                        }
                    default:
                        {
                            return false; //Unsupported document type
                        }
                }
                docHandler.AttachEventHandlers();
                openDocs.Add(modDoc, docHandler);
            }
            return true;
        }

        public bool DetachModelEventHandler(ModelDoc2 modDoc)
        {
            DocumentEventHandler docHandler;
            docHandler = (DocumentEventHandler)openDocs[modDoc];
            openDocs.Remove(modDoc);
            modDoc = null;
            docHandler = null;
            return true;
        }

        public bool DetachEventHandlers()
        {
            DetachSwEvents();

            //Close events on all currently open docs
            DocumentEventHandler docHandler;
            int numKeys = openDocs.Count;
            object[] keys = new object[numKeys];

            //Remove all document event handlers
            openDocs.Keys.CopyTo(keys, 0);
            foreach (ModelDoc2 key in keys)
            {
                docHandler = (DocumentEventHandler)openDocs[key];
                docHandler.DetachEventHandlers(); //This also removes the pair from the hash
                docHandler = null;
            }
            return true;
        }
        #endregion

        #region Event Handlers
        //Events
        public int OnDocChange()
        {
            return 0;
        }

        public int OnDocLoad(string docTitle, string docPath)
        {
            ModelDoc2 modDoc = (ModelDoc2)iSwApp.GetFirstDocument();
            while (modDoc != null)
            {
                if (modDoc.GetTitle() == docTitle)
                {
                    if (!openDocs.Contains(modDoc))
                    {
                        AttachModelDocEventHandler(modDoc);
                    }
                }
                modDoc = (ModelDoc2)modDoc.GetNext();
            }
            return 0;
        }

        public int OnFileNew(object newDoc, int docType, string templateName)
        {
            return 0;
        }

        public int OnModelChange()
        {
            return 0;
        }

        #endregion

        #endregion


        public void loadFile()
        {
            System.Windows.Forms.OpenFileDialog openFileDialog = new System.Windows.Forms.OpenFileDialog();
            openFileDialog.InitialDirectory = "C:/Samples";
            openFileDialog.Filter = "DMC Files (*.DMC)|*.DMC;*.dmc|STL Files (*.stl, *.STL)|*.stl;*.STL";
            openFileDialog.FilterIndex = 2;
            openFileDialog.RestoreDirectory = false;
            openFileDialog.Title = "Select File";

            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                try
                {
                    fileName = openFileDialog.FileName;
                    path = Path.GetDirectoryName(fileName);
                    string extension = new FileInfo(fileName).Extension;
                    switch (extension)
                    {
                        case ".dmc":
                        case ".DMC":
                            LoadedType = fileType.DMC;
                            if (stlLoaded)
                            {
                                tph.SwitchType.Visible = true;
                                tph.SwitchType.Text = "Switch To STL";
                            }
                            break;
                        case ".stl":
                        case ".STL":
                            LoadedType = fileType.STL;
                            if (stlLoaded)
                            {
                                tph.SwitchType.Visible = true;
                                tph.SwitchType.Text = "Switch To DMC";
                            }
                            break;
                        default:
                            LoadedType = fileType.None;
                            break;
                    }
                    
                    if (fileName != null)
                    {
                        var reader = new StreamReader(fileName);
                        string partTemplate = iSwApp.GetUserPreferenceStringValue((int)swUserPreferenceStringValue_e.swDefaultTemplatePart);
                        if (modDoc == null)
                        {
                            modDoc = (IModelDoc2)iSwApp.NewDocument(partTemplate, (int)swDwgPaperSizes_e.swDwgPaperA2size, 0.0, 0.0);
                            modDoc.ShowNamedView2("*Isometric", -1);
                            swSelectionMgr = modDoc.SelectionManager;
                            vis = new Vis(iSwApp, modDoc);
                        }

                        while (!reader.EndOfStream)
                        {
                            string line = reader.ReadLine();
                            switch (LoadedType)
                            {
                                case fileType.DMC:
                                    scanDMC(line);
                                    break;
                                case fileType.STL:
                                    scanSTL(line);
                                    break;
                                default:
                                    break;
                            }
                        }
                        switch (LoadedType)
                        {
                            case fileType.DMC:
                                StartSwLines();
                                break;
                            case fileType.STL:
                                MakeTriangles();
                                break;
                            default:
                                break;
                        }
                    }
                }
                catch { }
            }
        }

        #region STL

        private float xMinSTL = 0;
        private float xMaxSTL = 0;
        private float yMinSTL = 0;
        private float yMaxSTL = 0;
        private float zMinSTL = 0;
        private float zMaxSTL = 0;
        public static float stlScale = 1f;
        private List<Vector3> currentVertices = new List<Vector3>();

        void scanSTL(string _line)
        {
            _line = _line.Trim();
            stlCode.Add(_line.ToString() + "\r\n");
            var chunks = _line.Split(' ');
            if (_line.Contains("outer"))
            {
                
            }
            else if (_line.Contains("endloop"))
            {
                endloop(_line, modDoc);
            }
            else if (_line.Contains("vertex"))
            {
                vertex(_line);
            }
        }
        public void endloop(string _line, IModelDoc2 modDoc)
        {
            try
            {
                if (model_code_xrefSTL.Count == 0)
                    firstStlLineInCode = stlCode.Count - 5;
                model_code_xrefSTL.Add(stlCode.Count - 5);
                var verts = currentVertices.ToArray();
                stlSurfaceVertices.Add(verts);
                var newTriangle = new Triangle(modDoc, tph, stlCounter);
                stlSurfaces.Add(newTriangle);
                currentVertices.Clear();
                stlCounter++;
                modDoc.ViewZoomtofit();
            }
            catch { }
        }

        public void vertex(string _line)
        {
            {
                float x;
                float y;
                float z;
                var coordSep = _line.Split('x');
                var coords = coordSep[1].TrimStart(' ').Split(' ');

                var xString = coords[0];
                var yString = coords[1];
                var zString = coords[2];
                var xStrSplit = xString.Split('e');
                if (float.TryParse(xStrSplit[0], out x))
                {
                    float xE;
                    if (float.TryParse(xStrSplit[1], out xE))
                        x *= (Mathf.Pow(10f, xE));
                }

                var yStrSplit = yString.Split('e');
                if (float.TryParse(yStrSplit[0], out y))
                {
                    float yE;
                    if (float.TryParse(yStrSplit[1], out yE))
                        y *= (Mathf.Pow(10f, yE));
                }

                var zStrSplit = zString.Split('e');
                if (float.TryParse(zStrSplit[0], out z))
                {
                    float zE;
                    if (float.TryParse(zStrSplit[1], out zE))
                        z *= (Mathf.Pow(10f, zE));
                }
                var newVertex = new Vector3(x, y, z) * stlScale;
                if (x > xMaxSTL) xMaxSTL = x;
                if (x < xMinSTL) xMinSTL = x;
                if (y > yMaxSTL) yMaxSTL = y;
                if (y < yMinSTL) yMinSTL = y;
                if (z > zMaxSTL) zMaxSTL = z;
                if (z < zMinSTL) zMinSTL = z;
                currentVertices.Add(newVertex);
            }
        }

        public void MakeTriangles()
        {
            swSketchManager = modDoc.SketchManager;
            var i = 0;
            foreach (var verts in stlSurfaceVertices)
            {
                swSketchManager.Insert3DSketch(true);
                var p1 = verts[0];
                var p2 = verts[1];
                var p3 = verts[2];
                stlSurfaces[i].l1 = swSketchManager.CreateLine((double)p1.x, (double)p1.y, (double)p1.z, (double)p2.x, (double)p2.y, (double)p2.z) as SketchLine;
                stlSurfaces[i].l2 = swSketchManager.CreateLine((double)p2.x, (double)p2.y, (double)p2.z, (double)p3.x, (double)p3.y, (double)p3.z) as SketchLine;
                stlSurfaces[i].l3 = swSketchManager.CreateLine((double)p3.x, (double)p3.y, (double)p3.z, (double)p1.x, (double)p1.y, (double)p1.z) as SketchLine;
                var plane = modDoc.InsertPlanarRefSurface();
                var name = "Surface-Plane" + (i + 1).ToString();
                modDoc.Extension.SelectByID(name, "SURFACEBODY", 0, 0, 0, false, 0, null);
                Body2 body = modDoc.ISelectionManager.GetSelectedObject(1);
                body.SetMaterialProperty("Default", "solidworks materials.sldmat", "Pure Gold");
                stlSurfaces[i].b = body;
                i++;
            }
            modDoc.ViewZoomtofit2();
            //swSketchManager.InsertSketch(true);
            Finish();
        }

        #endregion

        #region DMC

        public float xMinDMC = 0;
        public float xMaxDMC = 0;
        public float yMinDMC = 0;
        public float yMaxDMC = 0;
        public float zMinDMC = 0;
        public float zMaxDMC = 0;
        public float yDMC = 0;

        public void scanDMC(string _line)
        {
            _line = _line.Trim();
            dmcCode.Add(_line.ToString() + "\r\n");
            var chunks = _line.Split(' ');
            switch (chunks[0])
            {
                case "PA":
                    StartsWithPA(_line);
                    break;
                case "REM":
                    StartsWithREM(_line);
                    break;
                default:
                    break;
            }
        }

        public void StartsWithPA(string _line)
        {
            var _chunks = _line.Split(' ');
            var _xy = _chunks[1].Split(',');
            float x;
            float z;
            Vector3 newVertex = new Vector3();
            if (float.TryParse(_xy[0], out x))
                newVertex.x = x;
            if (float.TryParse(_xy[1], out z))
                newVertex.z = z;
            newVertex.y = yDMC;
            newVertex *= dmcScale;
            //newVertex /= 50f;
            //if (newVertex.x > xMax) xMax = newVertex.x;
            //if (newVertex.x < xMin) xMin = newVertex.x;
            //if (newVertex.y > yMax) yMax = newVertex.y;
            //if (newVertex.y < yMin) yMin = newVertex.y;
            //if (newVertex.z > zMax) zMax = newVertex.z;
            //if (newVertex.z < zMin) zMin = newVertex.z;
            if (LastPoint == new Vector3 (0.12345678f, 8.7654321f, 99999999))
            {
                LastPoint = newVertex;
            }
            else
            {
                CreateDxfLine(LastPoint, newVertex);
                CreateSwLine(LastPoint, newVertex);
                LastPoint = newVertex;
            }
        }

        public void StartsWithREM(string _line)
        {
            var _chunks = _line.Split(' ');
            if (_chunks[1] == "Layer" && _chunks[2] == "Change")
            {
                yDMC += 0.015f * 100000;
            }
        }
        public void StartSwLines()
        {
            swSketchManager = modDoc.SketchManager;
            swSketchManager.Insert3DSketch(true);
            foreach (var l in dmcLines)
            {
                DrawSwLine(l);
            }
            swSketchManager.ActiveSketch.SetAutomaticSolve(true);
            Finish();
        }

        public void CreateDxfLine(Vector3 p1, Vector3 p2)
        {
            dxfCode += "\n0\nLINE\n8\nPolygon";
            dxfCode += "\n10\n" + p1.x.ToString("f7");
            dxfCode += "\n20\n" + p1.y.ToString("f7");
            dxfCode += "\n30\n" + p1.z.ToString("f7");

            dxfCode += "\n11\n" + p2.x.ToString("f7");
            dxfCode += "\n21\n" + p2.y.ToString("f7");
            dxfCode += "\n31\n" + p2.z.ToString("f7");
        }

        public void CreateSwLine(Vector3 p1, Vector3 p2)
        {
            if (model_code_xrefDMC.Count == 0)
                firstDmcLineInCode = dmcCode.Count - 1;
            model_code_xrefDMC.Add(dmcCode.Count - 1);
            var l = new SwLine(p1, p2, dmcCode.Count - 1);
            dmcLines.Add(l);
            modDoc.ViewZoomtofit2();            
        }

        public void DrawSwLine(SwLine swLine)
        {
            var p1 = swLine.p1;
            var p2 = swLine.p2;
            swLine.ThisSketchLine = swSketchManager.CreateLine(p1.x, p1.y, p1.z, p2.x, p2.y, p2.z) as SketchLine;
        }

        public void ClearDMC()
        {
            xMinDMC = 0;
            xMaxDMC = 0;
            yMinDMC = 0;
            yMaxDMC = 0;
            zMinDMC = 0;
            zMaxDMC = 0;
            yDMC = 0;
            Vector3 LastPoint = new Vector3(0.12345678f, 8.7654321f, 99999999);
            dmcCode.Clear();
            dmcLines.Clear();
            model_code_xrefDMC.Clear();
            firstDmcLineInCode = 0;
            dxfCode = "";
            path = "";
            fileName = "";
            //dmcLoaded = false;
        }

        public void WriteDMCtoDXF()
        {
            string extension = new FileInfo(fileName).Extension;
            var dxfFullName = fileName.Replace(extension, ".dxf");
            string shortName = new FileInfo(dxfFullName).Name;
            var sw = File.CreateText(dxfFullName);
            sw.Write(header);
            sw.Write(dxfCode);
            sw.Write("\n0\nENDSEC\n0\nEOF");
            sw.Close();
        }

        #endregion

        public void Finish()
        {
            switch (LoadedType)
            {
                case fileType.DMC:
                    dmcLoaded = true;
                    SwitchTaskPane();
                    break;
                case fileType.STL:
                    stlLoaded = true;
                    SwitchTaskPane();
                    break;
                default:
                    break;
            }            
            //modDoc.InsertSketch2(true);
        }

        public void AddTaskPane()
        {
            tpv = iSwApp.CreateTaskpaneView2("", "FTL VAME Explorer");
            tpv.ShowView();
            tph = tpv.AddControl("TestPane", "");
            tph.getSwApp(iSwApp);
            tph.getSwAddin(this);
        }

        public void SwitchTaskPane()
        {
            switch (LoadedType)
            {
                case fileType.DMC:
                    tph.BeginCode(model_code_xrefDMC.Count - 1);
                    break;
                case fileType.STL:
                    tph.BeginCode(model_code_xrefSTL.Count - 1);
                    break;
                default:
                    break;
            }
        }
    }

    #region SwAddin class attributes
    [AttributeUsage(AttributeTargets.Class)]
    public class SwAddinAttribute : System.Attribute
    {
        public string Description = "";
        public string Title = "";
        public bool LoadAtStartup = false;

        public SwAddinAttribute()
        {
        } 
    }
    #endregion
}
