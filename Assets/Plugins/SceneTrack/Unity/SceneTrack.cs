// Copyright 2018 E*D Films. All Rights Reserved.

/**
 * SceneTrack.cs
 *
 * Static controller for SceneTrack project integration.
 * 
 * @author  dotBunny <hello@dotbunny.com>
 * @version 1
 * @since	1.0.0
 */

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using SceneTrack;
using UnityEditor;
using UnityEngine;

namespace SceneTrack.Unity
{
    /// <summary>
    /// SceneTrack System Controller
    /// </summary>
    public static class System
    {
        public static string FilenameOverride = string.Empty;
        public static List<SceneTrackObject> CachedKnownObjects = new List<SceneTrackObject>();


        public static Dictionary<Mesh, uint>  SharedMeshes = new Dictionary<Mesh, uint>();
        public static Dictionary<Material, uint>  SharedMaterials = new Dictionary<Material, uint>();

        /// <summary>
        /// Has SceneTrack been initialized for this recording.
        /// </summary>
        public static bool InPlayMode { get; private set; }

        /// <summary>
        /// The current instance handle for SceneTrack
        /// </summary>
        public static uint InstanceHandle { get; private set; }

        public static uint LastUpdate { get; set; }

        public static uint FrameCount { get; private set; }

        /// <summary>
        /// Called when play mode is entered, sets up object in SceneTrack core
        /// </summary>
        public static void EnterPlayMode()
        {
            // Bail Out Check
            if (InPlayMode || InstanceHandle != 0) return;

            var fileName = Cache.GetNextTakeFilename();
            if ( !string.IsNullOrEmpty(FilenameOverride))
            {
                // Find next number
                fileName = FilenameOverride;
            }

            // Start Recording
            InstanceHandle = Recording.CreateRecording();

            int recordTime = Mathf.Clamp(EditorPrefs.GetInt("SceneTrack_AppendRecordFrames", 2), 1, 30);
            int framePool  = EditorPrefs.GetInt("SceneTrack_MemoryReserveFramePool", 0);
            int frameDataPool = EditorPrefs.GetInt("SceneTrack_MemoryReserveFrameDataPool", 0);
            
            Recording.MemoryReserve(0, framePool);
            Recording.MemoryReserve(1, frameDataPool);
            
            // Setup the new take for recording, writing every 2 frames
            Recording.AppendSaveRecording(new StringBuilder(Cache.Folder + fileName + "." + Cache.FileExtension), Format.Binary, (uint) recordTime);
            
            // Create Schema
            Classes.CreateSchema();

            // Set Flag
            InPlayMode = true;
        }

        /// <summary>
        /// Called when exiting play mode
        /// </summary>
        public static void ExitPlayMode()
        {
            // Bail Out Check
            if (!InPlayMode || InstanceHandle == 0) return;

            // Clean up anything left in writing the files
            Recording.CloseRecording(InstanceHandle);

            InstanceHandle = 0;

            // Set Flag
            InPlayMode = false;
        }

        /// <summary>
        /// Submit recording to core
        /// </summary>
        /// <param name="frameCount">Frame which data is at</param>
        /// <param name="deltaTime">Delta time for data</param>
        public static void SubmitRecording(uint frameCount, float deltaTime)
        {
            if ( frameCount != FrameCount ) return;

            Recording.Submit(deltaTime);

            // Increment
            FrameCount++;
        }

        /// <summary>
        /// Cache known objects in the scene
        /// </summary>
        public static void CacheKnownObjects()
        {
            CachedKnownObjects.Clear();
            CachedKnownObjects.AddRange((SceneTrackObject[])Resources.FindObjectsOfTypeAll(typeof(SceneTrackObject)));
        }
    }
}