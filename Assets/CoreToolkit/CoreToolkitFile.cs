using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CoreToolkit {
    public static class CoreToolkitFile {

        public static void WriteToFile(string data, string filePath) {
            string fullPath = Path.Combine(Application.persistentDataPath, filePath);

            StreamWriter file;

            if(!File.Exists(fullPath)) {
                file = File.CreateText(fullPath);
                file.WriteLine(data);
                file.Close();
            } else {
                File.WriteAllText(fullPath, data);
            }
        }

        public static bool LoadFile(ref string data, string filePath) {
            string fullPath = Path.Combine(Application.persistentDataPath, filePath);

            if(!File.Exists(fullPath)) {
                return false;
            } else {
                data = File.ReadAllText(fullPath);
                return true;
            }
        }

        public static void DeleteFile(string filePath) {
            string fullPath = Path.Combine(Application.persistentDataPath, filePath);

            if (File.Exists(fullPath))
                File.Delete(fullPath);
        }
    }
}
