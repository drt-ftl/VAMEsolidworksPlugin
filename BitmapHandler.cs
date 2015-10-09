using System;
using System.Collections;
using System.Reflection;
using System.IO;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Drawing.Imaging;

namespace SolidWorksTools.File
{
    public class BitmapHandler : IDisposable
    {
        //The list of bitmap files that have been created in the Temp folder
        ArrayList files;

        public BitmapHandler()
        {
            files = new ArrayList();
        }


        public void Dispose()
        {
            CleanFiles();
        }


        //Creates a file on disk from the specified bitmap resource
        public string CreateFileFromResourceBitmap(string bitmapName, Assembly callingAssy)
        {
            Stream byteStream;
            Bitmap image;
            string filePath;

            //Initial Values
            filePath = System.IO.Path.GetTempFileName();

            try
            {
                //Obtain the bitmap
                byteStream = callingAssy.GetManifestResourceStream(bitmapName);
                image = new Bitmap(byteStream);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return "";
            }

            try
            {
                //Try to save the image to disk
                image.Save(filePath);
                files.Add(filePath);
            }
            catch (Exception e)
            {
                //If it failed to save, return an empy path string
                Console.WriteLine(e.Message);
                return "";
            }
            finally
            {
                //Clean things up 
                image.Dispose();
                byteStream.Close();
                byteStream = null;
            }
            return filePath;
        }


        //Removes the image files from your temp folder
        public bool CleanFiles()
        {
            foreach (string file in files)
            {
                try
                {
                    //Try to delete the file 
                    System.IO.File.Delete(file);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            files.Clear();
            files = null;
            return true;
        }
    }
}

