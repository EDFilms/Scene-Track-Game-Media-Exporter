// Copyright 2018 E*D Films. All Rights Reserved.

/**
 * SceneTrackCamera.cs
 *
 * Render camera viewport to SceneTrack at current time
 * 
 * @author  dotBunny <hello@dotbunny.com>
 * @version 1
 * @since	  1.0.0
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
  /// <summary>
  /// A drop-in camera recorder component
  /// </summary>
  public class SceneTrackCamera : MonoBehaviour
  {
    /// <summary>
    /// The resolution which the camera should record.
    /// </summary>
    /// <remarks>
    /// This should never be full resolution, and is meant purely for reference.
    /// </remarks>
    public enum ResolutionType
    {
      _320x240,
      _480x320,
      _640x480,
      _1024x768,
      _1280x720
    }

    /// <summary>
    /// Disable camera at runtime
    /// </summary>
    public bool DisableCamera = true;
  
    public ResolutionType Resolution = ResolutionType._640x480;

    private Camera _cameraReference;
    private RenderTexture _renderTexture;
    private Texture2D _proxyTexture;
    private uint _frameID;
    private int _width, _height;
    
    /// <summary>
    /// Initialize SceneTrack Component
    /// </summary>
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
          case ResolutionType._320x240: _width = 320; _height = 240;
            break;
          case ResolutionType._480x320: _width = 480; _height = 320;
            break;
          case ResolutionType._640x480: _width = 640; _height = 480;
            break;
          case ResolutionType._1024x768: _width = 1024; _height = 768;
            break;
          case ResolutionType._1280x720: _width = 1280; _height = 720;
            break;
        }

      // Initialize SceneTrack
      System.EnterPlayMode();

      // Create Camera
      SceneTrack.Unity.Classes.CreateSchema();

      // Create new proxy texture
      _proxyTexture = new Texture2D(_width, _height);

      // Create Camera Frame ID
      _frameID = SceneTrack.Object.CreateObject(SceneTrack.Unity.Classes.VideoFrame.Type);
    }

    /// <summary>
    /// Render and save the frame data for this camera
    /// </summary>
    public void LateUpdate()
    {
      // Render the camera (we really could add framerate delays here)
      if ( _cameraReference != null && _cameraReference.enabled)
      {
        RenderCamera();

         // Save Frame
        SceneTrack.Object.SetValue_2_uint32(_frameID, SceneTrack.Unity.Classes.VideoFrame.Size, (uint) _width, (uint) _height);
        SceneTrack.Unity.Helper.SubmitArray(_frameID, SceneTrack.Unity.Classes.VideoFrame.Image, _proxyTexture.GetPixels32(), SceneTrack.Object.CalculateStride1(SceneTrack.Type.Uint8, 4));
      }
    }

    /// <summary>
    /// Instruct the target camera to render to a render texture, save the data to a local texture, and release the render texture.
    /// </summary>
    private void RenderCamera()
    {
      // Create render texture
      _renderTexture = RenderTexture.GetTemporary(_width, _height, 32, RenderTextureFormat.ARGB32);

      // Assign camera output
      _cameraReference.targetTexture = _renderTexture;

      // Render camera
      _cameraReference.Render();

      // Make render texture the current active in engine (so the ReadPixels gets it)
      RenderTexture.active = _renderTexture;

      // Read RenderTexture data
      _proxyTexture.ReadPixels(new Rect(0, 0, _width, _height), 0, 0, false);

      // Reset things
      _cameraReference.targetTexture = null;
      RenderTexture.active = null;
      RenderTexture.ReleaseTemporary(_renderTexture);
    }
  }
}