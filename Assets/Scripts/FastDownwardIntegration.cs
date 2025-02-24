using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;

public class FastDownwardIntegration : MonoBehaviour
{
    private static string translatePath = "Assets/Plugins/translate/translate.py";
    private static string PDDLPath = "Assets/PDDL";

    private static string translateFile = "Assets/PDDL/output.sas";

    private static string downwardPath = "Assets/Plugins/downward.exe";

    // translator command line string: /usr/bin/python3 /home/jachyman/fast_downward/downward/builds/release/bin/translate/translate.py my_domain.pddl my_problem.pddl --sas-file output.sas
    // search command line string: /home/jachyman/fast_downward/downward/builds/release/bin/downward --search 'astar(blind())' --internal-plan-file sas_plan < output.sas
    public static void RunFastDownward(string problemName, string domainName, string planName)
    {
        string problemFile = $"{PDDLPath}/{problemName}.pddl";
        string domainFile = $"{PDDLPath}/{domainName}.pddl";
        string outputPlan = $"{PDDLPath}/{planName}";

        var translateArgs = $"{translatePath} {domainFile} {problemFile} --sas-file {translateFile}";
        ProcessStartInfo translateStartInfo = new ProcessStartInfo
        {
            FileName = "python3",
            Arguments = translateArgs,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
            CreateNoWindow = true
        };

        Process translateProcess = new Process();
        translateProcess.StartInfo = translateStartInfo;
        translateProcess.Start();
        translateProcess.WaitForExit();

        
        string downwardArgs = $"--search \"astar(blind())\" --internal-plan-file {outputPlan}";
        if (!File.Exists(downwardPath))
        {
            Debug.LogError($"Executable not found: {downwardPath}");
            return;
        }

        if (!File.Exists(translateFile))
        {
            Debug.LogError($"Input file not found: {translateFile}");
            return;
        }
        /*

        ProcessStartInfo downwardStartInfo = new ProcessStartInfo
        {
            FileName = downwardPath,
            Arguments = downwardArgs,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            //RedirectStandardInput = true,
            UseShellExecute = false,
            CreateNoWindow = true
        };

        Process downwardProcess = new Process();
        downwardProcess.StartInfo = downwardStartInfo;
        Debug.Log("Downward process start");
        downwardProcess.Start();
        downwardProcess.WaitForExit();
        Debug.Log("Downward process end");
        */

        try
        {
            ProcessStartInfo downwardStartInfo = new ProcessStartInfo
            {
                FileName = downwardPath,
                Arguments = downwardArgs,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                RedirectStandardInput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            using (Process process = Process.Start(downwardStartInfo))
            {
                string inputContent = File.ReadAllText(translateFile);
                //Debug.Log("INPUT CONTENT");
                //Debug.Log(inputContent);
                process.StandardInput.Write(inputContent);
                process.StandardInput.Close();

                string output = process.StandardOutput.ReadToEnd();
                string error = process.StandardError.ReadToEnd();

                process.WaitForExit();

                //Debug.Log($"Process Output: {output}");

                if (!string.IsNullOrEmpty(error))
                {
                    Debug.LogError($"Process Error: {error}");
                }
            }
        }
        catch (System.Exception ex)
        {
            Debug.LogError($"Error running process: {ex.Message}");
        }
    }

    /*
    void Start()
    {
        string problemName = "my_new_problem";
        string domainName = "my_domain";
        PDDLHelper.CreatePDDLProblemFile(problemName, board, domainName);

        string planName = "my_plan";
        RunFastDownward(problemName, domainName, planName);
    }
    */
}
