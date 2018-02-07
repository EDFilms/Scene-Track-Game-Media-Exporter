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

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Networking;

namespace SceneTrack.Unity
{

    public class SceneTrackCamera : MonoBehaviour
    {
    
        public enum ResolutionType
        {
          _320x240,
          _480x320,
          _640x480,
          _1024x768,
          _1280x720
        }

        public bool TrackCamera { get; set; }
        public bool DisableCamera = true;
        public ResolutionType Resolution = ResolutionType._640x480;

        private Camera _cameraReference;
        private RenderTexture _renderTexture;
        private Texture2D _proxyTexture;
        private uint _frameID;
        private int Width, Height;
        
        public void Awake()
        {
            // Get local reference to camera
            _cameraReference = GetComponent<Camera>();

            if (_cameraReference != Camera.main)
            {
                if (DisableCamera)
                {
                    _cameraReference.enabled = false;
                }
            }

            switch(Resolution)
            {
              default:
              case ResolutionType._320x240: Width = 320; Height = 240;
                break;
              case ResolutionType._480x320: Width = 480; Height = 320;
                break;
              case ResolutionType._640x480: Width = 640; Height = 480;
                break;
              case ResolutionType._1024x768: Width = 1024; Height = 768;
                break;
              case ResolutionType._1280x720: Width = 1280; Height = 720;
                break;
            }

          // Initialize SceneTrack
          System.EnterPlayMode();

          // Create Camera
          SceneTrack.Unity.Classes.CreateSchema();

          // Create new proxy texture
          _proxyTexture = new Texture2D(Width, Height);

          // Create Camera Frame ID
          _frameID = SceneTrack.Object.CreateObject(SceneTrack.Unity.Classes.VideoFrame.Type);
        }

      public void LateUpdate()
      {
        // Render the camera (we really could add framerate delays here)
        RenderCamera();

        // Save Frame
        SceneTrack.Object.SetValue_2_uint32(_frameID, SceneTrack.Unity.Classes.VideoFrame.Size, (uint) Width, (uint) Height);
        SceneTrack.Unity.Helper.SubmitArray(_frameID, SceneTrack.Unity.Classes.VideoFrame.Image, _proxyTexture.GetPixels32(), SceneTrack.Object.CalculateStride1(SceneTrack.Type.Uint8, 4));
      }


      private void RenderCamera()
      {
        // Create render texture
        _renderTexture = RenderTexture.GetTemporary(Width, Height, 32, RenderTextureFormat.ARGB32);

        // Assign camera output
        _cameraReference.targetTexture = _renderTexture;

        // Render camera
        _cameraReference.Render();

        // Make render texture the current active in engine (so the ReadPixels gets it)
        RenderTexture.active = _renderTexture;

        // Read RenderTexture data
        _proxyTexture.ReadPixels(new Rect(0, 0, Width, Height), 0, 0, false);

        // Reset things
        _cameraReference.targetTexture = null;
        RenderTexture.active = null;
        RenderTexture.ReleaseTemporary(_renderTexture);
      }
    }
}