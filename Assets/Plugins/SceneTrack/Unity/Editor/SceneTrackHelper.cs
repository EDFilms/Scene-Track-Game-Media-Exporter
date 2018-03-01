// Copyright 2018 E*D Films. All Rights Reserved.

/**
 * SceneTrackHelper.cs
 *
 * A collection of helper functions used when populating a scene with tracker objects.
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
    /// SceneTrack Helper Functions
    /// </summary>
    public static class Helper
    {
        /// <summary>
        /// Add SceneTrackObject's properly to a MeshRenderer/SkinnedMeshRenderer.
        /// </summary>
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

        /// <summary>
        /// Add SceneTrackObject's properly to any collider.
        /// </summary>
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