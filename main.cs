using System;
using System.IO;
using System.Reflection;
using HarmonyLib;

class Program
{
    static void Main()
    {
        Console.WriteLine("Stardew Valley Mod Launcher Installer");
        Console.WriteLine("Enter the path to your Stardew Valley directory:");

        string stardewDirectory = Console.ReadLine();

        if (!Directory.Exists(stardewDirectory))
        {
            Console.WriteLine("Invalid directory. Please provide a valid Stardew Valley directory.");
            return;
        }

        // Install the mod launcher files
        InstallFiles(stardewDirectory);

        // Patch mods using Harmony
        PatchMods(stardewDirectory);

        // Initialize and load mods
        InitializeMods(stardewDirectory);

        // Run the modded Stardew Valley
        RunModdedStardew(stardewDirectory);

        Console.WriteLine("Installation and launch complete. Press any key to exit.");
        Console.ReadKey();
    }

    static void InstallFiles(string stardewDirectory)
    {
        try
        {
            // Copy the executable
            string exePath = Path.Combine(stardewDirectory, "ModLauncher.exe");
            File.WriteAllBytes(exePath, Properties.Resources.ModLauncher);

            // Copy the DLL file
            string dllPath = Path.Combine(stardewDirectory, "ModLauncher.dll");
            File.WriteAllBytes(dllPath, Properties.Resources.ModLauncherDLL);

            Console.WriteLine("Files successfully copied to Stardew Valley directory.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error installing files: {ex.Message}");
        }
    }

    static void PatchMods(string stardewDirectory)
    {
        try
        {
            // Initialize Harmony
            Harmony harmony = new Harmony("com.example.modlauncher");

            // Patch mods using Harmony
            harmony.PatchAll(Assembly.GetExecutingAssembly());

            Console.WriteLine("Mods successfully patched using Harmony.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error patching mods: {ex.Message}");
        }
    }

    static void InitializeMods(string stardewDirectory)
    {
        try
        {
            Console.WriteLine("Scanning for mods...");

            // Search for DLLs with the specified naming convention
            string[] modFiles = Directory.GetFiles(stardewDirectory, "[SDVM]*.dll");

            foreach (var modFile in modFiles)
            {
                Console.WriteLine($"Loading mod: {modFile}");

                // Load the mod assembly
                Assembly modAssembly = Assembly.LoadFrom(modFile);

                // Handle mod initialization here, if necessary
            }

            Console.WriteLine("Mod loading complete.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error initializing mods: {ex.Message}");
        }
    }

    static void RunModdedStardew(string stardewDirectory)
    {
        try
        {
            // Run the modded Stardew Valley
            Assembly assembly = Assembly.LoadFrom(Path.Combine(stardewDirectory, "ModLauncher.exe"));
            Type type = assembly.GetType("ModLauncher");
            MethodInfo method = type.GetMethod("Run");
            object modLauncherInstance = Activator.CreateInstance(type);

            method.Invoke(modLauncherInstance, null);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error running modded Stardew Valley: {ex.Message}");
        }
    }
}
