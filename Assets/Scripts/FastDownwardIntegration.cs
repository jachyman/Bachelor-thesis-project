using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Debug = UnityEngine.Debug;

public class FastDownwardIntegration : MonoBehaviour
{

    // translator command line string: /usr/bin/python3 /home/jachyman/fast_downward/downward/builds/release/bin/translate/translate.py my_domain.pddl my_problem.pddl --sas-file output.sas
    // search command line string: /home/jachyman/fast_downward/downward/builds/release/bin/downward --search 'astar(blind())' --internal-plan-file sas_plan < output.sas
    public static void RunFastDownward(string problemName, string domainName, string planName)
    {
        string GeneratedPDDLFilesPath = Application.persistentDataPath;
        //string translatePath = Application.streamingAssetsPath + "/translate/translate.py";
        string translatePath = Path.Combine(Application.streamingAssetsPath, "translate", "translate.py");
        //string translateFile = Application.persistentDataPath + "/output.sas";
        string translateFile = Path.Combine(Application.persistentDataPath, "output.sas");
        //string downwardPath = Application.streamingAssetsPath + "/downward.exe";
        string downwardPath = Path.Combine(Application.streamingAssetsPath, "downward.exe");

        //string problemFile = GeneratedPDDLFilesPath + $"/{problemName}.pddl";
        string problemFile = Path.Combine(GeneratedPDDLFilesPath, $"{problemName}.pddl");
        //string domainFile = Application.streamingAssetsPath + $"/{domainName}.pddl";
        string domainFile = Path.Combine(Application.streamingAssetsPath, $"{domainName}.pddl");
        //string outputPlan = GeneratedPDDLFilesPath + $"/{planName}.pddl";
        string outputPlan =  Path.Combine(GeneratedPDDLFilesPath, $"{planName}.pddl");

        if (!File.Exists(translatePath))
        {
            Debug.LogError($"Executable not found: {translatePath}");
            return;
        }
        if (!File.Exists(downwardPath))
        {
            Debug.LogError($"Executable not found: {downwardPath}");
            return;
        }
        if (!File.Exists(problemFile))
        {
            Debug.LogError($"Executable not found: {problemFile}");
            return;
        }
        if (!File.Exists(domainFile))
        {
            Debug.LogError($"Executable not found: {domainFile}");
            return;
        }

        var translateArgs = $"\"{translatePath}\" \"{domainFile}\" \"{problemFile}\" --sas-file \"{translateFile}\"";
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

        // for debugging translate.py

        // Configure to capture output and error streams
        translateProcess.StartInfo.RedirectStandardOutput = true;
        translateProcess.StartInfo.RedirectStandardError = true;
        translateProcess.StartInfo.UseShellExecute = false;

        // Event handlers for asynchronous output reading
        string translateOutput = "";
        string translateError = "";
        translateProcess.OutputDataReceived += (sender, args) => { if (args.Data != null) translateOutput += args.Data + "\n"; };
        translateProcess.ErrorDataReceived += (sender, args) => { if (args.Data != null) translateError += args.Data + "\n"; };

        translateProcess.Start();

        translateProcess.BeginOutputReadLine();
        translateProcess.BeginErrorReadLine();

        translateProcess.WaitForExit();

        Debug.Log("translate exit code " + translateProcess.ExitCode);
        Debug.Log($"Standard output: {translateOutput}");
        Debug.Log($"Error output: {translateError}");
        translateProcess.Close();


        if (!File.Exists(translateFile))
        {
            Debug.LogError($"Executable not found: {translateFile}");
            return;
        }

        string downwardArgs = $"--search \"astar(blind())\" --internal-plan-file \"{outputPlan}\"";
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
                process.StandardInput.Write(inputContent);
                process.StandardInput.Close();

                string output = process.StandardOutput.ReadToEnd();
                string error = process.StandardError.ReadToEnd();

                process.WaitForExit();

                Debug.Log("downward output: " + output);
                Debug.Log("downward error: " + error);

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
}
