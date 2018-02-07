using UnityEditor;
using UnityEngine;

namespace SceneTrack.Unity.Editor
{
    public static class Helper
    {
        public static void AddToMeshRenderer()
        {
            foreach (var skMr in UnityEngine.Object.FindObjectsOfType<SkinnedMeshRenderer>())
            {
                if (skMr.GetComponent<SceneTrackObject>() == null)
                {
                    Unity.Helper.RecursiveBackwardsAddObject(skMr.transform);
                }
            }

            foreach (var mr in UnityEngine.Object.FindObjectsOfType<MeshRenderer>())
            {
                if (mr.GetComponent<SceneTrackObject>() == null)
                {
                    Unity.Helper.RecursiveBackwardsAddObject(mr.transform);
                }
            }
        }

        public static void AddToColliders()
        {
            foreach (var collider in UnityEngine.Object.FindObjectsOfType<Collider>())
            {
                if (collider.GetComponent<SceneTrackObject>() == null)
                {
                    collider.gameObject.AddComponent<SceneTrackObject>();
                }
            }
        }
    }
}