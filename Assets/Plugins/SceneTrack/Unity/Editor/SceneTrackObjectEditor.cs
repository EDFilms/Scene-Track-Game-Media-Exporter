using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

namespace SceneTrack.Unity.Editor
{


    [CustomEditor(typeof(SceneTrackObject))]
    public class SceneTrackObjectEditor : UnityEditor.Editor
    {
        static SceneTrackObject  lastObject;

        private SceneTrackObject _targetObject;
        
        enum Warnings
        {
          NoRootBone,
          DisabledMeshRecording,
          NoMesh,
          NoSkinnedMesh,
          NoBones,
          NotAllBonesHaveObjects,
          ScaledSkinnedMeshRenderer,
          NoMaterial,
          NoMaterialMainTexture,
          NoMeshRenderer,
          NoRendererButWantsRecording,
          DisabledButChildTracking,
          MissingExportPoseMesh
        }
        
        static List<Warnings>         warnings;
        static List<Transform>        missingObjectsOnBones; 
        static List<SceneTrackObject> enabledChildren; 
        
        public static uint DJB2Hash(string str)
        {
          uint hash = 5381;
          uint i = 0;

          for (i = 0; i < str.Length; i++)
          {
            hash = ((hash << 5) + hash) + ((byte)str[(int)i]);
          }

          return hash;
        }
        
        static void PushWarning(Warnings warning)
        {
          if (warnings == null)
            warnings = new List<Warnings>(4);

          if (warnings.Contains(warning) == false)
          {
            warnings.Add(warning);
          }
        }

        static void CheckWarnings(SceneTrackObject obj)
        {
          if (warnings == null)
            warnings = new List<Warnings>(4);

          if (missingObjectsOnBones == null)
            missingObjectsOnBones = new List<Transform>(1);

          if (enabledChildren == null)
            enabledChildren = new List<SceneTrackObject>(1);

          warnings.Clear();
          missingObjectsOnBones.Clear();
          enabledChildren.Clear();

          var tr = obj.transform;
          var mf = obj.GetComponent<MeshFilter>();
          var mr = obj.GetComponent<MeshRenderer>();
          var smr = obj.GetComponent<SkinnedMeshRenderer>();
          
          var localScale = tr.localScale;

          if (smr != null)
          {

            if (obj.TrackMeshRenderer == false)
            {
              PushWarning(Warnings.DisabledMeshRecording);
            }

            if (smr.rootBone == null)
            {
              PushWarning(Warnings.NoRootBone);
            }

            if (smr.sharedMesh == null)
            {
              PushWarning(Warnings.NoSkinnedMesh);
            }

            if (obj.overrideMesh == true && obj.overrideMeshMesh == null)
            {
              PushWarning(Warnings.MissingExportPoseMesh);
            }

            if (smr.sharedMaterials.Length == 0)
            {
              PushWarning(Warnings.NoMaterial);
            }
            else
            {
              if (smr.sharedMaterials[0].mainTexture == null)
              {
                PushWarning(Warnings.NoMaterialMainTexture);
              }
            }
            
            if (Mathf.Approximately(localScale.x, 1.0f) == false ||
                Mathf.Approximately(localScale.y, 1.0f) == false ||
                Mathf.Approximately(localScale.z, 1.0f) == false
            )
            {
              PushWarning(Warnings.ScaledSkinnedMeshRenderer);
            }

            if (smr.bones != null)
            {
              foreach(var boneTr in smr.bones)
              {
                SceneTrackObject boneObject = boneTr.GetComponent<SceneTrackObject>();

                if (boneObject == null)
                {
                  PushWarning(Warnings.NotAllBonesHaveObjects);
                  missingObjectsOnBones.Add(boneTr);
                }

              }

              missingObjectsOnBones.Sort((x, y) => x.name.CompareTo(y.name));
            }
            else
            {
              PushWarning(Warnings.NoBones);
            }
          }

          if (mr != null)
          { 

            Material mat = null;

            if (mr.sharedMaterial != null)
            {
              mat = mr.sharedMaterial;
            }
            else if (mr.sharedMaterials != null)
            {
              if (mr.sharedMaterials[0] != null)
              {
                mat = mr.sharedMaterials[0];
              }
            }

            if (mat == null)
            {
              PushWarning(Warnings.NoMaterial);
            }
            else
            {
              if (mat.mainTexture == null)
              {
                PushWarning(Warnings.NoMaterialMainTexture);
              }
            }
          }
          else
          { 
            if (mf != null)
            {
              PushWarning(Warnings.NoMeshRenderer);
            }
          }

          if (mf != null)
          {
            if (mr != null && obj.TrackMeshRenderer == false)
            {
             PushWarning(Warnings.DisabledMeshRecording);
            }
           

            if (mf.sharedMesh == null)
            {
              PushWarning(Warnings.NoMesh);
            }
          }

          if (obj.TrackObject == false)
          {
            foreach(var so in obj.GetComponentsInChildren<SceneTrackObject>())
            {
              if (so != obj && so.TrackObject == true)
              {
                PushWarning(Warnings.DisabledButChildTracking);
                enabledChildren.Add(so);
              }
            }
          }

          if (smr == null && mr == null && mf == null && obj.TrackMeshRenderer)
          {
            PushWarning(Warnings.NoRendererButWantsRecording);
          }

        }

        static void ListChildren<T>(List<T> m) where T : Component
        {
          if (m == null || m.Count == 0)
            return;
          
          EditorGUILayout.BeginHorizontal();
            GUILayout.Space(10);
            EditorGUILayout.BeginVertical();
                EditorGUILayout.BeginHorizontal();
                  int i = 0;
                  foreach(var t in m)
                  {
                    if (GUILayout.Button(t.name, EditorStyles.miniBoldLabel, GUILayout.Width(50), GUILayout.MaxWidth(50)))
                    {
                      Selection.activeGameObject = t.gameObject;
                    }
                    if (i++ > 4)
                    {
                      i = 0;
                      GUILayout.FlexibleSpace();
                      EditorGUILayout.EndHorizontal();
                      EditorGUILayout.BeginHorizontal();
                    }
                  }
                EditorGUILayout.EndHorizontal();
            EditorGUILayout.EndVertical();
          EditorGUILayout.EndHorizontal();
        } 

        static void InspectWarnings(SceneTrackObject obj, ref bool dirty)
        {
          if (warnings == null || warnings.Count == 0)
            return;
          
          if (warnings != null && warnings.Count > 0)
          {
            GUILayout.Label(String.Format("{0} Issues", warnings.Count), EditorStyles.boldLabel);
          }

          foreach(var warning in warnings)
          {
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);

            switch(warning)
            {
              case Warnings.NoRootBone:
              {
                GUILayout.Label("\u2022 No Root Bone is attached the Skinned Mesh Renderer");
              }
              break;
              case Warnings.DisabledMeshRecording:
              {
                GUILayout.Label("\u2022 Mesh recording is disabled");
                  
                EditorGUILayout.BeginHorizontal();

                GUILayout.Label("Suggested Fix:");

                if (GUILayout.Button("Enable Mesh Recording", EditorStyles.miniButton))
                {
                  obj.TrackMeshRenderer = true;
                  dirty = true;
                }
                GUILayout.FlexibleSpace();
                EditorGUILayout.EndHorizontal();
              }
              break;
              case Warnings.NoMesh:
              {
                GUILayout.Label("\u2022 No Mesh is attached to the MeshFilter");
              }
              break;
              case Warnings.NoSkinnedMesh:
              {
                GUILayout.Label("\u2022 No Mesh is attached to the Skinned Mesh Filter");
              }
              break;
              case Warnings.NoBones:
              {
                GUILayout.Label("\u2022 There are no Bones assigned to this Skinned Mesh Renderer");
              }
              break;
              case Warnings.ScaledSkinnedMeshRenderer:
              {
                GUILayout.Label("LocalScale is not [1, 1, 1]", EditorStyles.boldLabel);
                EditorGUILayout.BeginHorizontal();
                GUILayout.Label("Suggested Fix:");

                  if (GUILayout.Button("Reset Scale", EditorStyles.miniButton, GUILayout.MaxWidth(100)))
                  {
                    obj.transform.localScale = Vector3.one;
                    dirty = true;
                  }
                GUILayout.FlexibleSpace();
                EditorGUILayout.EndHorizontal();
              }
              break;
              case Warnings.NotAllBonesHaveObjects:
              {
                GUILayout.Label("\u2022 Some Bones do not have Trackers attached");
                
                if (missingObjectsOnBones != null && missingObjectsOnBones.Count > 0)
                {
                  
                  ListChildren(missingObjectsOnBones);
                  
                  EditorGUILayout.BeginHorizontal();

                  GUILayout.Label("Suggested Fix:");
                  if (GUILayout.Button("Add Trackers to Bones", EditorStyles.miniButton))
                  {
                    foreach(var bone in missingObjectsOnBones)
                    {
                      SceneTrackObject boneObject = bone.gameObject.AddComponent<SceneTrackObject>();
                      EditorUtility.SetDirty(boneObject);
                      EditorUtility.SetDirty(bone.gameObject);
                    }
                    dirty = true;
                  }
                  GUILayout.FlexibleSpace();
                  EditorGUILayout.EndHorizontal();
                }
              }
              break;
              case Warnings.NoMaterial:
              {
                GUILayout.Label("\u2022 No Material is assigned", EditorStyles.boldLabel);
              }
              break;
              case Warnings.NoMaterialMainTexture:
              {
                GUILayout.Label("\u2022 No Main Texture was assigned to the material");
              }
              break;
              case Warnings.NoMeshRenderer:
              {
                GUILayout.Label("\u2022 No Mesh Renderer attached", EditorStyles.boldLabel);
                EditorGUILayout.BeginHorizontal();
                GUILayout.Label("Suggested Fix:");
                if (GUILayout.Button("Add Mesh Renderer Component", EditorStyles.miniButton))
                {
                  var mr = obj.gameObject.AddComponent<MeshRenderer>();
                  EditorUtility.SetDirty(mr);
                  dirty = true;
                }
                GUILayout.FlexibleSpace();
                EditorGUILayout.EndHorizontal();
              }
              break;
              case Warnings.NoRendererButWantsRecording:
              {
                obj.TrackMeshRenderer = false;
                dirty = true;
              }
              break;
              case Warnings.DisabledButChildTracking:
              {
                GUILayout.Label("\u2022 Tracking is disabled, but child objects are tracking");
                
                ListChildren(enabledChildren);
                
                EditorGUILayout.BeginHorizontal();
                  GUILayout.Label("Suggested Fixes:");
                  if (GUILayout.Button("Turn off tracking for Children", EditorStyles.miniButton))
                  {
                    foreach(var childObj in enabledChildren)
                    {
                      childObj.TrackObject = false;
                      EditorUtility.SetDirty(childObj);
                    }
                    dirty = true;
                  }
                  GUILayout.Label(" or ");
                  if (GUILayout.Button("Enable Object Tracking", EditorStyles.miniButton))
                  {
                    obj.TrackObject = true;
                    dirty = true;
                  }
                  GUILayout.FlexibleSpace();
                EditorGUILayout.EndHorizontal();
              }
              break;
              case Warnings.MissingExportPoseMesh:
              {
                GUILayout.Label("\u2022 Export Pose Mesh Missing", EditorStyles.boldLabel);
                EditorGUILayout.BeginHorizontal();
                GUILayout.Label("Suggested Fix:");
                if (GUILayout.Button("Set As Export Pose", EditorStyles.miniButton))
                {
                  SetExportPose(obj, obj.GetComponent<SkinnedMeshRenderer>());
                  dirty = true;
                }
                GUILayout.FlexibleSpace();
                EditorGUILayout.EndHorizontal();
              }
              break;

            }
            EditorGUILayout.EndVertical();
            GUILayout.Space(5);
          }
          
         if (warnings != null && warnings.Count > 0)
         {
          EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
         }
          
        }

      static void SetExportPose(SceneTrackObject obj, SkinnedMeshRenderer smr)
      {
        Mesh baked = new Mesh();
        smr.BakeMesh(baked);
        Mesh overrideMesh = global::UnityEngine.Object.Instantiate(smr.sharedMesh);

        String meshName = smr.sharedMesh.name;
        String fileName = String.Format("SceneTrackPose_{0:X8}", DJB2Hash(String.Format("{0}_{1}", AssetDatabase.GetAssetPath(smr.sharedMesh), smr.sharedMesh.name)));

        overrideMesh.name = String.Format("SceneTrackMeshCache_{0}", meshName);
        overrideMesh.vertices = baked.vertices;
        overrideMesh.RecalculateBounds();
        overrideMesh.RecalculateNormals();

        if (AssetDatabase.IsValidFolder("Assets/SceneTrackCache") == false)
        {
          AssetDatabase.CreateFolder("Assets", "SceneTrackCache");
        }

        AssetDatabase.CreateAsset(overrideMesh, String.Format("Assets/SceneTrackCache/{0}.asset", fileName));

        obj.overrideMesh = true;
        obj.overrideMeshMesh = overrideMesh;

        foreach (Transform boneTransform in smr.bones)
        {
          SceneTrackObject boneObject = boneTransform.GetComponent<SceneTrackObject>();

          if (boneObject == null)
          {
            Debug.LogWarningFormat(
              "[SceneTrack] Bone '{0}' for Skinned Mesh Renderer '{1}' has no SceneTrack Object attached. This will not record correctly.", 
              boneTransform.name, 
              smr.gameObject.name);
            continue;
          }

          if (boneObject == obj)
            continue;

          boneObject.overridePose = true;
          boneObject.overridePosePosition = boneTransform.localPosition;
          boneObject.overridePoseRotation = boneTransform.localRotation;
          boneObject.overridePoseScale = boneTransform.localScale;

          EditorUtility.SetDirty(boneObject);
        }
                
        DestroyImmediate(baked);
      }

      static void RemoveExportPose(SceneTrackObject obj, SkinnedMeshRenderer smr)
      {
        String path = AssetDatabase.GetAssetPath(obj.overrideMeshMesh);
        AssetDatabase.DeleteAsset(path);
        obj.overrideMeshMesh = null;
        obj.overrideMesh = false;

        foreach (Transform transform in smr.bones)
        {
          SceneTrackObject child = transform.GetComponent<SceneTrackObject>();

          if (child == null)
          {
            Debug.LogWarningFormat(
              "[SceneTrack] Bone '{0}' for Skinned Mesh Renderer '{1}' has no SceneTrack Object attached. This will not record correctly.",
              transform.name, 
              smr.gameObject.name);
            continue;
          }

          if (child == obj)
            continue;

          child.overridePose = false;
          child.overridePosePosition = Vector3.zero;
          child.overridePoseRotation = Quaternion.identity;
          child.overridePoseScale = Vector3.one;

          EditorUtility.SetDirty(child);
        }
      }

      public override void OnInspectorGUI()
      {
        if (Application.isPlaying)
        {
          EditorGUILayout.HelpBox("Changes Are Not Permitted During PlayMode", MessageType.Info);
          return;
        }

        bool dirty = false;


        // Get Current Reference
        _targetObject = (SceneTrackObject) target;

        if (_targetObject != lastObject)
        {
          lastObject = _targetObject;
        }

        CheckWarnings(_targetObject);

        InspectWarnings(_targetObject, ref dirty);

        SkinnedMeshRenderer smr = _targetObject.GetComponent<SkinnedMeshRenderer>();
        bool hasMeshRenderer = _targetObject.GetComponent<MeshRenderer>() != null || smr != null;
        bool hasCollider = _targetObject.GetComponent<Collider>() != null;


        GUILayout.Space(5);
        
        EditorGUILayout.LabelField("Tracking", EditorStyles.boldLabel);

        _targetObject.TrackObject = EditorGUILayout.Toggle("Track Object", _targetObject.TrackObject);
        
        EditorGUI.BeginDisabledGroup(!_targetObject.TrackObject);

        EditorGUILayout.LabelField("Components", EditorStyles.miniBoldLabel);
        EditorGUI.indentLevel++;

        if (hasMeshRenderer)
        {
          EditorGUILayout.BeginHorizontal();

          _targetObject.TrackMeshRenderer = EditorGUILayout.Toggle("Mesh", _targetObject.TrackMeshRenderer);
        
          if (smr != null && smr.sharedMesh != null && _targetObject.TrackMeshRenderer)
          {
            if (_targetObject.overrideMesh == false && _targetObject.overrideMeshMesh == null)
            {
              if (GUILayout.Button("Set as Export Pose", EditorStyles.miniButton, GUILayout.Width(140)))
              {
                SetExportPose(_targetObject, smr);
                dirty = true;
              }
            }
            else
            {
              if (_targetObject.overrideMeshMesh == null)
              {
                GUILayout.Label("Missing Export Pose Mesh Asset!", EditorStyles.boldLabel);
              }
              else
              {
                if (GUILayout.Button("Remove Export Pose", EditorStyles.miniButton, GUILayout.Width(140)) && _targetObject.overrideMeshMesh != null)
                {
                  RemoveExportPose(_targetObject, smr);
                  dirty = true;
                }
              }
            }
          }
          GUILayout.FlexibleSpace();
          EditorGUILayout.EndHorizontal();
        }

        if (hasCollider)
        {
          _targetObject.TrackPhysics = EditorGUILayout.Toggle("Physics Events", _targetObject.TrackPhysics);
        }

        EditorGUI.indentLevel--;

        if (_targetObject.TrackPhysics)
        {
          GUILayout.Space(10);
          EditorGUILayout.LabelField("User Defined Data", EditorStyles.boldLabel);
          _targetObject.UserDefinedData = EditorGUILayout.TextField(_targetObject.UserDefinedData);
        }


        EditorGUI.EndDisabledGroup();

        GUILayout.Space(10);

        // Calculate Children

        EditorGUILayout.LabelField("Children Trackers", EditorStyles.boldLabel);
        EditorGUILayout.BeginHorizontal();


        if (GUILayout.Button("Add", EditorStyles.miniButtonLeft))
        {
          // Look through all children (even inactive ones)
          foreach (var t in _targetObject.GetComponentsInChildren<Transform>(true))
          {
            if (t.GetComponent<SceneTrackObject>() == null)
            {
              t.gameObject.AddComponent<SceneTrackObject>();
            }
          }

          // Update Cache List
          SceneTrack.Unity.System.CacheKnownObjects();
        }

        if (GUILayout.Button("Remove", EditorStyles.miniButtonMid))
        {
          // Look through all children (even inactive ones)
          foreach (var t in _targetObject.GetComponentsInChildren<Transform>(true))
          {
            var reference = t.GetComponent<SceneTrackObject>();
            if (reference != null && reference != _targetObject)
            {
              UnityEngine.Object.DestroyImmediate(reference);
            }
          }

          // Update Cache List
          SceneTrack.Unity.System.CacheKnownObjects();
        }

        if (GUILayout.Button("Enable", EditorStyles.miniButtonMid))
        {
          // Look through all children (even inactive ones)
          foreach (var t in _targetObject.GetComponentsInChildren<Transform>(true))
          {
            var reference = t.GetComponent<SceneTrackObject>();
            if (reference != null && reference != _targetObject)
            {
              reference.TrackObject = true;
            }
          }

          // Update Cache List
          SceneTrack.Unity.System.CacheKnownObjects();
        }

        if (GUILayout.Button("Disable", EditorStyles.miniButtonRight))
        {
          // Look through all children (even inactive ones)
          foreach (var t in _targetObject.GetComponentsInChildren<Transform>(true))
          {
            var reference = t.GetComponent<SceneTrackObject>();
            if (reference != null && reference != _targetObject)
            {
              reference.TrackObject = false;
            }
          }

          // Update Cache List
          SceneTrack.Unity.System.CacheKnownObjects();
        }

        EditorGUILayout.EndHorizontal();

        GUILayout.Space(10);

        if (dirty || GUI.changed)
        {
          EditorUtility.SetDirty(_targetObject);
          EditorSceneManager.MarkAllScenesDirty();
          CheckWarnings(_targetObject);
        }
      }
    }
}
