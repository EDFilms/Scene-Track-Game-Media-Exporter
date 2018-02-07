using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;

namespace SceneTrackMidi
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

	public static class Conversion
	{

		#if UNITY_EDITOR
		[DllImport("SceneTrackMidi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "midiConvertSceneTrackFile"), SuppressUnmanagedCodeSecurity]
		public static extern int ConvertSceneTrackFile(StringBuilder src, StringBuilder dst);
		#else
		public static int ConvertSceneTrackFile(StringBuilder src, StringBuilder dst) { return default(int); }
		#endif

		#if UNITY_EDITOR
		[DllImport("SceneTrackMidi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "midiStepConvertSceneTrackFileBegin"), SuppressUnmanagedCodeSecurity]
		public static extern int StepConvertSceneTrackFileBegin(StringBuilder src, StringBuilder dst);
		#else
		public static int StepConvertSceneTrackFileBegin(StringBuilder src, StringBuilder dst) { return default(int); }
		#endif

		#if UNITY_EDITOR
		[DllImport("SceneTrackMidi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "midiStepConvertSceneTrackFileUpdate"), SuppressUnmanagedCodeSecurity]
		public static extern int StepConvertSceneTrackFileUpdate();
		#else
		public static int StepConvertSceneTrackFileUpdate() { return default(int); }
		#endif

		#if UNITY_EDITOR
		[DllImport("SceneTrackMidi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "midiStepConvertProgress"), SuppressUnmanagedCodeSecurity]
		public static extern float StepConvertProgress();
		#else
		public static float StepConvertProgress() { return default(float); }
		#endif

	}
	public static class Library
	{

		#if UNITY_EDITOR
		[DllImport("SceneTrackMidi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "midiGetVersionStrCapacity"), SuppressUnmanagedCodeSecurity]
		public static extern int GetVersionStrCapacity();
		#else
		public static int GetVersionStrCapacity() { return default(int); }
		#endif

		#if UNITY_EDITOR
		[DllImport("SceneTrackMidi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "midiGetVersion"), SuppressUnmanagedCodeSecurity]
		public static extern int GetVersion(StringBuilder outStr, int strCapacity);
		#else
		public static int GetVersion(StringBuilder outStr, int strCapacity) { return default(int); }
		#endif

	}
	public static class Settings
	{

		#if UNITY_EDITOR
		[DllImport("SceneTrackMidi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "midiSetAxisSwizzle"), SuppressUnmanagedCodeSecurity]
		public static extern int SetAxisSwizzle(int nodeType, int trsMask, int srcAxis, int dstAxis, int sign);
		#else
		public static int SetAxisSwizzle(int nodeType, int trsMask, int srcAxis, int dstAxis, int sign) { return default(int); }
		#endif

		#if UNITY_EDITOR
		[DllImport("SceneTrackMidi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "midiSetAxisOperation"), SuppressUnmanagedCodeSecurity]
		public static extern int SetAxisOperation(int nodeType, int trsMask, int dstAxis, int operatorType, float constantValue);
		#else
		public static int SetAxisOperation(int nodeType, int trsMask, int dstAxis, int operatorType, float constantValue) { return default(int); }
		#endif

		#if UNITY_EDITOR
		[DllImport("SceneTrackMidi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "midiSetAxisRotationOrder"), SuppressUnmanagedCodeSecurity]
		public static extern int SetAxisRotationOrder(int axisOrder);
		#else
		public static int SetAxisRotationOrder(int axisOrder) { return default(int); }
		#endif

		#if UNITY_EDITOR
		[DllImport("SceneTrackMidi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "midiSetFileType"), SuppressUnmanagedCodeSecurity]
		public static extern int SetFileType(int fileType);
		#else
		public static int SetFileType(int fileType) { return default(int); }
		#endif

		#if UNITY_EDITOR
		[DllImport("SceneTrackMidi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "midiSetTimingMode"), SuppressUnmanagedCodeSecurity]
		public static extern int SetTimingMode(int timingMode);
		#else
		public static int SetTimingMode(int timingMode) { return default(int); }
		#endif

		#if UNITY_EDITOR
		[DllImport("SceneTrackMidi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "midiSetTimingValues"), SuppressUnmanagedCodeSecurity]
		public static extern int SetTimingValues(int first, int second);
		#else
		public static int SetTimingValues(int first, int second) { return default(int); }
		#endif

	}
}

