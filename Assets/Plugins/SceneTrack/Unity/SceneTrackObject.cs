// Copyright 2018 E*D Films. All Rights Reserved.

/**
 * SceneTrackObject.cs
 *
 * Object used to track a Unity GameObject
 * 
 * @author  dotBunny <hello@dotbunny.com>
 * @version 1
 * @since	1.0.0
 */
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using UnityEngine;

namespace SceneTrack.Unity
{
    public class SceneTrackObject : MonoBehaviour
    {

#if UNITY_EDITOR

        /// <summary>
        /// Should SceneTrack use the SkinnedMeshRenderer
        /// </summary>
        public bool IsSkinned { get; private set; }
    
        /// <summary>
        /// Is this object at the root of the heiarchy?
        /// </summary>
        public bool RootObject
        {
            get { return _transformParentHandle == 0 ; }
        }

        /// <summary>
        /// Reference to the parent SceneTrackObject if available
        /// </summary>
        public SceneTrackObject ParentSceneTrackObject { get; private set; }

        /// <summary>
        /// Handle of the Transform Component
        /// </summary>
        /// <remarks>All GameObjects in Unity have transforms, so we always will have one.</remarks>
        public uint TransformHandle { get; private set; }

        [SerializeField]
        public bool TrackObject = true;
        
        [SerializeField]
        public bool TrackMeshRenderer;

        [SerializeField]
        public bool TrackPhysics;

        public string UserDefinedData = string.Empty;

        private uint _frameCount;
        /// <summary>
        /// Game Object Handle
        /// </summary>
        private uint _handle;

        public uint GameObjectHandle {
            get { return _handle; }
        }

        private bool _initialized;
        private bool _initializing;

        public bool IsInitialized { get { return _initialized; } }
        public bool IsInitializedOrInitializing { get { return _initialized || _initializing; } }

        private MeshFilter _meshFilter;
        private MeshRenderer _meshRenderer;
        private SkinnedMeshRenderer _skinnedMeshRenderer;
        private uint _meshRendererHandle;
        private uint _skinnedMeshRendererHandle;
        private SceneTrackObject _parentSceneTrackObject;
        private SceneTrackObject _boneRootObject;
        private Transform _transform;
        private Transform _transformParent;
        private uint _transformParentHandle;
        private uint _physicsEventHandle;

        private uint[] _materialHandles;
        private uint _meshHandle;
        
        public bool       overrideMesh;
        public Mesh       overrideMeshMesh;
        public bool       overridePose;
        public Vector3    overridePosePosition,
                          overridePoseScale;
        public Quaternion overridePoseRotation;

        #region Unity Specific Events

        /// <summary>
        /// Unity's Awake Event
        /// </summary>
        /// <remarks>This starts up SceneTrack's initialization, only once, and then initializes the component.</remarks>
        private void Awake()
        {
            if (!TrackObject) return;

            // Initialize SceneTrack
            System.EnterPlayMode();


            // Initialize
            Init();
        }

        /// <summary>
        /// Unity's LateUpdate Event
        /// </summary>
        /// <remarks>Submits our data to SceneTrack to be saved, it only happens once across all components.</remarks>
        private void LateUpdate()
        {
            if (!TrackObject) return;
            System.SubmitRecording(_frameCount, Time.deltaTime);
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (!TrackPhysics) return;
            RecordPhysicsEvent(Classes.PhysicsEvent.EventType.Start, collision.contacts[0].point,
                collision.relativeVelocity.magnitude, collision.collider.gameObject.GetComponent<SceneTrackObject>());
        }

        private void OnCollisionExit(Collision collision)
        {
            if (!TrackPhysics) return;
            RecordPhysicsEvent(Classes.PhysicsEvent.EventType.Stop, _transform.position,
                collision.relativeVelocity.magnitude, collision.collider.gameObject.GetComponent<SceneTrackObject>());
        }

        private void OnCollisionStay(Collision collision)
        {
//            if (!TrackPhysics) return;
//            RecordPhysicsEvent(Classes.PhysicsEvent.EventType.Continue, collision.contacts[0].point,
//                collision.relativeVelocity.magnitude, collision.collider.gameObject.GetComponent<SceneTrackObject>());
        }

        private void OnTriggerEnter(Collider otherCollider)
        {
            if (!TrackPhysics) return;
            RecordPhysicsEvent(Classes.PhysicsEvent.EventType.Start, _transform.position, 1f, otherCollider.gameObject.GetComponent<SceneTrackObject>());
        }

        private void OnTriggerExit(Collider otherCollider)
        {
            if (!TrackPhysics) return;
            RecordPhysicsEvent(Classes.PhysicsEvent.EventType.Stop, _transform.position, 1f, otherCollider.gameObject.GetComponent<SceneTrackObject>());
        }

        private void OnTriggerStay(Collider otherCollider)
        {
//            if (!TrackPhysics) return;
//            RecordPhysicsEvent(Classes.PhysicsEvent.EventType.Continue, _transform.position, 1f, otherCollider.gameObject.GetComponent<SceneTrackObject>());
        }

        private void OnEnable()
        {
            if (!TrackObject) return;
            Object.SetValue_uint8(_handle, Classes.GameObject.Visibility, 1);
        }

        private void OnDisable()
        {
            if (!TrackObject) return;
            Object.SetValue_uint8(_handle, Classes.GameObject.Visibility, 0);
        }

        /// <summary>
        /// Unity's OnDestroy Event
        /// </summary>
        /// <remarks>Shutdown SceneTrack, cleaning out buffers, etc.</remarks>
        private void OnDestroy()
        {
            // Leave PlayMode
            System.ExitPlayMode();
        }

        /// <summary>
        /// Unity's Component Reset
        /// </summary>
        /// <remarks>Setup default tracking values based on what is on the Game Object</remarks>
        private void Reset()
        {
            // Check if there is a mesh render and enable
            if ( GetComponent<MeshRenderer>() != null ) {
                TrackMeshRenderer = true;
            }

            // Check if there is a skinned mesh render and enable
            if ( GetComponent<SkinnedMeshRenderer>() != null ) {
                TrackMeshRenderer = true;
            }

            if (GetComponent<Collider>() != null)
            {
                TrackPhysics = true;
            }
        }

        /// <summary>
        /// Unity's Update
        /// </summary>
        /// <remarks>Record any changes to the tracked objects</remarks>
        private void Update()
        {
            if (!TrackObject) return;

            // Get last used frame number, we use this to determine if this is the first late update being called!
            _frameCount = System.FrameCount;

            // Record whats being tracked
            RecordTransform();
            if (TrackMeshRenderer) { RecordMeshRenderer(); }
        }

        #endregion

        #region SceneTrack Logic

        /// <summary>
        /// One-Time Initialization Of Object Description
        /// </summary>
        /// <remarks>Defines the object, and its properties inside of SceneTrack for recording purposes.</remarks>
        public void Init()
        {
            // Return if we've already initialized
            if (_initialized || _initializing || !TrackObject) return;
            _initializing = true;

            // Cache transform reference
            _transform = transform;

            // Register GameObject
            _handle = Object.CreateObject(Classes.GameObject.Type);


            // Register name
            if (name.Length > 0)
            {
                Helper.SubmitString(_handle, Classes.GameObject.Name, name);
            }

            if (TrackPhysics)
            {
                InitPhysicsEvent();
            }

            // Register Components
            InitTransform();
            

            if (_transform.parent != null)
            { 
                  Helper.RecursiveBackwardsAddObjectAndInitialise(_transform.parent);
            }

            if ( TrackMeshRenderer )
            {
                InitMeshRenderer();
            }

            // Set flag as initialized
            _initialized = true;
            _initializing = false;
        }

        /// <summary>
        /// Initialize SceenTrack MeshFilter/MeshRenderer Component
        /// </summary>
        private void InitMeshRenderer()
        {
            // Cache references
            _meshFilter = GetComponent<MeshFilter>();
            _meshRenderer = GetComponent<MeshRenderer>();
            _skinnedMeshRenderer = GetComponent<SkinnedMeshRenderer>();

            // Determine if we need to use the SkinnedMeshRenderer Logic
            IsSkinned = _skinnedMeshRenderer != null;

            // Create Materials
            InitMaterials(IsSkinned ? _skinnedMeshRenderer.sharedMaterials : _meshRenderer.sharedMaterials);

            // Create Mesh
            Mesh mesh = null;

            if (overrideMeshMesh != null)
            {
              mesh = overrideMeshMesh;
            }
            else
            {
              if (_skinnedMeshRenderer != null)
              {
                mesh = _skinnedMeshRenderer.sharedMesh;
              }
              else if (_meshFilter != null)
              {
                mesh = _meshFilter.sharedMesh;
              }
            }

            var foundMesh = InitMesh(mesh);

            if (foundMesh == false)
            {
                TrackMeshRenderer = false;

                if (_skinnedMeshRenderer != null)
                {
                  global::UnityEngine.Debug.LogWarningFormat("[SceneTrack] No mesh attached to Skinned Mesh Renderer '{0}'. Recording will be affected.");
                }
                else if (_meshFilter != null)
                {
                  global::UnityEngine.Debug.LogWarningFormat("[SceneTrack] No mesh attached Mesh Filter '{0}'. Recording will be affected.");
                }
                
                return;
            }

            // Create Renderer
            if (IsSkinned && _skinnedMeshRenderer != null)
            {
                _skinnedMeshRendererHandle = SceneTrack.Object.CreateObject(Classes.SkinnedMeshRenderer.Type);

                // Assign Mesh (shared reference if found) to MeshRenderer
                Object.SetValue_uint32(_skinnedMeshRendererHandle, Classes.SkinnedMeshRenderer.Mesh, _meshHandle);
                
                // Assign Parent (transform) , Duplication Understood.
                Object.SetValue_uint32(_skinnedMeshRendererHandle, Classes.SkinnedMeshRenderer.Parent, TransformHandle);

                // Assign Materials (shared references as found)
                Helper.SubmitArray(_skinnedMeshRendererHandle, Classes.SkinnedMeshRenderer.Materials, _materialHandles, Helper.GetTypeMemorySize(typeof(uint), 1));

                // Assign Bone Transform (Root Object)
                // Check if we have a SceneTrackObject on the bone root, if we don't add one
                // TODO: Robin/Matthew
                //  Check to see if this is true. I've seen RootBones that aren't a direct child of the SkinnedMeshRenderer.

                if (_skinnedMeshRenderer.rootBone != null)
                {
                  _boneRootObject = _skinnedMeshRenderer.rootBone.GetComponent<SceneTrackObject>() ??
                                    _skinnedMeshRenderer.rootBone.gameObject.AddComponent<SceneTrackObject>();
                  Object.SetValue_uint32(_skinnedMeshRendererHandle, Classes.SkinnedMeshRenderer.BoneTransform, _boneRootObject.TransformHandle);
                }
                else
                {
                  Debug.LogErrorFormat("[SceneTrack] Root Bone Missing for {0}", name);
                }

                // Assign Bones
                Transform[] cachedBones = _skinnedMeshRenderer.bones;
                int cachedBonesCount = cachedBones.Length;
        
                uint[] boneHandles = new uint[cachedBones.Length];

                for (int i=0;i < cachedBonesCount;i++)
                {
                  Helper.RecursiveBackwardsAddObjectAndInitialise(cachedBones[i]);
                  SceneTrackObject boneObject = cachedBones[i].GetComponent<SceneTrackObject>();
                  boneHandles[i] = boneObject.TransformHandle;
                }
                
                Helper.SubmitArray(_skinnedMeshRendererHandle, Classes.SkinnedMeshRenderer.Bones, boneHandles, Helper.GetTypeMemorySize(typeof(uint), 1));

                // BoneTransform.  Assumed to be the parent of the Root Bone.
                Transform rootBone = _skinnedMeshRenderer.rootBone;

                if (rootBone != null)
                {
                  Transform rootBoneParent = rootBone.parent;
                  if (rootBoneParent != null)
                  {
                    Helper.RecursiveBackwardsAddObjectAndInitialise(rootBoneParent);
                    
                    SceneTrackObject rootBoneParentObject = rootBoneParent.GetComponent<SceneTrackObject>();
                    
                    SceneTrack.Object.SetValue_uint32(_skinnedMeshRendererHandle, Classes.SkinnedMeshRenderer.BoneTransform, rootBoneParentObject.TransformHandle);
                  }
                }
            }
            else if (_meshFilter != null)
            {
                _meshRendererHandle = SceneTrack.Object.CreateObject(Classes.StandardMeshRenderer.Type);

                // Assign Mesh (shared reference if found) to MeshRenderer
                Object.SetValue_uint32(_meshRendererHandle, Classes.StandardMeshRenderer.Mesh, _meshHandle);
        
                // Assign Parent (transform) , Duplication Understood.
                Object.SetValue_uint32(_meshRendererHandle, Classes.StandardMeshRenderer.Parent, TransformHandle);

                // Assign Materials (shared references as found)
                if (_materialHandles.Length > 0)
                {
                  Helper.SubmitArray(_meshRendererHandle, Classes.StandardMeshRenderer.Materials, _materialHandles, Helper.GetTypeMemorySize(typeof(uint), 1));
                }
            }
            
        }

        /// <summary>
        /// Initialize references to materials used on the renderers.
        /// </summary>
        /// <param name="materials">Array of materials used in the mesh.</param>
        private void InitMaterials(Material[] materials)
        {
            var materialHandles = new List<uint>(materials.Length);
            foreach (var m in materials)
            {
             uint materialHandle = 0;


             if (!Unity.System.SharedMaterials.TryGetValue(m, out materialHandle))
             {
                 materialHandle = Object.CreateObject(Classes.Material.Type);

                 if (m.mainTexture != null)
                 {
                     Helper.SubmitString(materialHandle, Classes.Material.MainTexture, m.mainTexture.name);
                     Helper.SubmitString(materialHandle, Classes.Material.Name, m.name);
                     Helper.SubmitString(materialHandle, Classes.Material.Shader, m.shader.name);
                 }

                 System.SharedMaterials.Add(m, materialHandle);

                 Color mainColor = m.color;
                 SceneTrack.Object.SetValue_3_float32(materialHandle, Classes.Material.Color, mainColor.r, mainColor.g, mainColor.b);
             }

             // Store material in temp array
             materialHandles.Add(materialHandle);
            }
            _materialHandles = materialHandles.ToArray();
        }

        /// <summary>
        /// Initialize reference to the mesh used on the renderer
        /// </summary>
        /// <param name="mesh">The mesh</param>
        /// <returns>Was a mesh found?</returns>
        private bool InitMesh(Mesh cachedMesh)
        {
            if (cachedMesh == null) 
            {
              return false;
            }

            // Create Mesh
            _meshHandle = 0;

            if (!System.SharedMeshes.TryGetValue(cachedMesh, out _meshHandle))
            {
                _meshHandle = Object.CreateObject(Classes.Mesh.Type);

                // Handle Name (C# String)
                Helper.SubmitString(_meshHandle, Classes.Mesh.Name, cachedMesh.name);

                // Handle Vertices (Vector3)
                Helper.SubmitArray(_meshHandle, Classes.Mesh.Vertices, cachedMesh.vertices);

                // Handle Normals (Vector3)
                Vector3[] meshNormals = cachedMesh.normals;

                if (meshNormals != null && meshNormals.Length != 0)
                { 
                  Helper.SubmitArray(_meshHandle, Classes.Mesh.Normals, meshNormals);
                }

                // Handle Tangents (Vector4)
                Vector4[] meshTangents = cachedMesh.tangents;

                if (meshTangents != null && meshTangents.Length != 0)
                {
                  Helper.SubmitArray(_meshHandle, Classes.Mesh.Tangents, meshTangents);
                }

                // Handle Colors (Color)
                // We have to make an array of values as colors are stored differently
                Color[] meshColors = cachedMesh.colors;

                if (meshColors != null && meshColors.Length != 0)
                {
                    Helper.SubmitArray(_meshHandle, Classes.Mesh.Colors, meshColors);
                }

                // Handle UV (Vector2)
                Vector2[] meshUv = cachedMesh.uv;
                if (meshUv != null && meshUv.Length != 0)
                {
                    Helper.SubmitArray(_meshHandle, Classes.Mesh.UV, meshUv);
                }

                // Handle UV2 (Vector2)
                Vector2[] meshUv2 = cachedMesh.uv2;
                if (meshUv2 != null && meshUv2.Length != 0)
                {
                    Helper.SubmitArray(_meshHandle, Classes.Mesh.UV2, meshUv2);
                }

                // Handle UV3 (Vector2)
                Vector2[] meshUv3 = cachedMesh.uv3;
                if (meshUv3 != null && meshUv3.Length != 0)
                {
                    Helper.SubmitArray(_meshHandle, Classes.Mesh.UV3, meshUv3);
                }

                // Handle UV4 (Vector2)
                Vector2[] meshUv4 = cachedMesh.uv4;
                if (meshUv4 != null && meshUv4.Length != 0)
                {
                    Helper.SubmitArray(_meshHandle, Classes.Mesh.UV4, meshUv4);
                }

                // Assign Bones
                BoneWeight[] meshBoneWeights = cachedMesh.boneWeights;
                Matrix4x4[] meshBindPoses = cachedMesh.bindposes;

                if (IsSkinned && meshBoneWeights != null && meshBoneWeights.Length != 0 && meshBindPoses != null && meshBindPoses.Length != 0)
                {

                    var cachedBoneWeightLength = meshBoneWeights.Length;
                    var boneIndexes = new byte[cachedBoneWeightLength * 4];
                    var boneWeights = new Vector4[cachedBoneWeightLength];

                    for (var i = 0; i < cachedBoneWeightLength; i++)
                    {
                        var indexLocation = i * 4;
                        boneIndexes[indexLocation + 0] = (byte) meshBoneWeights[i].boneIndex0;
                        boneIndexes[indexLocation + 1] = (byte) meshBoneWeights[i].boneIndex1;
                        boneIndexes[indexLocation + 2] = (byte) meshBoneWeights[i].boneIndex2;
                        boneIndexes[indexLocation + 3] = (byte) meshBoneWeights[i].boneIndex3;

                        boneWeights[i] = new Vector4(
                            meshBoneWeights[i].weight0,
                            meshBoneWeights[i].weight1,
                            meshBoneWeights[i].weight2,
                            meshBoneWeights[i].weight3);
                    }
                    
                    // The indices are stored as a ByteVector4 array inside of ST, we have to alter the data description here accordingly.
                    // Handle BoneWeight Indexes (ByteVector4 like)
                    Helper.SubmitArray(_meshHandle, Classes.Mesh.BoneWeightIndex, boneIndexes, 4, (uint) (boneIndexes.Length / 4));
                    // Handle BoneWeight Vector  (Vector4 like)
                    Helper.SubmitArray(_meshHandle, Classes.Mesh.BoneWeightWeight, boneWeights);

                    boneIndexes = null;
                    boneWeights = null;

                    // Handle Bind Pose (Matrix44)
                    Helper.SubmitArray(_meshHandle, Classes.Mesh.BindPoses, meshBindPoses);
                }

                // Handle Bounds (Vector3[2])

                /*
                var bounds = new Vector3[2];
                bounds[0] = _meshRenderer.bounds.min;
                bounds[1] = _meshRenderer.bounds.max;
                Helper.SubmitArray(_meshHandle, Classes.Mesh.Bounds, bounds);
                bounds = null;
                */

                // Create Sub Meshes (If we have any!)
                if (cachedMesh.subMeshCount > 0)
                {
                    var subMeshHandles = new uint[cachedMesh.subMeshCount];


                    for (var i = 0; i < cachedMesh.subMeshCount; i++)
                    {
                        var subMeshIndices = cachedMesh.GetIndices(i);

                        // Create component
                        var subMeshHandle = SceneTrack.Object.CreateObject(Classes.SubMesh.Type);
            
                        // Handle Indexes (int (written as UInt))
                        Helper.SubmitArrayForceUInt32(subMeshHandle, Classes.SubMesh.Indexes, subMeshIndices, Helper.GetTypeMemorySize(typeof(uint), 1));
            
                        // Assign Submesh Index
                        subMeshHandles[i] = subMeshHandle;
                    }

                    // Assign index to submesh on mesh
                    Helper.SubmitArray(_meshHandle, Classes.Mesh.SubMesh, subMeshHandles, Helper.GetTypeMemorySize(typeof(uint), 1));
                }

                System.SharedMeshes.Add(cachedMesh, _meshHandle);
            }
            return true;
        }


        private void InitPhysicsEvent()
        {

        }

        /// <summary>
        /// Initialize SceneTrack Transform Component
        /// </summary>
        private void InitTransform()
        {
            // Create transform object in SceneTrack
            TransformHandle = Object.CreateObject(Classes.Transform.Type);

            // Assign Transform to the GameObject in SceneTrack
            Object.SetValue_uint32(_handle, Classes.GameObject.Transform, TransformHandle);

            // Do a force record of the transform, starting positions, etc.
            RecordTransform(true);
        }

        private void RecordMeshRenderer()
        {

        }

        private void RecordPhysicsEvent(Classes.PhysicsEvent.EventType eventType, Vector3 worldLocation, float intensity = 0, SceneTrackObject other = null)
        {
            _physicsEventHandle = Object.CreateObject(Classes.PhysicsEvent.Type);

            Object.SetValue_uint8(_physicsEventHandle, Classes.PhysicsEvent.Event, (byte)eventType);
            Object.SetValue_3_float32(_physicsEventHandle, Classes.PhysicsEvent.ContactPoint, worldLocation.x, worldLocation.y, worldLocation.z);
            Object.SetValue_float32(_physicsEventHandle, Classes.PhysicsEvent.Strength, intensity);

            Object.SetValue_2_uint32(_physicsEventHandle, Classes.PhysicsEvent.RelationReference, _handle,
                other != null ? other.GameObjectHandle : 0);

            // Helper.SubmitString(_physicsEventHandle, Classes.PhysicsEvent.UserData, UserDefinedData);
        }

        /// <summary>
        /// Record Transform Changes
        /// </summary>
        /// <param name="forceUpdate">Force the data to be sent to SceneTrack</param>
        private void RecordTransform(bool forceUpdate = false)
        {
            if (overridePose)
              forceUpdate = true;

            // No need to update
            if (!forceUpdate && !_transform.hasChanged && !(_transform.parent != _transformParent)) return;

            // Check our parent, and reassign and setup it if necessary in SceneTrack
            if (_transform.parent != _transformParent)
            {
                // If we have no parent, make sure to set the handle to 0
                if (_transform.parent == null)
                {
                    _transformParentHandle = 0;
                }
                else
                {
                    // Check if we have a SceneTrackObject on the new parent, if we don't add one
                    _parentSceneTrackObject = _transform.parent.GetComponent<SceneTrackObject>() ??
                                              _transform.parent.gameObject.AddComponent<SceneTrackObject>();

                    // TODO: Possibly add forced init of parent if the order is wrong
                    // ADDED BY ROBIN: Looks like this does happen. Children get initialised before parents.
                    if (_parentSceneTrackObject._initialized == false)
                    {
                      _parentSceneTrackObject.Init();
                    }

                    // Setup some cached references
                    _transformParentHandle = _parentSceneTrackObject.TransformHandle;
                    _transformParent = _transform.parent;
                }

                // Assign our transform handle inside of SceneTrack as it will have changed
                Object.SetValue_uint32(TransformHandle, Classes.Transform.Parent, _transformParentHandle);
            }

            Vector3 localP, localS;
            Quaternion localR;

            if (overridePose)
            {
              overridePose = false;
              localP = overridePosePosition;
              localR = overridePoseRotation;
              localS = overridePoseScale;
            }
            else
            {
              localP = _transform.localPosition;
              localR = _transform.localRotation;
              localS = _transform.localScale;
            }
              
            // Update Transform Position, Rotation and Scale.
            Object.SetValue_3_float32(TransformHandle, Classes.Transform.LocalPosition, localP.x, localP.y, localP.z);
            Object.SetValue_4_float32(TransformHandle, Classes.Transform.LocalRotation, localR.x, localR.y, localR.z, localR.w);
            Object.SetValue_3_float32(TransformHandle, Classes.Transform.LocalScale, localS.x, localS.y, localS.z);
        }
        #endregion
    
#endif
  }
}
