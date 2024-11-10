using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.UI;
using Autodesk.Revit;

using Autodesk.Revit.DB.Mechanical;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI.Selection;

using System.Windows.Media.Imaging;
using System.Drawing.Imaging;
using System.Windows.Media;

namespace engBIM
{
    public class Class1 : IExternalApplication
    {
        public Result OnShutdown(UIControlledApplication application)
        {
            return Result.Succeeded;
        }

        public Result OnStartup(UIControlledApplication application)
        {
            string ribbonName = "Parameters";
            //Create a RibbonTab
            application.CreateRibbonTab(ribbonName);

            //Create a RibbonPanel named Parameters
            RibbonPanel ribbonPanel_parameter = application.CreateRibbonPanel(ribbonName, "Parameters");

            //Initialize the pushbuttonData
            PushButtonData pushButtonData_ParameterScanner = new PushButtonData("Parameter Scanner", "Parameter\nScanner", typeof(Class1).Assembly.Location, "engBIM.Class_ParameterScanner");

            //Add the Parameter Button to the RibbonPanel
            PushButton pushButton_ParameterScanner = ribbonPanel_parameter.AddItem(pushButtonData_ParameterScanner) as PushButton;

            //Initialize all the button data
            pushButton_ParameterScanner.ToolTip = "Allows selecting or isolating elements that contain a specific parameter value.";
            pushButton_ParameterScanner.LongDescription = "The button must be executed while in one of the following view types:\n Floor Plans\n Reflected Ceiling Plans\n 3D Views\n\nNOTES:\n1. Make sure to correctly write the parameter name as well as the value to search for.\n 2. The search is case-sensitive, therefore make sure to use the same uppercase and lowercase letters as in the parameter name.";            
            pushButton_ParameterScanner.LargeImage = ConvertImage(Properties.Resources.img_ParamterScanner_32x32);

            return Result.Succeeded;
        }


        //function to convert an imageFormat.png to a bitmap
        private System.Windows.Media.ImageSource ConvertImage(System.Drawing.Image image)
        {
            var bitmap = new System.Windows.Media.Imaging.BitmapImage();
            bitmap.BeginInit();
            System.IO.MemoryStream memoryStream = new System.IO.MemoryStream();
            image.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Png);
            memoryStream.Seek(0, System.IO.SeekOrigin.Begin);
            bitmap.StreamSource = memoryStream;
            bitmap.EndInit();
            return bitmap;
        }
    }
}
