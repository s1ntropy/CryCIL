﻿using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

using CryEngine.Native;

namespace CryEngine.NativeMemory
{
	/// <summary>
	/// Defines methods that allow to handle native memory.
	/// </summary>
	public static class CryMarshal
	{
		private static SortedList<long, ulong> AllocatedClusters;
		/// <summary>
		/// Gets a size of native memory that has been allocated by this
		/// </summary>
		public static ulong AllocatedMemory { get; private set; }
		/// <summary>
		/// Allocate memory cluster of certain size.
		/// </summary>
		/// <param name="size">Number of bytes to allocate.</param>
		/// <returns>Pointer to first byte in the memory cluster.</returns>
		public static IntPtr AllocateMemory(ulong size)
		{
			if (size == 0)
			{
				throw new ArgumentOutOfRangeException
					("size", "Attempt to allocate a native memory cluster of 0 length.");
			}
			IntPtr pointer = NativeMemoryHandlingMethods.AllocateMemory(size);
			if (pointer == null)
			{
				throw new NativeMemoryException("CryMarshal.AllocateMemory: Unable to allocate memory.");
			}
			GC.AddMemoryPressure((long)size);
			AllocatedClusters.Add(pointer.ToInt64(), size);
			AllocatedMemory += size;
			return pointer;
		}
		/// <summary>
		/// Frees memory cluster.
		/// </summary>
		/// <remarks>
		/// This function won't release memory that has not been allocated by <see
		/// cref="CryMarshal.AllocateMemory" />.
		/// </remarks>
		/// <param name="pointer">Pointer to memory cluster to free.</param>
		public static void FreeMemory(IntPtr pointer)
		{
			if (AllocatedClusters.ContainsKey(pointer.ToInt64()))
			{
				NativeMemoryHandlingMethods.FreeMemory(pointer);
				ulong freedBytes = AllocatedClusters[pointer.ToInt64()];
				GC.RemoveMemoryPressure((long)freedBytes);
				AllocatedMemory -= freedBytes;
			}
		}
		#region Getting Data
		/// <summary>
		/// Gets a byte from native memory cluster.
		/// </summary>
		/// <param name="pointer">Pointer that points to the beginning of the cluster.</param>
		/// <param name="shift">
		/// A number that should be added to address, contained in the <paramref name="pointer" />
		/// to get address of the byte we need.
		/// </param>
		/// <returns>
		/// 1-byte long struct that can be interpreted as a signed or unsigned 8-bit integer.
		/// </returns>
		public static Byte1 GetByte(IntPtr pointer, ulong shift)
		{
			if (pointer.ToInt64() != 0)
			{
				return NativeMemoryHandlingMethods.GetByte(pointer, shift);
			}
			throw new NullPointerException("CryMarshal.GetByte: Attempt to access native memory with zero-pointer.");
		}
		/// <summary>
		/// Gets two bytes from native memory cluster.
		/// </summary>
		/// <param name="pointer">Pointer that points to the beginning of the cluster.</param>
		/// <param name="shift">
		/// A number that should be added to address, contained in the <paramref name="pointer" />
		/// to get address of the bytes we need.
		/// </param>
		/// <returns>
		/// 2-byte long struct that can be interpreted as a signed or unsigned 16-bit integer, a
		/// half-precision floating point number or as a Unicode character.
		/// </returns>
		public static Bytes2 Get2Bytes(IntPtr pointer, ulong shift)
		{
			if (pointer.ToInt64() != 0)
			{
				return NativeMemoryHandlingMethods.Get2Bytes(pointer, shift);
			}
			throw new NullPointerException("CryMarshal.Get2Bytes: Attempt to access native memory with zero-pointer.");
		}
		/// <summary>
		/// Gets four bytes from native memory cluster.
		/// </summary>
		/// <param name="pointer">Pointer that points to the beginning of the cluster.</param>
		/// <param name="shift">
		/// A number that should be added to address, contained in the <paramref name="pointer" />
		/// to get address of the bytes we need.
		/// </param>
		/// <returns>
		/// 4-byte long struct that can be interpreted as a signed or unsigned 32-bit integer or a
		/// single-precision floating point number.
		/// </returns>
		public static Bytes4 Get4Bytes(IntPtr pointer, ulong shift)
		{
			if (pointer.ToInt64() != 0)
			{
				return NativeMemoryHandlingMethods.Get4Bytes(pointer, shift);
			}
			throw new NullPointerException("CryMarshal.Get4Bytes: Attempt to access native memory with zero-pointer.");
		}
		/// <summary>
		/// Gets eight bytes from native memory cluster.
		/// </summary>
		/// <param name="pointer">Pointer that points to the beginning of the cluster.</param>
		/// <param name="shift">
		/// A number that should be added to address, contained in the <paramref name="pointer" />
		/// to get address of the bytes we need.
		/// </param>
		/// <returns>
		/// 8-byte long struct that can be interpreted as a signed or unsigned 64-bit integer or a
		/// double-precision floating point number.
		/// </returns>
		public static Bytes8 Get8Bytes(IntPtr pointer, ulong shift)
		{
			if (pointer.ToInt64() != 0)
			{
				return NativeMemoryHandlingMethods.Get8Bytes(pointer, shift);
			}
			throw new NullPointerException("CryMarshal.Get8Bytes: Attempt to access native memory with zero-pointer.");
		}
		/// <summary>
		/// Gets 32 bytes from native memory cluster.
		/// </summary>
		/// <param name="pointer">Pointer that points to the beginning of the cluster.</param>
		/// <param name="shift">
		/// A number that should be added to address, contained in the <paramref name="pointer" />
		/// to get address of the bytes we need.
		/// </param>
		/// <returns>
		/// 32-byte long struct that can be interpreted as an array of a number of basic types.
		/// </returns>
		public static Buffer32 Get32Bytes(IntPtr pointer, ulong shift)
		{
			if (pointer.ToInt64() != 0)
			{
				return NativeMemoryHandlingMethods.Get32Bytes(pointer, shift);
			}
			throw new NullPointerException("CryMarshal.Get32Bytes: Attempt to access native memory with zero-pointer.");
		}
		/// <summary>
		/// Gets 64 bytes from native memory cluster.
		/// </summary>
		/// <param name="pointer">Pointer that points to the beginning of the cluster.</param>
		/// <param name="shift">
		/// A number that should be added to address, contained in the <paramref name="pointer" />
		/// to get address of the bytes we need.
		/// </param>
		/// <returns>
		/// 64-byte long struct that can be interpreted as an array of a number of basic types.
		/// </returns>
		public static Buffer64 Get64Bytes(IntPtr pointer, ulong shift)
		{
			if (pointer.ToInt64() != 0)
			{
				return NativeMemoryHandlingMethods.Get64Bytes(pointer, shift);
			}
			throw new NullPointerException("CryMarshal.Get64Bytes: Attempt to access native memory with zero-pointer.");
		}
		/// <summary>
		/// Gets 128 bytes from native memory cluster.
		/// </summary>
		/// <param name="pointer">Pointer that points to the beginning of the cluster.</param>
		/// <param name="shift">
		/// A number that should be added to address, contained in the <paramref name="pointer" />
		/// to get address of the bytes we need.
		/// </param>
		/// <returns>
		/// 128-byte long struct that can be interpreted as an array of a number of basic types.
		/// </returns>
		public static Buffer128 Get128Bytes(IntPtr pointer, ulong shift)
		{
			if (pointer.ToInt64() != 0)
			{
				return NativeMemoryHandlingMethods.Get128Bytes(pointer, shift);
			}
			throw new NullPointerException("CryMarshal.Get128Bytes: Attempt to access native memory with zero-pointer.");
		}
		/// <summary>
		/// Gets 256 bytes from native memory cluster.
		/// </summary>
		/// <param name="pointer">Pointer that points to the beginning of the cluster.</param>
		/// <param name="shift">
		/// A number that should be added to address, contained in the <paramref name="pointer" />
		/// to get address of the bytes we need.
		/// </param>
		/// <returns>
		/// 256-byte long struct that can be interpreted as an array of a number of basic types.
		/// </returns>
		public static Buffer256 Get256Bytes(IntPtr pointer, ulong shift)
		{
			if (pointer.ToInt64() != 0)
			{
				return NativeMemoryHandlingMethods.Get256Bytes(pointer, shift);
			}
			throw new NullPointerException("CryMarshal.Get256Bytes: Attempt to access native memory with zero-pointer.");
		}
		/// <summary>
		/// Gets 512 bytes from native memory cluster.
		/// </summary>
		/// <param name="pointer">Pointer that points to the beginning of the cluster.</param>
		/// <param name="shift">
		/// A number that should be added to address, contained in the <paramref name="pointer" />
		/// to get address of the bytes we need.
		/// </param>
		/// <returns>
		/// 512-byte long struct that can be interpreted as an array of a number of basic types.
		/// </returns>
		public static Buffer512 Get512Bytes(IntPtr pointer, ulong shift)
		{
			if (pointer.ToInt64() != 0)
			{
				return NativeMemoryHandlingMethods.Get512Bytes(pointer, shift);
			}
			throw new NullPointerException("CryMarshal.Get512Bytes: Attempt to access native memory with zero-pointer.");
		}
		#endregion
		#region Setting Data
		/// <summary>
		/// Writes <see cref="Byte" /> value to the native memory.
		/// </summary>
		/// <param name="pointer">Pointer to beginning of native memory cluster.</param>
		/// <param name="value">Value to write.</param>
		/// <param name="shift">
		/// Address of the byte within the cluster to which to write the value to.
		/// </param>
		public static void Set(IntPtr pointer, byte value, ulong shift)
		{
			if (pointer.ToInt64() == 0)
			{
				return;
			}
			NativeMemoryHandlingMethods.SetByte(pointer, shift, value);
		}
		/// <summary>
		/// Writes <see cref="SByte" /> value to the native memory.
		/// </summary>
		/// <param name="pointer">Pointer to beginning of native memory cluster.</param>
		/// <param name="value">Value to write.</param>
		/// <param name="shift">
		/// Address of the byte within the cluster to which to write the value to.
		/// </param>
		public static void Set(IntPtr pointer, sbyte value, ulong shift)
		{
			if (pointer.ToInt64() == 0)
			{
				return;
			}
			Byte1 val = new Byte1
			{
				SignedByte = value
			};
			NativeMemoryHandlingMethods.SetByte(pointer, shift, val.UnsignedByte);
		}
		/// <summary>
		/// Writes <see cref="Char" /> value to the native memory.
		/// </summary>
		/// <param name="pointer">Pointer to beginning of native memory cluster.</param>
		/// <param name="value">Value to write.</param>
		/// <param name="shift">
		/// Address of the byte within the cluster to which to write the value to.
		/// </param>
		public static void Set(IntPtr pointer, char value, ulong shift)
		{
			if (pointer.ToInt64() == 0)
			{
				return;
			}
			Bytes2 val = new Bytes2
			{
				Character = value
			};
			NativeMemoryHandlingMethods.Set2Bytes(pointer, shift, val.UnsignedShort);
		}
		/// <summary>
		/// Writes <see cref="UInt16" /> value to the native memory.
		/// </summary>
		/// <param name="pointer">Pointer to beginning of native memory cluster.</param>
		/// <param name="value">Value to write.</param>
		/// <param name="shift">
		/// Address of the byte within the cluster to which to write the value to.
		/// </param>
		public static void Set(IntPtr pointer, ushort value, ulong shift)
		{
			if (pointer.ToInt64() == 0)
			{
				return;
			}
			NativeMemoryHandlingMethods.Set2Bytes(pointer, shift, value);
		}
		/// <summary>
		/// Writes <see cref="Int16" /> value to the native memory.
		/// </summary>
		/// <param name="pointer">Pointer to beginning of native memory cluster.</param>
		/// <param name="value">Value to write.</param>
		/// <param name="shift">
		/// Address of the byte within the cluster to which to write the value to.
		/// </param>
		public static void Set(IntPtr pointer, short value, ulong shift)
		{
			if (pointer.ToInt64() == 0)
			{
				return;
			}
			Bytes2 val = new Bytes2
			{
				SignedShort = value
			};
			NativeMemoryHandlingMethods.Set2Bytes(pointer, shift, val.UnsignedShort);
		}
		/// <summary>
		/// Writes <see cref="UInt32" /> value to the native memory.
		/// </summary>
		/// <param name="pointer">Pointer to beginning of native memory cluster.</param>
		/// <param name="value">Value to write.</param>
		/// <param name="shift">
		/// Address of the byte within the cluster to which to write the value to.
		/// </param>
		public static void Set(IntPtr pointer, uint value, ulong shift)
		{
			if (pointer.ToInt64() == 0)
			{
				return;
			}
			NativeMemoryHandlingMethods.Set4Bytes(pointer, shift, value);
		}
		/// <summary>
		/// Writes <see cref="Int32" /> value to the native memory.
		/// </summary>
		/// <param name="pointer">Pointer to beginning of native memory cluster.</param>
		/// <param name="value">Value to write.</param>
		/// <param name="shift">
		/// Address of the byte within the cluster to which to write the value to.
		/// </param>
		public static void Set(IntPtr pointer, int value, ulong shift)
		{
			if (pointer.ToInt64() == 0)
			{
				return;
			}
			Bytes4 val = new Bytes4
			{
				SignedInt = value
			};
			NativeMemoryHandlingMethods.Set4Bytes(pointer, shift, val.UnsignedInt);
		}
		/// <summary>
		/// Writes <see cref="Single" /> value to the native memory.
		/// </summary>
		/// <param name="pointer">Pointer to beginning of native memory cluster.</param>
		/// <param name="value">Value to write.</param>
		/// <param name="shift">
		/// Address of the byte within the cluster to which to write the value to.
		/// </param>
		public static void Set(IntPtr pointer, float value, ulong shift)
		{
			if (pointer.ToInt64() == 0)
			{
				return;
			}
			Bytes4 val = new Bytes4
			{
				SingleFloat = value
			};
			NativeMemoryHandlingMethods.Set4Bytes(pointer, shift, val.UnsignedInt);
		}
		/// <summary>
		/// Writes <see cref="UInt64" /> value to the native memory.
		/// </summary>
		/// <param name="pointer">Pointer to beginning of native memory cluster.</param>
		/// <param name="value">Value to write.</param>
		/// <param name="shift">
		/// Address of the byte within the cluster to which to write the value to.
		/// </param>
		public static void Set(IntPtr pointer, ulong value, ulong shift)
		{
			if (pointer.ToInt64() == 0)
			{
				return;
			}
			NativeMemoryHandlingMethods.Set8Bytes(pointer, shift, value);
		}
		/// <summary>
		/// Writes <see cref="Int64" /> value to the native memory.
		/// </summary>
		/// <param name="pointer">Pointer to beginning of native memory cluster.</param>
		/// <param name="value">Value to write.</param>
		/// <param name="shift">
		/// Address of the byte within the cluster to which to write the value to.
		/// </param>
		public static void Set(IntPtr pointer, long value, ulong shift)
		{
			if (pointer.ToInt64() == 0)
			{
				return;
			}
			Bytes8 val = new Bytes8
			{
				SignedLong = value
			};
			NativeMemoryHandlingMethods.Set8Bytes(pointer, shift, val.UnsignedLong);
		}
		/// <summary>
		/// Writes <see cref="Double" /> value to the native memory.
		/// </summary>
		/// <param name="pointer">Pointer to beginning of native memory cluster.</param>
		/// <param name="value">Value to write.</param>
		/// <param name="shift">
		/// Address of the byte within the cluster to which to write the value to.
		/// </param>
		public static void Set(IntPtr pointer, double value, ulong shift)
		{
			if (pointer.ToInt64() == 0)
			{
				return;
			}
			Bytes8 val = new Bytes8
			{
				DoubleFloat = value
			};
			NativeMemoryHandlingMethods.Set8Bytes(pointer, shift, val.UnsignedLong);
		}
		/// <summary>
		/// Writes 32-byte long buffer to native memory.
		/// </summary>
		/// <param name="pointer">Pointer to beginning of native memory cluster.</param>
		/// <param name="buffer">Data to write.</param>
		/// <param name="shift">
		/// Address of the byte within the cluster to which to write the data to.
		/// </param>
		public static void Set(IntPtr pointer, ref Buffer32 buffer, ulong shift)
		{
			if (pointer.ToInt64() == 0)
			{
				return;
			}
			NativeMemoryHandlingMethods.Set32Bytes(pointer, shift, buffer);
		}
		/// <summary>
		/// Writes 64-byte long buffer to native memory.
		/// </summary>
		/// <param name="pointer">Pointer to beginning of native memory cluster.</param>
		/// <param name="buffer">Data to write.</param>
		/// <param name="shift">
		/// Address of the byte within the cluster to which to write the data to.
		/// </param>
		public static void Set(IntPtr pointer, ref Buffer64 buffer, ulong shift)
		{
			if (pointer.ToInt64() == 0)
			{
				return;
			}
			NativeMemoryHandlingMethods.Set64Bytes(pointer, shift, buffer);
		}
		/// <summary>
		/// Writes 128-byte long buffer to native memory.
		/// </summary>
		/// <param name="pointer">Pointer to beginning of native memory cluster.</param>
		/// <param name="buffer">Data to write.</param>
		/// <param name="shift">
		/// Address of the byte within the cluster to which to write the data to.
		/// </param>
		public static void Set(IntPtr pointer, ref Buffer128 buffer, ulong shift)
		{
			if (pointer.ToInt64() == 0)
			{
				return;
			}
			NativeMemoryHandlingMethods.Set128Bytes(pointer, shift, buffer);
		}
		/// <summary>
		/// Writes 256-byte long buffer to native memory.
		/// </summary>
		/// <param name="pointer">Pointer to beginning of native memory cluster.</param>
		/// <param name="buffer">Data to write.</param>
		/// <param name="shift">
		/// Address of the byte within the cluster to which to write the data to.
		/// </param>
		public static void Set(IntPtr pointer, ref Buffer256 buffer, ulong shift)
		{
			if (pointer.ToInt64() == 0)
			{
				return;
			}
			NativeMemoryHandlingMethods.Set256Bytes(pointer, shift, buffer);
		}
		/// <summary>
		/// Writes 512-byte long buffer to native memory.
		/// </summary>
		/// <param name="pointer">Pointer to beginning of native memory cluster.</param>
		/// <param name="buffer">Data to write.</param>
		/// <param name="shift">
		/// Address of the byte within the cluster to which to write the data to.
		/// </param>
		public static void Set(IntPtr pointer, ref Buffer512 buffer, ulong shift)
		{
			if (pointer.ToInt64() == 0)
			{
				return;
			}
			NativeMemoryHandlingMethods.Set512Bytes(pointer, shift, buffer);
		}
		/// <summary>
		/// Writes a buffer to native memory.
		/// </summary>
		/// <param name="pointer">Pointer to beginning of native memory cluster.</param>
		/// <param name="buffer">Data to write.</param>
		/// <param name="shift">
		/// Address of the byte within the cluster to which to write the data to.
		/// </param>
		public static void Set(IntPtr pointer, IBuffer buffer, ulong shift)
		{
			if (pointer == IntPtr.Zero)
			{
				return;
			}
			switch (buffer.Length)
			{
				case 1:
					NativeMemoryHandlingMethods.SetByte(pointer, shift, ((Byte1)buffer).UnsignedByte);
					break;
				case 2:
					NativeMemoryHandlingMethods.Set2Bytes(pointer, shift, ((Bytes2)buffer).UnsignedShort);
					break;
				case 4:
					NativeMemoryHandlingMethods.Set4Bytes(pointer, shift, ((Bytes4)buffer).UnsignedInt);
					break;
				case 8:
					NativeMemoryHandlingMethods.Set8Bytes(pointer, shift, ((Bytes8)buffer).UnsignedLong);
					break;
				case 32:
					NativeMemoryHandlingMethods.Set32Bytes(pointer, shift, (Buffer32)buffer);
					break;
				case 64:
					NativeMemoryHandlingMethods.Set64Bytes(pointer, shift, (Buffer64)buffer);
					break;
				case 128:
					NativeMemoryHandlingMethods.Set128Bytes(pointer, shift, (Buffer128)buffer);
					break;
				case 256:
					NativeMemoryHandlingMethods.Set256Bytes(pointer, shift, (Buffer256)buffer);
					break;
				case 512:
					NativeMemoryHandlingMethods.Set512Bytes(pointer, shift, (Buffer512)buffer);
					break;
			}
		}
		#endregion
	}
	/// <summary>
	/// Represents exception that is thrown whenever there is a problem with native memory.
	/// </summary>
	[SerializableAttribute]
	public class NativeMemoryException : Exception
	{
		/// <summary>
		/// Creates a default instance of <see cref="NativeMemoryException" /> class.
		/// </summary>
		public NativeMemoryException()
		{
		}
		/// <summary>
		/// Creates a new instance of <see cref="NativeMemoryException" /> class with specified message.
		/// </summary>
		/// <param name="message">Message to supply with exception.</param>
		public NativeMemoryException(string message)
			: base(message)
		{
		}
		/// <summary>
		/// Creates a new instance of <see cref="NativeMemoryException" /> class with specified
		/// message and exception object that caused new one to be created.
		/// </summary>
		/// <param name="message">Message to supply with exception.</param>
		/// <param name="inner">Exception that caused a new one to be created.</param>
		public NativeMemoryException(string message, Exception inner)
			: base(message, inner)
		{
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="NativeMemoryException" /> class with
		/// serialized data.
		/// </summary>
		/// <param name="info">The object that holds the serialized object data.</param>
		/// <param name="context">The contextual information about the source or destination.</param>
		protected NativeMemoryException(
			System.Runtime.Serialization.SerializationInfo info,
			System.Runtime.Serialization.StreamingContext context)
			: base(info, context)
		{
		}
	}
}