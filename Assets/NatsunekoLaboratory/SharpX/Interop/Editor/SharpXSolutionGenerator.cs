﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

using UnityEditor;

using UnityEditorInternal;

using UnityEngine;

namespace NatsunekoLaboratory.SharpX.Interop
{
    public class SharpXSolutionGenerator : AssetPostprocessor
    {
        private const string DotNetLatestCsProj = @"
<Project Sdk=""Microsoft.NET.Sdk"">
  <PropertyGroup>
    <TargetFramework>netstandard2.0</TargetFramework>
    <LangVersion>10.0</LangVersion>
    <Nullable>enable</Nullable>
    <DisableImplicitNamespaceImports>true</DisableImplicitNamespaceImports>
    <AddAdditionalExplicitAssemblyReferences>false</AddAdditionalExplicitAssemblyReferences>
    <ImplicitlyExpandNETStandardFacades>false</ImplicitlyExpandNETStandardFacades>
    <ImplicitlyExpandDesignTimeFacades>false</ImplicitlyExpandDesignTimeFacades>
  </PropertyGroup>
  <PropertyGroup>
    <DefineConstants>$(DefineConstants)TRACE;SHARPX_COMPILER</DefineConstants>
  </PropertyGroup>
  <PropertyGroup>
    <DefaultItemExcludes>$(DefaultItemExcludes);**/*.meta</DefaultItemExcludes>
    <EnableDefaultItems>false</EnableDefaultItems>
    <EnableDefaultCompileItems>false</EnableDefaultCompileItems>
    <EnableDefaultEmbeddedResourceItems>false</EnableDefaultEmbeddedResourceItems>
    <EnableDefaultNoneItems>false</EnableDefaultNoneItems>
  </PropertyGroup>
  <ItemGroup>
    <Reference Include=""netstandard"" />
  </ItemGroup>
</Project>
";

        private static string OnGeneratedCSProject(string path, string content)
        {
            try
            {
                var obj = AssetDatabase.LoadAssetAtPath<SharpXConfiguration>(SharpXEditorIntegration.Path);
                if (obj == null)
                    return content;
                var assembly = obj.Assemblies.FirstOrDefault(w => JsonUtility.FromJson<AssemblyDefinitionJson>(w.text).name == Path.GetFileNameWithoutExtension(path));
                if (assembly == null)
                    return content;

                var document = XDocument.Parse(content);
                var @namespace = (XNamespace) "http://schemas.microsoft.com/developer/msbuild/2003";
                var project = document.Element(@namespace + "Project");
                var references = new List<string>();

                foreach (var value in project?.Descendants(@namespace + "HintPath") ?? Array.Empty<XElement>())
                {
                    var hintPath = value.Value;
                    if (Path.IsPathRooted(hintPath))
                        continue;
                    if (hintPath.StartsWith("Library"))
                        continue;

                    references.Add(hintPath);
                }

                return GenerateCleanCsproj(obj, assembly, references);
            }
            catch
            {
                return content;
            }
        }

        private static string GenerateCleanCsproj(SharpXConfiguration obj, AssemblyDefinitionAsset assembly, List<string> references)
        {
            var document = XDocument.Parse(DotNetLatestCsProj);
            var project = document.Element("Project");

            // references
            var itemGroup = new XElement("ItemGroup");
            foreach (var reference in obj.References.Select(AssetDatabase.GetAssetPath).Where(w => !string.IsNullOrWhiteSpace(w)))
            {
                var attr = new XAttribute("Include", Path.GetFileNameWithoutExtension(reference));
                var hint = new XElement("HintPath", reference);
                var r = new XElement("Reference", attr, hint);

                itemGroup.Add(r);
            }

            // external references
            foreach (var reference in references)
            {
                var attr = new XAttribute("Include", Path.GetFileNameWithoutExtension(reference));
                var hint = new XElement("HintPath", reference);
                var r = new XElement("Reference", attr, hint);

                itemGroup.Add(r);
            }

            var root = Path.GetDirectoryName(AssetDatabase.GetAssetPath(assembly));

            itemGroup.Add(new XElement("Compile", new XAttribute("Include", $"{root}/**/*.cs")));
            itemGroup.Add(new XElement("None", new XAttribute("Include", $"{root}/**/*.*"), new XAttribute("Exclude", $"{root}/**/*.cs; {root}/**/*.meta")));

            // post process
            var target = new XElement(@"Target", new XAttribute("Name", "PostBuild"), new XAttribute("AfterTargets", "PostBuildEvent"));
            target.Add(new XElement("Exec", new XAttribute("Command", $"$(ProjectDir){AssetDatabase.GetAssetPath(obj.Executable)} build --project $(ProjectDir){root}/sxc.config.json")));

            project?.Add(itemGroup);
            project?.Add(target);

            return document.ToString();
        }

        private class AssemblyDefinitionJson
        {
            public string name;
        }
    }
}