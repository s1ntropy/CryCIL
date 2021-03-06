﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.CompilerServices;

namespace CryEngine.Native
{
	/// <summary>
	/// Defines native interop methods for working with static objects.
	/// </summary>
	internal static class StaticObjectInterop
	{
		/// <summary>
		/// Called to create a new empty static object.
		/// </summary>
		/// <returns>Pointer to fresh static object in native environment.</returns>
		[MethodImpl(MethodImplOptions.InternalCall)]
		extern internal static IntPtr CreateStaticObject();
		/// <summary>
		/// Called to decrement reference counter on a static object, when it is disposed
		/// of in Mono environment.
		/// </summary>
		/// <param name="handle">Pointer to static object in native environment.</param>
		[MethodImpl(MethodImplOptions.InternalCall)]
		extern internal static void ReleaseStaticObject(IntPtr handle);
		[MethodImpl(MethodImplOptions.InternalCall)]
		extern internal static MeshHandles GetMeshHandles(IntPtr handle);
	}
	internal struct MeshHandles
	{
		internal IntPtr IndexedMeshHandle;
		internal IntPtr MeshHandle;
	}
}