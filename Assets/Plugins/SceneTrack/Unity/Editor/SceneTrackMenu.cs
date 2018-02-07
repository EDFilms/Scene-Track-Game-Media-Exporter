using UnityEditor;
using UnityEngine;

namespace SceneTrack.Unity.Editor
{

    public static class SceneTrackMenu
    {
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

        [MenuItem("File/Export Take", true)]
        private static bool ExportTakeValidation()
        {
            return !(Cache.GetCacheFiles().Length > 0);
        }

        [MenuItem("GameObject/Scene Track/Add To MeshRenderers")]
        private static void AddToMeshRenderers()
        {
            SceneTrack.Unity.Editor.Helper.AddToMeshRenderer();
        }

        [MenuItem("GameObject/Scene Track/Add To Colliders")]
        private static void AddToColliders()
        {
            SceneTrack.Unity.Editor.Helper.AddToColliders();
        }
    }
}
