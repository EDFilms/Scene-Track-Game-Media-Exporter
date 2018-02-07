using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;

namespace SceneTrack
{
	public static class Error
	{
		public const int Memory = (1);
		public const int Disk = (2);
		public const int Recording = (4);
		public const int Playing = (8);
		public const int Writing = (16);
		public const int Reading = (32);
		public const int Type = (64);
		public const int Reserve = (128);
		public const int Iterator = (256);
	}

	public static class IteratorType
	{
		public const int None = (0);
		public const int End = (1);
		public const int Frame = (2);
		public const int Object = (3);
		public const int NewObject = (4);
		public const int DeleteObject = (5);
		public const int Value = (6);
	}

	public static class IteratorChangeFlags
	{
		public const int Frame = (1);
		public const int Object = (2);
		public const int Value = (4);
	}

	public static class Kind
	{
		public const int None = (0);
		public const int Length = (100);
		public const int Minimum = (101);
		public const int Maximum = (102);
		public const int Intensity = (103);
		public const int Named = (104);
		public const int Type = (105);
		public const int Repeat = (106);
		public const int Relationship = (201);
		public const int Spatial = (202);
		public const int Geometry = (203);
		public const int Audio = (204);
		public const int Video = (205);
		public const int Event = (206);
		public const int Surface = (207);
		public const int Layer = (300);
		public const int Tag = (301);
		public const int Parent = (302);
		public const int Child = (304);
		public const int Next = (305);
		public const int Previous = (306);
		public const int Size = (400);
		public const int Width = (401);
		public const int Height = (402);
		public const int Depth = (403);
		public const int X = (404);
		public const int Y = (405);
		public const int Z = (406);
		public const int Yaw = (407);
		public const int Pitch = (408);
		public const int Roll = (409);
		public const int Position = (410);
		public const int Rotation = (411);
		public const int Scale = (412);
		public const int Transform = (413);
		public const int LinearVelocity = (414);
		public const int LinearAcceleration = (415);
		public const int AngularVelocity = (416);
		public const int AngularAcceleration = (417);
		public const int Offset = (418);
		public const int Vertex = (500);
		public const int Index = (501);
		public const int Normal = (502);
		public const int Tangent = (503);
		public const int Color = (504);
		public const int UV0 = (505);
		public const int UV1 = (506);
		public const int UV2 = (507);
		public const int UV3 = (508);
		public const int UV4 = (509);
		public const int UV5 = (510);
		public const int UV6 = (511);
		public const int UV7 = (512);
		public const int BoneBegin = (513);
		public const int BoneEnd = (514);
		public const int BoneLength = (515);
		public const int BoneWeight = (516);
		public const int BoneIndex = (517);
		public const int Bone = (518);
		public const int Pose = (519);
		public const int PCM = (600);
		public const int Image = (700);
		public const int R = (701);
		public const int G = (702);
		public const int B = (703);
		public const int A = (704);
		public const int Ambient = (800);
		public const int Specular = (801);
		public const int Emissive = (802);
		public const int Refraction = (803);
		public const int Roughness = (804);
		public const int Reflection = (806);
		public const int Transparency = (807);
	}

	public static class Frequency
	{
		public const int Static = (0);
		public const int Dynamic = (1);
		public const int Stream = (2);
		public const int Event = (3);
	}

	public static class Memory
	{
		public const int FramePool = (0);
		public const int FramePage = (0);
	}

	public static class Reference
	{
		public const int Unspecified = (0);
		public const int World = (1);
		public const int Local = (2);
		public const int InternalAsset = (3);
		public const int ExternalAsset = (4);
	}

	public static class Type
	{
		public const int None = (0);
		public const int Uint8 = (1);
		public const int Uint16 = (2);
		public const int Uint32 = (3);
		public const int Uint64 = (4);
		public const int Int8 = (5);
		public const int Int16 = (6);
		public const int Int32 = (7);
		public const int Int64 = (8);
		public const int Float32 = (9);
		public const int Float64 = (10);
		public const int String = (11);
		public const int Object = (12);
		public const int ObjectType = (13);
		public const int CString = (21);
		public const int Enum = (22);
		public const int Marker = (255);
	}

	public static class Units
	{
		public const int Unspecified = (0);
		public const int Radian = (1);
		public const int Degree = (2);
		public const int Enumeration = (3);
	}

	public static class Format
	{
		public const int Binary = (0);
	}

	public static class Library
	{

		#if UNITY_EDITOR
		[DllImport("SceneTrack", CallingConvention = CallingConvention.Cdecl, EntryPoint = "stGetVersionStrCapacity"), SuppressUnmanagedCodeSecurity]
		public static extern int GetVersionStrCapacity();
		#else
		public static int GetVersionStrCapacity() { return default(int); }
		#endif

		#if UNITY_EDITOR
		[DllImport("SceneTrack", CallingConvention = CallingConvention.Cdecl, EntryPoint = "stGetVersion"), SuppressUnmanagedCodeSecurity]
		public static extern int GetVersion(StringBuilder outStr, int strCapacity);
		#else
		public static int GetVersion(StringBuilder outStr, int strCapacity) { return default(int); }
		#endif

	}
	public static class Object
	{

		#if UNITY_EDITOR
		[DllImport("SceneTrack", CallingConvention = CallingConvention.Cdecl, EntryPoint = "stCreateKind"), SuppressUnmanagedCodeSecurity]
		public static extern uint CreateKind(StringBuilder name);
		#else
		public static uint CreateKind(StringBuilder name) { return default(uint); }
		#endif

		#if UNITY_EDITOR
		[DllImport("SceneTrack", CallingConvention = CallingConvention.Cdecl, EntryPoint = "stCreateObjectType"), SuppressUnmanagedCodeSecurity]
		public static extern uint CreateObjectType(int objectFrequency);
		#else
		public static uint CreateObjectType(int objectFrequency) { return default(uint); }
		#endif

		#if UNITY_EDITOR
		[DllImport("SceneTrack", CallingConvention = CallingConvention.Cdecl, EntryPoint = "stCreateObjectTypeEx"), SuppressUnmanagedCodeSecurity]
		public static extern uint CreateObjectTypeEx(int objectFrequency, uint userData);
		#else
		public static uint CreateObjectTypeEx(int objectFrequency, uint userData) { return default(uint); }
		#endif

		#if UNITY_EDITOR
		[DllImport("SceneTrack", CallingConvention = CallingConvention.Cdecl, EntryPoint = "stFindObjectType"), SuppressUnmanagedCodeSecurity]
		public static extern uint FindObjectType(uint userData);
		#else
		public static uint FindObjectType(uint userData) { return default(uint); }
		#endif

		#if UNITY_EDITOR
		[DllImport("SceneTrack", CallingConvention = CallingConvention.Cdecl, EntryPoint = "stAddObjectTypeComponent"), SuppressUnmanagedCodeSecurity]
		public static extern uint AddObjectTypeComponent(uint objectType, int kind, int type, uint nbElements);
		#else
		public static uint AddObjectTypeComponent(uint objectType, int kind, int type, uint nbElements) { return default(uint); }
		#endif

		#if UNITY_EDITOR
		[DllImport("SceneTrack", CallingConvention = CallingConvention.Cdecl, EntryPoint = "stAddObjectTypeComponentEx"), SuppressUnmanagedCodeSecurity]
		public static extern uint AddObjectTypeComponentEx(uint objectType, int kind, int type, uint nbElements, uint arrayCapacity);
		#else
		public static uint AddObjectTypeComponentEx(uint objectType, int kind, int type, uint nbElements, uint arrayCapacity) { return default(uint); }
		#endif

		#if UNITY_EDITOR
		[DllImport("SceneTrack", CallingConvention = CallingConvention.Cdecl, EntryPoint = "stAddObjectTypeComponentEx2"), SuppressUnmanagedCodeSecurity]
		public static extern uint AddObjectTypeComponentEx2(uint objectType, int kind, int type, uint nbElements, uint arrayCapacity, int units, int reference);
		#else
		public static uint AddObjectTypeComponentEx2(uint objectType, int kind, int type, uint nbElements, uint arrayCapacity, int units, int reference) { return default(uint); }
		#endif

		#if UNITY_EDITOR
		[DllImport("SceneTrack", CallingConvention = CallingConvention.Cdecl, EntryPoint = "stFindObjectTypeComponent"), SuppressUnmanagedCodeSecurity]
		public static extern uint FindObjectTypeComponent(uint objectType, uint kind);
		#else
		public static uint FindObjectTypeComponent(uint objectType, uint kind) { return default(uint); }
		#endif

		#if UNITY_EDITOR
		[DllImport("SceneTrack", CallingConvention = CallingConvention.Cdecl, EntryPoint = "stCreateObject"), SuppressUnmanagedCodeSecurity]
		public static extern uint CreateObject(uint objectType);
		#else
		public static uint CreateObject(uint objectType) { return default(uint); }
		#endif

		#if UNITY_EDITOR
		[DllImport("SceneTrack", CallingConvention = CallingConvention.Cdecl, EntryPoint = "stDestroyObject"), SuppressUnmanagedCodeSecurity]
		public static extern void DestroyObject(uint objectHandle);
		#else
		public static void DestroyObject(uint objectHandle) {}
		#endif

		#if UNITY_EDITOR
		[DllImport("SceneTrack", CallingConvention = CallingConvention.Cdecl, EntryPoint = "stGetObjectType"), SuppressUnmanagedCodeSecurity]
		public static extern uint GetObjectType(uint objectHandle);
		#else
		public static uint GetObjectType(uint objectHandle) { return default(uint); }
		#endif

		#if UNITY_EDITOR
		[DllImport("SceneTrack", CallingConvention = CallingConvention.Cdecl, EntryPoint = "stSetValue_uint8"), SuppressUnmanagedCodeSecurity]
		public static extern void SetValue_uint8(uint objectHandle, uint component, byte x);
		#else
		public static void SetValue_uint8(uint objectHandle, uint component, byte x) {}
		#endif

		#if UNITY_EDITOR
		[DllImport("SceneTrack", CallingConvention = CallingConvention.Cdecl, EntryPoint = "stSetValue_uint16"), SuppressUnmanagedCodeSecurity]
		public static extern void SetValue_uint16(uint objectHandle, uint component, ushort x);
		#else
		public static void SetValue_uint16(uint objectHandle, uint component, ushort x) {}
		#endif

		#if UNITY_EDITOR
		[DllImport("SceneTrack", CallingConvention = CallingConvention.Cdecl, EntryPoint = "stSetValue_uint32"), SuppressUnmanagedCodeSecurity]
		public static extern void SetValue_uint32(uint objectHandle, uint component, uint x);
		#else
		public static void SetValue_uint32(uint objectHandle, uint component, uint x) {}
		#endif

		#if UNITY_EDITOR
		[DllImport("SceneTrack", CallingConvention = CallingConvention.Cdecl, EntryPoint = "stSetValue_uint64"), SuppressUnmanagedCodeSecurity]
		public static extern void SetValue_uint64(uint objectHandle, uint component, ulong x);
		#else
		public static void SetValue_uint64(uint objectHandle, uint component, ulong x) {}
		#endif

		#if UNITY_EDITOR
		[DllImport("SceneTrack", CallingConvention = CallingConvention.Cdecl, EntryPoint = "stSetValue_int8"), SuppressUnmanagedCodeSecurity]
		public static extern void SetValue_int8(uint objectHandle, uint component, sbyte x);
		#else
		public static void SetValue_int8(uint objectHandle, uint component, sbyte x) {}
		#endif

		#if UNITY_EDITOR
		[DllImport("SceneTrack", CallingConvention = CallingConvention.Cdecl, EntryPoint = "stSetValue_int16"), SuppressUnmanagedCodeSecurity]
		public static extern void SetValue_int16(uint objectHandle, uint component, short x);
		#else
		public static void SetValue_int16(uint objectHandle, uint component, short x) {}
		#endif

		#if UNITY_EDITOR
		[DllImport("SceneTrack", CallingConvention = CallingConvention.Cdecl, EntryPoint = "stSetValue_int32"), SuppressUnmanagedCodeSecurity]
		public static extern void SetValue_int32(uint objectHandle, uint component, int x);
		#else
		public static void SetValue_int32(uint objectHandle, uint component, int x) {}
		#endif

		#if UNITY_EDITOR
		[DllImport("SceneTrack", CallingConvention = CallingConvention.Cdecl, EntryPoint = "stSetValue_int64"), SuppressUnmanagedCodeSecurity]
		public static extern void SetValue_int64(uint objectHandle, uint component, long x);
		#else
		public static void SetValue_int64(uint objectHandle, uint component, long x) {}
		#endif

		#if UNITY_EDITOR
		[DllImport("SceneTrack", CallingConvention = CallingConvention.Cdecl, EntryPoint = "stSetValue_float32"), SuppressUnmanagedCodeSecurity]
		public static extern void SetValue_float32(uint objectHandle, uint component, float x);
		#else
		public static void SetValue_float32(uint objectHandle, uint component, float x) {}
		#endif

		#if UNITY_EDITOR
		[DllImport("SceneTrack", CallingConvention = CallingConvention.Cdecl, EntryPoint = "stSetValue_float64"), SuppressUnmanagedCodeSecurity]
		public static extern void SetValue_float64(uint objectHandle, uint component, double x);
		#else
		public static void SetValue_float64(uint objectHandle, uint component, double x) {}
		#endif

		#if UNITY_EDITOR
		[DllImport("SceneTrack", CallingConvention = CallingConvention.Cdecl, EntryPoint = "stSetValue_string"), SuppressUnmanagedCodeSecurity]
		public static extern void SetValue_string(uint objectHandle, uint component, StringBuilder str, uint strLength);
		#else
		public static void SetValue_string(uint objectHandle, uint component, StringBuilder str, uint strLength) {}
		#endif

		#if UNITY_EDITOR
		[DllImport("SceneTrack", CallingConvention = CallingConvention.Cdecl, EntryPoint = "stSetValue_2_uint8"), SuppressUnmanagedCodeSecurity]
		public static extern void SetValue_2_uint8(uint objectHandle, uint component, byte x, byte y);
		#else
		public static void SetValue_2_uint8(uint objectHandle, uint component, byte x, byte y) {}
		#endif

		#if UNITY_EDITOR
		[DllImport("SceneTrack", CallingConvention = CallingConvention.Cdecl, EntryPoint = "stSetValue_2_uint16"), SuppressUnmanagedCodeSecurity]
		public static extern void SetValue_2_uint16(uint objectHandle, uint component, ushort x, ushort y);
		#else
		public static void SetValue_2_uint16(uint objectHandle, uint component, ushort x, ushort y) {}
		#endif

		#if UNITY_EDITOR
		[DllImport("SceneTrack", CallingConvention = CallingConvention.Cdecl, EntryPoint = "stSetValue_2_uint32"), SuppressUnmanagedCodeSecurity]
		public static extern void SetValue_2_uint32(uint objectHandle, uint component, uint x, uint y);
		#else
		public static void SetValue_2_uint32(uint objectHandle, uint component, uint x, uint y) {}
		#endif

		#if UNITY_EDITOR
		[DllImport("SceneTrack", CallingConvention = CallingConvention.Cdecl, EntryPoint = "stSetValue_2_uint64"), SuppressUnmanagedCodeSecurity]
		public static extern void SetValue_2_uint64(uint objectHandle, uint component, ulong x, ulong y);
		#else
		public static void SetValue_2_uint64(uint objectHandle, uint component, ulong x, ulong y) {}
		#endif

		#if UNITY_EDITOR
		[DllImport("SceneTrack", CallingConvention = CallingConvention.Cdecl, EntryPoint = "stSetValue_2_int8"), SuppressUnmanagedCodeSecurity]
		public static extern void SetValue_2_int8(uint objectHandle, uint component, sbyte x, sbyte y);
		#else
		public static void SetValue_2_int8(uint objectHandle, uint component, sbyte x, sbyte y) {}
		#endif

		#if UNITY_EDITOR
		[DllImport("SceneTrack", CallingConvention = CallingConvention.Cdecl, EntryPoint = "stSetValue_2_int16"), SuppressUnmanagedCodeSecurity]
		public static extern void SetValue_2_int16(uint objectHandle, uint component, short x, short y);
		#else
		public static void SetValue_2_int16(uint objectHandle, uint component, short x, short y) {}
		#endif

		#if UNITY_EDITOR
		[DllImport("SceneTrack", CallingConvention = CallingConvention.Cdecl, EntryPoint = "stSetValue_2_int32"), SuppressUnmanagedCodeSecurity]
		public static extern void SetValue_2_int32(uint objectHandle, uint component, int x, int y);
		#else
		public static void SetValue_2_int32(uint objectHandle, uint component, int x, int y) {}
		#endif

		#if UNITY_EDITOR
		[DllImport("SceneTrack", CallingConvention = CallingConvention.Cdecl, EntryPoint = "stSetValue_2_int64"), SuppressUnmanagedCodeSecurity]
		public static extern void SetValue_2_int64(uint objectHandle, uint component, long x, long y);
		#else
		public static void SetValue_2_int64(uint objectHandle, uint component, long x, long y) {}
		#endif

		#if UNITY_EDITOR
		[DllImport("SceneTrack", CallingConvention = CallingConvention.Cdecl, EntryPoint = "stSetValue_2_float32"), SuppressUnmanagedCodeSecurity]
		public static extern void SetValue_2_float32(uint objectHandle, uint component, float x, float y);
		#else
		public static void SetValue_2_float32(uint objectHandle, uint component, float x, float y) {}
		#endif

		#if UNITY_EDITOR
		[DllImport("SceneTrack", CallingConvention = CallingConvention.Cdecl, EntryPoint = "stSetValue_2_float64"), SuppressUnmanagedCodeSecurity]
		public static extern void SetValue_2_float64(uint objectHandle, uint component, double x, double y);
		#else
		public static void SetValue_2_float64(uint objectHandle, uint component, double x, double y) {}
		#endif

		#if UNITY_EDITOR
		[DllImport("SceneTrack", CallingConvention = CallingConvention.Cdecl, EntryPoint = "stSetValue_3_uint8"), SuppressUnmanagedCodeSecurity]
		public static extern void SetValue_3_uint8(uint objectHandle, uint component, byte x, byte y, byte z);
		#else
		public static void SetValue_3_uint8(uint objectHandle, uint component, byte x, byte y, byte z) {}
		#endif

		#if UNITY_EDITOR
		[DllImport("SceneTrack", CallingConvention = CallingConvention.Cdecl, EntryPoint = "stSetValue_3_uint16"), SuppressUnmanagedCodeSecurity]
		public static extern void SetValue_3_uint16(uint objectHandle, uint component, ushort x, ushort y, ushort z);
		#else
		public static void SetValue_3_uint16(uint objectHandle, uint component, ushort x, ushort y, ushort z) {}
		#endif

		#if UNITY_EDITOR
		[DllImport("SceneTrack", CallingConvention = CallingConvention.Cdecl, EntryPoint = "stSetValue_3_uint32"), SuppressUnmanagedCodeSecurity]
		public static extern void SetValue_3_uint32(uint objectHandle, uint component, uint x, uint y, uint z);
		#else
		public static void SetValue_3_uint32(uint objectHandle, uint component, uint x, uint y, uint z) {}
		#endif

		#if UNITY_EDITOR
		[DllImport("SceneTrack", CallingConvention = CallingConvention.Cdecl, EntryPoint = "stSetValue_3_uint64"), SuppressUnmanagedCodeSecurity]
		public static extern void SetValue_3_uint64(uint objectHandle, uint component, ulong x, ulong y, ulong z);
		#else
		public static void SetValue_3_uint64(uint objectHandle, uint component, ulong x, ulong y, ulong z) {}
		#endif

		#if UNITY_EDITOR
		[DllImport("SceneTrack", CallingConvention = CallingConvention.Cdecl, EntryPoint = "stSetValue_3_int8"), SuppressUnmanagedCodeSecurity]
		public static extern void SetValue_3_int8(uint objectHandle, uint component, sbyte x, sbyte y, sbyte z);
		#else
		public static void SetValue_3_int8(uint objectHandle, uint component, sbyte x, sbyte y, sbyte z) {}
		#endif

		#if UNITY_EDITOR
		[DllImport("SceneTrack", CallingConvention = CallingConvention.Cdecl, EntryPoint = "stSetValue_3_int16"), SuppressUnmanagedCodeSecurity]
		public static extern void SetValue_3_int16(uint objectHandle, uint component, short x, short y, short z);
		#else
		public static void SetValue_3_int16(uint objectHandle, uint component, short x, short y, short z) {}
		#endif

		#if UNITY_EDITOR
		[DllImport("SceneTrack", CallingConvention = CallingConvention.Cdecl, EntryPoint = "stSetValue_3_int32"), SuppressUnmanagedCodeSecurity]
		public static extern void SetValue_3_int32(uint objectHandle, uint component, int x, int y, int z);
		#else
		public static void SetValue_3_int32(uint objectHandle, uint component, int x, int y, int z) {}
		#endif

		#if UNITY_EDITOR
		[DllImport("SceneTrack", CallingConvention = CallingConvention.Cdecl, EntryPoint = "stSetValue_3_int64"), SuppressUnmanagedCodeSecurity]
		public static extern void SetValue_3_int64(uint objectHandle, uint component, long x, long y, long z);
		#else
		public static void SetValue_3_int64(uint objectHandle, uint component, long x, long y, long z) {}
		#endif

		#if UNITY_EDITOR
		[DllImport("SceneTrack", CallingConvention = CallingConvention.Cdecl, EntryPoint = "stSetValue_3_float32"), SuppressUnmanagedCodeSecurity]
		public static extern void SetValue_3_float32(uint objectHandle, uint component, float x, float y, float z);
		#else
		public static void SetValue_3_float32(uint objectHandle, uint component, float x, float y, float z) {}
		#endif

		#if UNITY_EDITOR
		[DllImport("SceneTrack", CallingConvention = CallingConvention.Cdecl, EntryPoint = "stSetValue_3_float64"), SuppressUnmanagedCodeSecurity]
		public static extern void SetValue_3_float64(uint objectHandle, uint component, double x, double y, double z);
		#else
		public static void SetValue_3_float64(uint objectHandle, uint component, double x, double y, double z) {}
		#endif

		#if UNITY_EDITOR
		[DllImport("SceneTrack", CallingConvention = CallingConvention.Cdecl, EntryPoint = "stSetValue_4_uint8"), SuppressUnmanagedCodeSecurity]
		public static extern void SetValue_4_uint8(uint objectHandle, uint component, byte x, byte y, byte z, byte w);
		#else
		public static void SetValue_4_uint8(uint objectHandle, uint component, byte x, byte y, byte z, byte w) {}
		#endif

		#if UNITY_EDITOR
		[DllImport("SceneTrack", CallingConvention = CallingConvention.Cdecl, EntryPoint = "stSetValue_4_uint16"), SuppressUnmanagedCodeSecurity]
		public static extern void SetValue_4_uint16(uint objectHandle, uint component, ushort x, ushort y, ushort z, ushort w);
		#else
		public static void SetValue_4_uint16(uint objectHandle, uint component, ushort x, ushort y, ushort z, ushort w) {}
		#endif

		#if UNITY_EDITOR
		[DllImport("SceneTrack", CallingConvention = CallingConvention.Cdecl, EntryPoint = "stSetValue_4_uint32"), SuppressUnmanagedCodeSecurity]
		public static extern void SetValue_4_uint32(uint objectHandle, uint component, uint x, uint y, uint z, uint w);
		#else
		public static void SetValue_4_uint32(uint objectHandle, uint component, uint x, uint y, uint z, uint w) {}
		#endif

		#if UNITY_EDITOR
		[DllImport("SceneTrack", CallingConvention = CallingConvention.Cdecl, EntryPoint = "stSetValue_4_uint64"), SuppressUnmanagedCodeSecurity]
		public static extern void SetValue_4_uint64(uint objectHandle, uint component, ulong x, ulong y, ulong z, ulong w);
		#else
		public static void SetValue_4_uint64(uint objectHandle, uint component, ulong x, ulong y, ulong z, ulong w) {}
		#endif

		#if UNITY_EDITOR
		[DllImport("SceneTrack", CallingConvention = CallingConvention.Cdecl, EntryPoint = "stSetValue_4_int8"), SuppressUnmanagedCodeSecurity]
		public static extern void SetValue_4_int8(uint objectHandle, uint component, sbyte x, sbyte y, sbyte z, sbyte w);
		#else
		public static void SetValue_4_int8(uint objectHandle, uint component, sbyte x, sbyte y, sbyte z, sbyte w) {}
		#endif

		#if UNITY_EDITOR
		[DllImport("SceneTrack", CallingConvention = CallingConvention.Cdecl, EntryPoint = "stSetValue_4_int16"), SuppressUnmanagedCodeSecurity]
		public static extern void SetValue_4_int16(uint objectHandle, uint component, short x, short y, short z, short w);
		#else
		public static void SetValue_4_int16(uint objectHandle, uint component, short x, short y, short z, short w) {}
		#endif

		#if UNITY_EDITOR
		[DllImport("SceneTrack", CallingConvention = CallingConvention.Cdecl, EntryPoint = "stSetValue_4_int32"), SuppressUnmanagedCodeSecurity]
		public static extern void SetValue_4_int32(uint objectHandle, uint component, int x, int y, int z, int w);
		#else
		public static void SetValue_4_int32(uint objectHandle, uint component, int x, int y, int z, int w) {}
		#endif

		#if UNITY_EDITOR
		[DllImport("SceneTrack", CallingConvention = CallingConvention.Cdecl, EntryPoint = "stSetValue_4_int64"), SuppressUnmanagedCodeSecurity]
		public static extern void SetValue_4_int64(uint objectHandle, uint component, long x, long y, long z, long w);
		#else
		public static void SetValue_4_int64(uint objectHandle, uint component, long x, long y, long z, long w) {}
		#endif

		#if UNITY_EDITOR
		[DllImport("SceneTrack", CallingConvention = CallingConvention.Cdecl, EntryPoint = "stSetValue_4_float32"), SuppressUnmanagedCodeSecurity]
		public static extern void SetValue_4_float32(uint objectHandle, uint component, float x, float y, float z, float w);
		#else
		public static void SetValue_4_float32(uint objectHandle, uint component, float x, float y, float z, float w) {}
		#endif

		#if UNITY_EDITOR
		[DllImport("SceneTrack", CallingConvention = CallingConvention.Cdecl, EntryPoint = "stSetValue_4_float64"), SuppressUnmanagedCodeSecurity]
		public static extern void SetValue_4_float64(uint objectHandle, uint component, double x, double y, double z, double w);
		#else
		public static void SetValue_4_float64(uint objectHandle, uint component, double x, double y, double z, double w) {}
		#endif

		#if UNITY_EDITOR
		[DllImport("SceneTrack", CallingConvention = CallingConvention.Cdecl, EntryPoint = "stCalculatePODVectorMemorySize"), SuppressUnmanagedCodeSecurity]
		public static extern uint CalculatePODVectorMemorySize(int type, uint nbElements, uint arraySize);
		#else
		public static uint CalculatePODVectorMemorySize(int type, uint nbElements, uint arraySize) { return default(uint); }
		#endif

		#if UNITY_EDITOR
		[DllImport("SceneTrack", CallingConvention = CallingConvention.Cdecl, EntryPoint = "stCalculateStride1"), SuppressUnmanagedCodeSecurity]
		public static extern uint CalculateStride1(int type1, uint count1);
		#else
		public static uint CalculateStride1(int type1, uint count1) { return default(uint); }
		#endif

		#if UNITY_EDITOR
		[DllImport("SceneTrack", CallingConvention = CallingConvention.Cdecl, EntryPoint = "stCalculateStride2"), SuppressUnmanagedCodeSecurity]
		public static extern uint CalculateStride2(int type1, uint count1, int type2, uint count2);
		#else
		public static uint CalculateStride2(int type1, uint count1, int type2, uint count2) { return default(uint); }
		#endif

		#if UNITY_EDITOR
		[DllImport("SceneTrack", CallingConvention = CallingConvention.Cdecl, EntryPoint = "stCalculateStride3"), SuppressUnmanagedCodeSecurity]
		public static extern uint CalculateStride3(int type1, uint count1, int type2, uint count2, int type3, uint count3);
		#else
		public static uint CalculateStride3(int type1, uint count1, int type2, uint count2, int type3, uint count3) { return default(uint); }
		#endif

		#if UNITY_EDITOR
		[DllImport("SceneTrack", CallingConvention = CallingConvention.Cdecl, EntryPoint = "stCalculateStride4"), SuppressUnmanagedCodeSecurity]
		public static extern uint CalculateStride4(int type1, uint count1, int type2, uint count2, int type3, uint count3, int type4, uint count4);
		#else
		public static uint CalculateStride4(int type1, uint count1, int type2, uint count2, int type3, uint count3, int type4, uint count4) { return default(uint); }
		#endif

		#if UNITY_EDITOR
		[DllImport("SceneTrack", CallingConvention = CallingConvention.Cdecl, EntryPoint = "stCalculateStride5"), SuppressUnmanagedCodeSecurity]
		public static extern uint CalculateStride5(int type1, uint count1, int type2, uint count2, int type3, uint count3, int type4, uint count4, int type5, uint count5);
		#else
		public static uint CalculateStride5(int type1, uint count1, int type2, uint count2, int type3, uint count3, int type4, uint count4, int type5, uint count5) { return default(uint); }
		#endif

		#if UNITY_EDITOR
		[DllImport("SceneTrack", CallingConvention = CallingConvention.Cdecl, EntryPoint = "stCalculateStride6"), SuppressUnmanagedCodeSecurity]
		public static extern uint CalculateStride6(int type1, uint count1, int type2, uint count2, int type3, uint count3, int type4, uint count4, int type5, uint count5, int type6, uint count6);
		#else
		public static uint CalculateStride6(int type1, uint count1, int type2, uint count2, int type3, uint count3, int type4, uint count4, int type5, uint count5, int type6, uint count6) { return default(uint); }
		#endif

		#if UNITY_EDITOR
		[DllImport("SceneTrack", CallingConvention = CallingConvention.Cdecl, EntryPoint = "stCalculateStride7"), SuppressUnmanagedCodeSecurity]
		public static extern uint CalculateStride7(int type1, uint count1, int type2, uint count2, int type3, uint count3, int type4, uint count4, int type5, uint count5, int type6, uint count6, int type7, uint count7);
		#else
		public static uint CalculateStride7(int type1, uint count1, int type2, uint count2, int type3, uint count3, int type4, uint count4, int type5, uint count5, int type6, uint count6, int type7, uint count7) { return default(uint); }
		#endif

		#if UNITY_EDITOR
		[DllImport("SceneTrack", CallingConvention = CallingConvention.Cdecl, EntryPoint = "stCalculateStride8"), SuppressUnmanagedCodeSecurity]
		public static extern uint CalculateStride8(int type1, uint count1, int type2, uint count2, int type3, uint count3, int type4, uint count4, int type5, uint count5, int type6, uint count6, int type7, uint count7, int type8, uint count8);
		#else
		public static uint CalculateStride8(int type1, uint count1, int type2, uint count2, int type3, uint count3, int type4, uint count4, int type5, uint count5, int type6, uint count6, int type7, uint count7, int type8, uint count8) { return default(uint); }
		#endif

		#if UNITY_EDITOR
		[DllImport("SceneTrack", CallingConvention = CallingConvention.Cdecl, EntryPoint = "stSetValue_p_uint8"), SuppressUnmanagedCodeSecurity]
		public static extern void SetValue_p_uint8(uint objectHandle, uint component, IntPtr value, uint arraySize, uint stride);
		#else
		public static void SetValue_p_uint8(uint objectHandle, uint component, IntPtr value, uint arraySize, uint stride) {}
		#endif

		#if UNITY_EDITOR
		[DllImport("SceneTrack", CallingConvention = CallingConvention.Cdecl, EntryPoint = "stSetValue_p_uint16"), SuppressUnmanagedCodeSecurity]
		public static extern void SetValue_p_uint16(uint objectHandle, uint component, IntPtr value, uint arraySize, uint stride);
		#else
		public static void SetValue_p_uint16(uint objectHandle, uint component, IntPtr value, uint arraySize, uint stride) {}
		#endif

		#if UNITY_EDITOR
		[DllImport("SceneTrack", CallingConvention = CallingConvention.Cdecl, EntryPoint = "stSetValue_p_uint32"), SuppressUnmanagedCodeSecurity]
		public static extern void SetValue_p_uint32(uint objectHandle, uint component, IntPtr value, uint arraySize, uint stride);
		#else
		public static void SetValue_p_uint32(uint objectHandle, uint component, IntPtr value, uint arraySize, uint stride) {}
		#endif

		#if UNITY_EDITOR
		[DllImport("SceneTrack", CallingConvention = CallingConvention.Cdecl, EntryPoint = "stSetValue_p_uint64"), SuppressUnmanagedCodeSecurity]
		public static extern void SetValue_p_uint64(uint objectHandle, uint component, IntPtr value, uint arraySize, uint stride);
		#else
		public static void SetValue_p_uint64(uint objectHandle, uint component, IntPtr value, uint arraySize, uint stride) {}
		#endif

		#if UNITY_EDITOR
		[DllImport("SceneTrack", CallingConvention = CallingConvention.Cdecl, EntryPoint = "stSetValue_p_int8"), SuppressUnmanagedCodeSecurity]
		public static extern void SetValue_p_int8(uint objectHandle, uint component, IntPtr value, uint arraySize, uint stride);
		#else
		public static void SetValue_p_int8(uint objectHandle, uint component, IntPtr value, uint arraySize, uint stride) {}
		#endif

		#if UNITY_EDITOR
		[DllImport("SceneTrack", CallingConvention = CallingConvention.Cdecl, EntryPoint = "stSetValue_p_int16"), SuppressUnmanagedCodeSecurity]
		public static extern void SetValue_p_int16(uint objectHandle, uint component, IntPtr value, uint arraySize, uint stride);
		#else
		public static void SetValue_p_int16(uint objectHandle, uint component, IntPtr value, uint arraySize, uint stride) {}
		#endif

		#if UNITY_EDITOR
		[DllImport("SceneTrack", CallingConvention = CallingConvention.Cdecl, EntryPoint = "stSetValue_p_int32"), SuppressUnmanagedCodeSecurity]
		public static extern void SetValue_p_int32(uint objectHandle, uint component, IntPtr value, uint arraySize, uint stride);
		#else
		public static void SetValue_p_int32(uint objectHandle, uint component, IntPtr value, uint arraySize, uint stride) {}
		#endif

		#if UNITY_EDITOR
		[DllImport("SceneTrack", CallingConvention = CallingConvention.Cdecl, EntryPoint = "stSetValue_p_int64"), SuppressUnmanagedCodeSecurity]
		public static extern void SetValue_p_int64(uint objectHandle, uint component, IntPtr value, uint arraySize, uint stride);
		#else
		public static void SetValue_p_int64(uint objectHandle, uint component, IntPtr value, uint arraySize, uint stride) {}
		#endif

		#if UNITY_EDITOR
		[DllImport("SceneTrack", CallingConvention = CallingConvention.Cdecl, EntryPoint = "stSetValue_p_float32"), SuppressUnmanagedCodeSecurity]
		public static extern void SetValue_p_float32(uint objectHandle, uint component, IntPtr value, uint arraySize, uint stride);
		#else
		public static void SetValue_p_float32(uint objectHandle, uint component, IntPtr value, uint arraySize, uint stride) {}
		#endif

		#if UNITY_EDITOR
		[DllImport("SceneTrack", CallingConvention = CallingConvention.Cdecl, EntryPoint = "stSetValue_p_float64"), SuppressUnmanagedCodeSecurity]
		public static extern void SetValue_p_float64(uint objectHandle, uint component, IntPtr value, uint arraySize, uint stride);
		#else
		public static void SetValue_p_float64(uint objectHandle, uint component, IntPtr value, uint arraySize, uint stride) {}
		#endif

		#if UNITY_EDITOR
		[DllImport("SceneTrack", CallingConvention = CallingConvention.Cdecl, EntryPoint = "stSetValue_p_uint8Ex"), SuppressUnmanagedCodeSecurity]
		public static extern void SetValue_p_uint8Ex(uint objectHandle, uint component, IntPtr value, uint arraySize, uint stride, long valuePtrOffsetBytes);
		#else
		public static void SetValue_p_uint8Ex(uint objectHandle, uint component, IntPtr value, uint arraySize, uint stride, long valuePtrOffsetBytes) {}
		#endif

		#if UNITY_EDITOR
		[DllImport("SceneTrack", CallingConvention = CallingConvention.Cdecl, EntryPoint = "stSetValue_p_uint16Ex"), SuppressUnmanagedCodeSecurity]
		public static extern void SetValue_p_uint16Ex(uint objectHandle, uint component, IntPtr value, uint arraySize, uint stride, long valuePtrOffsetBytes);
		#else
		public static void SetValue_p_uint16Ex(uint objectHandle, uint component, IntPtr value, uint arraySize, uint stride, long valuePtrOffsetBytes) {}
		#endif

		#if UNITY_EDITOR
		[DllImport("SceneTrack", CallingConvention = CallingConvention.Cdecl, EntryPoint = "stSetValue_p_uint32Ex"), SuppressUnmanagedCodeSecurity]
		public static extern void SetValue_p_uint32Ex(uint objectHandle, uint component, IntPtr value, uint arraySize, uint stride, long valuePtrOffsetBytes);
		#else
		public static void SetValue_p_uint32Ex(uint objectHandle, uint component, IntPtr value, uint arraySize, uint stride, long valuePtrOffsetBytes) {}
		#endif

		#if UNITY_EDITOR
		[DllImport("SceneTrack", CallingConvention = CallingConvention.Cdecl, EntryPoint = "stSetValue_p_uint64Ex"), SuppressUnmanagedCodeSecurity]
		public static extern void SetValue_p_uint64Ex(uint objectHandle, uint component, IntPtr value, uint arraySize, uint stride, long valuePtrOffsetBytes);
		#else
		public static void SetValue_p_uint64Ex(uint objectHandle, uint component, IntPtr value, uint arraySize, uint stride, long valuePtrOffsetBytes) {}
		#endif

		#if UNITY_EDITOR
		[DllImport("SceneTrack", CallingConvention = CallingConvention.Cdecl, EntryPoint = "stSetValue_p_int8Ex"), SuppressUnmanagedCodeSecurity]
		public static extern void SetValue_p_int8Ex(uint objectHandle, uint component, IntPtr value, uint arraySize, uint stride, long valuePtrOffsetBytes);
		#else
		public static void SetValue_p_int8Ex(uint objectHandle, uint component, IntPtr value, uint arraySize, uint stride, long valuePtrOffsetBytes) {}
		#endif

		#if UNITY_EDITOR
		[DllImport("SceneTrack", CallingConvention = CallingConvention.Cdecl, EntryPoint = "stSetValue_p_int16Ex"), SuppressUnmanagedCodeSecurity]
		public static extern void SetValue_p_int16Ex(uint objectHandle, uint component, IntPtr value, uint arraySize, uint stride, long valuePtrOffsetBytes);
		#else
		public static void SetValue_p_int16Ex(uint objectHandle, uint component, IntPtr value, uint arraySize, uint stride, long valuePtrOffsetBytes) {}
		#endif

		#if UNITY_EDITOR
		[DllImport("SceneTrack", CallingConvention = CallingConvention.Cdecl, EntryPoint = "stSetValue_p_int32Ex"), SuppressUnmanagedCodeSecurity]
		public static extern void SetValue_p_int32Ex(uint objectHandle, uint component, IntPtr value, uint arraySize, uint stride, long valuePtrOffsetBytes);
		#else
		public static void SetValue_p_int32Ex(uint objectHandle, uint component, IntPtr value, uint arraySize, uint stride, long valuePtrOffsetBytes) {}
		#endif

		#if UNITY_EDITOR
		[DllImport("SceneTrack", CallingConvention = CallingConvention.Cdecl, EntryPoint = "stSetValue_p_int64Ex"), SuppressUnmanagedCodeSecurity]
		public static extern void SetValue_p_int64Ex(uint objectHandle, uint component, IntPtr value, uint arraySize, uint stride, long valuePtrOffsetBytes);
		#else
		public static void SetValue_p_int64Ex(uint objectHandle, uint component, IntPtr value, uint arraySize, uint stride, long valuePtrOffsetBytes) {}
		#endif

		#if UNITY_EDITOR
		[DllImport("SceneTrack", CallingConvention = CallingConvention.Cdecl, EntryPoint = "stSetValue_p_float32Ex"), SuppressUnmanagedCodeSecurity]
		public static extern void SetValue_p_float32Ex(uint objectHandle, uint component, IntPtr value, uint arraySize, uint stride, long valuePtrOffsetBytes);
		#else
		public static void SetValue_p_float32Ex(uint objectHandle, uint component, IntPtr value, uint arraySize, uint stride, long valuePtrOffsetBytes) {}
		#endif

		#if UNITY_EDITOR
		[DllImport("SceneTrack", CallingConvention = CallingConvention.Cdecl, EntryPoint = "stSetValue_p_float64Ex"), SuppressUnmanagedCodeSecurity]
		public static extern void SetValue_p_float64Ex(uint objectHandle, uint component, IntPtr value, uint arraySize, uint stride, long valuePtrOffsetBytes);
		#else
		public static void SetValue_p_float64Ex(uint objectHandle, uint component, IntPtr value, uint arraySize, uint stride, long valuePtrOffsetBytes) {}
		#endif

	}
	public static class Reading
	{

		#if UNITY_EDITOR
		[DllImport("SceneTrack", CallingConvention = CallingConvention.Cdecl, EntryPoint = "stDisposeIterator"), SuppressUnmanagedCodeSecurity]
		public static extern uint DisposeIterator(uint iteratorId);
		#else
		public static uint DisposeIterator(uint iteratorId) { return default(uint); }
		#endif

		#if UNITY_EDITOR
		[DllImport("SceneTrack", CallingConvention = CallingConvention.Cdecl, EntryPoint = "stCreateForwardIterator"), SuppressUnmanagedCodeSecurity]
		public static extern uint CreateForwardIterator();
		#else
		public static uint CreateForwardIterator() { return default(uint); }
		#endif

		#if UNITY_EDITOR
		[DllImport("SceneTrack", CallingConvention = CallingConvention.Cdecl, EntryPoint = "stIteratorNext"), SuppressUnmanagedCodeSecurity]
		public static extern int IteratorNext(uint iterator);
		#else
		public static int IteratorNext(uint iterator) { return default(int); }
		#endif

		#if UNITY_EDITOR
		[DllImport("SceneTrack", CallingConvention = CallingConvention.Cdecl, EntryPoint = "stIteratorEnded"), SuppressUnmanagedCodeSecurity]
		public static extern int IteratorEnded(uint iterator);
		#else
		public static int IteratorEnded(uint iterator) { return default(int); }
		#endif

		#if UNITY_EDITOR
		[DllImport("SceneTrack", CallingConvention = CallingConvention.Cdecl, EntryPoint = "stIteratorGetType"), SuppressUnmanagedCodeSecurity]
		public static extern int IteratorGetType(uint iterator);
		#else
		public static int IteratorGetType(uint iterator) { return default(int); }
		#endif

		#if UNITY_EDITOR
		[DllImport("SceneTrack", CallingConvention = CallingConvention.Cdecl, EntryPoint = "stIteratorGetChangeFlags"), SuppressUnmanagedCodeSecurity]
		public static extern int IteratorGetChangeFlags(uint iterator);
		#else
		public static int IteratorGetChangeFlags(uint iterator) { return default(int); }
		#endif

		#if UNITY_EDITOR
		[DllImport("SceneTrack", CallingConvention = CallingConvention.Cdecl, EntryPoint = "stIteratorGetFrame"), SuppressUnmanagedCodeSecurity]
		public static extern int IteratorGetFrame(uint iterator);
		#else
		public static int IteratorGetFrame(uint iterator) { return default(int); }
		#endif

		#if UNITY_EDITOR
		[DllImport("SceneTrack", CallingConvention = CallingConvention.Cdecl, EntryPoint = "stIteratorGetFrameTime"), SuppressUnmanagedCodeSecurity]
		public static extern double IteratorGetFrameTime(uint iterator);
		#else
		public static double IteratorGetFrameTime(uint iterator) { return default(double); }
		#endif

		#if UNITY_EDITOR
		[DllImport("SceneTrack", CallingConvention = CallingConvention.Cdecl, EntryPoint = "stIteratorGetFrameLength"), SuppressUnmanagedCodeSecurity]
		public static extern double IteratorGetFrameLength(uint iterator);
		#else
		public static double IteratorGetFrameLength(uint iterator) { return default(double); }
		#endif

		#if UNITY_EDITOR
		[DllImport("SceneTrack", CallingConvention = CallingConvention.Cdecl, EntryPoint = "stIteratorGetObjectHandle"), SuppressUnmanagedCodeSecurity]
		public static extern uint IteratorGetObjectHandle(uint iterator);
		#else
		public static uint IteratorGetObjectHandle(uint iterator) { return default(uint); }
		#endif

		#if UNITY_EDITOR
		[DllImport("SceneTrack", CallingConvention = CallingConvention.Cdecl, EntryPoint = "stIteratorGetObjectType"), SuppressUnmanagedCodeSecurity]
		public static extern uint IteratorGetObjectType(uint iterator);
		#else
		public static uint IteratorGetObjectType(uint iterator) { return default(uint); }
		#endif

		#if UNITY_EDITOR
		[DllImport("SceneTrack", CallingConvention = CallingConvention.Cdecl, EntryPoint = "stIteratorGetIsEvent"), SuppressUnmanagedCodeSecurity]
		public static extern uint IteratorGetIsEvent(uint iterator);
		#else
		public static uint IteratorGetIsEvent(uint iterator) { return default(uint); }
		#endif

		#if UNITY_EDITOR
		[DllImport("SceneTrack", CallingConvention = CallingConvention.Cdecl, EntryPoint = "stIteratorGetComponentHandle"), SuppressUnmanagedCodeSecurity]
		public static extern uint IteratorGetComponentHandle(uint iterator);
		#else
		public static uint IteratorGetComponentHandle(uint iterator) { return default(uint); }
		#endif

		#if UNITY_EDITOR
		[DllImport("SceneTrack", CallingConvention = CallingConvention.Cdecl, EntryPoint = "stIteratorGetValueType"), SuppressUnmanagedCodeSecurity]
		public static extern uint IteratorGetValueType(uint iterator);
		#else
		public static uint IteratorGetValueType(uint iterator) { return default(uint); }
		#endif

		#if UNITY_EDITOR
		[DllImport("SceneTrack", CallingConvention = CallingConvention.Cdecl, EntryPoint = "stIteratorGetValueArraySize"), SuppressUnmanagedCodeSecurity]
		public static extern uint IteratorGetValueArraySize(uint iterator);
		#else
		public static uint IteratorGetValueArraySize(uint iterator) { return default(uint); }
		#endif

		#if UNITY_EDITOR
		[DllImport("SceneTrack", CallingConvention = CallingConvention.Cdecl, EntryPoint = "stIteratorGetValueNbElements"), SuppressUnmanagedCodeSecurity]
		public static extern uint IteratorGetValueNbElements(uint iterator);
		#else
		public static uint IteratorGetValueNbElements(uint iterator) { return default(uint); }
		#endif

		#if UNITY_EDITOR
		[DllImport("SceneTrack", CallingConvention = CallingConvention.Cdecl, EntryPoint = "stIteratorGetValue_uint8"), SuppressUnmanagedCodeSecurity]
		public static extern byte IteratorGetValue_uint8(uint iterator, uint element);
		#else
		public static byte IteratorGetValue_uint8(uint iterator, uint element) { return default(byte); }
		#endif

		#if UNITY_EDITOR
		[DllImport("SceneTrack", CallingConvention = CallingConvention.Cdecl, EntryPoint = "stIteratorGetValue_uint16"), SuppressUnmanagedCodeSecurity]
		public static extern ushort IteratorGetValue_uint16(uint iterator, uint element);
		#else
		public static ushort IteratorGetValue_uint16(uint iterator, uint element) { return default(ushort); }
		#endif

		#if UNITY_EDITOR
		[DllImport("SceneTrack", CallingConvention = CallingConvention.Cdecl, EntryPoint = "stIteratorGetValue_uint32"), SuppressUnmanagedCodeSecurity]
		public static extern uint IteratorGetValue_uint32(uint iterator, uint element);
		#else
		public static uint IteratorGetValue_uint32(uint iterator, uint element) { return default(uint); }
		#endif

		#if UNITY_EDITOR
		[DllImport("SceneTrack", CallingConvention = CallingConvention.Cdecl, EntryPoint = "stIteratorGetValue_uint64"), SuppressUnmanagedCodeSecurity]
		public static extern ulong IteratorGetValue_uint64(uint iterator, uint element);
		#else
		public static ulong IteratorGetValue_uint64(uint iterator, uint element) { return default(ulong); }
		#endif

		#if UNITY_EDITOR
		[DllImport("SceneTrack", CallingConvention = CallingConvention.Cdecl, EntryPoint = "stIteratorGetValue_int8"), SuppressUnmanagedCodeSecurity]
		public static extern sbyte IteratorGetValue_int8(uint iterator, uint element);
		#else
		public static sbyte IteratorGetValue_int8(uint iterator, uint element) { return default(sbyte); }
		#endif

		#if UNITY_EDITOR
		[DllImport("SceneTrack", CallingConvention = CallingConvention.Cdecl, EntryPoint = "stIteratorGetValue_int16"), SuppressUnmanagedCodeSecurity]
		public static extern short IteratorGetValue_int16(uint iterator, uint element);
		#else
		public static short IteratorGetValue_int16(uint iterator, uint element) { return default(short); }
		#endif

		#if UNITY_EDITOR
		[DllImport("SceneTrack", CallingConvention = CallingConvention.Cdecl, EntryPoint = "stIteratorGetValue_int32"), SuppressUnmanagedCodeSecurity]
		public static extern int IteratorGetValue_int32(uint iterator, uint element);
		#else
		public static int IteratorGetValue_int32(uint iterator, uint element) { return default(int); }
		#endif

		#if UNITY_EDITOR
		[DllImport("SceneTrack", CallingConvention = CallingConvention.Cdecl, EntryPoint = "stIteratorGetValue_int64"), SuppressUnmanagedCodeSecurity]
		public static extern long IteratorGetValue_int64(uint iterator, uint element);
		#else
		public static long IteratorGetValue_int64(uint iterator, uint element) { return default(long); }
		#endif

		#if UNITY_EDITOR
		[DllImport("SceneTrack", CallingConvention = CallingConvention.Cdecl, EntryPoint = "stIteratorGetValue_float32"), SuppressUnmanagedCodeSecurity]
		public static extern float IteratorGetValue_float32(uint iterator, uint element);
		#else
		public static float IteratorGetValue_float32(uint iterator, uint element) { return default(float); }
		#endif

		#if UNITY_EDITOR
		[DllImport("SceneTrack", CallingConvention = CallingConvention.Cdecl, EntryPoint = "stIteratorGetValue_float64"), SuppressUnmanagedCodeSecurity]
		public static extern double IteratorGetValue_float64(uint iterator, uint element);
		#else
		public static double IteratorGetValue_float64(uint iterator, uint element) { return default(double); }
		#endif

		#if UNITY_EDITOR
		[DllImport("SceneTrack", CallingConvention = CallingConvention.Cdecl, EntryPoint = "stIteratorGetValue_stringLength"), SuppressUnmanagedCodeSecurity]
		public static extern int IteratorGetValue_stringLength(uint iterator);
		#else
		public static int IteratorGetValue_stringLength(uint iterator) { return default(int); }
		#endif

		#if UNITY_EDITOR
		[DllImport("SceneTrack", CallingConvention = CallingConvention.Cdecl, EntryPoint = "stIteratorGetValue_string"), SuppressUnmanagedCodeSecurity]
		public static extern int IteratorGetValue_string(uint iterator, StringBuilder out_String, uint strCapacity);
		#else
		public static int IteratorGetValue_string(uint iterator, StringBuilder out_String, uint strCapacity) { return default(int); }
		#endif

		#if UNITY_EDITOR
		[DllImport("SceneTrack", CallingConvention = CallingConvention.Cdecl, EntryPoint = "stIteratorGetValueArray"), SuppressUnmanagedCodeSecurity]
		public static extern uint IteratorGetValueArray(uint iterator, IntPtr out_targetArray, uint targetArraySizeInBytes, uint stride);
		#else
		public static uint IteratorGetValueArray(uint iterator, IntPtr out_targetArray, uint targetArraySizeInBytes, uint stride) { return default(uint); }
		#endif

	}
	public static class Recording
	{

		#if UNITY_EDITOR
		[DllImport("SceneTrack", CallingConvention = CallingConvention.Cdecl, EntryPoint = "stSubmit"), SuppressUnmanagedCodeSecurity]
		public static extern void Submit(float frameLength);
		#else
		public static void Submit(float frameLength) {}
		#endif

		#if UNITY_EDITOR
		[DllImport("SceneTrack", CallingConvention = CallingConvention.Cdecl, EntryPoint = "stGetNumRecordedFrames"), SuppressUnmanagedCodeSecurity]
		public static extern uint GetNumRecordedFrames();
		#else
		public static uint GetNumRecordedFrames() { return default(uint); }
		#endif

		#if UNITY_EDITOR
		[DllImport("SceneTrack", CallingConvention = CallingConvention.Cdecl, EntryPoint = "stGetNumFrames"), SuppressUnmanagedCodeSecurity]
		public static extern uint GetNumFrames();
		#else
		public static uint GetNumFrames() { return default(uint); }
		#endif

		#if UNITY_EDITOR
		[DllImport("SceneTrack", CallingConvention = CallingConvention.Cdecl, EntryPoint = "stGetRecordingTime"), SuppressUnmanagedCodeSecurity]
		public static extern double GetRecordingTime();
		#else
		public static double GetRecordingTime() { return default(double); }
		#endif

		#if UNITY_EDITOR
		[DllImport("SceneTrack", CallingConvention = CallingConvention.Cdecl, EntryPoint = "stGetFirstFrame"), SuppressUnmanagedCodeSecurity]
		public static extern uint GetFirstFrame();
		#else
		public static uint GetFirstFrame() { return default(uint); }
		#endif

		#if UNITY_EDITOR
		[DllImport("SceneTrack", CallingConvention = CallingConvention.Cdecl, EntryPoint = "stGetLastFrame"), SuppressUnmanagedCodeSecurity]
		public static extern uint GetLastFrame();
		#else
		public static uint GetLastFrame() { return default(uint); }
		#endif

		#if UNITY_EDITOR
		[DllImport("SceneTrack", CallingConvention = CallingConvention.Cdecl, EntryPoint = "stReorderFrames"), SuppressUnmanagedCodeSecurity]
		public static extern void ReorderFrames();
		#else
		public static void ReorderFrames() {}
		#endif

		#if UNITY_EDITOR
		[DllImport("SceneTrack", CallingConvention = CallingConvention.Cdecl, EntryPoint = "stGetError"), SuppressUnmanagedCodeSecurity]
		public static extern uint GetError();
		#else
		public static uint GetError() { return default(uint); }
		#endif

		#if UNITY_EDITOR
		[DllImport("SceneTrack", CallingConvention = CallingConvention.Cdecl, EntryPoint = "stCreateRecording"), SuppressUnmanagedCodeSecurity]
		public static extern uint CreateRecording();
		#else
		public static uint CreateRecording() { return default(uint); }
		#endif

		#if UNITY_EDITOR
		[DllImport("SceneTrack", CallingConvention = CallingConvention.Cdecl, EntryPoint = "stOpenRecording"), SuppressUnmanagedCodeSecurity]
		public static extern uint OpenRecording(StringBuilder path);
		#else
		public static uint OpenRecording(StringBuilder path) { return default(uint); }
		#endif

		#if UNITY_EDITOR
		[DllImport("SceneTrack", CallingConvention = CallingConvention.Cdecl, EntryPoint = "stCloseRecording"), SuppressUnmanagedCodeSecurity]
		public static extern void CloseRecording(uint recording);
		#else
		public static void CloseRecording(uint recording) {}
		#endif

		#if UNITY_EDITOR
		[DllImport("SceneTrack", CallingConvention = CallingConvention.Cdecl, EntryPoint = "stChangeActiveRecording"), SuppressUnmanagedCodeSecurity]
		public static extern void ChangeActiveRecording(uint recording);
		#else
		public static void ChangeActiveRecording(uint recording) {}
		#endif

		#if UNITY_EDITOR
		[DllImport("SceneTrack", CallingConvention = CallingConvention.Cdecl, EntryPoint = "stAppendSaveRecording"), SuppressUnmanagedCodeSecurity]
		public static extern void AppendSaveRecording(StringBuilder path, int format, uint frames);
		#else
		public static void AppendSaveRecording(StringBuilder path, int format, uint frames) {}
		#endif

		#if UNITY_EDITOR
		[DllImport("SceneTrack", CallingConvention = CallingConvention.Cdecl, EntryPoint = "stSaveRecording"), SuppressUnmanagedCodeSecurity]
		public static extern void SaveRecording(StringBuilder path, int format);
		#else
		public static void SaveRecording(StringBuilder path, int format) {}
		#endif

		#if UNITY_EDITOR
		[DllImport("SceneTrack", CallingConvention = CallingConvention.Cdecl, EntryPoint = "stMemoryReserve"), SuppressUnmanagedCodeSecurity]
		public static extern void MemoryReserve(int type, int value);
		#else
		public static void MemoryReserve(int type, int value) {}
		#endif

	}
}

