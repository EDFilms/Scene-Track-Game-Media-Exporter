using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;

namespace SceneTrackVideo
{
	public static class FileType
	{
		public const int FILETYPE_JPEG_SEQUENCE = (0);
		public const int FILETYPE_PNG_SEQUENCE = (1);
		public const int FILETYPE_MJPEG_SEQUENCE = (2);
		public const int FILETYPE_MPEG1_SEQUENCE = (3);
		public const int FILETYPE_RAW_SEQUENCE = (4);
	}

	public static class Flip
	{
		public const int None = (0);
		public const int Vertical = (1);
		public const int Horizontal = (2);
		public const int Both = (3);
	}

	public static class Conversion
	{

		#if UNITY_EDITOR
		[DllImport("SceneTrackVideo", CallingConvention = CallingConvention.Cdecl, EntryPoint = "videoConvertSceneTrackFile"), SuppressUnmanagedCodeSecurity]
		public static extern int ConvertSceneTrackFile(StringBuilder src, StringBuilder dst);
		#else
		public static int ConvertSceneTrackFile(StringBuilder src, StringBuilder dst) { return default(int); }
		#endif

		#if UNITY_EDITOR
		[DllImport("SceneTrackVideo", CallingConvention = CallingConvention.Cdecl, EntryPoint = "videoStepConvertSceneTrackFileBegin"), SuppressUnmanagedCodeSecurity]
		public static extern int StepConvertSceneTrackFileBegin(StringBuilder src, StringBuilder dst);
		#else
		public static int StepConvertSceneTrackFileBegin(StringBuilder src, StringBuilder dst) { return default(int); }
		#endif

		#if UNITY_EDITOR
		[DllImport("SceneTrackVideo", CallingConvention = CallingConvention.Cdecl, EntryPoint = "videoStepConvertSceneTrackFileUpdate"), SuppressUnmanagedCodeSecurity]
		public static extern int StepConvertSceneTrackFileUpdate();
		#else
		public static int StepConvertSceneTrackFileUpdate() { return default(int); }
		#endif

		#if UNITY_EDITOR
		[DllImport("SceneTrackVideo", CallingConvention = CallingConvention.Cdecl, EntryPoint = "videoStepConvertProgress"), SuppressUnmanagedCodeSecurity]
		public static extern float StepConvertProgress();
		#else
		public static float StepConvertProgress() { return default(float); }
		#endif

	}
	public static class Library
	{

		#if UNITY_EDITOR
		[DllImport("SceneTrackVideo", CallingConvention = CallingConvention.Cdecl, EntryPoint = "videoGetVersionStrCapacity"), SuppressUnmanagedCodeSecurity]
		public static extern int GetVersionStrCapacity();
		#else
		public static int GetVersionStrCapacity() { return default(int); }
		#endif

		#if UNITY_EDITOR
		[DllImport("SceneTrackVideo", CallingConvention = CallingConvention.Cdecl, EntryPoint = "videoGetVersion"), SuppressUnmanagedCodeSecurity]
		public static extern int GetVersion(StringBuilder outStr, int strCapacity);
		#else
		public static int GetVersion(StringBuilder outStr, int strCapacity) { return default(int); }
		#endif

	}
	public static class Settings
	{

		#if UNITY_EDITOR
		[DllImport("SceneTrackVideo", CallingConvention = CallingConvention.Cdecl, EntryPoint = "videoSetFileType"), SuppressUnmanagedCodeSecurity]
		public static extern int SetFileType(int fileType);
		#else
		public static int SetFileType(int fileType) { return default(int); }
		#endif

		#if UNITY_EDITOR
		[DllImport("SceneTrackVideo", CallingConvention = CallingConvention.Cdecl, EntryPoint = "videoSetImageFlip"), SuppressUnmanagedCodeSecurity]
		public static extern int SetImageFlip(int imageFlip);
		#else
		public static int SetImageFlip(int imageFlip) { return default(int); }
		#endif

	}
}

