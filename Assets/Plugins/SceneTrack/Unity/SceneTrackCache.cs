// Copyright 2018 E*D Films. All Rights Reserved.

/**
 * SceneTrack.cs
 *
 * [[[BREIF DESCRIPTION]]]
 * 
 * @author  dotBunny <hello@dotbunny.com>
 * @version 1
 * @since     1.0.0
 */

using System.IO;
using UnityEngine;

namespace SceneTrack.Unity
{
    public static class Cache
    {
        /// <summary>
        /// Location to store cached take information
        /// </summary>
        public static string Folder
        {
            get
            {
                return Application.dataPath + Path.DirectorySeparatorChar + ".." + Path.DirectorySeparatorChar + "SceneTrack" + Path.DirectorySeparatorChar ;
            }

        }

        public static string TempFolder
        {
            get { return Folder + Path.DirectorySeparatorChar + "Temp" + Path.DirectorySeparatorChar; }
        }

        /// <summary>
        /// Extension used with cache files
        /// </summary>
        public static string FileExtension
        {
            get
            {
                return "st";
            }
        }

        public static string[] CachedFiles
        {
            get { return _cachedFiles; }
            set { _cachedFiles = value; }
        }

        private static string[] _cachedFiles = new string[0];

        public static string[] GetCacheFiles()
        {
            // Make Sure The Directory Exists
            if (!Directory.Exists(Folder))
            {
                Directory.CreateDirectory(Folder);
            }

            CachedFiles = Directory.GetFiles(Folder, "*." + FileExtension) ?? new string[0];

            return CachedFiles;
        }

        public static void ClearFiles()
        {
            foreach (var s in GetCacheFiles())
            {
                File.Delete(s);
            }
            _cachedFiles = new string[0];
        }

        public static void ClearFile(string path)
        {
            File.Delete(Folder +  path + "." + FileExtension);
            GetCacheFiles();
        }

        public static int CurrentTakeNumber = 0;

        public static int GetNextTakeNumber()
        {
            return GetCacheFiles().Length + 1;
        }

        public static string GetNextTakeFilename()
        {
            return "Take_" + GetNextTakeNumber().ToString();
        }

        public static void InitTemp()
        {
            if (!Directory.Exists(TempFolder))
            {
                Directory.CreateDirectory(TempFolder);
            }

            var TempFiles = Directory.GetFiles(TempFolder, "*.*") ?? new string[0];
            foreach (var s in TempFiles)
            {
                File.Delete(s);
            }
        }
    }
}