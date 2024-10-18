using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.ServiceProcess;
using WindowsServiceTutorial;

public class Delete_Service_BatchFileGenerator
{
    // Function to generate a batch file
    public static void CreateBatchFile(string filePath, string serviceName)
    {
        // Create the content of the batch file without using the placeholder
        string batContent = $"@echo off\n" +
                            $"REM This command must be run as an administrator\n" +
                            $"sc delete {serviceName}\n" +
                            $"if %errorlevel% == 0 (\n" +
                            $"    echo Service deleted successfully.\n" +
                            $") else (\n" +
                            $"    echo Failed to delete the service. Check if the service name is correct.\n" +
                            $")\n" +
                            $"pause";

        try
        {
            // Write the batch content to the specified file
            File.WriteAllText(filePath, batContent);
            Console.WriteLine("Batch file created successfully at: " + filePath);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error writing to file: " + ex.Message);
        }
    }
}

public class Start_Service_BatchFileGenerator
{
    // Function to generate a batch file
    public static void CreateBatchFile(string filePath)
    {

        // Get the directory of the batch file, to construct the path dynamically
        string Framework = @"C:\Windows\Microsoft.NET\Framework\v4.0.30319";
        string projectDirectory = @"C:\Users\blazz\source\repos\WindowsServiceTutorial\WindowsServiceTutorial\bin\Debug";
        string Executable = @"\WindowsServiceTutorial.exe";

        // Create the batch file content
        string batContent = $"@echo off\n" +
                            $"cd {Framework}\n" +
                            $"InstallUtil.exe {projectDirectory}{Executable}\n" +
                            $"pause";

        try
        {
            // Write the batch content to the specified file
            File.WriteAllText(filePath, batContent);
            Console.WriteLine("Batch file created successfully at: " + filePath);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error writing to file: " + ex.Message);
        }
    }
}

public class Generate_Batch_Files
{

    public static void Create_all_Batch_files()
    {
        // Get the path to the project directory
        string projectPath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\.."));
        string start_filePath = Path.Combine(projectPath, "start_service.bat"); // Combine with the batch file name
        string delete_filePath = Path.Combine(projectPath, "delete_service.bat"); // Combine with the batch file name


        ProjectInstaller installer = new ProjectInstaller();

        // Access the exported service name
        string serviceName = installer.ExportedServiceName;

        //Console.WriteLine("The service name is: " + serviceName);  // Output: "The service name is: Service1"
        Start_Service_BatchFileGenerator.CreateBatchFile(start_filePath);
        Delete_Service_BatchFileGenerator.CreateBatchFile(delete_filePath, serviceName);
    }
}
