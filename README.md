
# PARAMETER SCANNER

## Description
This Revit 2020 add-in enables users to efficiently scan and filter model elements based on specific parameter values. By isolating or selecting elements according to parameter criteria and the identity of the last user who modified them, this tool significantly optimizes the model review process. It provides architects, engineers, and BIM coordinators with a streamlined way to assess and manage elements based on parameter compliance, improving quality control and workflow efficiency in Revit.

## Requirements
- Revit version: 2020
- .NET Framework: .NET Framework 4.8 

## Installation
To install the addin follow the instructions below:

    1. Clone the repository:
        git clone https://github.com/SebBIMDeveloper/engBIM.git
    2. Build the project:
        Open the project in Visual Studio and compile it in Release mode.
    3. Copy the DLL and ADDIN:
        Copy the resulting DLL and ADDIN file to the Revit add-ins folder:
        %APPDATA%\Autodesk\Revit\Addins\2020\       

## Usage
Once the installation is complete:

![UI](https://github.com/SebBIMDeveloper/engBIM/blob/50431689e135a1b4041ae0879265a1f7268c1976/UI.png)

    1. Open Autodesk Revit 2020
    2. Go to the Parameter Ribbon Tab
    3. Click on the Parameters button, the following window will pop up:
    4. Enter a parameter name.
    5. Enter a parameter value.
    6. Use the left button to isolate the elements that contain the value in the specified parameter.
    7. Use the right button to select the elements that contain the value in the specified parameter.

## License
This project is intended for evaluation purposes only and is not licensed for public distribution or commercial use. The code is submitted as part of an application process with [eng] and is restricted to internal review. All rights to the code remain with the author until a formal agreement is established.


## Contact
Juan Sebastian Galindo Leal.

Githb User: SebBIMDeveloper
