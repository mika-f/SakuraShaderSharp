using System;
using System.Diagnostics;
using System.IO;
using System.Linq;

using UnityEditor;

using UnityEngine;

using Debug = UnityEngine.Debug;

namespace NatsunekoLaboratory.SharpX.Interop
{
    internal static class SharpXCompilationHook
    {
        [InitializeOnLoadMethod]
        public static void Initialize()
        {
            AssemblyReloadEvents.afterAssemblyReload += OnAssemblyReloaded;
        }

        private static void OnAssemblyReloaded()
        {
            var obj = AssetDatabase.LoadAssetAtPath<SharpXConfiguration>(SharpXEditorIntegration.Path);
            if (obj == null)
                return;

            if (obj.Solution == null)
            {
                foreach (var (assembly, i) in obj.Assemblies.Select(AssetDatabase.GetAssetPath).Select((w, i) => (w, i)))
                {
                    EditorUtility.DisplayProgressBar("Compiling SharpX Projects...", $"Compiling {i + 1} of {obj.Assemblies.Length} projects", (float)i / obj.Assemblies.Length);
                    CompileProjectAssembly(assembly, AssetDatabase.GetAssetPath(obj.Executable));
                }
            }
            else
            {
                EditorUtility.DisplayProgressBar("Compiling SharpX Solution...", "Compiling SharpX Solution Projects...", 0f);
                CompileSolutionAssembly(AssetDatabase.GetAssetPath(obj.Solution), AssetDatabase.GetAssetPath(obj.Executable));
            }

            EditorUtility.ClearProgressBar();
        }

        private static void CompileProjectAssembly(string assembly, string executable)
        {
            try
            {
                var root = Path.GetFullPath(Path.Combine(Application.dataPath, ".."));
                var dir = Path.GetDirectoryName(Path.Combine(root, assembly));
                var info = new ProcessStartInfo(Path.Combine(root, executable), $"build --project {Path.Combine(dir, "sxc.config.json")}")
                {
                    CreateNoWindow = true,
                    UseShellExecute = false
                };

                var compiler = Process.Start(info);
                compiler.WaitForExit();
            }
            catch (Exception ex)
            {
                Debug.LogError(ex.Message);
            }
        }

        private static void CompileSolutionAssembly(string solution, string executable)
        {
            try
            {
                var root = Path.GetFullPath(Path.Combine(Application.dataPath, ".."));
                var info = new ProcessStartInfo(Path.Combine(root, executable), $"build --solution {Path.Combine(root, solution)}")
                {
                    CreateNoWindow = true,
                    UseShellExecute = false
                };

                var compiler = Process.Start(info);
                compiler.WaitForExit();
            }
            catch (Exception ex)
            {
                Debug.LogError(ex.Message);
            }
        }
    }
}