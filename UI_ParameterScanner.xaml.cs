using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

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

        //auxiliary function to get the element IDs that meet the criteria
        private List<ElementId> GetElementIdsByParameterCriteria(string parameterName, string parameterValue)
        {
            return new FilteredElementCollector(doc, view.Id)
                .WhereElementIsNotElementType()
                .Where(a =>
                {
                    // Get the parameter by name
                    var parameter = a.LookupParameter(parameterName);

                    // Check if parameter is null or not valid
                    if (parameter == null)
                        return false;

                    // If parameter exists and no value is specified
                    if (string.IsNullOrEmpty(parameterValue))
                        return string.IsNullOrEmpty(parameter.AsString());

                    // If there's a parameter value to search for, check if the parameter matches
                    string paramValue = parameter.AsString();
                    return paramValue != null && paramValue.Contains(parameterValue);

                }).Select(e => e.Id).ToList(); // Select only the ID of each matching element
        }

        private void IsolateInView(object sender, RoutedEventArgs e)
        {
            if (txtBox_ParameterName.Text.Length == 0)           
                TaskDialog.Show("Warning", "The parameter name input cannot be left empty.");            
            else
            {
                // Validated that it is in the correct view type
                if (view.ViewType == ViewType.FloorPlan || view.ViewType == ViewType.CeilingPlan || view.ViewType == ViewType.ThreeD)
                {
                    // Call the auxiliary function to get the element IDs that meet the criteria
                    var selectedElementIds = GetElementIdsByParameterCriteria(txtBox_ParameterName.Text, txtBox_ParameterValue.Text);

                    // Check if any element has the Parameter Name
                    if (selectedElementIds.Count == 0)                   
                        TaskDialog.Show("INFO", "No element was found, please make sure the parameter name is correctly written");                    
                    else
                    {
                        // Show how many elements meet the requirements
                        TaskDialog.Show("INFO", selectedElementIds.Count.ToString() + " Elements found.");

                        // Use a transaction to isolate the elements that meet the requirements
                        using (Transaction t = new Transaction(doc, "Isolate elements"))
                        {
                            t.Start();
                            view.IsolateElementsTemporary(selectedElementIds);
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
                // Validated that it is in the correct view type
                if (view.ViewType == ViewType.FloorPlan || view.ViewType == ViewType.CeilingPlan || view.ViewType == ViewType.ThreeD)
                {
                    // Call the auxiliary function to get the element IDs that meet the criteria
                    var selectedElementIds = GetElementIdsByParameterCriteria(txtBox_ParameterName.Text, txtBox_ParameterValue.Text);

                    // Check if any element has the Parameter Name
                    if (selectedElementIds.Count == 0)                    
                        TaskDialog.Show("INFO", "No element was found, please make sure the parameter name is correctly written");                    
                    else
                    {
                        // Show how many elements meet the requirements
                        TaskDialog.Show("INFO", selectedElementIds.Count.ToString() + " Elements found.");

                        // Select elements that meet the requirements
                        uidoc.Selection.SetElementIds(selectedElementIds);
                    }
                }
                else                
                    TaskDialog.Show("Warning", "To run the code, you must be in one of the following view types:\n \n Floor Plan \n Reflected Ceiling Plans \n 3D Views");                
            }
        }
    }
}
