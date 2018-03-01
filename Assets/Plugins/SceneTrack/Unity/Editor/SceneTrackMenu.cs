// Copyright 2018 E*D Films. All Rights Reserved.

/**
 * SceneTrackMenu.cs
 *
 * Menu Items for use within the Unity Editor
 * 
 * @author  dotBunny <hello@dotbunny.com>
 * @version 1
 * @since   1.0.0
 */

using UnityEditor;
using UnityEngine;

namespace SceneTrack.Unity.Editor
{
    /// <summary>
    /// SceneTrack Menu Items
    /// </summary>
    public static class SceneTrackMenu
    {
        /// <summary>
        /// Export Take
        /// </summary>
        [MenuItem("File/Export Take...")]
        private static void ExportTake()
        {
            // Take Selection
            var inputFile = UnityEditor.EditorUtility.OpenFilePanelWithFilters(
                "Select Take File",
                SceneTrack.Unity.Cache.Folder, new string[] {"SceneTrack", Cache.FileExtension});

            if (!string.IsNullOrEmpty(inputFile))
            {
                SceneTrack.Unity.Editor.FbxOutputRunner.Export(inputFile);
            }
        }

        /// <summary>
        /// Should "Export Take" be available?
        /// </summary>
        /// <returns>true/false</returns>
        [MenuItem("File/Export Take", true)]
        private static bool ExportTakeValidation()
        {
            return !(Cache.GetCacheFiles().Length > 0);
        }

        /// <summary>
        /// Add To MeshRenderes
        /// </summary>
        [MenuItem("GameObject/Scene Track/Add To MeshRenderers")]
        private static void AddToMeshRenderers()
        {
            SceneTrack.Unity.Editor.Helper.AddToMeshRenderer();
        }

        /// <summary>
        /// Add To Colliders
        /// </summary>
        [MenuItem("GameObject/Scene Track/Add To Colliders")]
        private static void AddToColliders()
        {
            SceneTrack.Unity.Editor.Helper.AddToColliders();
        }
    }
}