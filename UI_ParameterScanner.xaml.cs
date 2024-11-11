using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;

namespace engBIM
{
    /// <summary>
    /// Interaction logic for UI_ParameterScanner.xaml
    /// </summary>
    public partial class UI_ParameterScanner : Window
    {
        public UIDocument uidoc { get; }
        public Document doc { get; }
        public View view { get; }

        public UI_ParameterScanner(UIDocument UiDoc)
        {
            uidoc = UiDoc;
            doc = UiDoc.Document;
            view = doc.ActiveView;
            InitializeComponent();
            Title = "Parameter Scanner";
        }

        private void IsolateInView(object sender, RoutedEventArgs e)
        {
            if (txtBox_ParameterName.Text.Length == 0)            
                TaskDialog.Show("Warning", "The parameter name input cannot be left empty.");
           
            else
            {
                //validated that it is in the correct view type
                if (view.ViewType == ViewType.FloorPlan || view.ViewType == ViewType.CeilingPlan || view.ViewType == ViewType.ThreeD)
                {
                    //Get all the elements that have both the parameter name and parameter value
                    var selectedElements = new FilteredElementCollector(doc, view.Id)
                    .WhereElementIsNotElementType()
                    .Where(a =>
                    {
                        // Get the parameter by name
                        var parameterName = a.LookupParameter(txtBox_ParameterName.Text);
                        string parameterValue = txtBox_ParameterValue.Text;

                        // Check if parameter is null or not valid
                        if (parameterName == null)
                            return false;

                        // If parameter exists and no value is specified
                        if (parameterValue == "")
                            return string.IsNullOrEmpty(parameterName.AsString());

                        else
                        {
                            // If there's a parameter value to search for, check if the parameter matches
                            string paramValue = parameterName.AsString();
                            return paramValue != null && paramValue.Contains(parameterValue);
                        }
                    }).ToList();

                    //The list of 'IDs' is initialized, where all the elements that meet the requirements will be stored.
                    ICollection<ElementId> idElements = new List<ElementId>();

                    //Check if any element has the Parameter Name
                    if (selectedElements.Count == 0)
                        TaskDialog.Show("INFO", "No element was found, please make sure the parameter name is correctly wrriten");

                    else
                    {
                        //It shows how many elements meet the requirements.
                        TaskDialog.Show("INFO", selectedElements.Count.ToString() + " Elements found.");

                        //It iterates through all the elements that meet the requirements and stores the ID of each one.
                        foreach (var element in selectedElements)
                        {
                            idElements.Add(element.Id);
                        }

                        //Use a transaction to isolate the elements that meet the requirements.
                        using (Transaction t = new Transaction(doc, "Isolate elements"))
                        {
                            t.Start();
                            view.IsolateElementsTemporary(idElements);
                            t.Commit();
                        }
                    }
                }
                else
                    TaskDialog.Show("Warning", "To run the code, you must be in one of the following view types:\n \n Floor Plan \n Reflected Ceiling Plans \n 3D Views");
            }
        }

        private void Select(object sender, RoutedEventArgs e)
        {
            if (txtBox_ParameterName.Text.Length == 0)            
                TaskDialog.Show("Warning", "The parameter name input cannot be left empty.");
            
            else
            {
                //validated that it is in the correct view type
                if (view.ViewType == ViewType.FloorPlan || view.ViewType == ViewType.CeilingPlan || view.ViewType == ViewType.ThreeD)
                {
                    var selectedElements = new FilteredElementCollector(doc, view.Id)
                    .WhereElementIsNotElementType()
                    .Where(a =>
                    {
                        // Get the parameter by name
                        var parameterName = a.LookupParameter(txtBox_ParameterName.Text);
                        string parameterValue = txtBox_ParameterValue.Text;

                        // Check if parameter is null or not valid
                        if (parameterName == null)
                            return false; 

                        // If parameter exists and no value is specified
                        if (parameterValue == "")                                                    
                            return string.IsNullOrEmpty(parameterName.AsString());
                        
                        else
                        {
                            // If there's a parameter value to search for, check if the parameter matches
                            string paramValue = parameterName.AsString();
                            return paramValue != null && paramValue.Contains(parameterValue);
                        }
                    }).ToList();

                    //The list of 'IDs' is initialized, where all the elements that meet the requirements will be stored.
                    ICollection<ElementId> idElements = new List<ElementId>();

                    //Check if any element has the Parameter Name
                    if (selectedElements.Count == 0)
                        TaskDialog.Show("INFO", "No element was found, please make sure the parameter name is correctly wrriten");

                    else
                    {
                        //It shows how many elements meet the requirements.
                        TaskDialog.Show("INFO", selectedElements.Count.ToString() + " Elements found.");

                        //It iterates through all the elements that meet the requirements and stores the ID of each one.
                        foreach (var element in selectedElements)
                        {
                            idElements.Add(element.Id);
                        }

                        //Select elements that meet the requirements.
                        uidoc.Selection.SetElementIds(idElements);
                    }
                }
                else
                    TaskDialog.Show("Warning", "To run the code, you must be in one of the following view types:\n \n Floor Plan \n Reflected Ceiling Plans \n 3D Views");
            }
        }
    }
}
