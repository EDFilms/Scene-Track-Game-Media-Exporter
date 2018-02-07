using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;

namespace SceneTrackFbx
{
	public static class Node
	{
		public const int Transform = (0);
		public const int Bone = (1);
		public const int Mesh = (2);
	}

	public static class NodeProperty
	{
		public const int Translation = (1);
		public const int Vertex = (1);
		public const int Rotation = (2);
		public const int Normal = (2);
		public const int Scale = (4);
	}

	public static class Axis
	{
		public const int X = (0);
		public const int Y = (1);
		public const int Z = (2);
		public const int W = (3);
	}

	public static class AxisOrder
	{
		public const int XYZ = (0);
		public const int XZY = (1);
		public const int YXZ = (2);
		public const int YZX = (3);
		public const int ZXY = (4);
		public const int ZYX = (5);
	}

	public static class Operator
	{
		public const int SetZero = (0);
		public const int SetOne = (1);
		public const int Value = (2);
		public const int Constant = (3);
		public const int Add = (4);
		public const int Subtract = (5);
		public const int Multiply = (6);
		public const int Divide = (7);
		public const int Negate = (8);
	}

	public static class FbxFileVersion
	{
		public const int FBX_53_MB55 = (0);
		public const int FBX_60_MB60 = (1);
		public const int FBX_200508_MB70 = (2);
		public const int FBX_200602_MB75 = (3);
		public const int FBX_200608 = (4);
		public const int FBX_200611 = (5);
		public const int FBX_200900 = (6);
		public const int FBX_200900v7 = (7);
		public const int FBX_201000 = (8);
		public const int FBX_201100 = (9);
		public const int FBX_201200 = (10);
		public const int FBX_201300 = (11);
		public const int FBX_201400 = (12);
		public const int FBX_201600 = (13);
	}

	public static class Fps
	{
		public const int DEFAULT = (0);
		public const int CUSTOM = (1);
		public const int FPS_23_976 = (2);
		public const int FPS_24 = (3);
		public const int FPS_30 = (4);
		public const int FPS_30_DROP = (5);
		public const int FPS_48 = (6);
		public const int FPS_50 = (7);
		public const int FPS_59_94 = (8);
		public const int FPS_60 = (9);
		public const int FPS_72 = (10);
		public const int FPS_96 = (11);
		public const int FPS_100 = (12);
		public const int FPS_120 = (13);
		public const int FPS_1000 = (14);
		public const int FPS_PAL = (15);
		public const int FPS_NTSC = (16);
		public const int FPS_NTSC_DROP = (17);
	}

	public static class ReferenceFrame
	{
		public const int Local = (0);
		public const int World = (1);
	}

	public static class Conversion
	{

		#if UNITY_EDITOR
		[DllImport("SceneTrackFbx", CallingConvention = CallingConvention.Cdecl, EntryPoint = "fbxConvertSceneTrackFile"), SuppressUnmanagedCodeSecurity]
		public static extern int ConvertSceneTrackFile(StringBuilder src, StringBuilder dst);
		#else
		public static int ConvertSceneTrackFile(StringBuilder src, StringBuilder dst) { return default(int); }
		#endif

		#if UNITY_EDITOR
		[DllImport("SceneTrackFbx", CallingConvention = CallingConvention.Cdecl, EntryPoint = "fbxStepConvertSceneTrackFileBegin"), SuppressUnmanagedCodeSecurity]
		public static extern int StepConvertSceneTrackFileBegin(StringBuilder src, StringBuilder dst);
		#else
		public static int StepConvertSceneTrackFileBegin(StringBuilder src, StringBuilder dst) { return default(int); }
		#endif

		#if UNITY_EDITOR
		[DllImport("SceneTrackFbx", CallingConvention = CallingConvention.Cdecl, EntryPoint = "fbxStepConvertSceneTrackFileUpdate"), SuppressUnmanagedCodeSecurity]
		public static extern int StepConvertSceneTrackFileUpdate();
		#else
		public static int StepConvertSceneTrackFileUpdate() { return default(int); }
		#endif

		#if UNITY_EDITOR
		[DllImport("SceneTrackFbx", CallingConvention = CallingConvention.Cdecl, EntryPoint = "fbxStepConvertProgress"), SuppressUnmanagedCodeSecurity]
		public static extern float StepConvertProgress();
		#else
		public static float StepConvertProgress() { return default(float); }
		#endif

	}
	public static class Library
	{

		#if UNITY_EDITOR
		[DllImport("SceneTrackFbx", CallingConvention = CallingConvention.Cdecl, EntryPoint = "fbxGetVersionStrCapacity"), SuppressUnmanagedCodeSecurity]
		public static extern int GetVersionStrCapacity();
		#else
		public static int GetVersionStrCapacity() { return default(int); }
		#endif

		#if UNITY_EDITOR
		[DllImport("SceneTrackFbx", CallingConvention = CallingConvention.Cdecl, EntryPoint = "fbxGetVersion"), SuppressUnmanagedCodeSecurity]
		public static extern int GetVersion(StringBuilder outStr, int strCapacity);
		#else
		public static int GetVersion(StringBuilder outStr, int strCapacity) { return default(int); }
		#endif

	}
	public static class Settings
	{

		#if UNITY_EDITOR
		[DllImport("SceneTrackFbx", CallingConvention = CallingConvention.Cdecl, EntryPoint = "fbxSetAxisSwizzle"), SuppressUnmanagedCodeSecurity]
		public static extern int SetAxisSwizzle(int nodeType, int trsMask, int srcAxis, int dstAxis, int sign);
		#else
		public static int SetAxisSwizzle(int nodeType, int trsMask, int srcAxis, int dstAxis, int sign) { return default(int); }
		#endif

		#if UNITY_EDITOR
		[DllImport("SceneTrackFbx", CallingConvention = CallingConvention.Cdecl, EntryPoint = "fbxSetAxisOperation"), SuppressUnmanagedCodeSecurity]
		public static extern int SetAxisOperation(int nodeType, int trsMask, int dstAxis, int operatorType, float constantValue);
		#else
		public static int SetAxisOperation(int nodeType, int trsMask, int dstAxis, int operatorType, float constantValue) { return default(int); }
		#endif

		#if UNITY_EDITOR
		[DllImport("SceneTrackFbx", CallingConvention = CallingConvention.Cdecl, EntryPoint = "fbxSetAxisRotationOrder"), SuppressUnmanagedCodeSecurity]
		public static extern int SetAxisRotationOrder(int axisOrder);
		#else
		public static int SetAxisRotationOrder(int axisOrder) { return default(int); }
		#endif

		#if UNITY_EDITOR
		[DllImport("SceneTrackFbx", CallingConvention = CallingConvention.Cdecl, EntryPoint = "fbxSetReverseTriangleWinding"), SuppressUnmanagedCodeSecurity]
		public static extern int SetReverseTriangleWinding(int reverseWinding);
		#else
		public static int SetReverseTriangleWinding(int reverseWinding) { return default(int); }
		#endif

		#if UNITY_EDITOR
		[DllImport("SceneTrackFbx", CallingConvention = CallingConvention.Cdecl, EntryPoint = "fbxSetReferenceFrame"), SuppressUnmanagedCodeSecurity]
		public static extern int SetReferenceFrame(int referenceFrame);
		#else
		public static int SetReferenceFrame(int referenceFrame) { return default(int); }
		#endif

		#if UNITY_EDITOR
		[DllImport("SceneTrackFbx", CallingConvention = CallingConvention.Cdecl, EntryPoint = "fbxSetFileVersion"), SuppressUnmanagedCodeSecurity]
		public static extern int SetFileVersion(int sdkFileVersion);
		#else
		public static int SetFileVersion(int sdkFileVersion) { return default(int); }
		#endif

		#if UNITY_EDITOR
		[DllImport("SceneTrackFbx", CallingConvention = CallingConvention.Cdecl, EntryPoint = "fbxSetFrameRate"), SuppressUnmanagedCodeSecurity]
		public static extern int SetFrameRate(int frameRate, double customRate);
		#else
		public static int SetFrameRate(int frameRate, double customRate) { return default(int); }
		#endif

		#if UNITY_EDITOR
		[DllImport("SceneTrackFbx", CallingConvention = CallingConvention.Cdecl, EntryPoint = "fbxClearAssetSearchPaths"), SuppressUnmanagedCodeSecurity]
		public static extern void ClearAssetSearchPaths();
		#else
		public static void ClearAssetSearchPaths() {}
		#endif

		#if UNITY_EDITOR
		[DllImport("SceneTrackFbx", CallingConvention = CallingConvention.Cdecl, EntryPoint = "fbxAddAssetSearchPath"), SuppressUnmanagedCodeSecurity]
		public static extern void AddAssetSearchPath(StringBuilder path);
		#else
		public static void AddAssetSearchPath(StringBuilder path) {}
		#endif

	}
}

