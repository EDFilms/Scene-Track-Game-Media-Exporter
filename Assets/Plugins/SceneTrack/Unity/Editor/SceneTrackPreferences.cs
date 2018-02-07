using System;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEditor;

namespace SceneTrack.Unity.Editor
{


    public static class SceneTrackPreferences
    {
        private static int _currentTab = 0;
        
        public static int AppendRecordFrames
        {
          get { return EditorPrefs.GetInt("SceneTrack_AppendRecordFrames", 2); }
          set { EditorPrefs.SetInt("SceneTrack_AppendRecordFrames", value); }
        }
        
        public static int MemoryReserveFramePool
        {
          get { return EditorPrefs.GetInt("SceneTrack_MemoryReserveFramePool", 0); }
          set { EditorPrefs.SetInt("SceneTrack_MemoryReserveFramePool", value); }
        }
        
        public static int MemoryReserveFrameDataPool
        {
          get { return EditorPrefs.GetInt("SceneTrack_MemoryReserveFrameDataPool", 0); }
          set { EditorPrefs.SetInt("SceneTrack_MemoryReserveFrameDataPool", value); }
        }

        public static bool OpenAfterExporting
        {
          get { return EditorPrefs.GetBool("SceneTrack_OpenAfterExporting", false); }
          set { EditorPrefs.SetBool("SceneTrack_OpenAfterExporting", value); }
        }

        public static int OutputAxisSettings
        {
            get
            {
                return EditorPrefs.GetInt("SceneTrack_OutputAxisSettings", 1);
            }
            set
            {
                EditorPrefs.SetInt("SceneTrack_OutputAxisSettings", value);
            }
        }
        
        static int[] AppendRecordFramesInt = new int[]
        {
          1, 2, 4, 8, 10, 30, 60
        };
        
        static string[] AppendRecordFramesStr = new string[]
        {
          "Continous", "Flip-Flop (Default)", "4 Frames", "8 Frames", "10 Frames", "30 Frames", "60 Frames"
        };
        

        [PreferenceItem("SceneTrack")]
        private static void SceneTrackPreferencesItem()
        {
            EditorGUILayout.BeginHorizontal();
      
            if (GUILayout.Toggle(_currentTab == 0, "Recording", EditorStyles.miniButtonLeft))
            {
              _currentTab = 0;
            }
            
            if (GUILayout.Toggle(_currentTab == 1, "Animation", EditorStyles.miniButtonMid))
            {
              _currentTab = 1;
            }
            
            if (GUILayout.Toggle(_currentTab == 2, "Events", EditorStyles.miniButtonMid))
            {
              _currentTab = 2;
            }
            
            if (GUILayout.Toggle(_currentTab == 3, "Video", EditorStyles.miniButtonMid))
            {
              _currentTab = 3;
            }
            
            if (GUILayout.Toggle(_currentTab == 4, "About", EditorStyles.miniButtonRight))
            {
              _currentTab = 4;
            }
            EditorGUILayout.EndHorizontal();

            GUILayout.Space(10);
            EditorGUILayout.BeginVertical();

            switch (_currentTab)
            {
                case 1:
                    FBXOutput.EditorPreferences();
                    break;
                case 2:
                    MidiOutput.EditorPreferences();
                    break;
                case 3:
                    VideoOutput.EditorPreferences();
                    break;
                case 4:
                    About();
                break;
                default:
                {
                    EditorGUILayout.BeginVertical();
                      EditorGUILayout.BeginVertical();
                        EditorGUILayout.BeginHorizontal();
                          GUILayout.FlexibleSpace();
                          if (GUILayout.Button("Default", EditorStyles.miniButton))
                          {
                              AppendRecordFrames = 2;
                              MemoryReserveFramePool = 0;
                              MemoryReserveFrameDataPool = 0;
                          }
                          EditorGUILayout.EndHorizontal();

                          OpenAfterExporting = EditorGUILayout.Toggle("Open Output after Exporting", OpenAfterExporting);
                    
                        EditorGUILayout.EndVertical();

                        GUILayout.FlexibleSpace();

                        EditorGUILayout.BeginVertical();
                          GUILayout.Label("Advanced Recording Settings", EditorStyles.boldLabel);

                          AppendRecordFrames = Mathf.Clamp(EditorGUILayout.IntPopup("Save Frame Time", AppendRecordFrames, AppendRecordFramesStr, AppendRecordFramesInt), 1, 60);
                    
                          if (AppendRecordFrames > 2)
                          {
                            GUILayout.BeginHorizontal();
                            GUILayout.Label("Warning:\nLonger save times will result in increased memory usage.\nThis can cause reduced recording performance or errors.", EditorStyles.miniLabel);
                            GUILayout.EndHorizontal();
                          }
                    
                          MemoryReserveFramePool = Mathf.Clamp(EditorGUILayout.IntField("Extra Frame Pools", MemoryReserveFramePool), 0, 16);
                          MemoryReserveFrameDataPool = Mathf.Clamp(EditorGUILayout.IntField("Extra Frame Data Pools", MemoryReserveFrameDataPool), 0, 16);

                          GUILayout.Space(60);

                        EditorGUILayout.EndVertical();
                    EditorGUILayout.EndVertical();

                    break;

                }
            }
      
            EditorGUILayout.EndVertical();
        }

        const String ReleaseVersion = "SceneTrack (Preview) 0.3";

        static void About()
        {
          StringBuilder stVersion, fbxVersion, midiVersion, videoVersion, versions;

          GUILayout.Label(ReleaseVersion, EditorStyles.boldLabel);

          versions = new StringBuilder(200);

          try
          {
            stVersion = new StringBuilder(SceneTrack.Library.GetVersionStrCapacity());
            int r = SceneTrack.Library.GetVersion(stVersion, (int) stVersion.Capacity);
            if (r == 0)
            {
              if (Application.platform == RuntimePlatform.WindowsEditor)
              {
                versions.Append("SceneTrack.dll: ");
              }
              else if (Application.platform == RuntimePlatform.OSXEditor)
              {
                versions.Append("SceneTrack.bundle: ");
              }
              versions.Append(stVersion);
              versions.AppendLine();
            }
          }
          catch
          {
            // ignored
          }
          
          try
          {
            fbxVersion = new StringBuilder(SceneTrackFbx.Library.GetVersionStrCapacity());
            int r = SceneTrackFbx.Library.GetVersion(fbxVersion, (int) fbxVersion.Capacity);
            if (r == 0)
            {
              if (Application.platform == RuntimePlatform.WindowsEditor)
              {
                versions.Append("SceneTrackFbx.dll: ");
              }
              else if (Application.platform == RuntimePlatform.OSXEditor)
              {
                versions.Append("SceneTrackFbx.bundle: ");
              }
              versions.Append(fbxVersion);
              versions.AppendLine();
            }
          }
          catch
          {
            // ignored
          }
          
          try
          {
            midiVersion = new StringBuilder(SceneTrackMidi.Library.GetVersionStrCapacity());
            int r = SceneTrackMidi.Library.GetVersion(midiVersion, (int) midiVersion.Capacity);
            if (r == 0)
            {
              if (Application.platform == RuntimePlatform.WindowsEditor)
              {
                versions.Append("SceneTrackMidi.dll: ");
              }
              else if (Application.platform == RuntimePlatform.OSXEditor)
              {
                versions.Append("SceneTrackMidi.bundle: ");
              }
              versions.Append(midiVersion);
              versions.AppendLine();
            }
          }
          catch
          {
            // ignored
          }
          
          try
          {
            videoVersion = new StringBuilder(SceneTrackVideo.Library.GetVersionStrCapacity());
            int r = SceneTrackVideo.Library.GetVersion(videoVersion, (int) videoVersion.Capacity);
            if (r == 0)
            {
              if (Application.platform == RuntimePlatform.WindowsEditor)
              {
                versions.Append("SceneTrackVideo.dll: ");
              }
              else if (Application.platform == RuntimePlatform.OSXEditor)
              {
                versions.Append("SceneTrackVideo.bundle: ");
              }
              versions.Append(videoVersion);
              versions.AppendLine();
            }
          }
          catch
          {
            // ignored
          }

          if (versions.Length > 0)
          {
            GUILayout.Label("Library and Exporters versions:", EditorStyles.miniBoldLabel);
            GUILayout.TextArea(versions.ToString(), EditorStyles.miniLabel);
          }
          
        }

        public static void SetupUnity()
        {





        }
    }
}