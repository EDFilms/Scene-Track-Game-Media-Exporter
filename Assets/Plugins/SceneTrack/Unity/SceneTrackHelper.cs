// Copyright 2018 E*D Films. All Rights Reserved.

/**
 * SceneTrackHelper.cs
 *
 * Runtime support functions to assist SceneTrack
 * 
 * @author  dotBunny <hello@dotbunny.com>
 * @version 1
 * @since	1.0.0
 */

/// <summary>
/// Tell SceneTrack to make fast copies of Matrix4x4
/// </summary>
#define ST_FAST_COPY_MATRIX44

using System;
using System.CodeDom;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using UnityEngine;

namespace SceneTrack.Unity
{
    public static class Helper
    {
        /// <summary>
        /// Get the memory size of a type.
        /// </summary>
        /// <param name="objectType">Type</param>
        /// <param name="elements">Number of elements</param>
        /// <returns></returns>
        public static uint GetTypeMemorySize(global::System.Type objectType, uint elements)
        {
            if (objectType == typeof(Single))
            {
                return (uint) sizeof(Single) * elements;
            }
            else if (objectType == typeof(Double))
            {
                return (uint) sizeof(Double) * elements;
            }
            else if (objectType == typeof(Single))
            {
                return (uint) sizeof(Single) * elements;
            }
            else if (objectType == typeof(Int64))
            {
                return (uint) sizeof(Int64) * elements;
            }
            else if (objectType == typeof(UInt64))
            {
                return (uint) sizeof(UInt64) * elements;
            }
            else if (objectType == typeof(Int32))
            {
                return (uint) sizeof(Int32) * elements;
            }
            else if (objectType == typeof(UInt32))
            {
                return (uint) sizeof(UInt32) * elements;
            }
            else if (objectType == typeof(Int16))
            {
                return (uint) sizeof(Int16) * elements;
            }
            else if (objectType == typeof(UInt16))
            {
                return (uint) sizeof(UInt16) * elements;
            }
            else if (objectType == typeof(SByte))
            {
                return sizeof(SByte) * elements;
            }
            else if (objectType == typeof(Byte))
            {
                return sizeof(Byte) * elements;
            }
            return 0;
        }

        public static Vector4 ToVector4(this Color color)
        {
            return new Vector4(color.r,color.g, color.b, color.a);
        }

        public static void SubmitArray(uint objectHandle, uint componentHandle, byte[] array, uint stride)
        {
            var handle = GCHandle.Alloc(array, GCHandleType.Pinned);
            var arrayPointer = handle.AddrOfPinnedObject();
            Object.SetValue_p_uint8(objectHandle, componentHandle, arrayPointer, (uint) array.Length, stride);
            handle.Free();
        }

        public static void RecursiveBackwardsAddObjectAndInitialise(Transform transform)
        {
          SceneTrackObject obj = transform.GetComponent<SceneTrackObject>();
          
          if (obj == null)
          {
            obj = transform.gameObject.AddComponent<SceneTrackObject>();
          }

          if (obj.IsInitializedOrInitializing == false)
          {
            obj.Init();
          }

          Transform parent = transform.parent;

          if (parent != null)
          {
            RecursiveBackwardsAddObjectAndInitialise(parent);
          }

        }
    
        public static void RecursiveBackwardsAddObject(Transform transform)
        {
          SceneTrackObject obj = transform.GetComponent<SceneTrackObject>();
          
          if (obj == null)
          {
            obj = transform.gameObject.AddComponent<SceneTrackObject>();
          }

          Transform parent = transform.parent;

          if (parent != null)
          {
            RecursiveBackwardsAddObject(parent);
          }

        }

        public static void SubmitArray(uint objectHandle, uint componentHandle, byte[] array, uint stride, uint arrayLength)
        {
            var handle = GCHandle.Alloc(array, GCHandleType.Pinned);
            var arrayPointer = handle.AddrOfPinnedObject();
            Object.SetValue_p_uint8(objectHandle, componentHandle, arrayPointer, arrayLength, stride);
            handle.Free();
        }

        public static void SubmitArray(uint objectHandle, uint componentHandle, int[] array, uint stride)
        {
            var handle = GCHandle.Alloc(array, GCHandleType.Pinned);
            var arrayPointer = handle.AddrOfPinnedObject();
            Object.SetValue_p_int32(objectHandle, componentHandle, arrayPointer, (uint) array.Length, stride);
            handle.Free();
        }
    
        public static void SubmitArray(uint objectHandle, uint componentHandle, uint[] array, uint stride)
        {
            var handle = GCHandle.Alloc(array, GCHandleType.Pinned);
            var arrayPointer = handle.AddrOfPinnedObject();
            Object.SetValue_p_uint32(objectHandle, componentHandle, arrayPointer, (uint) array.Length, stride);
            handle.Free();
        }

        public static void SubmitArray(uint objectHandle, uint componentHandle, Color32[] array, uint stride)
        {
            var handle = GCHandle.Alloc(array, GCHandleType.Pinned);
            var arrayPointer = handle.AddrOfPinnedObject();
            Object.SetValue_p_uint8(objectHandle, componentHandle, arrayPointer, (uint) array.Length, stride);
            handle.Free();
        }
    
        public static void SubmitArrayForceUInt32(uint objectHandle, uint componentHandle, int[] array, uint stride)
        {
            var handle = GCHandle.Alloc(array, GCHandleType.Pinned);
            var arrayPointer = handle.AddrOfPinnedObject();
            Object.SetValue_p_int32(objectHandle, componentHandle, arrayPointer, (uint) array.Length, stride);
            handle.Free();
        }

        public static void SubmitArray(uint objectHandle, uint componentHandle, float[] array, uint stride)
        {
            var handle = GCHandle.Alloc(array, GCHandleType.Pinned);
            var arrayPointer = handle.AddrOfPinnedObject();
            Object.SetValue_p_float32(objectHandle, componentHandle, arrayPointer, (uint) array.Length, stride);
            handle.Free();
        }

        public static void SubmitArray(uint objectHandle, uint componentHandle, Vector2[] array)
        {
            var handle = GCHandle.Alloc(array, GCHandleType.Pinned);
            var arrayPointer = handle.AddrOfPinnedObject();
            Object.SetValue_p_float32(objectHandle, componentHandle, arrayPointer, (uint) array.Length, sizeof(float) * 2);
            handle.Free();
        }

        public static void SubmitArray(uint objectHandle, uint componentHandle, Vector3[] array)
        {
            var handle = GCHandle.Alloc(array, GCHandleType.Pinned);
            var arrayPointer = handle.AddrOfPinnedObject();
            Object.SetValue_p_float32(objectHandle, componentHandle, arrayPointer, (uint) array.Length, sizeof(float) * 3);
            handle.Free();
        }
    
        public static void SubmitArray(uint objectHandle, uint componentHandle, Vector4[] array)
        {
            var handle = GCHandle.Alloc(array, GCHandleType.Pinned);
            var arrayPointer = handle.AddrOfPinnedObject();
            Object.SetValue_p_float32(objectHandle, componentHandle, arrayPointer, (uint) array.Length, sizeof(float) * 4);
            handle.Free();
        }
    
        public static void SubmitArray(uint objectHandle, uint componentHandle, Color[] array)
        {
            var handle = GCHandle.Alloc(array, GCHandleType.Pinned);
            var arrayPointer = handle.AddrOfPinnedObject();
            Object.SetValue_p_float32(objectHandle, componentHandle, arrayPointer, (uint) array.Length, sizeof(float) * 4);
            handle.Free();
        }
    
        public static void SubmitArray(uint objectHandle, uint componentHandle, Matrix4x4[] array)
        {
#if ST_FAST_COPY_MATRIX44
            var handle = GCHandle.Alloc(array, GCHandleType.Pinned);
            var arrayPointer = handle.AddrOfPinnedObject();
            Object.SetValue_p_float32(objectHandle, componentHandle, arrayPointer, (uint) array.Length, sizeof(float) * 16);
            handle.Free();
#else
            int arrayLength = array.Length;
            float[] float16Matrix = new float[arrayLength * 16];

            for(int i=0, j = 0;i < arrayLength;i++, j+=16)
            {
              Matrix4x4 m = array[i];
              float16Matrix[j + 0]  = m.m00;
              float16Matrix[j + 1]  = m.m01;
              float16Matrix[j + 2]  = m.m02;
              float16Matrix[j + 3]  = m.m03;
              float16Matrix[j + 4]  = m.m10;
              float16Matrix[j + 5]  = m.m11;
              float16Matrix[j + 6]  = m.m12;
              float16Matrix[j + 7]  = m.m13;
              float16Matrix[j + 8]  = m.m20;
              float16Matrix[j + 9]  = m.m21;
              float16Matrix[j + 10] = m.m22;
              float16Matrix[j + 11] = m.m23;
              float16Matrix[j + 12] = m.m30;
              float16Matrix[j + 13] = m.m31;
              float16Matrix[j + 14] = m.m32;
              float16Matrix[j + 15] = m.m33;
            }

            SubmitArray(objectHandle, componentHandle, float16Matrix, 16);

            float16Matrix = null;
#endif
          }

        public static void SubmitString(uint objectHandle, uint componentHandle, string value)
        {
          if (value.Length > 0)
          {
              Object.SetValue_string(objectHandle, componentHandle, new StringBuilder(value), (uint) value.Length);
          }
        }
    
    }
}