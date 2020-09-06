using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;
using UnityEditor;

public static class RPGStatTypeGenerator
{
    private const string DEFAULT_FILE_NAME = "RPGStatType.cs";

    public static void CheckAnGenerateFile()
    {
        string assetPath = GetAssetPathForFile(DEFAULT_FILE_NAME);
        if (string.IsNullOrEmpty(assetPath))
        {
            assetPath = GetAssetPathForFile("RPGStatTypeGenerator.cs").Replace("Editor/RPGStatTypeGenerator.cs", DEFAULT_FILE_NAME);
        }

        WriteStatTypesToFile(assetPath);
    }

    private static string GetAssetPathForFile(string fileName)
    {
        string[] assetPaths = AssetDatabase.GetAllAssetPaths();
        foreach (var path in assetPaths)
        {
            if (path.Contains(fileName))
            {
                return path;
            }
        }
        
        return string.Empty;
    }

    private static void WriteStatTypesToFile(string filePath)
    {
        using (StreamWriter file = File.CreateText(filePath))
        {
            file.WriteLine("public enum RPGStatType {");

            for (int i = 0; i < RPGStatTypeDatabase.Instance.Count; ++i)
            {
                var statType = RPGStatTypeDatabase.Instance.GetAtIndex(i);
                if (!string.IsNullOrEmpty(statType.Name))
                {
                    file.WriteLine($"\t{statType.Name.Replace(" ", string.Empty)} = {statType.Id},");
                }
            }
            
            file.WriteLine("}\n");
        }
        
        AssetDatabase.Refresh();
    }
}
