using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;

namespace engBIM
{
    [Transaction(TransactionMode.Manual)]
    internal class Class_ParameterScanner : IExternalCommand
    {    
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            //Get the document and the UIDocumnet
            UIDocument uidoc = commandData.Application.ActiveUIDocument;
            Document doc = uidoc.Document;

            //Try to initialize the UI_Window
            try
            {
                UI_ParameterScanner window = new UI_ParameterScanner(uidoc);
                window.ShowDialog();
            }
            //Cath exception if any
            catch (Exception e)
            {
                message = e.Message;
                return Result.Failed;
            }

            return Result.Succeeded;
        }
    }
}
