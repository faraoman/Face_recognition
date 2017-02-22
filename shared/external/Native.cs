//#define NO_AGGRESSIVE_INLINING
//#define ALLOW_UNSAFE
namespace Native
{
	using System;
	using System.Diagnostics.CodeAnalysis;
	using System.IO;
	using System.Runtime.CompilerServices;
	using System.Runtime.InteropServices;
	using System.Security;
	using System.Text;

	using Mallenom;

	using Microsoft.Win32.SafeHandles;

	/// <summary>Константы для native-функций.</summary>
	static partial class Constants
	{
		/// <summary>Invalid handle value.</summary>
		public static readonly IntPtr INVALID_HANDLE_VALUE = new IntPtr(-1);

		/// <summary>
		/// Places the window at the bottom of the Z order.
		/// If the hWnd parameter identifies a topmost window, the window loses its topmost status and is placed at the bottom of all other windows.
		/// </summary>
		public static readonly IntPtr HWND_BOTTOM = (IntPtr)1;

		/// <summary>
		/// Places the window above all non-topmost windows (that is, behind all topmost windows).
		/// This flag has no effect if the window is already a non-topmost window.
		/// </summary>
		public static readonly IntPtr HWND_NOTOPMOST = (IntPtr)(-2);

		/// <summary>Places the window at the top of the Z order.</summary>
		public static readonly IntPtr HWND_TOP = (IntPtr)(0);

		/// <summary>Places the window above all non-topmost windows. The window maintains its topmost position even when it is deactivated.</summary>
		public static readonly IntPtr HWND_TOPMOST = (IntPtr)(-1);

		public const uint INFINITE = 0xFFFFFFFF;
		public const uint WAIT_ABANDONED_0 = 0x00000080;
		public const uint WAIT_OBJECT_0 = 0x00000000;
		public const uint WAIT_TIMEOUT = 0x00000102;
		public const uint WAIT_FAILED = 0xFFFFFFFF;
		public const int MAXIMUM_WAIT_OBJECTS = 64;

		#region File Share Mode

		/// <summary>
		/// Prevents other processes from opening a file or device if they request delete, read, or write access.
		/// </summary>
		public const uint FILE_SHARE_NONE = 0x00000000;
		/// <summary>
		/// Enables subsequent open operations on a file or device to request read access.
		/// Otherwise, other processes cannot open the file or device if they request read access.
		/// If this flag is not specified, but the file or device has been opened for read access, the function fails.
		/// </summary>
		public const uint FILE_SHARE_READ = 0x00000001;
		/// <summary>
		/// Enables subsequent open operations on a file or device to request write access.
		/// Otherwise, other processes cannot open the file or device if they request write access.
		/// If this flag is not specified, but the file or device has been opened for write access or has a file mapping with write access, the function fails.
		/// </summary>
		public const uint FILE_SHARE_WRITE = 0x00000002;
		/// <summary>
		/// Enables subsequent open operations on a file or device to request delete access.
		/// Otherwise, other processes cannot open the file or device if they request delete access.
		/// If this flag is not specified, but the file or device has been opened for delete access, the function fails.
		/// </summary>
		/// <remarks>Delete access allows both delete and rename operations.</remarks>
		public const uint FILE_SHARE_DELETE = 0x00000004;

		#endregion

		#region File Create Mode

		/// <summary>
		/// Creates a new file, always.
		/// If the specified file exists and is writable, the function overwrites the file, the function succeeds, and last-error code is set to ERROR_ALREADY_EXISTS (183).
		/// If the specified file does not exist and is a valid path, a new file is created, the function succeeds, and the last-error code is set to zero.
		/// </summary>
		public const uint CREATE_ALWAYS = 2;
		/// <summary>
		/// Creates a new file, only if it does not already exist.
		/// If the specified file exists, the function fails and the last-error code is set to ERROR_FILE_EXISTS (80).
		/// If the specified file does not exist and is a valid path to a writable location, a new file is created.
		/// </summary>
		public const uint CREATE_NEW = 1;
		/// <summary>
		/// Opens a file, always.
		/// If the specified file exists, the function succeeds and the last-error code is set to ERROR_ALREADY_EXISTS (183).
		/// If the specified file does not exist and is a valid path to a writable location, the function creates a file and the last-error code is set to zero.
		/// </summary>
		public const uint OPEN_ALWAYS = 4;
		/// <summary>
		/// Opens a file or device, only if it exists.
		/// If the specified file or device does not exist, the function fails and the last-error code is set to ERROR_FILE_NOT_FOUND (2).
		/// </summary>
		public const uint OPEN_EXISTING = 3;
		/// <summary>
		/// Opens a file and truncates it so that its size is zero bytes, only if it exists.
		/// If the specified file does not exist, the function fails and the last-error code is set to ERROR_FILE_NOT_FOUND (2).
		/// The calling process must open the file with the GENERIC_WRITE bit set as part of the dwDesiredAccess parameter.
		/// </summary>
		public const uint TRUNCATE_EXISTING = 5;

		#endregion

		#region File Attributes

		/// <summary>
		/// The file should be archived. Applications use this attribute to mark files for backup or removal.
		/// </summary>
		public const uint FILE_ATTRIBUTE_ARCHIVE = 0x20;
		/// <summary>
		/// The file or directory is encrypted. For a file, this means that all data in the file is encrypted.
		/// For a directory, this means that encryption is the default for newly created files and subdirectories.
		/// This flag has no effect if <see cref="FILE_ATTRIBUTE_SYSTEM"/> is also specified.
		/// This flag is not supported on Home, Home Premium, Starter, or ARM editions of Windows.
		/// </summary>
		public const uint FILE_ATTRIBUTE_ENCRYPTED = 0x4000;
		/// <summary>
		/// The file is hidden. Do not include it in an ordinary directory listing.
		/// </summary>
		public const uint FILE_ATTRIBUTE_HIDDEN = 0x2;
		/// <summary>
		/// The file does not have other attributes set. This attribute is valid only if used alone.
		/// </summary>
		public const uint FILE_ATTRIBUTE_NORMAL = 0x80;
		/// <summary>
		/// The data of a file is not immediately available.
		/// This attribute indicates that file data is physically moved to offline storage.
		/// This attribute is used by Remote Storage, the hierarchical storage management software.
		/// Applications should not arbitrarily change this attribute.
		/// </summary>
		public const uint FILE_ATTRIBUTE_OFFLINE = 0x1000;
		/// <summary>
		/// The file is read only. Applications can read the file, but cannot write to or delete it.
		/// </summary>
		public const uint FILE_ATTRIBUTE_READONLY = 0x1;
		/// <summary>
		/// The file is part of or used exclusively by an operating system.
		/// </summary>
		public const uint FILE_ATTRIBUTE_SYSTEM = 0x4;
		/// <summary>
		/// The file is being used for temporary storage.
		/// </summary>
		public const uint FILE_ATTRIBUTE_TEMPORARY = 0x100;

		#endregion

		#region File Flags

		/// <summary>
		/// The file is being opened or created for a backup or restore operation.
		/// The system ensures that the calling process overrides file security checks when the process has SE_BACKUP_NAME and SE_RESTORE_NAME privileges.
		/// You must set this flag to obtain a handle to a directory.
		/// A directory handle can be passed to some functions instead of a file handle.
		/// </summary>
		public const uint FILE_FLAG_BACKUP_SEMANTICS = 0x02000000;
		/// <summary>
		/// The file is to be deleted immediately after all of its handles are closed, which includes the specified handle and any other open or duplicated handles.
		/// If there are existing open handles to a file, the call fails unless they were all opened with the <see cref="FILE_SHARE_DELETE"/> share mode.
		/// Subsequent open requests for the file fail, unless the FILE_SHARE_DELETE share mode is specified.
		/// </summary>
		public const uint FILE_FLAG_DELETE_ON_CLOSE = 0x04000000;
		/// <summary>
		/// The file or device is being opened with no system caching for data reads and writes.
		/// This flag does not affect hard disk caching or memory mapped files.
		/// </summary>
		public const uint FILE_FLAG_NO_BUFFERING = 0x20000000;
		/// <summary>
		/// The file data is requested, but it should continue to be located in remote storage.
		/// It should not be transported back to local storage.
		/// This flag is for use by remote storage systems.
		/// </summary>
		public const uint FILE_FLAG_OPEN_NO_RECALL = 0x00100000;
		/// <summary>
		/// Normal reparse point processing will not occur;
		/// CreateFile will attempt to open the reparse point. When a file is opened, a file handle is returned,
		/// whether or not the filter that controls the reparse point is operational.
		/// This flag cannot be used with the <see cref="CREATE_ALWAYS"/> flag.
		/// If the file is not a reparse point, then this flag is ignored.
		/// </summary>
		public const uint FILE_FLAG_OPEN_REPARSE_POINT = 0x00200000;
		/// <summary>
		/// The file or device is being opened or created for asynchronous I/O.
		/// When subsequent I/O operations are completed on this handle, the event specified in the OVERLAPPED structure will be set to the signaled state.
		/// If this flag is specified, the file can be used for simultaneous read and write operations.
		/// If this flag is not specified, then I/O operations are serialized, even if the calls to the read and write functions specify an OVERLAPPED structure.
		/// </summary>
		public const uint FILE_FLAG_OVERLAPPED = 0x40000000;
		/// <summary>
		/// Access will occur according to POSIX rules.
		/// This includes allowing multiple files with names, differing only in case, for file systems that support that naming.
		/// Use care when using this option, because files created with this flag may not be accessible by applications that are written for MS-DOS or 16-bit Windows.
		/// </summary>
		public const uint FILE_FLAG_POSIX_SEMANTICS = 0x0100000;
		/// <summary>
		/// Access is intended to be random. The system can use this as a hint to optimize file caching.
		/// This flag has no effect if the file system does not support cached I/O and <see cref="FILE_FLAG_NO_BUFFERING"/>.
		/// </summary>
		public const uint FILE_FLAG_RANDOM_ACCESS = 0x10000000;
		/// <summary>
		/// The file or device is being opened with session awareness.
		/// If this flag is not specified, then per-session devices (such as a redirected USB device) cannot be opened by processes running in session 0.
		/// This flag has no effect for callers not in session 0.
		/// This flag is supported only on server editions of Windows.
		/// </summary>
		public const uint FILE_FLAG_SESSION_AWARE = 0x00800000;
		/// <summary>
		/// Access is intended to be sequential from beginning to end. The system can use this as a hint to optimize file caching.
		/// This flag should not be used if read-behind (that is, reverse scans) will be used.
		/// This flag has no effect if the file system does not support cached I/O and <see cref="FILE_FLAG_NO_BUFFERING"/>.
		/// </summary>
		public const uint FILE_FLAG_SEQUENTIAL_SCAN = 0x08000000;
		/// <summary>
		/// Write operations will not go through any intermediate cache, they will go directly to disk.
		/// </summary>
		public const uint FILE_FLAG_WRITE_THROUGH = 0x80000000;

		#endregion

		#region File Access Flags

		/// <summary>Read access.</summary>
		public const uint GENERIC_READ = 0x80000000;
		/// <summary>Write access.</summary>
		public const uint GENERIC_WRITE = 0x40000000;
		/// <summary>Execute access.</summary>
		public const uint GENERIC_EXECUTE = 0x20000000;
		/// <summary>All possible access rights.</summary>
		public const uint GENERIC_ALL = 0x10000000;

		#endregion

		#region (Set)GetWindowLongPtr Special Indexes

		/// <summary>Extended window style.</summary>
		public const int GWL_EXSTYLE = -20;
		/// <summary>Application instance handle.</summary>
		public const int GWLP_HINSTANCE = -6;
		/// <summary>Identifier of the child window.</summary>
		public const int GWLP_ID = -12;
		/// <summary>Window style.</summary>
		public const int GWL_STYLE = -16;
		/// <summary>
		/// user data associated with the window.
		/// This data is intended for use by the application that created the window.
		/// Its value is initially zero.
		/// </summary>
		public const int GWLP_USERDATA = -21;
		/// <summary>Address for the window procedure.</summary>
		public const int GWLP_WNDPROC = -4;

		#endregion

		public const int MAX_PATH = 256;
	}

	static class Macro
	{
#if !NO_AGGRESSIVE_INLINING
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static int LOWORD(IntPtr lp)
		{
			return (int)(lp.ToInt64() & 0xffff);
		}

#if !NO_AGGRESSIVE_INLINING
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static int HIWORD(IntPtr lp)
		{
			return (int)((lp.ToInt64() >> 16) & 0xffff);
		}

#if !NO_AGGRESSIVE_INLINING
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static int GET_X_LPARAM(IntPtr lp)
		{
			return LOWORD(lp);
		}

#if !NO_AGGRESSIVE_INLINING
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static int GET_Y_LPARAM(IntPtr lp)
		{
			return HIWORD(lp);
		}

#if !NO_AGGRESSIVE_INLINING
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static bool IS_POINTER_FLAG_SET_WPARAM(IntPtr wParam, POINTER_MESSAGE_FLAG flag)
		{
			return (HIWORD(wParam) & (int)flag) == (int)flag;
		}

#if !NO_AGGRESSIVE_INLINING
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static int GET_POINTERID_WPARAM(IntPtr wParam)
		{
			return LOWORD(wParam);
		}

#if !NO_AGGRESSIVE_INLINING
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static bool IS_POINTER_NEW_WPARAM(IntPtr wParam)
		{
			return IS_POINTER_FLAG_SET_WPARAM(wParam, POINTER_MESSAGE_FLAG.NEW);
		}

#if !NO_AGGRESSIVE_INLINING
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static bool IS_POINTER_INRANGE_WPARAM(IntPtr wParam)
		{
			return IS_POINTER_FLAG_SET_WPARAM(wParam, POINTER_MESSAGE_FLAG.INRANGE);
		}

#if !NO_AGGRESSIVE_INLINING
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static bool IS_POINTER_INCONTACT_WPARAM(IntPtr wParam)
		{
			return IS_POINTER_FLAG_SET_WPARAM(wParam, POINTER_MESSAGE_FLAG.INCONTACT);
		}

#if !NO_AGGRESSIVE_INLINING
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static bool IS_POINTER_FIRSTBUTTON_WPARAM(IntPtr wParam)
		{
			return IS_POINTER_FLAG_SET_WPARAM(wParam, POINTER_MESSAGE_FLAG.FIRSTBUTTON);
		}

#if !NO_AGGRESSIVE_INLINING
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static bool IS_POINTER_SECONDBUTTON_WPARAM(IntPtr wParam)
		{
			return IS_POINTER_FLAG_SET_WPARAM(wParam, POINTER_MESSAGE_FLAG.SECONDBUTTON);
		}

#if !NO_AGGRESSIVE_INLINING
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static bool IS_POINTER_THIRDBUTTON_WPARAM(IntPtr wParam)
		{
			return IS_POINTER_FLAG_SET_WPARAM(wParam, POINTER_MESSAGE_FLAG.THIRDBUTTON);
		}

#if !NO_AGGRESSIVE_INLINING
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static bool IS_POINTER_FOURTHBUTTON_WPARAM(IntPtr wParam)
		{
			return IS_POINTER_FLAG_SET_WPARAM(wParam, POINTER_MESSAGE_FLAG.FOURTHBUTTON);
		}

#if !NO_AGGRESSIVE_INLINING
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static bool IS_POINTER_FIFTHBUTTON_WPARAM(IntPtr wParam)
		{
			return IS_POINTER_FLAG_SET_WPARAM(wParam, POINTER_MESSAGE_FLAG.FIFTHBUTTON);
		}

#if !NO_AGGRESSIVE_INLINING
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static bool IS_POINTER_PRIMARY_WPARAM(IntPtr wParam)
		{
			return IS_POINTER_FLAG_SET_WPARAM(wParam, POINTER_MESSAGE_FLAG.PRIMARY);
		}

#if !NO_AGGRESSIVE_INLINING
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static bool HAS_POINTER_CONFIDENCE_WPARAM(IntPtr wParam)
		{
			return IS_POINTER_FLAG_SET_WPARAM(wParam, POINTER_MESSAGE_FLAG.CONFIDENCE);
		}

#if !NO_AGGRESSIVE_INLINING
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static bool IS_POINTER_CANCELED_WPARAM(IntPtr wParam)
		{
			return IS_POINTER_FLAG_SET_WPARAM(wParam, POINTER_MESSAGE_FLAG.CANCELED);
		}
	}

	/// <summary>Функции библиотеки kernel32.dll.</summary>
	[SuppressUnmanagedCodeSecurity]
	static partial class Kernel32
	{
		/// <summary>Имя библиотеки.</summary>
		private const string LibName = "kernel32.dll";

		#region CopyMemory

		/// <summary>Точка входа функции CopyMemory.</summary>
		private const string CopyMemoryEntryPoint = "CopyMemory";

#if ALLOW_UNSAFE
		/// <summary>Copies a block of memory from one location to another.</summary>
		/// <param name="destination">A pointer to the starting address of the copied block's destination.</param>
		/// <param name="source">A pointer to the starting address of the block of memory to copy.</param>
		/// <param name="length">The size of the block of memory to copy, in bytes.</param>
		[DllImport(LibName, EntryPoint = CopyMemoryEntryPoint, SetLastError = false)]
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		[SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
		public static extern unsafe void CopyMemory([In] byte* destination, [In] byte* source, [In] UIntPtr length);
#endif

		/// <summary>Copies a block of memory from one location to another.</summary>
		/// <param name="destination">A pointer to the starting address of the copied block's destination.</param>
		/// <param name="source">A pointer to the starting address of the block of memory to copy.</param>
		/// <param name="length">The size of the block of memory to copy, in bytes.</param>
		[DllImport(LibName, EntryPoint = CopyMemoryEntryPoint, SetLastError = false)]
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		[SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
		public static extern void CopyMemory([In] IntPtr destination, [In] IntPtr source, [In] UIntPtr length);

#if ALLOW_UNSAFE
		/// <summary>Copies a block of memory from one location to another.</summary>
		/// <param name="destination">A pointer to the starting address of the copied block's destination.</param>
		/// <param name="source">A pointer to the starting address of the block of memory to copy.</param>
		/// <param name="length">The size of the block of memory to copy, in bytes.</param>
#if !NO_AGGRESSIVE_INLINING
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		public static unsafe void CopyMemory([In] byte* destination, [In] byte* source, [In] int length)
		{
			CopyMemory(destination, source, (UIntPtr)length);
		}
#endif

		/// <summary>Copies a block of memory from one location to another.</summary>
		/// <param name="destination">A pointer to the starting address of the copied block's destination.</param>
		/// <param name="source">A pointer to the starting address of the block of memory to copy.</param>
		/// <param name="length">The size of the block of memory to copy, in bytes.</param>
#if !NO_AGGRESSIVE_INLINING
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		[SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
		public static void CopyMemory(IntPtr destination, IntPtr source, int length)
		{
			CopyMemory(destination, source, (UIntPtr)length);
		}

		#endregion

		#region MoveMemory

		/// <summary>Точка входа функции CopyMemory.</summary>
		private const string MoveMemoryEntryPoint = "MoveMemory";

		/// <summary>Moves a block of memory from one location to another.</summary>
		/// <param name="destination">A pointer to the starting address of the move destination.</param>
		/// <param name="source">A pointer to the starting address of the block of memory to be moved.</param>
		/// <param name="length">The size of the block of memory to move, in bytes.</param>
		[DllImport(LibName, EntryPoint = MoveMemoryEntryPoint, SetLastError = false)]
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		[SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
		public static extern void MoveMemory([In] IntPtr destination, [In] IntPtr source, [In] UIntPtr length);

#if ALLOW_UNSAFE
		/// <summary>Moves a block of memory from one location to another.</summary>
		/// <param name="destination">A pointer to the starting address of the move destination.</param>
		/// <param name="source">A pointer to the starting address of the block of memory to be moved.</param>
		/// <param name="length">The size of the block of memory to move, in bytes.</param>
		[DllImport(LibName, EntryPoint = MoveMemoryEntryPoint, SetLastError = false)]
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		[SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
		public static extern unsafe void MoveMemory([In] byte* destination, [In] byte* source, [In] UIntPtr length);
#endif

		/// <summary>Moves a block of memory from one location to another.</summary>
		/// <param name="destination">A pointer to the starting address of the move destination.</param>
		/// <param name="source">A pointer to the starting address of the block of memory to be moved.</param>
		/// <param name="length">The size of the block of memory to move, in bytes.</param>
#if !NO_AGGRESSIVE_INLINING
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		public static void MoveMemory(IntPtr destination, IntPtr source, int length)
		{
			MoveMemory(destination, source, (UIntPtr)length);
		}

#if ALLOW_UNSAFE
		/// <summary>Moves a block of memory from one location to another.</summary>
		/// <param name="destination">A pointer to the starting address of the move destination.</param>
		/// <param name="source">A pointer to the starting address of the block of memory to be moved.</param>
		/// <param name="length">The size of the block of memory to move, in bytes.</param>
#if !NO_AGGRESSIVE_INLINING
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		public static unsafe void MoveMemory(byte* destination, byte* source, int length)
		{
			MoveMemory(destination, source, (UIntPtr)length);
		}
#endif

		#endregion

		#region CloseHandle

		/// <summary>Точка входа функции CloseHandle.</summary>
		private const string CloseHandleEntryPoint = "CloseHandle";

		/// <summary>Closes an open object handle.</summary>
		/// <param name="hObject">A valid handle to an open object.</param>
		/// <returns>
		/// If the function succeeds, the return value is <c>true</c>.
		/// If the function fails, the return value is <c>false</c>.
		/// </returns>
		/// <seealso>http://msdn.microsoft.com/en-us/library/windows/desktop/ms724211.aspx</seealso>
		[DllImport(LibName, EntryPoint = CloseHandleEntryPoint, SetLastError = true)]
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		[SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool CloseHandle([In] IntPtr hObject);

		#endregion

		#region MulDiv

		/// <summary>Точка входа функции MulDiv.</summary>
		private const string MulDivEntryPoint = "MulDiv";

		/// <summary>
		/// Multiplies two 32-bit values and then divides the 64-bit result by a third 32-bit value.
		/// The final result is rounded to the nearest integer.
		/// </summary>
		/// <param name="nNumber">The multiplicand.</param>
		/// <param name="nNumerator">The multiplier.</param>
		/// <param name="nDenominator">The number by which the result of the multiplication operation is to be divided.</param>
		/// <returns>
		/// If the function succeeds, the return value is the result of the multiplication and division, rounded to the nearest integer.
		/// If the result is a positive half integer (ends in .5), it is rounded up. If the result is a negative half integer, it is rounded down.
		/// If either an overflow occurred or nDenominator was 0, the return value is -1.
		/// </returns>
		[DllImport(LibName, EntryPoint = MulDivEntryPoint)]
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		[SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
		public static extern int MulDiv(int nNumber, int nNumerator, int nDenominator);

		#endregion

		#region CreateFile

		/// <summary>Точка входа функции CreateFile.</summary>
		private const string CreateFileEntryPoint = "CreateFileW";

		/// <summary>
		/// Creates or opens a file or I/O device.
		/// The most commonly used I/O devices are as follows:
		///		file, file stream, directory, physical disk, volume, console buffer, tape drive, communications resource, mailslot, and pipe.
		///	The function returns a handle that can be used to access the file or device for various types of I/O depending
		///	on the file or device and the flags and attributes specified.
		/// </summary>
		/// <param name="lpFileName">
		/// The name of the file or device to be created or opened.
		/// You may use either forward slashes (/) or backslashes (\) in this name.</param>
		/// <param name="dwDesiredAccess">
		/// The requested access to the file or device, which can be summarized as read, write, both or neither zero).
		/// </param>
		/// <param name="dwShareMode">
		/// The requested sharing mode of the file or device, which can be read, write, both, delete, all of these, or none (refer to the following table).
		/// Access requests to attributes or extended attributes are not affected by this flag.
		/// </param>
		/// <param name="lpSecurityAttributes">
		/// A pointer to a SECURITY_ATTRIBUTES structure that contains two separate but related data members:
		/// an optional security descriptor, and a Boolean value that determines whether the returned handle can be inherited by child processes.
		/// </param>
		/// <param name="dwCreationDisposition">
		/// An action to take on a file or device that exists or does not exist.
		/// </param>
		/// <param name="dwFlagsAndAttributes">
		/// The file or device attributes and flags, <see cref="Constants.FILE_ATTRIBUTE_NORMAL"/> being the most common default value for files.
		/// </param>
		/// <param name="hTemplateFile">
		/// A valid handle to a template file with the GENERIC_READ access right.
		/// The template file supplies file attributes and extended attributes for the file that is being created.
		/// </param>
		/// <returns>
		/// If the function succeeds, the return value is an open handle to the specified file, device, named pipe, or mail slot.
		/// If the function fails, the return value is <see cref="Constants.INVALID_HANDLE_VALUE"/>.
		/// </returns>
		[DllImport(LibName, ExactSpelling = true, SetLastError = true, EntryPoint = "CreateFileW")]
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		[SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
		public static extern IntPtr CreateFile(
			[MarshalAs(UnmanagedType.LPWStr)] [In] string lpFileName,
			[In] uint dwDesiredAccess,
			[In] uint dwShareMode,
			[In] IntPtr lpSecurityAttributes,
			[In] uint dwCreationDisposition,
			[In] uint dwFlagsAndAttributes,
			[In] IntPtr hTemplateFile);

		#endregion

		#region DeviceIOControl

		/// <summary>Точка входа функции DeviceIoControl.</summary>
		private const string DeviceIOControlEntryPoint = "DeviceIoControl";

		/// <summary>
		/// Sends a control code directly to a specified device driver, causing the corresponding device to perform the corresponding operation.
		/// </summary>
		/// <param name="hDevice">
		/// A handle to the device on which the operation is to be performed.
		/// The device is typically a volume, directory, file, or stream.
		/// To retrieve a device handle, use the <see cref="CreateFile"/> function.</param>
		/// <param name="dwIoControlCode">
		/// The control code for the operation. This value identifies the specific operation to be performed and the type of device on which to perform it.
		/// </param>
		/// <param name="lpInBuffer">
		/// A pointer to the input buffer that contains the data required to perform the operation.
		/// The format of this data depends on the value of the <paramref name="dwIoControlCode"/> parameter.
		/// This parameter can be NULL if <paramref name="dwIoControlCode"/> specifies an operation that does not require input data.
		/// </param>
		/// <param name="nInBufferSize">The size of the input buffer, in bytes.</param>
		/// <param name="lpOutBuffer">
		/// A pointer to the output buffer that is to receive the data returned by the operation.
		/// The format of this data depends on the value of the <paramref name="dwIoControlCode"/> parameter.
		/// This parameter can be NULL if <paramref name="dwIoControlCode"/> specifies an operation that does not return data.
		/// </param>
		/// <param name="nOutBufferSize">The size of the output buffer, in bytes.</param>
		/// <param name="lpBytesReturned">A pointer to a variable that receives the size of the data stored in the output buffer, in bytes.</param>
		/// <param name="lpOverlapped">
		/// A pointer to an OVERLAPPED structure.
		/// If hDevice was opened without specifying <see cref="Constants.FILE_FLAG_OVERLAPPED"/>, <paramref name="lpOverlapped"/> is ignored.
		/// </param>
		/// <returns>
		/// If the operation completes successfully, the return value is nonzero.
		/// If the operation fails or is pending, the return value is zero.
		/// </returns>
		/// <seealso>http://msdn.microsoft.com/en-us/library/windows/desktop/aa363216.aspx</seealso>
		[DllImport(LibName, EntryPoint = DeviceIOControlEntryPoint, ExactSpelling = true, SetLastError = true)]
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		[SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
		public static extern int DeviceIoControl(
			[In] IntPtr hDevice,
			[In] uint dwIoControlCode,
			[In] IntPtr lpInBuffer,
			[In] uint nInBufferSize,
			[In] IntPtr lpOutBuffer,
			[In] uint nOutBufferSize,
			[Out] out uint lpBytesReturned,
			[In] IntPtr lpOverlapped);

#if ALLOW_UNSAFE
		/// <summary>
		/// Sends a control code directly to a specified device driver, causing the corresponding device to perform the corresponding operation.
		/// </summary>
		/// <param name="hDevice">
		/// A handle to the device on which the operation is to be performed.
		/// The device is typically a volume, directory, file, or stream.
		/// To retrieve a device handle, use the <see cref="CreateFile"/> function.</param>
		/// <param name="dwIoControlCode">
		/// The control code for the operation. This value identifies the specific operation to be performed and the type of device on which to perform it.
		/// </param>
		/// <param name="lpInBuffer">
		/// A pointer to the input buffer that contains the data required to perform the operation.
		/// The format of this data depends on the value of the <paramref name="dwIoControlCode"/> parameter.
		/// This parameter can be NULL if <paramref name="dwIoControlCode"/> specifies an operation that does not require input data.
		/// </param>
		/// <param name="nInBufferSize">The size of the input buffer, in bytes.</param>
		/// <param name="lpOutBuffer">
		/// A pointer to the output buffer that is to receive the data returned by the operation.
		/// The format of this data depends on the value of the <paramref name="dwIoControlCode"/> parameter.
		/// This parameter can be NULL if <paramref name="dwIoControlCode"/> specifies an operation that does not return data.
		/// </param>
		/// <param name="nOutBufferSize">The size of the output buffer, in bytes.</param>
		/// <param name="lpBytesReturned">A pointer to a variable that receives the size of the data stored in the output buffer, in bytes.</param>
		/// <param name="lpOverlapped">
		/// A pointer to an OVERLAPPED structure.
		/// If hDevice was opened without specifying <see cref="Constants.FILE_FLAG_OVERLAPPED"/>, <paramref name="lpOverlapped"/> is ignored.
		/// </param>
		/// <returns>
		/// If the operation completes successfully, the return value is nonzero.
		/// If the operation fails or is pending, the return value is zero.
		/// </returns>
		/// <seealso>http://msdn.microsoft.com/en-us/library/windows/desktop/aa363216.aspx</seealso>
		[DllImport(LibName, EntryPoint = DeviceIOControlEntryPoint, ExactSpelling = true, SetLastError = true)]
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		[SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
		public static extern unsafe int DeviceIoControl(
			[In] IntPtr hDevice,
			[In] uint dwIoControlCode,
			[In] void* lpInBuffer,
			[In] uint nInBufferSize,
			[In] void* lpOutBuffer,
			[In] uint nOutBufferSize,
			[In] uint* lpBytesReturned,
			[In] void* lpOverlapped);
#endif

		#endregion

		#region GetProductInfo

		/// <summary>Точка входа функции GetProductInfo.</summary>
		private const string GetProductInfoEntryPoint = "GetProductInfo";

		/// <summary>
		/// Retrieves the product type for the operating system on the local computer, and maps the type to the product types supported by the specified operating system.
		/// To retrieve product type information on versions of Windows prior to the minimum supported operating systems specified in the Requirements section, use the <see cref="GetVersionEx(ref OSVERSIONINFO)"/> or <see cref="GetVersionEx(ref OSVERSIONINFOEX)"/> function. You can also use the OperatingSystemSKU property of the Win32_OperatingSystem WMI class.
		/// </summary>
		/// <param name="dwOSMajorVersion">
		/// The major version number of the operating system. The minimum value is 6.
		/// The combination of the dwOSMajorVersion, dwOSMinorVersion, dwSpMajorVersion, and dwSpMinorVersion parameters describes the maximum target operating system version for the application. For example, Windows Vista and Windows Server 2008 are version 6.0.0.0 and Windows 7 and Windows Server 2008 R2 are version 6.1.0.0.
		/// </param>
		/// <param name="dwOSMinorVersion">
		/// The minor version number of the operating system. The minimum value is 0.
		/// </param>
		/// <param name="dwSpMajorVersion">
		/// The major version number of the operating system service pack. The minimum value is 0.
		/// </param>
		/// <param name="dwSpMinorVersion">
		/// The minor version number of the operating system service pack. The minimum value is 0.
		/// </param>
		/// <param name="pdwReturnedProductType">
		/// The product type. This parameter cannot be NULL. If the specified operating system is less than the current operating system, this information is mapped to the types supported by the specified operating system. If the specified operating system is greater than the highest supported operating system, this information is mapped to the types supported by the current operating system.
		/// </param>
		/// <returns>
		/// If the function succeeds, the return value is a nonzero value.
		/// If the function fails, the return value is zero. This function fails if one of the input parameters is invalid.
		/// </returns>
		[DllImport(LibName, EntryPoint = GetProductInfoEntryPoint)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool GetProductInfo(
			[In] int dwOSMajorVersion,
			[In] int dwOSMinorVersion,
			[In] int dwSpMajorVersion,
			[In] int dwSpMinorVersion,
			[Out] out uint pdwReturnedProductType);

		#endregion

		#region GetVersionEx

		/// <summary>Точка входа функции GetProductInfo.</summary>
		private const string GetVersionExEntryPoint = "GetVersionEx";

		/// <summary>
		/// GetVersionEx may be altered or unavailable for releases after Windows 8.1. Instead, use the Version Helper functions.
		/// With the release of Windows 8.1, the behavior of the GetVersionEx API has changed in the value it will return for the operating system version.
		/// The value returned by the GetVersionEx function now depends on how the application is manifested.
		/// Applications not manifested for Windows 8.1 or Windows 10 will return the Windows 8 OS version value (6.2).
		/// Once an application is manifested for a given operating system version, GetVersionEx will always return the version that the application is manifested for in future releases.
		/// To manifest your applications for Windows 8.1 or Windows 10, refer to Targeting your application for Windows.
		/// </summary>
		/// <remarks>
		/// Identifying the current operating system is usually not the best way to determine whether a particular operating system feature is present.
		/// This is because the operating system may have had new features added in a redistributable DLL.
		/// Rather than using GetVersionEx to determine the operating system platform or version number, test for the presence of the feature itself.
		/// For more information, see Operating System Version.
		/// </remarks>
		/// <param name="lpVersionInfo">
		/// An <see cref="OSVERSIONINFO"/> structure that receives the operating system information.
		/// Before calling the GetVersionEx function, set the dwOSVersionInfoSize member of the structure as appropriate to indicate which data structure is being passed to this function.
		/// </param>
		/// <returns>
		/// If the function succeeds, the return value is a nonzero value.
		/// If the function fails, the return value is zero. To get extended error information, call GetLastError.
		/// The function fails if you specify an invalid value for the dwOSVersionInfoSize member of the <see cref="OSVERSIONINFO"/> structure.
		/// </returns>
		[DllImport(LibName, EntryPoint = GetVersionExEntryPoint)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool GetVersionEx(
			[In, Out] ref OSVERSIONINFO lpVersionInfo);

		/// <summary>
		/// GetVersionEx may be altered or unavailable for releases after Windows 8.1. Instead, use the Version Helper functions.
		/// With the release of Windows 8.1, the behavior of the GetVersionEx API has changed in the value it will return for the operating system version.
		/// The value returned by the GetVersionEx function now depends on how the application is manifested.
		/// Applications not manifested for Windows 8.1 or Windows 10 will return the Windows 8 OS version value (6.2).
		/// Once an application is manifested for a given operating system version, GetVersionEx will always return the version that the application is manifested for in future releases.
		/// To manifest your applications for Windows 8.1 or Windows 10, refer to Targeting your application for Windows.
		/// </summary>
		/// <remarks>
		/// Identifying the current operating system is usually not the best way to determine whether a particular operating system feature is present.
		/// This is because the operating system may have had new features added in a redistributable DLL.
		/// Rather than using GetVersionEx to determine the operating system platform or version number, test for the presence of the feature itself.
		/// For more information, see Operating System Version.
		/// </remarks>
		/// <param name="lpVersionInfo">
		/// An <see cref="OSVERSIONINFOEX"/> structure that receives the operating system information.
		/// Before calling the GetVersionEx function, set the dwOSVersionInfoSize member of the structure as appropriate to indicate which data structure is being passed to this function.
		/// </param>
		/// <returns>
		/// If the function succeeds, the return value is a nonzero value.
		/// If the function fails, the return value is zero. To get extended error information, call GetLastError.
		/// The function fails if you specify an invalid value for the dwOSVersionInfoSize member of the <see cref="OSVERSIONINFOEX"/> structure.
		/// </returns>
		[DllImport(LibName, EntryPoint = GetVersionExEntryPoint)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool GetVersionEx(
			[In, Out] ref OSVERSIONINFOEX lpVersionInfo);

		#endregion

		#region WaitForMultipleObjects

		/// <summary>Точка входа функции WaitForMultipleObjects.</summary>
		private const string WaitForMultipleObjectsEntryPoint = "WaitForMultipleObjects";

		/// <summary>
		/// Waits until one or all of the specified objects are in the signaled state or the time-out interval elapses.
		/// To enter an alertable wait state, use the <see cref="WaitForMultipleObjectsEx"/> function.
		/// </summary>
		/// <param name="nCount">
		/// The number of object handles in the array pointed to by lpHandles.
		/// The maximum number of object handles is <see cref="Constants.MAXIMUM_WAIT_OBJECTS"/>.
		/// This parameter cannot be zero.
		/// </param>
		/// <param name="lpHandles">
		/// An array of object handles. The array can contain handles to objects of different types.
		/// It may not contain multiple copies of the same handle.
		/// </param>
		/// <param name="bWaitAll">
		/// If this parameter is <c>true</c>, the function returns when the state of all objects in the lpHandles array is signaled.
		/// If <c>false</c>, the function returns when the state of any one of the objects is set to signaled.
		/// In the latter case, the return value indicates the object whose state caused the function to return.
		/// </param>
		/// <param name="dwMilliseconds">
		/// The time-out interval, in milliseconds.
		/// If a nonzero value is specified, the function waits until the specified objects are signaled or the interval elapses.
		/// If dwMilliseconds is zero, the function does not enter a wait state if the specified objects are not signaled; it always returns immediately.
		/// If dwMilliseconds is <see cref="Constants.INFINITE"/>, the function will return only when the specified objects are signaled.
		/// </param>
		/// <returns>
		/// If the function succeeds, the return value indicates the event that caused the function to return.
		/// </returns>
		/// <seealso>http://msdn.microsoft.com/en-us/library/windows/desktop/ms687025.aspx</seealso>
		[DllImport(LibName, EntryPoint = WaitForMultipleObjectsEntryPoint)]
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		[SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
		public static extern uint WaitForMultipleObjects(
			[In] int nCount,
			[In] IntPtr[] lpHandles,
			[MarshalAs(UnmanagedType.Bool)] [In] bool bWaitAll,
			[In] uint dwMilliseconds);

		#endregion

		#region WaitForMultipleObjectsEx

		/// <summary>Точка входа функции WaitForMultipleObjectsEx.</summary>
		private const string WaitForMultipleObjectsExEntryPoint = "WaitForMultipleObjectsEx";

		/// <summary>
		/// Waits until one or all of the specified objects are in the signaled state, an I/O completion routine
		/// or asynchronous procedure call (APC) is queued to the thread, or the time-out interval elapses.
		/// </summary>
		/// <param name="nCount">
		/// The number of object handles in the array pointed to by lpHandles.
		/// The maximum number of object handles is <see cref="Constants.MAXIMUM_WAIT_OBJECTS"/>.
		/// This parameter cannot be zero.
		/// </param>
		/// <param name="lpHandles">
		/// An array of object handles. The array can contain handles to objects of different types.
		/// It may not contain multiple copies of the same handle.
		/// </param>
		/// <param name="bWaitAll">
		/// If this parameter is <c>true</c>, the function returns when the state of all objects in the lpHandles array is signaled.
		/// If <c>false</c>, the function returns when the state of any one of the objects is set to signaled.
		/// In the latter case, the return value indicates the object whose state caused the function to return.
		/// </param>
		/// <param name="dwMilliseconds">
		/// The time-out interval, in milliseconds.
		/// If a nonzero value is specified, the function waits until the specified objects are signaled or the interval elapses.
		/// If dwMilliseconds is zero, the function does not enter a wait state if the specified objects are not signaled; it always returns immediately.
		/// If dwMilliseconds is <see cref="Constants.INFINITE"/>, the function will return only when the specified objects are signaled.
		/// </param>
		/// <param name="bAlertable">
		/// If this parameter is TRUE and the thread is in the waiting state, the function returns when the
		/// system queues an I/O completion routine or APC, and the thread runs the routine or function.
		/// Otherwise, the function does not return and the completion routine or APC function is not executed.
		/// </param>
		/// <returns>
		/// If the function succeeds, the return value indicates the event that caused the function to return.
		/// </returns>
		/// <seealso>http://msdn.microsoft.com/en-us/library/windows/desktop/ms687028.aspx</seealso>
		[DllImport(LibName, EntryPoint = WaitForMultipleObjectsEntryPoint)]
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		[SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
		public static extern uint WaitForMultipleObjectsEx(
			[In] int nCount,
			[In] IntPtr[] lpHandles,
			[MarshalAs(UnmanagedType.Bool)] [In] bool bWaitAll,
			[In] uint dwMilliseconds,
			[MarshalAs(UnmanagedType.Bool)] [In] bool bAlertable);

		#endregion

		#region GetShortPathName

		/// <summary>Retrieves the short path form of the specified path.</summary>
		/// <param name="lpszLongPath">
		/// The path string.
		/// In the ANSI version of this function, the name is limited to MAX_PATH characters.
		/// To extend this limit to 32,767 wide characters, call the Unicode version of the function and prepend "\\?\" to the path.
		/// </param>
		/// <param name="lpszShortPath">
		/// A pointer to a buffer to receive the null-terminated short form of the path that <paramref name="lpszLongPath"/> specifies.
		/// Passing NULL for this parameter and zero for cchBuffer will always return the required buffer size for a specified <paramref name="lpszLongPath"/>.
		/// </param>
		/// <param name="cchBuffer">
		/// The size of the buffer that <paramref name="lpszShortPath"/> points to, in chars.
		/// Set this parameter to zero if <paramref name="lpszShortPath"/> is set to <c>null</c>.
		/// </param>
		/// <returns>
		/// If the function succeeds, the return value is the length, in TCHARs, of the string that is copied to <paramref name="lpszShortPath"/>, not including the terminating null character.
		/// If the lpszShortPath buffer is too small to contain the path, the return value is the size of the buffer, in TCHARs, that is required to hold the path and the terminating null character.
		/// If the function fails for any other reason, the return value is zero. To get extended error information, call GetLastError().
		/// </returns>
		[DllImport(LibName, CharSet = CharSet.Unicode, EntryPoint = "GetShortPathNameW")]
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		[SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
		public static extern int GetShortPathName([In] string lpszLongPath, char[] lpszShortPath, [In] int cchBuffer);

		#endregion

		#region GetProcAddress

		/// <summary>Retrieves the address of an exported function or variable from the specified dynamic-link library (DLL).</summary>
		/// <param name="hModule">
		/// A handle to the DLL module that contains the function or variable. The LoadLibrary, LoadLibraryEx, LoadPackagedLibrary, or GetModuleHandle function returns this handle.
		/// The <see cref="M:GetProcAddress()"/> function does not retrieve addresses from modules that were loaded using the LOAD_LIBRARY_AS_DATAFILE flag.
		/// </param>
		/// <param name="procName">
		/// The function or variable name, or the function's ordinal value.
		/// </param>
		/// <returns>
		/// If the function succeeds, the return value is the address of the exported function or variable.
		/// If the function fails, the return value is <see cref="M:IntPtr.Zero"/>.
		/// To get extended error information, call GetLastError.
		/// </returns>
		[DllImport(LibName, CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		[SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
		public static extern IntPtr GetProcAddress(ModuleHandle hModule, string procName);

		/// <summary>Retrieves the address of an exported function or variable from the specified dynamic-link library (DLL).</summary>
		/// <param name="hModule">
		/// A handle to the DLL module that contains the function or variable. The LoadLibrary, LoadLibraryEx, LoadPackagedLibrary, or GetModuleHandle function returns this handle.
		/// The <see cref="M:GetProcAddress()"/> function does not retrieve addresses from modules that were loaded using the LOAD_LIBRARY_AS_DATAFILE flag.
		/// </param>
		/// <param name="procName">
		/// The function or variable name, or the function's ordinal value.
		/// </param>
		/// <returns>
		/// If the function succeeds, the return value is the address of the exported function or variable.
		/// If the function fails, the return value is <see cref="M:IntPtr.Zero"/>.
		/// To get extended error information, call GetLastError.
		/// </returns>
		[DllImport(LibName, CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		[SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
		public static extern IntPtr GetProcAddress(IntPtr hModule, string procName);

		#endregion

		#region LoadLibrary

		/// <summary>
		/// Loads the specified module into the address space of the calling process. The specified module may cause other modules to be loaded.
		///	For additional load options, use the <see cref="M:LoadLibraryEx()"/> function.
		/// </summary>
		/// <param name="lpFileName">
		/// The name of the module. This can be either a library module (a .dll file) or an executable module (an .exe file).
		/// The name specified is the file name of the module and is not related to the name stored in the library module itself,
		/// as specified by the LIBRARY keyword in the module-definition (.def) file.
		///	If the string specifies a full path, the function searches only that path for the module.
		/// If the string specifies a relative path or a module name without a path, the function uses a standard search strategy to find the module.
		/// If the function cannot find the module, the function fails.When specifying a path, be sure to use backslashes (\), not forward slashes(/).
		/// For more information about paths, see Naming a File or Directory.
		/// If the string specifies a module name without a path and the file name extension is omitted,
		/// the function appends the default library extension .dll to the module name.To prevent the function from appending .dll
		/// to the module name, include a trailing point character (.) in the module name string.
		/// </param>
		/// <returns>If the function succeeds, the return value is a handle to the module.
		/// If the function fails, the return value is invalid handle.
		/// To get extended error information, call GetLastError.
		/// </returns>
		[DllImport(LibName, SetLastError = true, CharSet = CharSet.Unicode, EntryPoint = "LoadLibraryW", ExactSpelling = true)]
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		[SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
		public static extern ModuleHandle LoadLibrary([MarshalAs(UnmanagedType.LPWStr)] string lpFileName);

		#endregion

		#region LoadLibraryEx

		/// <summary>
		/// Loads the specified module into the address space of the calling process.
		/// The specified module may cause other modules to be loaded.
		/// </summary>
		/// <param name="lpFileName">
		/// The name of the module. This can be either a library module (a .dll file) or an executable module (an .exe file).
		/// The name specified is the file name of the module and is not related to the name stored in the library module itself,
		/// as specified by the LIBRARY keyword in the module-definition (.def) file.
		///	If the string specifies a full path, the function searches only that path for the module.
		/// If the string specifies a relative path or a module name without a path, the function uses a standard search strategy to find the module.
		/// If the function cannot find the module, the function fails.When specifying a path, be sure to use backslashes (\), not forward slashes(/).
		/// For more information about paths, see Naming a File or Directory.
		/// If the string specifies a module name without a path and the file name extension is omitted,
		/// the function appends the default library extension .dll to the module name.To prevent the function from appending .dll
		/// to the module name, include a trailing point character (.) in the module name string.
		/// </param>
		/// <param name="hFile">This parameter is reserved for future use. It must be <see cref="M:IntPtr.Zero"/>.</param>
		/// <param name="flags">
		/// The action to be taken when loading the module.
		/// If no flags are specified, the behavior of this function is identical to that of the <see cref="M:LoadLibrary()"/> function.
		/// </param>
		/// <returns></returns>
		[DllImport(LibName, SetLastError = true, CharSet = CharSet.Unicode, EntryPoint = "LoadLibraryExW", ExactSpelling = true)]
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		[SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
		public static extern ModuleHandle LoadLibraryEx([MarshalAs(UnmanagedType.LPWStr)]string lpFileName, IntPtr hFile, LoadLibraryFlags flags);

		#endregion

		#region FreeLibrary

		/// <summary>
		/// Frees the loaded dynamic-link library (DLL) module and, if necessary, decrements its reference count.
		/// When the reference count reaches zero, the module is unloaded from the address space of the calling process and the handle is no longer valid.
		/// </summary>
		/// <param name="hModule">
		/// A handle to the loaded library module.
		/// The LoadLibrary, LoadLibraryEx, GetModuleHandle, or GetModuleHandleEx function returns this handle.
		/// </param>
		/// <returns></returns>
		[DllImport(LibName, SetLastError = true)]
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		[SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool FreeLibrary(IntPtr hModule);

		#endregion

		#region SetDllDirectory

		/// <summary>Adds a directory to the search path used to locate DLLs for the application.</summary>
		/// <param name="lpPathName">
		/// The directory to be added to the search path.
		/// If this parameter is an empty string (""), the call removes the current directory from the default DLL search order.
		/// If this parameter is <c>null</c>, the function restores the default search order.
		/// </param>
		/// <returns>
		/// If the function succeeds, the return value is <c>true</c>.
		/// If the function fails, the return value is <c>false</c>.
		/// To get extended error information, call GetLastError.
		/// </returns>
		[DllImport(LibName, CharSet = CharSet.Auto, SetLastError = true)]
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		[SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool SetDllDirectory(string lpPathName);

		#endregion

		#region SetErrorMode

		/// <summary>
		/// Controls whether the system will handle the specified types of serious errors or whether the process will handle them.
		/// </summary>
		/// <param name="mode">The process error mode.</param>
		/// <returns>The return value is the previous state of the error-mode bit flags.</returns>
		[DllImport(LibName, SetLastError = true, ExactSpelling = true)]
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		[SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
		public static extern SEM SetErrorMode(SEM mode);

		#endregion

		#region GetErrorMode

		/// <summary>Retrieves the error mode for the current process.</summary>
		/// <returns>The process error mode.</returns>
		[DllImport(LibName, SetLastError = true, ExactSpelling = true)]
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		[SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
		public static extern SEM GetErrorMode();

		#endregion

		#region GetProcessHeap

		[DllImport(LibName, SetLastError = true)]
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		[SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
		public static extern IntPtr GetProcessHeap();

		#endregion

		#region HeapAlloc

		[DllImport(LibName, SetLastError = false)]
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		[SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
		public static extern IntPtr HeapAlloc(IntPtr hHeap, uint dwFlags, UIntPtr dwBytes);

		#endregion

		#region HeapFree

		[DllImport(LibName, SetLastError = true)]
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		[SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
		public static extern bool HeapFree(IntPtr hHeap, uint dwFlags, IntPtr lpMem);

		#endregion

		#region HeapReAlloc

		[DllImport(LibName)]
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		[SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
		public static extern IntPtr HeapReAlloc(IntPtr hHeap, uint dwFlags, IntPtr lpMem, UIntPtr dwBytes);

		#endregion

		#region HeapSize

		[DllImport(LibName)]
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		[SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
		public static extern UIntPtr HeapSize(IntPtr hHeap, uint dwFlags, IntPtr lpMem);

		#endregion

		#region HeapCompact

		[DllImport(LibName, SetLastError = true)]
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		[SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
		public static extern UIntPtr HeapCompact(IntPtr hHeap, uint dwFlags);

		#endregion

		#region HeapLock

		[DllImport(LibName, SetLastError = true)]
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		[SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool HeapLock(IntPtr hHeap);

		#endregion

		#region HeapUnlock

		[DllImport(LibName, SetLastError = true)]
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		[SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool HeapUnlock(IntPtr hHeap);

		#endregion

		#region GetFileAttributes

		/// <summary>Retrieves file system attributes for a specified file or directory.</summary>
		/// <param name="lpFileName">The name of the file or directory.</param>
		/// <returns>
		/// If the function succeeds, the return value contains the attributes of the specified file or directory.
		/// If the function fails, the return value is <see cref="M:FILE_ATTRIBUTE.INVALID"/>.
		/// To get extended error information, call GetLastError.
		/// </returns>
		[DllImport(LibName, CharSet = CharSet.Unicode, EntryPoint = "GetFileAttributesW", ExactSpelling = true, SetLastError = true)]
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		[SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
		public static extern FILE_ATTRIBUTE GetFileAttributes(string lpFileName);

		#endregion

		#region SetFileAttributes

		/// <summary>Sets the attributes for a file or directory.</summary>
		/// <param name="lpFileName">The name of the file whose attributes are to be set.</param>
		/// <param name="dwFileAttributes">The file attributes to set for the file.</param>
		/// <returns>
		/// If the function succeeds, the return value is <c>true</c>.
		/// If the function fails, the return value is <c>false</c>.
		/// To get extended error information, call GetLastError.
		/// </returns>
		[DllImport(LibName, CharSet = CharSet.Unicode, EntryPoint = "SetFileAttributesW", ExactSpelling = true, SetLastError = true)]
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		[SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool SetFileAttributes(string lpFileName, FILE_ATTRIBUTE dwFileAttributes);

		#endregion

		#region DeleteFile

		/// <summary>Deletes an existing file.</summary>
		/// <param name="lpFileName">The name of the file to be deleted.</param>
		/// <returns>
		/// If the function succeeds, the return value is <c>true</c>.
		/// If the function fails, the return value is <c>false</c>.
		/// To get extended error information, call GetLastError.
		/// </returns>
		[DllImport(LibName, CharSet = CharSet.Unicode, EntryPoint = "DeleteFileW", ExactSpelling = true, SetLastError = true)]
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		[SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool DeleteFile(string lpFileName);

		#endregion

		#region GetFileType

		/// <summary>Retrieves the file type of the specified file.</summary>
		/// <param name="hFile">A handle to the file.</param>
		/// <returns>One of <see cref="FILE_TYPE"/> values.</returns>
		[DllImport(LibName, ExactSpelling = true, SetLastError = true)]
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		[SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
		public static extern FILE_TYPE GetFileType(IntPtr hFile);

		/// <summary>Retrieves the file type of the specified file.</summary>
		/// <param name="hFile">A handle to the file.</param>
		/// <returns>One of <see cref="FILE_TYPE"/> values.</returns>
		[DllImport(LibName, ExactSpelling = true, SetLastError = true)]
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		[SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
		public static extern FILE_TYPE GetFileType(SafeFileHandle hFile);

		#endregion

		#region GetFileSizeEx

		/// <summary>Retrieves the size of the specified file.</summary>
		/// <param name="hFile">
		/// A handle to the file.
		/// The handle must have been created with the FILE_READ_ATTRIBUTES access right or equivalent,
		/// or the caller must have sufficient permission on the directory that contains the file.
		/// </param>
		/// <param name="lpFileSize">Variable that receives the file size, in bytes.</param>
		/// <returns>
		/// If the function succeeds, the return value is <c>true</c>.
		/// If the function fails, the return value is <c>false</c>.
		/// To get extended error information, call GetLastError.
		/// </returns>
		[DllImport(LibName, ExactSpelling = true, SetLastError = true)]
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		[SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool GetFileSizeEx(IntPtr hFile, out long lpFileSize);

		/// <summary>Retrieves the size of the specified file.</summary>
		/// <param name="hFile">
		/// A handle to the file.
		/// The handle must have been created with the FILE_READ_ATTRIBUTES access right or equivalent,
		/// or the caller must have sufficient permission on the directory that contains the file.
		/// </param>
		/// <param name="lpFileSize">Variable that receives the file size, in bytes.</param>
		/// <returns>
		/// If the function succeeds, the return value is <c>true</c>.
		/// If the function fails, the return value is <c>false</c>.
		/// To get extended error information, call GetLastError.
		/// </returns>
		[DllImport(LibName, ExactSpelling = true, SetLastError = true)]
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		[SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool GetFileSizeEx(SafeFileHandle hFile, out long lpFileSize);

		#endregion

		#region BackupRead

		/// <summary>
		/// The BackupRead function can be used to back up a file or directory, including the security information.
		/// The function reads data associated with a specified file or directory into a buffer,
		/// which can then be written to the backup medium using the WriteFile function.
		/// </summary>
		/// <param name="hFile">
		/// Handle to the file or directory to be backed up.
		/// To obtain the handle, call the CreateFile function.
		/// The SACLs are not read unless the file handle was created with the ACCESS_SYSTEM_SECURITY access right.
		/// </param>
		/// <param name="lpBuffer">Pointer to a buffer that receives the data.</param>
		/// <param name="nNumberOfBytesToRead">
		/// Length of the buffer, in bytes.The buffer size must be greater than the size
		/// of a WIN32_STREAM_ID structure.
		/// </param>
		/// <param name="lpNumberOfBytesRead">
		/// Pointer to a variable that receives the number of bytes read.
		/// If the function returns a nonzero value, and the variable pointed to by <paramref name="lpNumberOfBytesRead"/> is zero,
		/// then all the data associated with the file handle has been read.
		/// </param>
		/// <param name="bAbort">
		/// Indicates whether you have finished using BackupRead on the handle.
		/// While you are backing up the file, specify this parameter as <c>false</c>.
		/// Once you are done using BackupRead, you must call BackupRead one more time
		/// specifying <c>true</c> for this parameter and passing the appropriate <paramref name="lpContext"/>.
		/// <paramref name="lpContext"/> must be passed when bAbort is <c>true</c>; all other parameters are ignored.
		/// </param>
		/// <param name="bProcessSecurity">
		/// Indicates whether the function will restore the access-control list(ACL) data for the file or directory.
		/// If <paramref name="bProcessSecurity"/> is <c>true</c>, the ACL data will be backed up.
		/// </param>
		/// <param name="lpContext">
		/// Pointer to a variable that receives a pointer to an internal data structure used by BackupRead
		/// to maintain context information during a backup operation. 
		/// </param>
		/// <returns>
		/// If the function succeeds, the return value is <c>true</c>.
		/// If the function fails, the return value is <c>false</c>, indicating that
		/// an I/O error occurred.To get extended error information, call GetLastError.
		/// </returns>
		[DllImport(LibName, ExactSpelling = true, SetLastError = true)]
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		[SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool BackupRead(
			IntPtr     hFile,
			IntPtr     lpBuffer,
			uint       nNumberOfBytesToRead,
			out uint   lpNumberOfBytesRead,
			bool       bAbort,
			bool       bProcessSecurity,
			ref IntPtr lpContext);

		/// <summary>
		/// The BackupRead function can be used to back up a file or directory, including the security information.
		/// The function reads data associated with a specified file or directory into a buffer,
		/// which can then be written to the backup medium using the WriteFile function.
		/// </summary>
		/// <param name="hFile">
		/// Handle to the file or directory to be backed up.
		/// To obtain the handle, call the CreateFile function.
		/// The SACLs are not read unless the file handle was created with the ACCESS_SYSTEM_SECURITY access right.
		/// </param>
		/// <param name="lpBuffer">Pointer to a buffer that receives the data.</param>
		/// <param name="nNumberOfBytesToRead">
		/// Length of the buffer, in bytes.The buffer size must be greater than the size
		/// of a WIN32_STREAM_ID structure.
		/// </param>
		/// <param name="lpNumberOfBytesRead">
		/// Pointer to a variable that receives the number of bytes read.
		/// If the function returns a nonzero value, and the variable pointed to by <paramref name="lpNumberOfBytesRead"/> is zero,
		/// then all the data associated with the file handle has been read.
		/// </param>
		/// <param name="bAbort">
		/// Indicates whether you have finished using BackupRead on the handle.
		/// While you are backing up the file, specify this parameter as <c>false</c>.
		/// Once you are done using BackupRead, you must call BackupRead one more time
		/// specifying <c>true</c> for this parameter and passing the appropriate <paramref name="lpContext"/>.
		/// <paramref name="lpContext"/> must be passed when bAbort is <c>true</c>; all other parameters are ignored.
		/// </param>
		/// <param name="bProcessSecurity">
		/// Indicates whether the function will restore the access-control list(ACL) data for the file or directory.
		/// If <paramref name="bProcessSecurity"/> is <c>true</c>, the ACL data will be backed up.
		/// </param>
		/// <param name="lpContext">
		/// Pointer to a variable that receives a pointer to an internal data structure used by BackupRead
		/// to maintain context information during a backup operation. 
		/// </param>
		/// <returns>
		/// If the function succeeds, the return value is <c>true</c>.
		/// If the function fails, the return value is <c>false</c>, indicating that
		/// an I/O error occurred.To get extended error information, call GetLastError.
		/// </returns>
		[DllImport(LibName, ExactSpelling = true, SetLastError = true)]
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		[SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool BackupRead(
			SafeFileHandle hFile,
			IntPtr         lpBuffer,
			uint           nNumberOfBytesToRead,
			out uint       lpNumberOfBytesRead,
			bool           bAbort,
			bool           bProcessSecurity,
			ref IntPtr     lpContext);

		#endregion

		#region BackupSeek

		/// <summary>
		/// The BackupSeek function seeks forward in a data stream initially accessed by
		/// using the BackupRead or BackupWrite function.
		/// </summary>
		/// <param name="hFile">
		/// Handle to the file or directory.This handle is created by using the CreateFile function.
		/// The handle must be synchronous (nonoverlapped). This means that the FILE_FLAG_OVERLAPPED
		/// flag must not be set when CreateFile is called.
		/// This function does not validate that the handle it receives is synchronous, so it does
		/// not return an error code for a synchronous handle, but calling it with an asynchronous(overlapped)
		/// handle can result in subtle errors that are very difficult to debug.
		/// </param>
		/// <param name="dwLowBytesToSeek">Low-order part of the number of bytes to seek.</param>
		/// <param name="dwHighBytesToSeek">High-order part of the number of bytes to seek.</param>
		/// <param name="lpdwLowByteSeeked">Pointer to a variable that receives the low-order bits of the number of bytes the function actually seeks.</param>
		/// <param name="lpdwHighByteSeeked">Pointer to a variable that receives the high-order bits of the number of bytes the function actually seeks.</param>
		/// <param name="context">
		/// Pointer to an internal data structure used by the function.
		/// This structure must be the same structure that was initialized by the BackupRead or BackupWrite function. An application must not touch the contents of this structure.
		/// </param>
		/// <returns>
		/// If the function could seek the requested amount, the function returns <c>true</c>.
		/// If the function could not seek the requested amount, the function returns <c>false</c>.
		/// To get extended error information, call GetLastError.
		/// </returns>
		[DllImport(LibName, ExactSpelling = true, SetLastError = true)]
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		[SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool BackupSeek(
			IntPtr     hFile,
			uint       dwLowBytesToSeek,
			uint       dwHighBytesToSeek,
			out uint   lpdwLowByteSeeked,
			out uint   lpdwHighByteSeeked,
			ref IntPtr context);

		/// <summary>
		/// The BackupSeek function seeks forward in a data stream initially accessed by
		/// using the BackupRead or BackupWrite function.
		/// </summary>
		/// <param name="hFile">
		/// Handle to the file or directory.This handle is created by using the CreateFile function.
		/// The handle must be synchronous (nonoverlapped). This means that the FILE_FLAG_OVERLAPPED
		/// flag must not be set when CreateFile is called.
		/// This function does not validate that the handle it receives is synchronous, so it does
		/// not return an error code for a synchronous handle, but calling it with an asynchronous(overlapped)
		/// handle can result in subtle errors that are very difficult to debug.
		/// </param>
		/// <param name="dwLowBytesToSeek">Low-order part of the number of bytes to seek.</param>
		/// <param name="dwHighBytesToSeek">High-order part of the number of bytes to seek.</param>
		/// <param name="lpdwLowByteSeeked">Pointer to a variable that receives the low-order bits of the number of bytes the function actually seeks.</param>
		/// <param name="lpdwHighByteSeeked">Pointer to a variable that receives the high-order bits of the number of bytes the function actually seeks.</param>
		/// <param name="context">
		/// Pointer to an internal data structure used by the function.
		/// This structure must be the same structure that was initialized by the BackupRead or BackupWrite function. An application must not touch the contents of this structure.
		/// </param>
		/// <returns>
		/// If the function could seek the requested amount, the function returns <c>true</c>.
		/// If the function could not seek the requested amount, the function returns <c>false</c>.
		/// To get extended error information, call GetLastError.
		/// </returns>
		[DllImport(LibName, ExactSpelling = true, SetLastError = true)]
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		[SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool BackupSeek(
			SafeFileHandle hFile,
			uint           dwLowBytesToSeek,
			uint           dwHighBytesToSeek,
			out uint       lpdwLowByteSeeked,
			out uint       lpdwHighByteSeeked,
			ref IntPtr     context);

		[DllImport(LibName, CharSet = CharSet.Auto)]
		public static extern int GetModuleFileName(HandleRef hModule, StringBuilder buffer, int length);

		#endregion
	}

	/// <summary>Функции библиотеки gdi32.dll.</summary>
	[SuppressUnmanagedCodeSecurity]
	static partial class Gdi32
	{
		/// <summary>Имя библиотеки.</summary>
		private const string LibName = "gdi32.dll";

		#region SetStretchBltMode

		/// <summary>Точка входа функции SetStretchBltMode.</summary>
		private const string SetStretchBltModeEntryPoint = "SetStretchBltMode";

		/// <summary>This function sets the bitmap stretching mode in the specified device context. </summary>
		/// <param name="hdc">Handle to the device context.</param>
		/// <param name="iStretchMode">Specifies the stretching mode.</param>
		/// <returns>
		/// If the function succeeds, the return value is the previous stretching mode.
		/// If the function fails, the return value is
		/// <see cref="M:StretchBltMode.INVALID"/> or
		/// <see cref="M:StretchBltMode.INVALID_PARAMETER"/>.
		/// </returns>
		/// <seealso>http://msdn.microsoft.com/en-us/library/windows/desktop/dd145089.aspx</seealso>
		[DllImport(LibName, EntryPoint = SetStretchBltModeEntryPoint)]
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		[SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
		public static extern StretchBltMode SetStretchBltMode(
			[In] IntPtr hdc,
			[In] StretchBltMode iStretchMode
			);

		#endregion

		#region StretchBlt

		/// <summary>Точка входа функции StretchBlt.</summary>
		private const string StretchBltEntryPoint = "StretchBlt";

		/// <summary>
		/// The StretchBlt function copies a bitmap from a source rectangle into a destination rectangle,
		/// stretching or compressing the bitmap to fit the dimensions of the destination rectangle, if necessary.
		/// The system stretches or compresses the bitmap according to the stretching mode currently set in the
		/// destination device context.
		/// </summary>
		/// <param name="hDCDest">A handle to the destination device context.</param>
		/// <param name="xOriginDest">The x-coordinate, in logical units, of the upper-left corner of the destination rectangle.</param>
		/// <param name="yOriginDest">The y-coordinate, in logical units, of the upper-left corner of the destination rectangle.</param>
		/// <param name="widthDest">The width, in logical units, of the destination rectangle.</param>
		/// <param name="heightDest">The height, in logical units, of the destination rectangle.</param>
		/// <param name="hDCSrc">A handle to the source device context.</param>
		/// <param name="xOriginScr">The x-coordinate, in logical units, of the upper-left corner of the source rectangle.</param>
		/// <param name="yOriginSrc">The y-coordinate, in logical units, of the upper-left corner of the source rectangle.</param>
		/// <param name="widthScr">The width, in logical units, of the source rectangle.</param>
		/// <param name="heightScr">The height, in logical units, of the source rectangle.</param>
		/// <param name="rop">
		/// The raster operation to be performed. Raster operation codes define how the system combines
		/// colors in output operations that involve a brush, a source bitmap, and a destination bitmap.
		/// </param>
		/// <returns>
		/// If the function succeeds, the return value is <c>true</c>.
		/// If the function fails, the return value is <c>false</c>.
		/// </returns>
		[DllImport(LibName, EntryPoint = StretchBltEntryPoint)]
		[return: MarshalAs(UnmanagedType.Bool)]
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		[SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
		public static extern bool StretchBlt(
			[In] IntPtr hDCDest, [In] int xOriginDest, [In] int yOriginDest, [In] int widthDest, [In] int heightDest,
			[In] IntPtr hDCSrc, [In] int xOriginScr, [In] int yOriginSrc, [In] int widthScr, [In] int heightScr,
			[In] RasterOperation rop);

		#endregion

		#region BitBlt

		/// <summary>Точка входа функции StretchBlt.</summary>
		private const string BitBltEntryPoint = "StretchBlt";

		/// <summary>
		/// The BitBlt function performs a bit-block transfer of the color data corresponding to a rectangle of pixels 
		/// from the specified source device context into a destination device context.
		/// </summary>
		/// <param name="hDCDest">A handle to the destination device context.</param>
		/// <param name="xOriginDest">The x-coordinate, in logical units, of the upper-left corner of the destination rectangle.</param>
		/// <param name="yOriginDest">The y-coordinate, in logical units, of the upper-left corner of the destination rectangle.</param>
		/// <param name="widthDest">The width, in logical units, of the source and destination rectangles.</param>
		/// <param name="heightDest">The height, in logical units, of the source and the destination rectangles.</param>
		/// <param name="hDCSrc">A handle to the source device context.</param>
		/// <param name="xOriginScr">The x-coordinate, in logical units, of the upper-left corner of the source rectangle.</param>
		/// <param name="yOriginSrc">The y-coordinate, in logical units, of the upper-left corner of the source rectangle.</param>
		/// <param name="rop">
		/// A raster-operation code. These codes define how the color data for the source rectangle is to be combined with the
		/// color data for the destination rectangle to achieve the final color.
		/// </param>
		/// <returns>
		/// If the function succeeds, the return value is <c>true</c>.
		/// If the function fails, the return value is <c>false</c>.
		/// </returns>
		[DllImport(LibName, EntryPoint = BitBltEntryPoint)]
		[return: MarshalAs(UnmanagedType.Bool)]
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		[SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
		public static extern bool BitBlt(
			[In] IntPtr hDCDest, [In] int xOriginDest, [In] int yOriginDest, [In] int widthDest, [In] int heightDest,
			[In] IntPtr hDCSrc, [In] int xOriginScr, [In] int yOriginSrc,
			[In] RasterOperation rop);

		#endregion

		#region StretchDIBits

		/// <summary>Точка входа функции StretchDIBits.</summary>
		private const string StretchDIBitsEntryPoint = "StretchDIBits";

		/// <summary>
		/// The StretchDIBits function copies the color data for a rectangle of pixels in a DIB, JPEG, or PNG image
		/// to the specified destination rectangle. If the destination rectangle is larger than the source rectangle,
		/// this function stretches the rows and columns of color data to fit the destination rectangle.
		/// If the destination rectangle is smaller than the source rectangle, this function compresses the rows and columns
		/// by using the specified raster operation.
		/// </summary>
		/// <param name="hDC">A handle to the destination device context.</param>
		/// <param name="xDest">The x-coordinate, in logical units, of the upper-left corner of the destination rectangle.</param>
		/// <param name="yDest">The y-coordinate, in logical units, of the upper-left corner of the destination rectangle.</param>
		/// <param name="widthDest">The width, in logical units, of the destination rectangle.</param>
		/// <param name="heightDest">The height, in logical units, of the destination rectangle.</param>
		/// <param name="xSrc">The x-coordinate, in pixels, of the source rectangle in the image.</param>
		/// <param name="ySrc">The y-coordinate, in pixels, of the source rectangle in the image.</param>
		/// <param name="srcWidth">The width, in pixels, of the source rectangle in the image.</param>
		/// <param name="srcHeight">The height, in pixels, of the source rectangle in the image.</param>
		/// <param name="lpBits">A pointer to the image bits, which are stored as an array of bytes.</param>
		/// <param name="lpBitsInfo">A pointer to a BITMAPINFO structure that contains information about the DIB.</param>
		/// <param name="iUsage"></param>
		/// <param name="dwRop">
		/// A raster-operation code that specifies how the source pixels, the destination device context's 
		/// current brush, and the destination pixels are to be combined to form the new image.
		/// </param>
		/// <returns>
		/// If the function succeeds, the return value is the number of scan lines copied. Note that this value can be negative for mirrored content.
		/// If the function fails, or no scan lines are copied, the return value is 0.
		/// </returns>
		/// <seealso>http://msdn.microsoft.com/en-us/library/windows/desktop/dd145121.aspx</seealso>
		[DllImport(LibName, EntryPoint = StretchDIBitsEntryPoint)]
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		[SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
		public static extern int StretchDIBits(
			[In] IntPtr hDC,
			[In] int xDest, [In] int yDest, [In] int widthDest, [In] int heightDest,
			[In] int xSrc, [In] int ySrc, [In] int srcWidth, [In] int srcHeight,
			[In] IntPtr lpBits,
			[In] IntPtr lpBitsInfo,
			[In] DibUsage iUsage,
			[In] RasterOperation dwRop);

		/// <summary>
		/// The StretchDIBits function copies the color data for a rectangle of pixels in a DIB, JPEG, or PNG image
		/// to the specified destination rectangle. If the destination rectangle is larger than the source rectangle,
		/// this function stretches the rows and columns of color data to fit the destination rectangle.
		/// If the destination rectangle is smaller than the source rectangle, this function compresses the rows and columns
		/// by using the specified raster operation.
		/// </summary>
		/// <param name="hDC">A handle to the destination device context.</param>
		/// <param name="xDest">The x-coordinate, in logical units, of the upper-left corner of the destination rectangle.</param>
		/// <param name="yDest">The y-coordinate, in logical units, of the upper-left corner of the destination rectangle.</param>
		/// <param name="widthDest">The width, in logical units, of the destination rectangle.</param>
		/// <param name="heightDest">The height, in logical units, of the destination rectangle.</param>
		/// <param name="xSrc">The x-coordinate, in pixels, of the source rectangle in the image.</param>
		/// <param name="ySrc">The y-coordinate, in pixels, of the source rectangle in the image.</param>
		/// <param name="srcWidth">The width, in pixels, of the source rectangle in the image.</param>
		/// <param name="srcHeight">The height, in pixels, of the source rectangle in the image.</param>
		/// <param name="lpBits">A pointer to the image bits, which are stored as an array of bytes.</param>
		/// <param name="lpBitsInfo">A pointer to a <see cref="BITMAPINFOHEADER"/> structure that contains information about the DIB.</param>
		/// <param name="iUsage"></param>
		/// <param name="dwRop">
		/// A raster-operation code that specifies how the source pixels, the destination device context's 
		/// current brush, and the destination pixels are to be combined to form the new image.
		/// </param>
		/// <returns>
		/// If the function succeeds, the return value is the number of scan lines copied. Note that this value can be negative for mirrored content.
		/// If the function fails, or no scan lines are copied, the return value is 0.
		/// </returns>
		/// <seealso>http://msdn.microsoft.com/en-us/library/windows/desktop/dd145121.aspx</seealso>
		[DllImport(LibName, EntryPoint = StretchDIBitsEntryPoint)]
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		[SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
		public static extern int StretchDIBits(
			[In] IntPtr hDC,
			[In] int xDest, [In] int yDest, [In] int widthDest, [In] int heightDest,
			[In] int xSrc, [In] int ySrc, [In] int srcWidth, [In] int srcHeight,
			[In] IntPtr lpBits,
			[In] ref BITMAPINFOHEADER lpBitsInfo,
			[In] DibUsage iUsage,
			[In] RasterOperation dwRop);

#if ALLOW_UNSAFE
		/// <summary>
		/// The StretchDIBits function copies the color data for a rectangle of pixels in a DIB, JPEG, or PNG image
		/// to the specified destination rectangle. If the destination rectangle is larger than the source rectangle,
		/// this function stretches the rows and columns of color data to fit the destination rectangle.
		/// If the destination rectangle is smaller than the source rectangle, this function compresses the rows and columns
		/// by using the specified raster operation.
		/// </summary>
		/// <param name="hDC">A handle to the destination device context.</param>
		/// <param name="xDest">The x-coordinate, in logical units, of the upper-left corner of the destination rectangle.</param>
		/// <param name="yDest">The y-coordinate, in logical units, of the upper-left corner of the destination rectangle.</param>
		/// <param name="widthDest">The width, in logical units, of the destination rectangle.</param>
		/// <param name="heightDest">The height, in logical units, of the destination rectangle.</param>
		/// <param name="xSrc">The x-coordinate, in pixels, of the source rectangle in the image.</param>
		/// <param name="ySrc">The y-coordinate, in pixels, of the source rectangle in the image.</param>
		/// <param name="srcWidth">The width, in pixels, of the source rectangle in the image.</param>
		/// <param name="srcHeight">The height, in pixels, of the source rectangle in the image.</param>
		/// <param name="lpBits">A pointer to the image bits, which are stored as an array of bytes.</param>
		/// <param name="lpBitsInfo">A pointer to a <see cref="BITMAPINFOHEADER"/> structure that contains information about the DIB.</param>
		/// <param name="iUsage"></param>
		/// <param name="dwRop">
		/// A raster-operation code that specifies how the source pixels, the destination device context's 
		/// current brush, and the destination pixels are to be combined to form the new image.
		/// </param>
		/// <returns>
		/// If the function succeeds, the return value is the number of scan lines copied. Note that this value can be negative for mirrored content.
		/// If the function fails, or no scan lines are copied, the return value is 0.
		/// </returns>
		/// <seealso>http://msdn.microsoft.com/en-us/library/windows/desktop/dd145121.aspx</seealso>
		[DllImport(LibName, EntryPoint = StretchDIBitsEntryPoint)]
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		[SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
		public static extern unsafe int StretchDIBits(
			[In] IntPtr hDC,
			[In] int xDest, [In] int yDest, [In] int widthDest, [In] int heightDest,
			[In] int xSrc, [In] int ySrc, [In] int srcWidth, [In] int srcHeight,
			[In] byte* lpBits,
			[In] ref BITMAPINFOHEADER lpBitsInfo,
			[In] DibUsage iUsage,
			[In] RasterOperation dwRop
			);

		/// <summary>
		/// The StretchDIBits function copies the color data for a rectangle of pixels in a DIB, JPEG, or PNG image
		/// to the specified destination rectangle. If the destination rectangle is larger than the source rectangle,
		/// this function stretches the rows and columns of color data to fit the destination rectangle.
		/// If the destination rectangle is smaller than the source rectangle, this function compresses the rows and columns
		/// by using the specified raster operation.
		/// </summary>
		/// <param name="hDC">A handle to the destination device context.</param>
		/// <param name="xDest">The x-coordinate, in logical units, of the upper-left corner of the destination rectangle.</param>
		/// <param name="yDest">The y-coordinate, in logical units, of the upper-left corner of the destination rectangle.</param>
		/// <param name="widthDest">The width, in logical units, of the destination rectangle.</param>
		/// <param name="heightDest">The height, in logical units, of the destination rectangle.</param>
		/// <param name="xSrc">The x-coordinate, in pixels, of the source rectangle in the image.</param>
		/// <param name="ySrc">The y-coordinate, in pixels, of the source rectangle in the image.</param>
		/// <param name="srcWidth">The width, in pixels, of the source rectangle in the image.</param>
		/// <param name="srcHeight">The height, in pixels, of the source rectangle in the image.</param>
		/// <param name="lpBits">A pointer to the image bits, which are stored as an array of bytes.</param>
		/// <param name="lpBitsInfo">A pointer to a <see cref="BITMAPINFOHEADER"/> structure that contains information about the DIB.</param>
		/// <param name="iUsage"></param>
		/// <param name="dwRop">
		/// A raster-operation code that specifies how the source pixels, the destination device context's 
		/// current brush, and the destination pixels are to be combined to form the new image.
		/// </param>
		/// <returns>
		/// If the function succeeds, the return value is the number of scan lines copied. Note that this value can be negative for mirrored content.
		/// If the function fails, or no scan lines are copied, the return value is 0.
		/// </returns>
		/// <seealso>http://msdn.microsoft.com/en-us/library/windows/desktop/dd145121.aspx</seealso>
		[DllImport(LibName, EntryPoint = StretchDIBitsEntryPoint)]
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		public static extern int StretchDIBits(
			[In] IntPtr hDC,
			[In] int xDest, [In] int yDest, [In] int widthDest, [In] int heightDest,
			[In] int xSrc, [In] int ySrc, [In] int srcWidth, [In] int srcHeight,
			[In] IntPtr lpBits,
			[In] ref BITMAPINFO256 lpBitsInfo,
			[In] DibUsage iUsage,
			[In] RasterOperation dwRop);
#endif

#if ALLOW_UNSAFE
		/// <summary>
		/// The StretchDIBits function copies the color data for a rectangle of pixels in a DIB, JPEG, or PNG image
		/// to the specified destination rectangle. If the destination rectangle is larger than the source rectangle,
		/// this function stretches the rows and columns of color data to fit the destination rectangle.
		/// If the destination rectangle is smaller than the source rectangle, this function compresses the rows and columns
		/// by using the specified raster operation.
		/// </summary>
		/// <param name="hDC">A handle to the destination device context.</param>
		/// <param name="xDest">The x-coordinate, in logical units, of the upper-left corner of the destination rectangle.</param>
		/// <param name="yDest">The y-coordinate, in logical units, of the upper-left corner of the destination rectangle.</param>
		/// <param name="widthDest">The width, in logical units, of the destination rectangle.</param>
		/// <param name="heightDest">The height, in logical units, of the destination rectangle.</param>
		/// <param name="xSrc">The x-coordinate, in pixels, of the source rectangle in the image.</param>
		/// <param name="ySrc">The y-coordinate, in pixels, of the source rectangle in the image.</param>
		/// <param name="srcWidth">The width, in pixels, of the source rectangle in the image.</param>
		/// <param name="srcHeight">The height, in pixels, of the source rectangle in the image.</param>
		/// <param name="lpBits">A pointer to the image bits, which are stored as an array of bytes.</param>
		/// <param name="lpBitsInfo">A pointer to a <see cref="BITMAPINFO256"/> structure that contains information about the DIB.</param>
		/// <param name="iUsage"></param>
		/// <param name="dwRop">
		/// A raster-operation code that specifies how the source pixels, the destination device context's 
		/// current brush, and the destination pixels are to be combined to form the new image.
		/// </param>
		/// <returns>
		/// If the function succeeds, the return value is the number of scan lines copied. Note that this value can be negative for mirrored content.
		/// If the function fails, or no scan lines are copied, the return value is 0.
		/// </returns>
		/// <seealso>http://msdn.microsoft.com/en-us/library/windows/desktop/dd145121.aspx</seealso>
		[DllImport(LibName, EntryPoint = StretchDIBitsEntryPoint)]
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		[SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
		public static extern unsafe int StretchDIBits(
			[In] IntPtr hDC,
			[In] int xDest, [In] int yDest, [In] int widthDest, [In] int heightDest,
			[In] int xSrc, [In] int ySrc, [In] int srcWidth, [In] int srcHeight,
			[In] byte* lpBits,
			[In] ref BITMAPINFO256 lpBitsInfo,
			[In] DibUsage iUsage,
			[In] RasterOperation dwRop
			);
#endif

		#endregion

		#region SelectObject

		/// <summary>Точка входа функции SelectObject.</summary>
		private const string SelectObjectEntryPoint = "SelectObject";

		/// <summary>
		/// The SelectObject function selects an object into the specified device context (DC).
		/// The new object replaces the previous object of the same type.
		/// </summary>
		/// <param name="hDC">A handle to the DC.</param>
		/// <param name="hObject">A handle to the object to be selected.</param>
		/// <returns>
		/// If the selected object is not a region and the function succeeds, the return value is a handle to the object being replaced.
		/// If the selected object is a region and the function succeeds, the return value is one of the following values:
		///   SIMPLEREGION, COMPLEXREGION, NULLREGION.
		/// If an error occurs and the selected object is not a region, the return value is NULL. Otherwise, it is HGDI_ERROR.
		/// </returns>
		/// <remarks>
		/// This function returns the previously selected object of the specified type.
		/// An application should always replace a new object with the original, default object after it has finished drawing with the new object.
		/// An application cannot select a single bitmap into more than one DC at a time.
		/// </remarks>
		/// <seealso>http://msdn.microsoft.com/en-us/library/windows/desktop/dd162957.aspx</seealso>
		[DllImport(LibName, EntryPoint = SelectObjectEntryPoint)]
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		[SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
		public static extern IntPtr SelectObject([In] IntPtr hDC, [In] IntPtr hObject);

		#endregion

		#region DeleteObject

		/// <summary>Точка входа функции DeleteObject.</summary>
		private const string DeleteObjectEntryPoint = "DeleteObject";

		/// <summary>
		/// The DeleteObject function deletes a logical pen, brush, font, bitmap, region, or palette, freeing all
		/// system resources associated with the object.
		/// After the object is deleted, the specified handle is no longer valid.
		/// </summary>
		/// <param name="hObject">A handle to a logical pen, brush, font, bitmap, region, or palette.</param>
		/// <returns>
		/// If the function succeeds, the return value is <c>true</c>.
		/// If the specified handle is not valid or is currently selected into a DC, the return value is <c>false</c>.
		/// </returns>
		/// <remarks>
		/// Do not delete a drawing object (pen or brush) while it is still selected into a DC.
		/// When a pattern brush is deleted, the bitmap associated with the brush is not deleted. The bitmap must be deleted independently.
		/// </remarks>
		/// <seealso>http://msdn.microsoft.com/en-us/library/windows/desktop/dd183539.aspx</seealso>
		[DllImport(LibName, EntryPoint = DeleteObjectEntryPoint)]
		[return: MarshalAs(UnmanagedType.Bool)]
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		[SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
		public static extern bool DeleteObject([In] IntPtr hObject);

		#endregion

		#region CreateCompatibleDC

		/// <summary>Точка входа функции CreateCompatibleDC.</summary>
		private const string CreateCompatibleDCEntryPoint = "CreateCompatibleDC";

		/// <summary>
		/// The CreateCompatibleDC function creates a memory device context (DC) compatible with the specified device.
		/// </summary>
		/// <param name="hDC">
		/// A handle to an existing DC. If this handle is NULL, the function creates a
		/// memory DC compatible with the application's current screen.
		/// </param>
		/// <returns>
		/// If the function succeeds, the return value is the handle to a memory DC.
		/// If the function fails, the return value is NULL.
		/// </returns>
		/// <seealso>http://msdn.microsoft.com/en-us/library/windows/desktop/dd183489.aspx</seealso>
		[DllImport(LibName, EntryPoint = CreateCompatibleDCEntryPoint)]
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		[SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
		public static extern IntPtr CreateCompatibleDC(IntPtr hDC);

		#endregion

		#region DeleteDC

		/// <summary>Точка входа функции DeleteDC.</summary>
		private const string DeleteDCEntryPoint = "DeleteDC";

		/// <summary>The DeleteDC function deletes the specified device context (DC).</summary>
		/// <param name="hDC">A handle to the device context.</param>
		/// <returns>
		/// If the function succeeds, the return value is <c>true</c>.
		/// If the function fails, the return value is <c>false</c>.
		/// </returns>
		/// <remarks>
		/// An application must not delete a DC whose handle was obtained by calling the GetDC function.
		/// Instead, it must call the ReleaseDC function to free the DC.
		/// </remarks>
		/// <seealso>http://msdn.microsoft.com/en-us/library/windows/desktop/dd183533.aspx</seealso>
		[DllImport(LibName, EntryPoint = DeleteDCEntryPoint)]
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		[SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool DeleteDC([In] IntPtr hDC);

		#endregion

		#region GetDeviceCaps

		/// <summary>Точка входа функции GetDeviceCaps.</summary>
		private const string GetDeviceCapsEntryPoint = "GetDeviceCaps";

		/// <summary>The GetDeviceCaps function retrieves device-specific information for the specified device.</summary>
		/// <param name="hdc">A handle to the DC.</param>
		/// <param name="nIndex">The item to be returned.</param>
		/// <returns>
		/// The return value specifies the value of the desired item.
		/// When nIndex is <see cref="M:DeviceCaps.BITSPIXEL"/> and the device has 15bpp or 16bpp, the return value is 16.
		/// </returns>
		/// <seealso>http://msdn.microsoft.com/en-us/library/windows/desktop/dd144877.aspx</seealso>
		[DllImport(LibName, EntryPoint = GetDeviceCapsEntryPoint)]
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		[SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
		public static extern int GetDeviceCaps(IntPtr hdc, DeviceCaps nIndex);

		#endregion

		#region CreateCompatibleBitmap

		/// <summary>Точка входа функции CreateCompatibleBitmap.</summary>
		private const string CreateCompatibleBitmapEntryPoint = "CreateCompatibleBitmap";

		/// <summary>
		/// The CreateCompatibleBitmap function creates a bitmap compatible with the device that is associated with the specified device context.
		/// </summary>
		/// <param name="hDC">A handle to a device context.</param>
		/// <param name="width">The bitmap width, in pixels.</param>
		/// <param name="weigth">The bitmap height, in pixels.</param>
		/// <returns>
		/// If the function succeeds, the return value is a handle to the compatible bitmap (DDB).
		/// If the function fails, the return value is <c>IntPtr.Zero</c>.
		/// </returns>
		/// <seealso>http://msdn.microsoft.com/en-us/library/windows/desktop/dd183488.aspx</seealso>
		[DllImport(LibName, EntryPoint = CreateCompatibleBitmapEntryPoint)]
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		[SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
		public static extern IntPtr CreateCompatibleBitmap(IntPtr hDC, int width, int weigth);

		#endregion

		#region GetStockObject

		private const string GetStockObjectEntryPoint = @"GetStockObject";

		/// <summary>Retrieves a handle to one of the stock pens, brushes, fonts, or palettes.</summary>
		/// <param name="fnObject">The type of stock object.</param>
		/// <returns>
		/// If the function succeeds, the return value is a handle to the requested logical object.
		/// If the function fails, the return value is <c>IntPtr.Zero</c>.
		/// </returns>
		/// <seealso>https://msdn.microsoft.com/en-us/library/windows/desktop/dd144925%28v=vs.85%29.aspx</seealso>
		[DllImport(LibName, EntryPoint = GetStockObjectEntryPoint)]
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		[SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
		public static extern IntPtr GetStockObject([In] StockObject fnObject);

		#endregion
	}

	/// <summary>Функции библиотеки user32.dll.</summary>
	[SuppressUnmanagedCodeSecurity]
	static partial class User32
	{
		/// <summary>Имя библиотеки.</summary>
		private const string LibName = "user32.dll";

		#region AttachThreadInput

		/// <summary>Точка входа функции AttachThreadInput.</summary>
		private const string AttachThreadInputEntryPoint = "AttachThreadInput";

		/// <summary>
		/// Attaches or detaches the input processing mechanism of one thread to that of another thread.
		/// </summary>
		/// <param name="idAttach">
		/// The identifier of the thread to be attached to another thread. The thread to be attached cannot be a system thread.
		/// </param>
		/// <param name="idAttachTo">
		/// The identifier of the thread to which idAttach will be attached. This thread cannot be a system thread.
		/// A thread cannot attach to itself. Therefore, idAttachTo cannot equal idAttach.
		/// </param>
		/// <param name="fAttach">
		/// If this parameter is <c>true</c>, the two threads are attached. If the parameter is <c>false</c>, the threads are detached.
		/// </param>
		/// <returns>
		/// If the function succeeds, the return value is <c>true</c>.
		/// If the function fails, the return value is <c>false</c>.
		/// To get extended error information, call GetLastError.
		/// </returns>
		[DllImport(LibName, EntryPoint = AttachThreadInputEntryPoint, SetLastError = true)]
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		[SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool AttachThreadInput(
			[In] int idAttach,
			[In] int idAttachTo,
			[MarshalAs(UnmanagedType.Bool)]
			[In] bool fAttach);

		#endregion

		#region AllowSetForegroundWindow

		/// <summary>Точка входа функции AllowSetForegroundWindow.</summary>
		private const string AllowSetForegroundWindowEntryPoint = "AllowSetForegroundWindow";

		/// <summary>
		/// Enables the specified process to set the foreground window using the SetForegroundWindow function.
		/// The calling process must already be able to set the foreground window.
		/// For more information, see Remarks later in this topic.
		/// </summary>
		/// <param name="dwProcessId">
		/// The identifier of the process that will be enabled to set the foreground window.
		/// If this parameter is ASFW_ANY, all processes will be enabled to set the foreground window.
		/// </param>
		/// <returns>
		/// If the function succeeds, the return value is <c>true</c>.
		/// If the function fails, the return value is <c>false</c>.
		/// The function will fail if the calling process cannot set the foreground window.
		/// To get extended error information, call GetLastError. 
		/// </returns>
		[DllImport(LibName, EntryPoint = AllowSetForegroundWindowEntryPoint, SetLastError = true)]
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		[SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool AllowSetForegroundWindow([In] int dwProcessId);

		#endregion

		#region LockSetForegroundWindow

		/// <summary>Точка входа функции LockSetForegroundWindow.</summary>
		private const string LockSetForegroundWindowEntryPoint = "LockSetForegroundWindow";

		/// <summary>
		/// The foreground process can call the LockSetForegroundWindow function to disable calls
		/// to the <see cref="M:SetForegroundWindow"/> function.
		/// </summary>
		/// <param name="uLockCode">
		/// Specifies whether to enable or disable calls to <see cref="M:SetForegroundWindow"/>.
		/// </param>
		/// <returns>
		/// If the function succeeds, the return value is <c>true</c>.
		/// If the function fails, the return value is <c>false</c>.
		/// To get extended error information, call GetLastError.
		/// </returns>
		[DllImport(LibName, EntryPoint = LockSetForegroundWindowEntryPoint, SetLastError = true)]
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		[SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool LockSetForegroundWindow([In] LSFW uLockCode);

		#endregion

		#region GetDC

		/// <summary>Точка входа функции GetDC.</summary>
		private const string GetDCEntryPoint = "GetDC";

		/// <summary>
		/// The GetDC function retrieves a handle to a device context (DC) for the client area of a specified
		/// window or for the entire screen. You can use the returned handle in subsequent GDI functions to draw
		/// in the DC. The device context is an opaque data structure, whose values are used internally by GDI.
		/// </summary>
		/// <param name="hWnd">
		/// A handle to the window whose DC is to be retrieved.
		/// If this value is NULL, GetDC retrieves the DC for the entire screen.
		/// </param>
		/// <returns>
		/// If the function succeeds, the return value is a handle to the DC for the specified window's client area.
		/// If the function fails, the return value is NULL.
		/// </returns>
		[DllImport(LibName, EntryPoint = GetDCEntryPoint)]
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		[SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
		public static extern IntPtr GetDC([In] IntPtr hWnd);

		#endregion

		#region ReleaseDC

		/// <summary>Точка входа функции ReleaseDC.</summary>
		private const string ReleaseDCEntryPoint = "ReleaseDC";

		/// <summary>
		/// 
		/// </summary>
		/// <param name="hWnd">A handle to the window whose DC is to be released.</param>
		/// <param name="hDC">A handle to the DC to be released.</param>
		/// <returns>
		/// The return value indicates whether the DC was released.
		/// If the DC was released, the return value is 1.
		/// If the DC was not released, the return value is zero.
		/// </returns>
		[DllImport(LibName, EntryPoint = ReleaseDCEntryPoint)]
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		[SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
		public static extern int ReleaseDC([In] IntPtr hWnd, [In] IntPtr hDC);

		#endregion

		#region ShowWindow

		/// <summary>Точка входа функции ShowWindow.</summary>
		private const string ShowWindowEntryPoint = "ShowWindow";

		/// <summary>Sets the specified window's show state.</summary>
		/// <param name="handle">A handle to the window. </param>
		/// <param name="flags">
		/// Controls how the window is to be shown.
		/// This parameter is ignored the first time an application calls <see cref="ShowWindow"/>,
		/// if the program that launched the application provides a STARTUPINFO structure.
		/// Otherwise, the first time <see cref="ShowWindow"/> is called, the value should be the value obtained by the WinMain function in its nCmdShow parameter.
		/// </param>
		/// <returns></returns>
		[DllImport(LibName, EntryPoint = ShowWindowEntryPoint)]
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		[SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool ShowWindow([In] IntPtr handle, [In] SW flags);

		#endregion

		#region GetSystemMetrics

		/// <summary>Точка входа функции GetSystemMetrics.</summary>
		private const string GetSystemMetricsEntryPoint = "GetSystemMetrics";

		/// <summary>Retrieves the specified system metric or system configuration setting.</summary>
		/// <param name="nIndex">The system metric or configuration setting to be retrieved.</param>
		/// <returns>
		/// If the function succeeds, the return value is the requested system metric or configuration setting.
		/// If the function fails, the return value is 0.
		/// GetLastError does not provide extended error information. 
		/// </returns>
		/// <remarks>Note that all dimensions retrieved by <see cref="GetSystemMetrics"/> are in pixels.</remarks>
		/// <seealso>http://msdn.microsoft.com/en-us/library/windows/desktop/ms724385.aspx</seealso>
		[DllImport(LibName, EntryPoint = GetSystemMetricsEntryPoint)]
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		[SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
		public static extern int GetSystemMetrics(SM nIndex);

		#endregion

		#region ScrollDC

		/// <summary>Точка входа функции ScrollDC.</summary>
		private const string ScrollDCEntryPoint = "ScrollDC";

		/// <summary>The ScrollDC function scrolls a rectangle of bits horizontally and vertically.</summary>
		/// <param name="hDC">Handle to the device context that contains the bits to be scrolled. </param>
		/// <param name="dx">
		/// Specifies the amount, in device units, of horizontal scrolling.
		/// This parameter must be a negative value to scroll to the left.
		/// </param>
		/// <param name="dy">
		/// Specifies the amount, in device units, of vertical scrolling.
		/// This parameter must be a negative value to scroll up.
		/// </param>
		/// <param name="lprcScroll">
		/// Pointer to a RECT structure containing the coordinates of the bits to be scrolled.
		/// The only bits affected by the scroll operation are bits in the intersection of this rectangle and the
		/// rectangle specified by lprcClip.
		/// If lprcScroll is NULL, the entire client area is used.
		/// </param>
		/// <param name="lprcClip">
		/// Pointer to a RECT structure containing the coordinates of the clipping rectangle.
		/// The only bits that will be painted are the bits that remain inside this rectangle after the
		/// scroll operation has been completed.
		/// If lprcClip is NULL, the entire client area is used.
		/// </param>
		/// <param name="hrgnUpdate">
		/// Handle to the region uncovered by the scrolling process.
		/// ScrollDC defines this region; it is not necessarily a rectangle.
		/// </param>
		/// <param name="lprcUpdate">
		/// Pointer to a RECT structure that receives the coordinates of the rectangle bounding the scrolling update region.
		/// This is the largest rectangular area that requires repainting. When the function returns,
		/// the values in the structure are in client coordinates, regardless of the mapping mode for
		/// the specified device context.
		/// This allows applications to use the update region in a call to the InvalidateRgn function, if required.
		/// </param>
		/// <returns>
		/// If the function succeeds, the return value is nonzero.
		/// If the function fails, the return value is <c>false</c>.
		/// </returns>
		[DllImport(LibName, EntryPoint = ScrollDCEntryPoint, SetLastError = true, ExactSpelling = true)]
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		[SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool ScrollDC(
			[In] IntPtr hDC,
			[In] int dx,
			[In] int dy,
			[In] ref RECT lprcScroll,
			[In] ref RECT lprcClip,
			[In] IntPtr hrgnUpdate,
			[Out] out RECT lprcUpdate);

		#endregion

		#region ScrollWindowEx

		/// <summary>Точка входа функции ScrollWindowEx.</summary>
		private const string ScrollWindowExEntryPoint = "ScrollWindowEx";

		/// <summary>
		/// The ScrollWindowEx function scrolls the contents of the specified window's client area.
		/// </summary>
		/// <param name="hWnd">Handle to the window where the client area is to be scrolled. </param>
		/// <param name="nXAmount">
		/// Specifies the amount, in device units, of horizontal scrolling.
		/// This parameter must be a negative value to scroll to the left.
		/// </param>
		/// <param name="nYAmount">
		/// Specifies the amount, in device units, of vertical scrolling.
		/// This parameter must be a negative value to scroll up. 
		/// </param>
		/// <param name="rectScrollRegion">
		/// Pointer to a <see cref="RECT"/> structure that specifies the portion of the client area to be scrolled.
		/// If this parameter is NULL, the entire client area is scrolled.
		/// </param>
		/// <param name="rectClip">
		/// Pointer to a RECT structure that contains the coordinates of the clipping rectangle.
		/// Only device bits within the clipping rectangle are affected.
		/// Bits scrolled from the outside of the rectangle to the inside are painted; bits scrolled from the inside
		/// of the rectangle to the outside are not painted.
		/// This parameter may be NULL.
		/// </param>
		/// <param name="hrgnUpdate">
		/// Handle to the region that is modified to hold the region invalidated by scrolling.
		/// This parameter may be NULL.
		/// </param>
		/// <param name="prcUpdate">
		/// Pointer to a RECT structure that receives the boundaries of the rectangle invalidated by scrolling.
		/// This parameter may be NULL.
		/// </param>
		/// <param name="flags">Specifies flags that control scrolling.</param>
		/// <returns>
		/// If the function succeeds, the return value is
		///		SIMPLEREGION (rectangular invalidated region),
		///		COMPLEXREGION (nonrectangular invalidated region; overlapping rectangles),
		///	 or NULLREGION (no invalidated region).
		/// If the function fails, the return value is ERROR.
		/// </returns>
		/// <seealso>http://msdn.microsoft.com/en-us/library/windows/desktop/bb787593.aspx</seealso>
		[DllImport(LibName, EntryPoint = ScrollWindowExEntryPoint, SetLastError = true, ExactSpelling = true)]
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		[SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
		public static extern int ScrollWindowEx(
			[In] IntPtr hWnd,
			[In] int nXAmount,
			[In] int nYAmount,
			[In] ref RECT rectScrollRegion,
			[In] ref RECT rectClip,
			[In] IntPtr hrgnUpdate,
			[In] IntPtr prcUpdate,
			[In] ScrollWindowFlags flags);

		/// <summary>
		/// The ScrollWindowEx function scrolls the contents of the specified window's client area.
		/// </summary>
		/// <param name="hWnd">Handle to the window where the client area is to be scrolled. </param>
		/// <param name="nXAmount">
		/// Specifies the amount, in device units, of horizontal scrolling.
		/// This parameter must be a negative value to scroll to the left.
		/// </param>
		/// <param name="nYAmount">
		/// Specifies the amount, in device units, of vertical scrolling.
		/// This parameter must be a negative value to scroll up. 
		/// </param>
		/// <param name="rectScrollRegion">
		/// Pointer to a <see cref="RECT"/> structure that specifies the portion of the client area to be scrolled.
		/// If this parameter is NULL, the entire client area is scrolled.
		/// </param>
		/// <param name="rectClip">
		/// Pointer to a RECT structure that contains the coordinates of the clipping rectangle.
		/// Only device bits within the clipping rectangle are affected.
		/// Bits scrolled from the outside of the rectangle to the inside are painted; bits scrolled from the inside
		/// of the rectangle to the outside are not painted.
		/// This parameter may be NULL.
		/// </param>
		/// <param name="hrgnUpdate">
		/// Handle to the region that is modified to hold the region invalidated by scrolling.
		/// This parameter may be NULL.
		/// </param>
		/// <param name="prcUpdate">
		/// Pointer to a RECT structure that receives the boundaries of the rectangle invalidated by scrolling.
		/// </param>
		/// <param name="flags">Specifies flags that control scrolling.</param>
		/// <returns>
		/// If the function succeeds, the return value is
		///		SIMPLEREGION (rectangular invalidated region),
		///		COMPLEXREGION (nonrectangular invalidated region; overlapping rectangles),
		///	 or NULLREGION (no invalidated region).
		/// If the function fails, the return value is ERROR. To get extended error information, call GetLastError.
		/// </returns>
		/// <seealso>http://msdn.microsoft.com/en-us/library/windows/desktop/bb787593.aspx</seealso>
		[DllImport(LibName, EntryPoint = ScrollWindowExEntryPoint, SetLastError = true, ExactSpelling = true)]
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		[SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
		public static extern int ScrollWindowEx(
			[In] IntPtr hWnd,
			[In] int nXAmount,
			[In] int nYAmount,
			[In] ref RECT rectScrollRegion,
			[In] ref RECT rectClip,
			[In] IntPtr hrgnUpdate,
			[Out] out RECT prcUpdate,
			[In] ScrollWindowFlags flags);

		#endregion

		#region WindowFromPoint

		/// <summary>Точка входа функции WindowFromPoint.</summary>
		private const string WindowFromPointEntryPoint = "WindowFromPoint";

		/// <summary>Retrieves a handle to the window that contains the specified point.</summary>
		/// <param name="point">The point to be checked.</param>
		/// <returns>
		/// The return value is a handle to the window that contains the point.
		/// If no window exists at the given point, the return value is NULL.
		/// If the point is over a static text control, the return value is a handle to the window under the static text control.
		/// </returns>
		[DllImport(LibName, EntryPoint = WindowFromPointEntryPoint)]
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		[SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
		[SuppressMessage("Microsoft.Portability", "CA1901:PInvokeDeclarationsShouldBePortable", MessageId = "0", Justification = "Known Code Analysis bug")]
		public static extern IntPtr WindowFromPoint([In] POINT point);

		#endregion

		#region ChildWindowFromPoint

		/// <summary>Точка входа функции ChildWindowFromPoint.</summary>
		private const string ChildWindowFromPointEntryPoint = "ChildWindowFromPoint";

		/// <summary>
		/// Determines which, if any, of the child windows belonging to a parent window contains the specified point.
		/// The search is restricted to immediate child windows.
		/// Grandchildren, and deeper descendant windows are not searched.
		/// To skip certain child windows, use the <see cref="ChildWindowFromPointEx"/> function.
		/// </summary>
		/// <param name="hWndParent">A handle to the parent window.</param>
		/// <param name="point">A structure that defines the client coordinates, relative to <paramref name="hWndParent"/>, of the point to be checked.</param>
		/// <returns>
		/// The return value is a handle to the child window that contains the point, even if the child window is hidden or disabled.
		/// If the point lies outside the parent window, the return value is NULL.
		/// If the point is within the parent window but not within any child window, the return value is a handle to the parent window. 
		/// </returns>
		[DllImport(LibName, EntryPoint = ChildWindowFromPointEntryPoint)]
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		[SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
		public static extern IntPtr ChildWindowFromPoint([In] IntPtr hWndParent, [In] POINT point);

		#endregion

		#region ChildWindowFromPointEx

		/// <summary>Точка входа функции ChildWindowFromPointEx.</summary>
		private const string ChildWindowFromPointExEntryPoint = "ChildWindowFromPointEx";

		/// <summary>
		/// Determines which, if any, of the child windows belonging to the specified parent window contains the specified point.
		/// The function can ignore invisible, disabled, and transparent child windows.
		/// The search is restricted to immediate child windows.
		/// Grandchildren and deeper descendants are not searched. 
		/// </summary>
		/// <param name="hWndParent">A handle to the parent window.</param>
		/// <param name="point">A structure that defines the client coordinates (relative to <paramref name="hWndParent"/>) of the point to be checked. </param>
		/// <param name="uFlags">The child windows to be skipped (<see cref="T:Native.CWP"/>).</param>
		/// <returns>
		/// The return value is a handle to the first child window that contains the point and meets the criteria specified by <paramref name="uFlags"/>.
		/// If the point is within the parent window but not within any child window that meets the criteria, the return value is a handle to the parent window.
		/// If the point lies outside the parent window or if the function fails, the return value is NULL.
		/// </returns>
		[DllImport(LibName, EntryPoint = ChildWindowFromPointExEntryPoint)]
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		[SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
		[SuppressMessage("Microsoft.Portability", "CA1901:PInvokeDeclarationsShouldBePortable", MessageId = "1", Justification = "Known Code Analysis bug")]
		public static extern IntPtr ChildWindowFromPointEx([In] IntPtr hWndParent, [In] POINT point, [In] CWP uFlags);

		#endregion

		#region SendMessage

		/// <summary>Точка входа функции SendMessage.</summary>
		private const string SendMessageEntryPoint = "SendMessage";

		/// <summary>
		/// Sends the specified message to a window or windows. The SendMessage function calls the window procedure for the specified
		/// window and does not return until the window procedure has processed the message.
		/// </summary>
		/// <param name="hWnd">
		/// A handle to the window whose window procedure will receive the message.
		/// If this parameter is HWND_BROADCAST ((IntPtr)0xffff), the message is sent to all top-level windows in the system,
		/// including disabled or invisible unowned windows, overlapped windows, and pop-up windows; but the message is not sent to child windows.
		/// </param>
		/// <param name="msg">The message to be sent.</param>
		/// <param name="wParam">Additional message-specific information.</param>
		/// <param name="lParam">Additional message-specific information.</param>
		/// <returns>The return value specifies the result of the message processing; it depends on the message sent.</returns>
		/// <seealso>http://msdn.microsoft.com/en-us/library/windows/desktop/ms644950.aspx</seealso>
		[DllImport(LibName, EntryPoint = SendMessageEntryPoint, CharSet = CharSet.Auto)]
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		[SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
		public static extern IntPtr SendMessage([In] IntPtr hWnd, [In] WM msg, [In] IntPtr wParam, [In] IntPtr lParam);

		#endregion

		#region PostMessage

		/// <summary>Точка входа функции PostMessage.</summary>
		private const string PostMessageEntryPoint = "PostMessage";

		/// <summary>
		/// Places (posts) a message in the message queue associated with the thread that created the
		/// specified window and returns without waiting for the thread to process the message.
		/// </summary>
		/// <param name="hWnd">
		/// A handle to the window whose window procedure is to receive the message. The following values have special meanings.
		///		((IntPtr)0xffff): The message is posted to all top-level windows in the system, including disabled or invisible unowned windows,
		///						  overlapped windows, and pop-up windows. The message is not posted to child windows.
		///		IntPtr.Zero: The function behaves like a call to PostThreadMessage with the dwThreadId parameter set to the identifier of the current thread.
		/// </param>
		/// <param name="msg">The message to be sent.</param>
		/// <param name="wParam">Additional message-specific information.</param>
		/// <param name="lParam">Additional message-specific information.</param>
		/// <returns>
		/// If the function succeeds, the return value is <c>true</c>.
		/// If the function fails, the return value is <c>false</c>.
		/// To get extended error information, call GetLastError.
		/// GetLastError returns ERROR_NOT_ENOUGH_QUOTA when the limit is hit. 
		/// </returns>
		/// <seealso>http://msdn.microsoft.com/en-us/library/windows/desktop/ms644950.aspx</seealso>
		[DllImport(LibName, EntryPoint = PostMessageEntryPoint, CharSet = CharSet.Auto, SetLastError = true)]
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		[SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool PostMessage([In] IntPtr hWnd, [In] WM msg, [In] IntPtr wParam, [In] IntPtr lParam);

		#endregion

		#region SetWindowLongPtr

		/// <summary>
		/// Changes an attribute of the specified window.
		/// The function also sets a value at the specified offset in the extra window memory.
		/// </summary>
		/// <param name="hWnd">
		/// A handle to the window and, indirectly, the class to which the window belongs.
		/// The <see cref="SetWindowLongPtr"/> function fails if the process that owns the window specified by the hWnd
		/// parameter is at a higher process privilege in the UIPI hierarchy than the process the calling thread resides in.
		/// </param>
		/// <param name="nIndex">
		/// The zero-based offset to the value to be set.
		/// Valid values are in the range zero through the number of bytes of extra window memory, minus the size of an integer.
		/// To set any other value, specify one of the following values:
		/// <see cref="M:Constants.GWL_EXSTYLE"/>
		/// <see cref="M:Constants.GWLP_HINSTANCE"/>
		/// <see cref="M:Constants.GWLP_ID"/>
		/// <see cref="M:Constants.GWL_STYLE"/>
		/// <see cref="M:Constants.GWLP_USERDATA"/>
		/// <see cref="M:Constants.GWLP_WNDPROC"/>
		/// </param>
		/// <param name="dwNewLong">The replacement value.</param>
		/// <returns>
		/// If the function succeeds, the return value is the previous value of the specified offset.
		/// If the function fails, the return value is zero.
		/// </returns>
		/// <remarks>
		/// Certain window data is cached, so changes you make using <see cref="SetWindowLongPtr"/> will not take effect
		/// until you call the <see cref="SetWindowPos"/> function.
		/// </remarks>
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		public static IntPtr SetWindowLongPtr(IntPtr hWnd, int nIndex, IntPtr dwNewLong)
		{
			if(IntPtr.Size == 8)
			{
				return _SetWindowLongPtr(hWnd, nIndex, dwNewLong);
			}
			else
			{
				return new IntPtr(_SetWindowLong(hWnd, nIndex, dwNewLong.ToInt32()));
			}
		}

		[DllImport(LibName, EntryPoint = "SetWindowLong")]
		[SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
		[SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist")]
		private static extern int _SetWindowLong([In] IntPtr hWnd, [In] int nIndex, [In] int dwNewLong);

		[DllImport(LibName, EntryPoint = "SetWindowLongPtr")]
		[SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
		[SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist")]
		private static extern IntPtr _SetWindowLongPtr([In] IntPtr hWnd, [In] int nIndex, [In] IntPtr dwNewLong);

		#endregion

		#region GetWindowLongPtr

		/// <summary>
		/// Retrieves information about the specified window.
		/// The function also retrieves the value at a specified offset into the extra window memory. 
		/// </summary>
		/// <param name="hWnd">A handle to the window and, indirectly, the class to which the window belongs.</param>
		/// <param name="nIndex">
		/// The zero-based offset to the value to be retrieved.
		/// Valid values are in the range zero through the number of bytes of extra window memory, minus the size of an integer.
		/// To retrieve any other value, specify one of the following values:
		/// <see cref="M:Constants.GWL_EXSTYLE"/>
		/// <see cref="M:Constants.GWLP_HINSTANCE"/>
		/// <see cref="M:Constants.GWLP_ID"/>
		/// <see cref="M:Constants.GWL_STYLE"/>
		/// <see cref="M:Constants.GWLP_USERDATA"/>
		/// <see cref="M:Constants.GWLP_WNDPROC"/>
		/// </param>
		/// <returns>
		/// If the function succeeds, the return value is the requested value.
		/// If the function fails, the return value is zero.
		/// </returns>
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		public static IntPtr GetWindowLongPtr(IntPtr hWnd, int nIndex)
		{
			if(IntPtr.Size == 8)
			{
				return _GetWindowLongPtr(hWnd, nIndex);
			}
			else
			{
				return new IntPtr(_GetWindowLong(hWnd, nIndex));
			}
		}

		[DllImport(LibName, EntryPoint = "GetWindowLong")]
		[SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
		[SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist")]
		private static extern int _GetWindowLong([In] IntPtr hWnd, [In] int nIndex);

		[DllImport(LibName, EntryPoint = "GetWindowLongPtr")]
		[SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
		[SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist")]
		private static extern IntPtr _GetWindowLongPtr([In] IntPtr hWnd, [In] int nIndex);

		#endregion

		#region GetWindowRect

		/// <summary>
		/// Retrieves the dimensions of the bounding rectangle of the specified window.
		/// The dimensions are given in screen coordinates that are relative to the upper-left corner of the screen.
		/// </summary>
		/// <param name="hWnd">A handle to the window.</param>
		/// <param name="lpRect">
		/// A pointer to a RECT structure that receives the screen coordinates of the upper-left and lower-right corners of the window.
		/// </param>
		/// <returns>
		/// If the function succeeds, the return value is <c>true</c>.
		/// If the function fails, the return value is <c>false</c>.
		/// To get extended error information, call GetLastError. 
		/// </returns>
		[DllImport(LibName, EntryPoint="GetWindowRect", SetLastError=true)]
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		[SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool GetWindowRect([In] IntPtr hWnd, [Out] out RECT lpRect);

		#endregion

		#region GetClientRect

		/// <summary>
		/// Retrieves the coordinates of a window's client area.
		/// The client coordinates specify the upper-left and lower-right corners of the client area.
		/// Because client coordinates are relative to the upper-left corner of a window's client area,
		/// the coordinates of the upper-left corner are (0,0). 
		/// </summary>
		/// <param name="hWnd">A handle to the window.</param>
		/// <param name="lpRect">
		/// A pointer to a <see cref="RECT"/> structure that receives the client coordinates.
		/// The left and top members are zero.
		/// The right and bottom members contain the width and height of the window.
		/// </param>
		/// <returns></returns>
		[DllImport(LibName, EntryPoint="GetClientRect", SetLastError=true)]
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		[SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool GetClientRect([In] IntPtr hWnd, [Out] out RECT lpRect);

		#endregion

		#region SetWindowPos

		/// <summary>
		/// Changes the size, position, and Z order of a child, pop-up, or top-level window.
		/// These windows are ordered according to their appearance on the screen.
		/// The topmost window receives the highest rank and is the first window in the Z order.
		/// </summary>
		/// <param name="hWnd">A handle to the window.</param>
		/// <param name="hWndInsertAfter">
		/// A handle to the window to precede the positioned window in the Z order.
		/// This parameter must be a window handle or one of the following values:
		/// <see cref="M:Constants.HWND_BOTTOM"/>
		/// <see cref="M:Constants.HWND_NOTOPMOST"/>
		/// <see cref="M:Constants.HWND_TOP"/>
		/// <see cref="M:Constants.HWND_TOPMOST"/>
		/// </param>
		/// <param name="x">The new position of the left side of the window, in client coordinates.</param>
		/// <param name="y">The new position of the top of the window, in client coordinates.</param>
		/// <param name="cx">The new width of the window, in pixels.</param>
		/// <param name="cy">The new height of the window, in pixels.</param>
		/// <param name="uFlags">The window sizing and positioning flags.</param>
		/// <returns>
		/// If the function succeeds, the return value is <c>true</c>.
		/// If the function fails, the return value is <c>false</c>.
		/// </returns>
		[DllImport(LibName, EntryPoint = "SetWindowPos", SetLastError = true)]
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		[SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool SetWindowPos(
			[In] IntPtr hWnd, [In] IntPtr hWndInsertAfter,
			[In] int x, [In] int y, [In] int cx, [In] int cy,
			[In] SWP uFlags);

		#endregion

		#region SetForegroundWindow

		/// <summary>Точка входа функции SetForegroundWindow.</summary>
		private const string SetForegroundWindowEntryPoint = "SetForegroundWindow";

		/// <summary>
		/// Brings the thread that created the specified window into the foreground and activates the window.
		/// Keyboard input is directed to the window, and various visual cues are changed for the user.
		/// The system assigns a slightly higher priority to the thread that created the foreground window than it does to other threads. 
		/// </summary>
		/// <param name="hWnd">A handle to the window that should be activated and brought to the foreground.</param>
		/// <returns>
		/// If the window was brought to the foreground, the return value is nonzero.
		/// If the window was not brought to the foreground, the return value is zero.
		/// </returns>
		[DllImport(LibName, EntryPoint = SetForegroundWindowEntryPoint)]
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		[SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool SetForegroundWindow([In] IntPtr hWnd);

		#endregion

		#region BringWindowToTop

		/// <summary>Точка входа функции BringWindowToTop.</summary>
		private const string BringWindowToTopEntryPoint = "BringWindowToTop";

		/// <summary>
		/// Brings the specified window to the top of the Z order.
		/// If the window is a top-level window, it is activated.
		/// If the window is a child window, the top-level parent window associated with the child window is activated. 
		/// </summary>
		/// <param name="hWnd">A handle to the window to bring to the top of the Z order.</param>
		/// <returns>
		/// If the function succeeds, the return value is nonzero.
		/// If the function fails, the return value is zero. To get extended error information, call GetLastError.
		/// </returns>
		[DllImport(LibName, EntryPoint = BringWindowToTopEntryPoint, SetLastError = true)]
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		[SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool BringWindowToTop([In] IntPtr hWnd);

		#endregion

		#region RegisterWindowMessage

		private const string RegisterWindowMessageEntryPoint = "RegisterWindowMessageW";

		/// <summary>
		/// Defines a new window message that is guaranteed to be unique throughout the system.
		/// The message value can be used when sending or posting messages.
		/// </summary>
		/// <param name="lpString">The message to be registered.</param>
		/// <returns>
		/// If the message is successfully registered, the return value is a message identifier in the range 0xC000 through 0xFFFF.
		/// If the function fails, the return value is zero. To get extended error information, call GetLastError().
		/// </returns>
		/// <remarks>
		/// The RegisterWindowMessage function is typically used to register messages for communicating between two cooperating applications.
		/// If two different applications register the same message string, the applications return the same message value.
		/// The message remains registered until the session ends.
		/// Only use RegisterWindowMessage when more than one application must process the same message.
		/// For sending private messages within a window class, an application can use any integer in the range WM_USER through 0x7FFF.
		/// (Messages in this range are private to a window class, not to an application. For example, predefined control classes such as
		/// BUTTON, EDIT, LISTBOX, and COMBOBOX may use values in this range.) 
		/// </remarks>
		[DllImport(LibName, EntryPoint = RegisterWindowMessageEntryPoint, SetLastError = true, CharSet = CharSet.Unicode)]
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		[SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
		public static extern int RegisterWindowMessage([MarshalAs(UnmanagedType.LPWStr)][In] string lpString);

		#endregion

		#region GetForegroundWindow

		/// <summary>
		/// Retrieves a handle to the foreground window (the window with which the user is currently working).
		/// The system assigns a slightly higher priority to the thread that creates the foreground window than it does to other threads.
		/// </summary>
		/// <returns>
		/// The return value is a handle to the foreground window.
		/// The foreground window can be <c>IntPtr.Zero</c> in certain circumstances, such as when a window is losing activation. 
		/// </returns>
		[DllImport(LibName, ExactSpelling = true)]
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		[SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
		public static extern IntPtr GetForegroundWindow();

		#endregion

		#region GetActiveWindow

		/// <summary>
		/// Retrieves the window handle to the active window attached to the calling thread's message queue.
		/// </summary>
		/// <returns>
		/// The return value is the handle to the active window attached to the calling thread's message queue.
		/// Otherwise, the return value is <c>IntPtr.Zero</c>.
		/// </returns>
		[DllImport(LibName, ExactSpelling = true)]
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		[SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
		public static extern IntPtr GetActiveWindow();

		#endregion

		#region FlashWindowEx

		/// <summary>Flashes the specified window. It does not change the active state of the window.</summary>
		/// <param name="pwfi">A pointer to a <see cref="T:FLASHWINFO"/> structure.</param>
		/// <returns>
		/// The return value specifies the window's state before the call to the FlashWindowEx function.
		/// If the window caption was drawn as active before the call, the return value is nonzero.
		/// Otherwise, the return value is zero.
		/// </returns>
		/// <remarks>
		/// Typically, you flash a window to inform the user that the window requires attention but does not currently have the keyboard focus.
		/// When a window flashes, it appears to change from inactive to active status. An inactive caption bar changes to an active caption bar;
		/// an active caption bar changes to an inactive caption bar.
		/// </remarks>
		[DllImport(LibName)]
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		[SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool FlashWindowEx([In] ref FLASHWINFO pwfi);

		#endregion

		#region MonitorFromWindow

		/// <summary>
		/// Retrieves a handle to the display monitor that has the largest area of intersection
		/// with the bounding rectangle of a specified window.
		/// </summary>
		/// <param name="hwnd">A handle to the window of interest.</param>
		/// <param name="dwFlags">Determines the function's return value if the window does not intersect any display monitor.</param>
		/// <returns></returns>
		[DllImport(LibName)]
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		[SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
		public static extern IntPtr MonitorFromWindow([In] IntPtr hwnd, [In] MONITOR dwFlags);

		#endregion

		#region GetMonitorInfo

		/// <summary>Retrieves information about a display monitor.</summary>
		/// <param name="hMonitor">A handle to the display monitor of interest.</param>
		/// <param name="lpmi">A pointer to a <see cref="MONITORINFO"/> structure that receives information about the specified display monitor.</param>
		/// <returns>
		/// If the function succeeds, the return value is <c>true</c>.
		/// If the function fails, the return value is <c>false</c>.
		/// </returns>
		[DllImport(LibName)]
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		[SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool GetMonitorInfo([In] IntPtr hMonitor, [In, Out] ref MONITORINFO lpmi);

		#endregion

		#region LockWorkStation

		/// <summary>Locks the workstation's display. Locking a workstation protects it from unauthorized use.</summary>
		/// <returns>
		/// If the function succeeds, the return value is <c>true</c>.
		/// Because the function executes asynchronously, a <c>true</c> return value indicates that the operation has been initiated.
		/// It does not indicate whether the workstation has been successfully locked.
		/// </returns>
		[DllImport(LibName, EntryPoint = "LockWorkStation")]
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		[SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool LockWorkStation();

		#endregion

		#region BeginPaint

		/// <summary>
		/// The BeginPaint function prepares the specified window for painting and fills a
		/// <see cref="PAINTSTRUCT"/> structure with information about the painting.
		/// </summary>
		/// <param name="hWnd">Handle to the window to be repainted.</param>
		/// <param name="ps">Pointer to the <see cref="PAINTSTRUCT"/> structure that will receive painting information.</param>
		/// <returns>
		/// If the function succeeds, the return value is the handle to a display device context for the specified window.
		/// If the function fails, the return value is <see cref="M:IntPtr.Zero"/>, indicating that no display device context is available.
		/// </returns>
		[DllImport(LibName, CharSet = CharSet.Auto)]
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		[SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
		public static extern IntPtr BeginPaint([In] IntPtr hWnd, [Out] out PAINTSTRUCT ps);

		#endregion

		#region EndPaint

		/// <summary>
		/// The EndPaint function marks the end of painting in the specified window.
		/// This function is required for each call to the <see cref="BeginPaint"/> function, but only after painting is complete.
		/// </summary>
		/// <param name="hWnd">Handle to the window that has been repainted.</param>
		/// <param name="ps">Pointer to a <see cref="PAINTSTRUCT"/> structure that contains the painting information retrieved by <see cref="BeginPaint"/>.</param>
		/// <returns>The return value is always <c>true</c>.</returns>
		[DllImport(LibName, CharSet = CharSet.Auto)]
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		[SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool EndPaint([In] IntPtr hWnd, [In] ref PAINTSTRUCT ps);

		#endregion

		#region UpdateLayeredWindow

		/// <summary>Updates the position, size, shape, content, and translucency of a layered window.</summary>
		/// <param name="hwnd">
		/// A handle to a layered window. A layered window is created by specifying WS_EX_LAYERED when creating the window with the CreateWindowEx function.
		/// Windows 8: The WS_EX_LAYERED style is supported for top-level windows and child windows.
		/// Previous Windows versions support WS_EX_LAYERED only for top-level windows.
		/// </param>
		/// <param name="hdcDst">
		/// A handle to a DC for the screen. This handle is obtained by specifying NULL when calling the function.
		/// It is used for palette color matching when the window contents are updated.
		/// If hdcDst isNULL, the default palette will be used.
		/// If hdcSrc is NULL, hdcDst must be NULL.
		/// </param>
		/// <param name="pptDst">
		/// A pointer to a structure that specifies the new screen position of the layered window.
		/// If the current position is not changing, pptDst can be NULL.
		/// </param>
		/// <param name="psize">
		/// A pointer to a structure that specifies the new size of the layered window.
		/// If the size of the window is not changing, psize can be NULL.
		/// If hdcSrc is NULL, psize must be NULL.
		/// </param>
		/// <param name="hdcSrc">
		/// A handle to a DC for the surface that defines the layered window.
		/// This handle can be obtained by calling the CreateCompatibleDC function.
		/// If the shape and visual context of the window are not changing, hdcSrc can be NULL. 
		/// </param>
		/// <param name="pprSrc">
		/// A pointer to a structure that specifies the location of the layer in the device context.
		/// If hdcSrc is NULL, pptSrc should be NULL. 
		/// </param>
		/// <param name="crKey">
		/// A structure that specifies the color key to be used when composing the layered window.
		/// To generate a COLORREF, use the RGB macro. 
		/// </param>
		/// <param name="pblend">
		/// A pointer to a structure that specifies the transparency value to be used when composing the layered window. 
		/// </param>
		/// <param name="dwFlags"><see cref="T:ULW"/>.</param>
		/// <returns>
		/// If the function succeeds, the return value is nonzero.
		/// If the function fails, the return value is zero.
		/// To get extended error information, call GetLastError. 
		/// </returns>
		[DllImport(LibName, CharSet = CharSet.Auto)]
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		[SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool UpdateLayeredWindow(
			[In]     IntPtr        hwnd,
			[In]     IntPtr        hdcDst,
			[In] ref POINT         pptDst,
			[In] ref SIZE          psize,
			[In]     IntPtr        hdcSrc,
			[In] ref POINT         pprSrc,
			[In]     int           crKey,
			[In] ref BLENDFUNCTION pblend,
			[In]     ULW           dwFlags);

		#endregion

		#region AnimateWindow

		/// <summary>
		/// Enables you to produce special effects when showing or hiding windows.
		/// There are four types of animation: roll, slide, collapse or expand, and alpha-blended fade.
		/// </summary>
		/// <param name="hWnd">
		/// A handle to the window to animate.
		/// The calling thread must own this window.
		/// </param>
		/// <param name="dwTime">
		/// The time it takes to play the animation, in milliseconds.
		/// Typically, an animation takes 200 milliseconds to play.
		/// </param>
		/// <param name="dwFlags">
		/// The type of animation. Note that, by default, these flags take effect when showing a window.
		/// To take effect when hiding a window, use <see cref="M:AW.HIDE"/> and a logical OR operator with the appropriate flags. 
		/// </param>
		/// <returns>
		/// If the function succeeds, the return value is <c>true</c>.
		/// If the function fails, the return value is <c>false</c>.
		/// </returns>
		[DllImport(LibName, CharSet = CharSet.Auto)]
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		[SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool AnimateWindow([In] IntPtr hWnd, [In] uint dwTime, [In] AW dwFlags);

		#endregion

		#region RedrawWindow

		/// <summary>
		/// Updates the specified rectangle or region in a window's client area.
		/// </summary>
		/// <param name="hWnd">
		/// A handle to the window to be redrawn.
		/// If this parameter is IntPtr.Zero, the desktop window is updated.
		/// </param>
		/// <param name="lprcUpdate">
		/// A pointer to a RECT structure containing the coordinates, in device units, of the update rectangle.
		/// This parameter is ignored if the <paramref name="hrgnUpdate"/> parameter identifies a region.
		/// </param>
		/// <param name="hrgnUpdate">
		/// A handle to the update region. If both the <paramref name="hrgnUpdate"/> and <paramref name="lprcUpdate"/> parameters are IntPtr.Zero,
		/// the entire client area is added to the update region.
		/// </param>
		/// <param name="flags">
		/// One or more redraw flags. This parameter can be used to invalidate or validate a window,
		/// control repainting, and control which windows are affected by RedrawWindow().
		/// </param>
		/// <returns>
		/// If the function succeeds, the return value is nonzero.
		/// If the function fails, the return value is zero.
		/// </returns>
		[DllImport(LibName, CharSet = CharSet.Auto)]
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		[SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool RedrawWindow([In] IntPtr hWnd, [In] ref RECT lprcUpdate, [In] IntPtr hrgnUpdate, [In] RDW flags);

		/// <summary>
		/// Updates the specified rectangle or region in a window's client area.
		/// </summary>
		/// <param name="hWnd">
		/// A handle to the window to be redrawn.
		/// If this parameter is IntPtr.Zero, the desktop window is updated.
		/// </param>
		/// <param name="lprcUpdate">
		/// A pointer to a RECT structure containing the coordinates, in device units, of the update rectangle.
		/// This parameter is ignored if the <paramref name="hrgnUpdate"/> parameter identifies a region.
		/// </param>
		/// <param name="hrgnUpdate">
		/// A handle to the update region. If both the <paramref name="hrgnUpdate"/> and <paramref name="lprcUpdate"/> parameters are IntPtr.Zero,
		/// the entire client area is added to the update region.
		/// </param>
		/// <param name="flags">
		/// One or more redraw flags. This parameter can be used to invalidate or validate a window,
		/// control repainting, and control which windows are affected by RedrawWindow().
		/// </param>
		/// <returns>
		/// If the function succeeds, the return value is nonzero.
		/// If the function fails, the return value is zero.
		/// </returns>
		[DllImport(LibName, CharSet = CharSet.Auto)]
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		[SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool RedrawWindow([In] IntPtr hWnd, [In] IntPtr lprcUpdate, [In] IntPtr hrgnUpdate, [In] RDW flags);

		#endregion

		#region GetParent

		/// <summary>
		/// Retrieves a handle to the specified window's parent or owner.
		/// To retrieve a handle to a specified ancestor, use the GetAncestor function.
		/// </summary>
		/// <param name="hWnd">A handle to the window whose parent window handle is to be retrieved.</param>
		/// <returns>
		/// If the window is a child window, the return value is a handle to the parent window.
		/// If the window is a top-level window with the WS_POPUP style, the return value is a handle to the owner window.
		/// If the function fails, the return value is NULL.
		/// </returns>
		[DllImport(LibName, CharSet = CharSet.Auto)]
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		[SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
		public static extern IntPtr GetParent(IntPtr hWnd);

		#endregion

		#region FillRect

		/// <summary>
		/// The FillRect function fills a rectangle by using the specified brush.
		/// This function includes the left and top borders, but excludes the right and bottom borders
		/// of the rectangle.
		/// </summary>
		/// <param name="hDC">A handle to the device context.</param>
		/// <param name="lprc">A pointer to a <see cref="RECT"/> structure that contains the logical coordinates of the rectangle to be filled.</param>
		/// <param name="hbr">A handle to the brush used to fill the rectangle.</param>
		/// <returns>
		/// If the function succeeds, the return value is nonzero.
		/// If the function fails, the return value is zero.
		/// </returns>
		/// <seealso>https://msdn.microsoft.com/en-us/library/windows/desktop/dd162719%28v=vs.85%29.aspx</seealso>
		[DllImport(LibName)]
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		[SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
		public static extern int FillRect([In] IntPtr hDC, [In] ref RECT lprc, [In] IntPtr hbr);

		#endregion

		#region MapWindowPoints

		const string MapWindowPointsEntryPoint = "MapWindowPoints";

		/// <summary>
		/// The MapWindowPoints function converts (maps) a set of points from a coordinate
		/// space relative to one window to a coordinate space relative to another window.
		/// </summary>
		/// <param name="hWndFrom">
		/// A handle to the window from which points are converted.
		/// If this parameter is NULL or HWND_DESKTOP, the points are presumed
		/// to be in screen coordinates.
		/// </param>
		/// <param name="hWndTo">
		/// A handle to the window to which points are converted. If this parameter is NULL or HWND_DESKTOP,
		/// the points are converted to screen coordinates.
		/// </param>
		/// <param name="lpPoints">
		/// A pointer to an array of POINT structures that contain the set of points
		/// to be converted. The points are in device units. This parameter can also
		/// point to a RECT structure, in which case the cPoints parameter should be set to 2.
		/// </param>
		/// <param name="cPoints">
		/// The number of POINT structures in the array pointed to by the lpPoints parameter.
		/// </param>
		/// <returns>
		/// If the function succeeds, the low-order word of the return value is the number of
		/// pixels added to the horizontal coordinate of each source point in order to compute
		/// the horizontal coordinate of each destination point. (In addition to that,
		/// if precisely one of hWndFrom and hWndTo is mirrored, then each resulting horizontal
		/// coordinate is multiplied by -1.) The high-order word is the number of pixels added
		/// to the vertical coordinate of each source point in order to compute the vertical
		/// coordinate of each destination point.
		/// </returns>
		[DllImport(LibName, EntryPoint = MapWindowPointsEntryPoint, SetLastError = true)]
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		[SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
		public static extern int MapWindowPoints(
			[In]      IntPtr  hWndFrom,
			[In]      IntPtr  hWndTo,
			[In, Out] POINT[] lpPoints,
			[In]      int     cPoints);

		/// <summary>
		/// The MapWindowPoints function converts (maps) a set of points from a coordinate
		/// space relative to one window to a coordinate space relative to another window.
		/// </summary>
		/// <param name="hWndFrom">
		/// A handle to the window from which points are converted.
		/// If this parameter is NULL or HWND_DESKTOP, the points are presumed
		/// to be in screen coordinates.
		/// </param>
		/// <param name="hWndTo">
		/// A handle to the window to which points are converted. If this parameter is NULL or HWND_DESKTOP,
		/// the points are converted to screen coordinates.
		/// </param>
		/// <param name="lpPoints">
		/// A pointer to an array of POINT structures that contain the set of points
		/// to be converted. The points are in device units. This parameter can also
		/// point to a RECT structure, in which case the cPoints parameter should be set to 2.
		/// </param>
		/// <param name="cPoints">
		/// The number of POINT structures in the array pointed to by the lpPoints parameter.
		/// </param>
		/// <returns>
		/// If the function succeeds, the low-order word of the return value is the number of
		/// pixels added to the horizontal coordinate of each source point in order to compute
		/// the horizontal coordinate of each destination point. (In addition to that,
		/// if precisely one of hWndFrom and hWndTo is mirrored, then each resulting horizontal
		/// coordinate is multiplied by -1.) The high-order word is the number of pixels added
		/// to the vertical coordinate of each source point in order to compute the vertical
		/// coordinate of each destination point.
		/// </returns>
		[DllImport(LibName, EntryPoint = MapWindowPointsEntryPoint, SetLastError = true)]
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		[SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
		public static extern int MapWindowPoints(
			[In]      IntPtr    hWndFrom,
			[In]      IntPtr    hWndTo,
			[In, Out] ref POINT lpPoints,
			[In]      int       cPoints);

		#endregion
	}

	/// <summary>Функции библиотеки dwmapi.dll.</summary>
	[SuppressUnmanagedCodeSecurity]
	static partial class Dwmapi
	{
		/// <summary>Имя библиотеки.</summary>
		private const string LibName = "dwmapi.dll";

		#region DwmSetWindowAttribute

		/// <summary>Sets the value of non-client rendering attributes for a window.</summary>
		/// <param name="hwnd">The handle to the window that will receive the attributes.</param>
		/// <param name="dwAttribute">
		/// A single <see cref="T:DWMWA"/> flag to apply to the window.
		/// This parameter specifies the attribute and the <paramref name="pvAttribute"/>
		/// parameter points to the value of that attribute.
		/// </param>
		/// <param name="pvAttribute">
		/// A pointer to the value of the attribute specified in the <paramref name="dwAttribute"/> parameter.
		/// Different <see cref="T:DWMWA"/> flags require different value types.
		/// </param>
		/// <param name="cbAttribute">
		/// The size, in bytes, of the value type pointed to by the <paramref name="pvAttribute"/> parameter.
		/// </param>
		/// <returns>If this function succeeds, it returns S_OK. Otherwise, it returns an HRESULT error code.</returns>
		/// <remarks>If Desktop Composition has been disabled, this function returns DWM_E_COMPOSITIONDISABLED.</remarks>
		[DllImport(LibName, PreserveSig = true)]
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		[SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
		public static extern int DwmSetWindowAttribute(
			[In] IntPtr hwnd,
			[In] DWMWA dwAttribute,
			[In] IntPtr pvAttribute,
			[In] int cbAttribute);

		/// <summary>Sets the value of non-client rendering attributes for a window.</summary>
		/// <param name="hwnd">The handle to the window that will receive the attributes.</param>
		/// <param name="dwAttribute">
		/// A single <see cref="T:DWMWA"/> flag to apply to the window.
		/// This parameter specifies the attribute and the <paramref name="pvAttribute"/>
		/// parameter points to the value of that attribute.
		/// </param>
		/// <param name="pvAttribute">
		/// A pointer to the value of the attribute specified in the <paramref name="dwAttribute"/> parameter.
		/// Different <see cref="T:DWMWA"/> flags require different value types.
		/// </param>
		/// <param name="cbAttribute">
		/// The size, in bytes, of the value type pointed to by the <paramref name="pvAttribute"/> parameter.
		/// </param>
		/// <returns>If this function succeeds, it returns S_OK. Otherwise, it returns an HRESULT error code.</returns>
		/// <remarks>If Desktop Composition has been disabled, this function returns DWM_E_COMPOSITIONDISABLED.</remarks>
		[DllImport(LibName, PreserveSig = true)]
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		[SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
		public static extern int DwmSetWindowAttribute(
			[In] IntPtr hwnd,
			[In] DWMWA dwAttribute,
			[In] ref int pvAttribute,
			[In] int cbAttribute);

		/// <summary>Sets the value of non-client rendering attributes for a window.</summary>
		/// <param name="hwnd">The handle to the window that will receive the attributes.</param>
		/// <param name="dwAttribute">
		/// A single <see cref="T:DWMWA"/> flag to apply to the window.
		/// This parameter specifies the attribute and the <paramref name="pvAttribute"/>
		/// parameter points to the value of that attribute.
		/// </param>
		/// <param name="pvAttribute">
		/// A pointer to the value of the attribute specified in the <paramref name="dwAttribute"/> parameter.
		/// Different <see cref="T:DWMWA"/> flags require different value types.
		/// </param>
		/// <param name="cbAttribute">
		/// The size, in bytes, of the value type pointed to by the <paramref name="pvAttribute"/> parameter.
		/// </param>
		/// <returns>If this function succeeds, it returns S_OK. Otherwise, it returns an HRESULT error code.</returns>
		/// <remarks>If Desktop Composition has been disabled, this function returns DWM_E_COMPOSITIONDISABLED.</remarks>
		[DllImport(LibName, PreserveSig = true)]
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		[SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
		public static extern int DwmSetWindowAttribute(
			[In] IntPtr hwnd,
			[In] DWMWA dwAttribute,
			[MarshalAs(UnmanagedType.Bool)] [In] ref bool pvAttribute,
			[In] int cbAttribute);

		/// <summary>Sets non-client rendering policy.</summary>
		/// <param name="hwnd">The handle to the window that will receive the attributes.</param>
		/// <param name="renderingPolicy">Non-client rendering policy.</param>
		/// <returns>If this function succeeds, it returns S_OK. Otherwise, it returns an HRESULT error code.</returns>
		/// <remarks>If Desktop Composition has been disabled, this function returns DWM_E_COMPOSITIONDISABLED.</remarks>
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		public static int DwmSetWindowAttribute(IntPtr hwnd, DWMNCRP renderingPolicy)
		{
			var value = (int)renderingPolicy;
			return DwmSetWindowAttribute(hwnd, DWMWA.NCRENDERING_POLICY, ref value, 4);
		}

		/// <summary>Sets Flip3D window behavior.</summary>
		/// <param name="hwnd">The handle to the window that will receive the attributes.</param>
		/// <param name="flip3DPolicy">Flip3D window behavior.</param>
		/// <returns>If this function succeeds, it returns S_OK. Otherwise, it returns an HRESULT error code.</returns>
		/// <remarks>If Desktop Composition has been disabled, this function returns DWM_E_COMPOSITIONDISABLED.</remarks>
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		public static int DwmSetWindowAttribute(IntPtr hwnd, DWMFLIP3D flip3DPolicy)
		{
			var value = (int)flip3DPolicy;
			return DwmSetWindowAttribute(hwnd, DWMWA.FLIP3D_POLICY, ref value, 4);
		}

		/// <summary>Sets boolean window attribute.</summary>
		/// <param name="hwnd">The handle to the window that will receive the attributes.</param>
		/// <param name="dwAttribute">
		/// A single <see cref="T:DWMWA"/> flag to apply to the window.
		/// This parameter specifies the attribute and the <paramref name="value"/>
		/// parameter specifies the value of that attribute.
		/// </param>
		/// <param name="value">Attribute value.</param>
		/// <returns>If this function succeeds, it returns S_OK. Otherwise, it returns an HRESULT error code.</returns>
		/// <remarks>If Desktop Composition has been disabled, this function returns DWM_E_COMPOSITIONDISABLED.</remarks>
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		public static int DwmSetWindowAttribute(IntPtr hwnd, DWMWA dwAttribute, bool value)
		{
			return DwmSetWindowAttribute(hwnd, dwAttribute, ref value, 1);
		}

		#endregion

		#region DwmGetWindowAttribute

		/// <summary>Retrieves the current value of a specified attribute applied to a window.</summary>
		/// <param name="hwnd">The handle to the window from which the attribute data is retrieved.</param>
		/// <param name="dwAttribute">The attribute to retrieve, specified as a <see cref="T:DWMWA"/> value.</param>
		/// <param name="pvAttribute">
		/// A pointer to a value that, when this function returns successfully, receives the current
		/// value of the attribute. The type of the retrieved value depends on the value of
		/// the <paramref name="dwAttribute"/> parameter.
		/// </param>
		/// <param name="cbAttribute">
		/// The size of the <see cref="T:DWMWA"/> value being retrieved.
		/// The size is dependent on the type of the <paramref name="pvAttribute"/> parameter.
		/// </param>
		/// <returns>If this function succeeds, it returns S_OK. Otherwise, it returns an HRESULT error code.</returns>
		[DllImport(LibName, PreserveSig = true)]
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		[SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
		public static extern int DwmGetWindowAttribute(
			[In] IntPtr hwnd,
			[In] DWMWA dwAttribute,
			[In] IntPtr pvAttribute,
			[In] uint cbAttribute);

		/// <summary>Retrieves the current value of a specified attribute applied to a window.</summary>
		/// <param name="hwnd">The handle to the window from which the attribute data is retrieved.</param>
		/// <param name="dwAttribute">The attribute to retrieve, specified as a <see cref="T:DWMWA"/> value.</param>
		/// <param name="pvAttribute">
		/// A pointer to a value that, when this function returns successfully, receives the current
		/// value of the attribute. The type of the retrieved value depends on the value of
		/// the <paramref name="dwAttribute"/> parameter.
		/// </param>
		/// <param name="cbAttribute">
		/// The size of the <see cref="T:DWMWA"/> value being retrieved.
		/// The size is dependent on the type of the <paramref name="pvAttribute"/> parameter.
		/// </param>
		/// <returns>If this function succeeds, it returns S_OK. Otherwise, it returns an HRESULT error code.</returns>
		[DllImport(LibName, PreserveSig = true)]
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		[SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
		public static extern int DwmGetWindowAttribute(
			[In] IntPtr hwnd,
			[In] DWMWA dwAttribute,
			[Out] out int pvAttribute,
			[In] uint cbAttribute);

		/// <summary>Retrieves the current value of a specified attribute applied to a window.</summary>
		/// <param name="hwnd">The handle to the window from which the attribute data is retrieved.</param>
		/// <param name="dwAttribute">The attribute to retrieve, specified as a <see cref="T:DWMWA"/> value.</param>
		/// <param name="pvAttribute">
		/// A pointer to a value that, when this function returns successfully, receives the current
		/// value of the attribute. The type of the retrieved value depends on the value of
		/// the <paramref name="dwAttribute"/> parameter.
		/// </param>
		/// <param name="cbAttribute">
		/// The size of the <see cref="T:DWMWA"/> value being retrieved.
		/// The size is dependent on the type of the <paramref name="pvAttribute"/> parameter.
		/// </param>
		/// <returns>If this function succeeds, it returns S_OK. Otherwise, it returns an HRESULT error code.</returns>
		[DllImport(LibName, PreserveSig = true)]
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		[SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
		public static extern int DwmGetWindowAttribute(
			[In] IntPtr hwnd,
			[In] DWMWA dwAttribute,
			[MarshalAs(UnmanagedType.Bool)] [Out] out bool pvAttribute,
			[In] uint cbAttribute);

		#endregion

		#region DwmExtendFrameIntoClientArea

		/// <summary>Extends the window frame into the client area.</summary>
		/// <param name="hWnd">The handle to the window in which the frame will be extended into the client area.</param>
		/// <param name="pMarInset">
		/// A reference to a <see cref="T:MARGINS"/> structure that describes the margins to use
		/// when extending the frame into the client area.
		/// </param>
		/// <returns>If this function succeeds, it returns S_OK. Otherwise, it returns an HRESULT error code.</returns>
		/// <remarks>
		/// This function must be called whenever Desktop Window Manager (DWM) composition is toggled.
		/// Handle the <see cref="M:WM.DWMCOMPOSITIONCHANGED"/> message for composition change notification.
		/// Use negative margin values to create the "sheet of glass" effect where the client area is rendered as
		/// a solid surface with no window border.
		/// </remarks>
		[DllImport(LibName, PreserveSig = true)]
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		[SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
		public static extern int DwmExtendFrameIntoClientArea(
			[In] IntPtr hWnd,
			[In] ref MARGINS pMarInset);

		#endregion

		#region DwmEnableBlurBehindWindow

		/// <summary>Enables the blur effect on a specified window.</summary>
		/// <param name="hWnd">The handle to the window on which the blur behind data is applied.</param>
		/// <param name="pBlurBehind">
		/// A reference to a <see cref="T:DWM_BLURBEHIND"/> structure that provides blur behind data.
		/// </param>
		/// <returns>If this function succeeds, it returns S_OK. Otherwise, it returns an HRESULT error code.</returns>
		[DllImport(LibName, PreserveSig = true)]
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		[SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
		public static extern int DwmEnableBlurBehindWindow(
			[In] IntPtr hWnd,
			[In] ref DWM_BLURBEHIND pBlurBehind);

		#endregion

		#region DwmEnableComposition

		/// <summary>Enables or disables Desktop Window Manager (DWM) composition.</summary>
		/// <param name="uCompositionAction">
		/// <see cref="M:DWM_EC.ENABLECOMPOSITION"/> to enable DWM composition;
		/// <see cref="M:DWM_EC.DISABLECOMPOSITION"/> to disable composition. 
		/// </param>
		/// <returns>If this function succeeds, it returns S_OK. Otherwise, it returns an HRESULT error code.</returns>
		/// <remarks>This function is deprecated as of Windows 8. DWM can no longer be programmatically disabled.</remarks>
		[DllImport(LibName, PreserveSig = true)]
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		[SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
		public static extern int DwmEnableComposition(
			[In] DWM_EC uCompositionAction);

		#endregion

		#region DwmIsCompositionEnabled

		/// <summary>
		/// Obtains a value that indicates whether Desktop Window Manager (DWM) composition is enabled.
		/// Applications can listen for composition state changes by handling the <see cref="M:WM.DWMCOMPOSITIONCHANGED"/> notification.
		/// </summary>
		/// <param name="pfEnabled">
		/// A pointer to a value that, when this function returns successfully, receives <c>true</c> if DWM
		/// composition is enabled; otherwise, <c>false</c>.
		/// </param>
		/// <returns>If this function succeeds, it returns S_OK. Otherwise, it returns an HRESULT error code.</returns>
		[DllImport(LibName, PreserveSig = true)]
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		[SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
		public static extern int DwmIsCompositionEnabled(
			[MarshalAs(UnmanagedType.Bool)] [Out] out bool pfEnabled);

		#endregion
	}

	#region Enums

	/// <summary>Растровые операции.</summary>
	enum RasterOperation
	{
		/// <summary>dest = source</summary>
		SRCCOPY = 0x00CC0020,
		/// <summary>dest = source OR dest</summary>
		SRCPAINT = 0x00EE0086,
		/// <summary>dest = source AND dest</summary>
		SRCAND = 0x008800C6,
		/// <summary>dest = source XOR dest</summary>
		SRCINVERT = 0x00660046,
		/// <summary>dest = source AND (NOT dest)</summary>
		SRCERASE = 0x00440328,
		/// <summary>dest = (NOT source)</summary>
		NOTSRCCOPY = 0x00330008,
		/// <summary>dest = (NOT src) AND (NOT dest)</summary>
		NOTSRCERASE = 0x001100A6,
		/// <summary>dest = (source AND pattern)</summary>
		MERGECOPY = 0x00C000CA,
		/// <summary>dest = (NOT source) OR dest</summary>
		MERGEPAINT = 0x00BB0226,
		/// <summary>dest = pattern</summary>
		PATCOPY = 0x00F00021,
		/// <summary>dest = DPSnoo</summary>
		PATPAINT = 0x00FB0A09,
		/// <summary>dest = pattern XOR dest</summary>
		PATINVERT = 0x005A0049,
		/// <summary>dest = (NOT dest)</summary>
		DSTINVERT = 0x00550009,
		/// <summary>dest = BLACK</summary>
		BLACKNESS = 0x00000042,
		/// <summary>dest = WHITE</summary>
		WHITENESS = 0x00FF0062,
	}

	/// <summary>Тип сжатия DIB.</summary>
	enum DibCompression : uint
	{
		/// <summary>An uncompressed format.</summary>
		RGB = 0,
		/// <summary>
		/// A run-length encoded (RLE) format for bitmaps with 8 bpp.
		/// The compression format is a 2-byte format consisting of a count byte followed by a byte containing a color index. 
		/// </summary>
		RLE8 = 1,
		/// <summary>
		/// An RLE format for bitmaps with 4 bpp. The compression format is a 2-byte format consisting of a count byte
		/// followed by two word-length color indexes. 
		/// </summary>
		RLE4 = 2,
		/// <summary>
		/// Specifies that the bitmap is not compressed and that the color table consists of three DWORD color masks that specify the red, green,
		/// and blue components, respectively, of each pixel. This is valid when used with 16- and 32-bpp bitmaps.
		/// </summary>
		BITFIELDS = 3,
		/// <summary>Indicates that the image is a JPEG image.</summary>
		JPEG = 4,
		/// <summary>Indicates that the image is a PNG image.</summary>
		PNG = 5,
	}

	/// <summary>Определяет тип изображения (с палитрой/без).</summary>
	enum DibUsage : uint
	{
		/// <summary>Color table in RGBs.</summary>
		DIB_RGB_COLORS = 0,
		/// <summary>Color table in palette indices.</summary>
		DIB_PAL_COLORS = 1,
	}

	/// <summary>Определяет способ растяжения/сжатия изображения.</summary>
	enum StretchBltMode
	{
		#region Invalid

		/// <summary>Invalid value.</summary>
		INVALID = 0,
		/// <summary>One or more of the input parameters is invalid.</summary>
		INVALID_PARAMETER = 87,

		#endregion

		/// <summary>
		/// Performs a Boolean AND operation using the color values for the eliminated and existing pixels.
		/// If the bitmap is a monochrome bitmap, this mode preserves black pixels at the expense of white pixels.
		/// </summary>
		BLACKONWHITE = 1,
		/// <summary>
		/// Performs a Boolean OR operation using the color values for the eliminated and existing pixels.
		/// If the bitmap is a monochrome bitmap, this mode preserves white pixels at the expense of black pixels.
		/// </summary>
		WHITEONBLACK = 2,
		/// <summary>
		/// Deletes the pixels. This mode deletes all eliminated lines of pixels without trying to preserve their information.
		/// </summary>
		COLORONCOLOR = 3,
		/// <summary>
		/// Maps pixels from the source rectangle into blocks of pixels in the destination rectangle.
		/// The average color over the destination block of pixels approximates the color of the source pixels.
		/// After setting the HALFTONE stretching mode, an application must call the SetBrushOrgEx function to set the brush origin.
		/// If it fails to do so, brush misalignment occurs.
		/// </summary>
		HALFTONE = 4,
	}

	/// <summary>Флаги для функции ScrollWindowEx.</summary>
	enum ScrollWindowFlags
	{
		/// <summary>
		/// Scroll children within *lprcScroll.
		/// </summary>
		SCROLLCHILDREN = 0x0001,
		/// <summary>
		/// Invalidate after scrolling.
		/// </summary>
		INVALIDATE = 0x0002,
		/// <summary>
		/// If SW_INVALIDATE, don't send <see cref="M:WM.ERASEBACKGROUND"/>.
		/// </summary>
		ERASE = 0x0004,
		/// <summary>
		/// Use smooth scrolling.
		/// </summary>
		SMOOTHSCROLL = 0x0010,
	}

	/// <summary>Опции для <see cref="User32.ShowWindow"/>.</summary>
	enum SW
	{
		/// <summary>
		/// Minimizes a window, even if the thread that owns the window is not responding.
		/// This flag should only be used when minimizing windows from a different thread.
		/// </summary>
		FORCEMINIMIZE = 11,
		/// <summary>Hides the window and activates another window.</summary>
		HIDE = 0,
		/// <summary>Maximizes the specified window.</summary>
		MAXIMIZE = 3,
		/// <summary>Minimizes the specified window and activates the next top-level window in the Z order.</summary>
		MINIMIZE = 6,
		/// <summary>
		/// Activates and displays the window.
		/// If the window is minimized or maximized, the system restores it to its original size and position.
		/// An application should specify this flag when restoring a minimized window.
		/// </summary>
		RESTORE = 9,
		/// <summary>
		/// Activates the window and displays it in its current size and position.
		/// </summary>
		SHOW = 5,
		/// <summary>
		/// Sets the show state based on the SW_ value specified in the STARTUPINFO structure passed to the CreateProcess
		/// function by the program that started the application. 
		/// </summary>
		SHOWDEFAULT = 10,
		/// <summary>
		/// Activates the window and displays it as a maximized window.
		/// </summary>
		SHOWMAXIMIZED = 3,
		/// <summary>
		/// Activates the window and displays it as a minimized window.
		/// </summary>
		SHOWMINIMIZED = 2,
		/// <summary>
		/// Displays the window as a minimized window.
		/// This value is similar to <see cref="SHOWMINIMIZED"/>, except the window is not activated.
		/// </summary>
		SHOWMINNOACTIVE = 7,
		/// <summary>
		/// Displays the window in its current size and position.
		/// This value is similar to <see cref="SHOW"/>, except that the window is not activated.
		/// </summary>
		SHOWNA = 8,
		/// <summary>
		/// Displays a window in its most recent size and position.
		/// This value is similar to <see cref="SHOWNORMAL"/>, except that the window is not activated.
		/// </summary>
		SHOWNOACTIVATE = 4,
		/// <summary>
		/// Activates and displays a window.
		/// If the window is minimized or maximized, the system restores it to its original size and position.
		/// An application should specify this flag when displaying the window for the first time.
		/// </summary>
		SHOWNORMAL = 1,
	}

	/// <summary>Windows message.</summary>
	enum WM
	{
		NULL = 0x0000,
		CREATE = 0x0001,
		DESTROY = 0x0002,
		MOVE = 0x0003,
		SIZE = 0x0005,
		ACTIVATE = 0x0006,
		SETFOCUS = 0x0007,
		KILLFOCUS = 0x0008,
		ENABLE = 0x000A,
		SETREDRAW = 0x000B,
		SETTEXT = 0x000C,
		GETTEXT = 0x000D,
		GETTEXTLENGTH = 0x000E,
		PAINT = 0x000F,
		CLOSE = 0x0010,
		QUERYENDSESSION = 0x0011,
		QUERYOPEN = 0x0013,
		ENDSESSION = 0x0016,
		QUIT = 0x0012,
		ERASEBKGND = 0x0014,
		SYSCOLORCHANGE = 0x0015,
		SHOWWINDOW = 0x0018,
		WININICHANGE = 0x001A,
		SETTINGCHANGE = WININICHANGE,
		DEVMODECHANGE = 0x001B,
		ACTIVATEAPP = 0x001C,
		FONTCHANGE = 0x001D,
		TIMECHANGE = 0x001E,
		CANCELMODE = 0x001F,
		SETCURSOR = 0x0020,
		MOUSEACTIVATE = 0x0021,
		CHILDACTIVATE = 0x0022,
		QUEUESYNC = 0x0023,
		GETMINMAXINFO = 0x0024,
		PAINTICON = 0x0026,
		ICONERASEBKGND = 0x0027,
		NEXTDLGCTL = 0x0028,
		SPOOLERSTATUS = 0x002A,
		DRAWITEM = 0x002B,
		MEASUREITEM = 0x002C,
		DELETEITEM = 0x002D,
		VKEYTOITEM = 0x002E,
		CHARTOITEM = 0x002F,
		SETFONT = 0x0030,
		GETFONT = 0x0031,
		SETHOTKEY = 0x0032,
		GETHOTKEY = 0x0033,
		QUERYDRAGICON = 0x0037,
		COMPAREITEM = 0x0039,
		GETOBJECT = 0x003D,
		COMPACTING = 0x0041,
		COMMNOTIFY = 0x0044,
		WINDOWPOSCHANGING = 0x0046,
		WINDOWPOSCHANGED = 0x0047,
		POWER = 0x0048,
		COPYDATA = 0x004A,
		CANCELJOURNAL = 0x004B,
		NOTIFY = 0x004E,
		INPUTLANGCHANGEREQUEST = 0x0050,
		INPUTLANGCHANGE = 0x0051,
		TCARD = 0x0052,
		HELP = 0x0053,
		USERCHANGED = 0x0054,
		NOTIFYFORMAT = 0x0055,
		CONTEXTMENU = 0x007B,
		STYLECHANGING = 0x007C,
		STYLECHANGED = 0x007D,
		DISPLAYCHANGE = 0x007E,
		GETICON = 0x007F,
		SETICON = 0x0080,
		NCCREATE = 0x0081,
		NCDESTROY = 0x0082,
		NCCALCSIZE = 0x0083,
		NCHITTEST = 0x0084,
		NCPAINT = 0x0085,
		NCACTIVATE = 0x0086,
		GETDLGCODE = 0x0087,
		SYNCPAINT = 0x0088,

		NCMOUSEMOVE = 0x00A0,
		NCLBUTTONDOWN = 0x00A1,
		NCLBUTTONUP = 0x00A2,
		NCLBUTTONDBLCLK = 0x00A3,
		NCRBUTTONDOWN = 0x00A4,
		NCRBUTTONUP = 0x00A5,
		NCRBUTTONDBLCLK = 0x00A6,
		NCMBUTTONDOWN = 0x00A7,
		NCMBUTTONUP = 0x00A8,
		NCMBUTTONDBLCLK = 0x00A9,
		NCXBUTTONDOWN = 0x00AB,
		NCXBUTTONUP = 0x00AC,
		NCXBUTTONDBLCLK = 0x00AD,

		INPUT_DEVICE_CHANGE = 0x00FE,
		INPUT = 0x00FF,

		KEYFIRST = 0x0100,
		KEYDOWN = 0x0100,
		KEYUP = 0x0101,
		CHAR = 0x0102,
		DEADCHAR = 0x0103,
		SYSKEYDOWN = 0x0104,
		SYSKEYUP = 0x0105,
		SYSCHAR = 0x0106,
		SYSDEADCHAR = 0x0107,
		UNICHAR = 0x0109,
		KEYLAST = 0x0109,

		IME_STARTCOMPOSITION = 0x010D,
		IME_ENDCOMPOSITION = 0x010E,
		IME_COMPOSITION = 0x010F,
		IME_KEYLAST = 0x010F,

		INITDIALOG = 0x0110,
		COMMAND = 0x0111,
		SYSCOMMAND = 0x0112,
		TIMER = 0x0113,
		HSCROLL = 0x0114,
		VSCROLL = 0x0115,
		INITMENU = 0x0116,
		INITMENUPOPUP = 0x0117,
		MENUSELECT = 0x011F,
		MENUCHAR = 0x0120,
		ENTERIDLE = 0x0121,
		MENURBUTTONUP = 0x0122,
		MENUDRAG = 0x0123,
		MENUGETOBJECT = 0x0124,
		UNINITMENUPOPUP = 0x0125,
		MENUCOMMAND = 0x0126,

		CHANGEUISTATE = 0x0127,
		UPDATEUISTATE = 0x0128,
		QUERYUISTATE = 0x0129,

		CTLCOLORMSGBOX = 0x0132,
		CTLCOLOREDIT = 0x0133,
		CTLCOLORLISTBOX = 0x0134,
		CTLCOLORBTN = 0x0135,
		CTLCOLORDLG = 0x0136,
		CTLCOLORSCROLLBAR = 0x0137,
		CTLCOLORSTATIC = 0x0138,
		GETHMENU = 0x01E1,

		MOUSEFIRST = 0x0200,
		MOUSEMOVE = 0x0200,
		LBUTTONDOWN = 0x0201,
		LBUTTONUP = 0x0202,
		LBUTTONDBLCLK = 0x0203,
		RBUTTONDOWN = 0x0204,
		RBUTTONUP = 0x0205,
		RBUTTONDBLCLK = 0x0206,
		MBUTTONDOWN = 0x0207,
		MBUTTONUP = 0x0208,
		MBUTTONDBLCLK = 0x0209,
		MOUSEWHEEL = 0x020A,
		XBUTTONDOWN = 0x020B,
		XBUTTONUP = 0x020C,
		XBUTTONDBLCLK = 0x020D,
		MOUSEHWHEEL = 0x020E,

		PARENTNOTIFY = 0x0210,
		ENTERMENULOOP = 0x0211,
		EXITMENULOOP = 0x0212,

		NEXTMENU = 0x0213,
		SIZING = 0x0214,
		CAPTURECHANGED = 0x0215,
		MOVING = 0x0216,

		POWERBROADCAST = 0x0218,

		DEVICECHANGE = 0x0219,

		MDICREATE = 0x0220,
		MDIDESTROY = 0x0221,
		MDIACTIVATE = 0x0222,
		MDIRESTORE = 0x0223,
		MDINEXT = 0x0224,
		MDIMAXIMIZE = 0x0225,
		MDITILE = 0x0226,
		MDICASCADE = 0x0227,
		MDIICONARRANGE = 0x0228,
		MDIGETACTIVE = 0x0229,

		MDISETMENU = 0x0230,
		ENTERSIZEMOVE = 0x0231,
		EXITSIZEMOVE = 0x0232,
		DROPFILES = 0x0233,
		MDIREFRESHMENU = 0x0234,

		IME_SETCONTEXT = 0x0281,
		IME_NOTIFY = 0x0282,
		IME_CONTROL = 0x0283,
		IME_COMPOSITIONFULL = 0x0284,
		IME_SELECT = 0x0285,
		IME_CHAR = 0x0286,
		IME_REQUEST = 0x0288,
		IME_KEYDOWN = 0x0290,
		IME_KEYUP = 0x0291,

		POINTERDEVICECHANGE = 0x0238,
		POINTERDEVICEINRANGE = 0x0239,
		POINTERDEVICEOUTOFRANGE = 0x023A,
		NCPOINTERUPDATE = 0x0241,
		NCPOINTERDOWN = 0x0242,
		NCPOINTERUP = 0x0243,
		POINTERUPDATE = 0x0245,
		POINTERDOWN = 0x0246,
		POINTERUP = 0x0247,
		POINTERENTER = 0x0249,
		POINTERLEAVE = 0x024A,
		POINTERACTIVATE = 0x024B,
		POINTERCAPTURECHANGED = 0x024C,
		TOUCHHITTESTING = 0x024D,
		POINTERWHEEL = 0x024E,
		POINTERHWHEEL = 0x024F,

		MOUSEHOVER = 0x02A1,
		MOUSELEAVE = 0x02A3,
		NCMOUSEHOVER = 0x02A0,
		NCMOUSELEAVE = 0x02A2,

		WTSSESSION_CHANGE = 0x02B1,

		TABLET_FIRST = 0x02c0,
		TABLET_FLICK = TABLET_FIRST + 11,
		TABLET_QUERYSYSTEMGESTURESTATUS = TABLET_FIRST + 12,
		TABLET_LAST = 0x02df,

		CUT = 0x0300,
		COPY = 0x0301,
		PASTE = 0x0302,
		CLEAR = 0x0303,
		UNDO = 0x0304,
		RENDERFORMAT = 0x0305,
		RENDERALLFORMATS = 0x0306,
		DESTROYCLIPBOARD = 0x0307,
		DRAWCLIPBOARD = 0x0308,
		PAINTCLIPBOARD = 0x0309,
		VSCROLLCLIPBOARD = 0x030A,
		SIZECLIPBOARD = 0x030B,
		ASKCBFORMATNAME = 0x030C,
		CHANGECBCHAIN = 0x030D,
		HSCROLLCLIPBOARD = 0x030E,
		QUERYNEWPALETTE = 0x030F,
		PALETTEISCHANGING = 0x0310,
		PALETTECHANGED = 0x0311,
		HOTKEY = 0x0312,

		PRINT = 0x0317,
		PRINTCLIENT = 0x0318,

		APPCOMMAND = 0x0319,

		THEMECHANGED = 0x031A,

		CLIPBOARDUPDATE = 0x031D,

		DWMCOMPOSITIONCHANGED = 0x031E,
		DWMNCRENDERINGCHANGED = 0x031F,
		DWMCOLORIZATIONCOLORCHANGED = 0x0320,
		DWMWINDOWMAXIMIZEDCHANGE = 0x0321,

		GETTITLEBARINFOEX = 0x033F,

		HANDHELDFIRST = 0x0358,
		HANDHELDLAST = 0x035F,

		AFXFIRST = 0x0360,
		AFXLAST = 0x037F,

		PENWINFIRST = 0x0380,
		PENWINLAST = 0x038F,

		APP = 0x8000,

		USER = 0x0400,

		REFLECT = USER + 0x1C00,
	}

	/// <summary>Window class styles.</summary>
	/// <seealso>http://msdn.microsoft.com/en-us/library/windows/desktop/ff729176.aspx</seealso>
	[Flags]
	enum CS
	{
		/// <summary>
		/// Aligns the window's client area on a byte boundary (in the x direction).
		/// This style affects the width of the window and its horizontal placement on the display.
		/// </summary>
		BYTEALIGNCLIENT = 0x1000,
		/// <summary>
		/// Aligns the window on a byte boundary (in the x direction).
		/// This style affects the width of the window and its horizontal placement on the display.
		/// </summary>
		BYTEALIGNWINDOW = 0x2000,
		/// <summary>
		/// Allocates one device context to be shared by all windows in the class.
		/// Because window classes are process specific, it is possible for multiple threads of an application to create a window of the same class.
		/// It is also possible for the threads to attempt to use the device context simultaneously.
		/// When this happens, the system allows only one thread to successfully finish its drawing operation. 
		/// </summary>
		CLASSDC = 0x0040,
		/// <summary>
		/// Sends a double-click message to the window procedure when the user double-clicks the mouse while the cursor is within a window belonging to the class.
		/// </summary>
		DBLCLKS = 0x0008,
		/// <summary>
		/// Enables the drop shadow effect on a window. The effect is turned on and off through SPI_SETDROPSHADOW.
		/// Typically, this is enabled for small, short-lived windows such as menus to emphasize their Z order relationship to other windows.
		/// </summary>
		DROPSHADOW = 0x00020000,
		/// <summary>
		/// Indicates that the window class is an application global class.
		/// For more information, see the "Application Global Classes" section of About Window Classes.
		/// </summary>
		GLOBALCLASS = 0x4000,
		/// <summary>
		/// Redraws the entire window if a movement or size adjustment changes the width of the client area.
		/// </summary>
		HREDRAW = 0x0002,
		/// <summary>
		/// Disables Close on the window menu.
		/// </summary>
		NOCLOSE = 0x0200,
		/// <summary>
		/// Allocates a unique device context for each window in the class.
		/// </summary>
		OWNDC = 0x0020,
		/// <summary>
		/// Sets the clipping rectangle of the child window to that of the parent window so that the child can draw on the parent.
		/// A window with the <see cref="PARENTDC"/> style bit receives a regular device context from the system's cache of device contexts.
		/// It does not give the child the parent's device context or device context settings.
		/// Specifying <see cref="PARENTDC"/> enhances an application's performance. 
		/// </summary>
		PARENTDC = 0x0080,
		/// <summary>
		/// Saves, as a bitmap, the portion of the screen image obscured by a window of this class.
		/// When the window is removed, the system uses the saved bitmap to restore the screen image, including other windows that were obscured.
		/// Therefore, the system does not send <see cref="M:WM.PAINT"/> messages to windows that were obscured if the memory used by the bitmap
		/// has not been discarded and if other screen actions have not invalidated the stored image. 
		/// </summary>
		SAVEBITS = 0x0800,
		/// <summary>
		/// Redraws the entire window if a movement or size adjustment changes the height of the client area.
		/// </summary>
		VREDRAW = 0x0001,
	}

	/// <summary>Window styles.</summary>
	/// <seealso>http://msdn.microsoft.com/en-us/library/windows/desktop/ms632600.aspx</seealso>
	[Flags]
	enum WS : uint
	{
		/// <summary>The window has a thin-line border.</summary>
		BORDER = 0x00800000,
		/// <summary>The window has a title bar (includes the <see cref="BORDER"/> style).</summary>
		CAPTION = 0x00C00000,
		/// <summary>
		/// The window is a child window. A window with this style cannot have a menu bar.
		/// This style cannot be used with the <see cref="POPUP"/> style.
		/// </summary>
		CHILD = 0x40000000,
		/// <summary>Same as <see cref="CHILD"/> style.</summary>
		CHILDWINDOW = 0x40000000,
		/// <summary>
		/// Excludes the area occupied by child windows when drawing occurs within the parent window.
		/// This style is used when creating the parent window.
		/// </summary>
		CLIPCHILDREN = 0x02000000,
		/// <summary>
		/// Clips child windows relative to each other; that is, when a particular child window receives a
		/// <see cref="M:WM.PAINT"/> message, the <see cref="CLIPSIBLINGS"/> style clips all other overlapping child
		/// windows out of the region of the child window to be updated.
		/// If <see cref="CLIPSIBLINGS"/> is not specified and child windows overlap, it is possible, when drawing within
		/// the client area of a child window, to draw within the client area of a neighboring child window.
		/// </summary>
		CLIPSIBLINGS = 0x04000000,
		/// <summary>
		/// The window is initially disabled. A disabled window cannot receive input from the user.
		/// To change this after a window has been created, use the EnableWindow function.
		/// </summary>
		DISABLED = 0x08000000,
		/// <summary>
		/// The window has a border of a style typically used with dialog boxes. A window with this style cannot have a title bar.
		/// </summary>
		DLGFRAME = 0x00400000,
		/// <summary>
		/// The window is the first control of a group of controls. The group consists of this first control and all controls defined after it,
		/// up to the next control with the <see cref="GROUP"/> style. The first control in each group usually has the <see cref="TABSTOP"/> style
		/// so that the user can move from group to group.
		/// The user can subsequently change the keyboard focus from one control in the group to the next control in the group by using the direction keys.
		/// </summary>
		GROUP = 0x00020000,
		/// <summary>The window has a horizontal scroll bar.</summary>
		HSCROLL = 0x00100000,
		/// <summary>The window is initially minimized. Same as the <see cref="MINIMIZE"/> style.</summary>
		ICONIC = 0x20000000,
		/// <summary>The window is initially maximized.</summary>
		MAXIMIZE = 0x01000000,
		/// <summary>
		/// The window has a maximize button. Cannot be combined with the <see cref="M:WS_EX.CONTEXTHELP"/> style.
		/// The <see cref="SYSMENU"/> style must also be specified. 
		/// </summary>
		MAXIMIZEBOX = 0x00010000,
		/// <summary>
		/// The window is initially minimized. Same as the <see cref="ICONIC"/> style.
		/// </summary>
		MINIMIZE = 0x20000000,
		/// <summary>
		/// The window has a minimize button. Cannot be combined with the <see cref="M:WS_EX.CONTEXTHELP"/> style.
		/// The <see cref="SYSMENU"/> style must also be specified. 
		/// </summary>
		MINIMIZEBOX = 0x00020000,
		/// <summary>
		/// The window is an overlapped window. An overlapped window has a title bar and a border.
		/// Same as the <see cref="TILED"/> style.
		/// </summary>
		OVERLAPPED = 0x00000000,
		/// <summary>
		/// The window is an overlapped window. Same as the <see cref="TILEDWINDOW"/> style.
		/// </summary>
		OVERLAPPEDWINDOW = (OVERLAPPED | CAPTION | SYSMENU | THICKFRAME | MINIMIZEBOX | MAXIMIZEBOX),
		/// <summary>
		/// The windows is a pop-up window. This style cannot be used with the <see cref="CHILD"/> style.
		/// </summary>
		POPUP = 0x80000000,
		/// <summary>
		/// The window is a pop-up window. The <see cref="CAPTION"/> and <see cref="POPUPWINDOW"/> styles
		/// must be combined to make the window menu visible.
		/// </summary>
		POPUPWINDOW = (POPUP | BORDER | SYSMENU),
		/// <summary>
		/// The window has a sizing border. Same as the <see cref="THICKFRAME"/> style.
		/// </summary>
		SIZEBOX = 0x00040000,
		/// <summary>
		/// The window has a window menu on its title bar. The <see cref="CAPTION"/> style must also be specified.
		/// </summary>
		SYSMENU = 0x00080000,
		/// <summary>
		/// The window is a control that can receive the keyboard focus when the user presses the TAB key.
		/// Pressing the TAB key changes the keyboard focus to the next control with the <see cref="TABSTOP"/> style.
		/// </summary>
		TABSTOP = 0x00010000,
		/// <summary>
		/// The window has a sizing border. Same as the <see cref="SIZEBOX"/> style.
		/// </summary>
		THICKFRAME = 0x00040000,
		/// <summary>
		/// The window is an overlapped window. An overlapped window has a title bar and a border.
		/// Same as the <see cref="OVERLAPPED"/> style. 
		/// </summary>
		TILED = 0x00000000,
		/// <summary>
		/// The window is an overlapped window. Same as the <see cref="OVERLAPPEDWINDOW"/> style.
		/// </summary>
		TILEDWINDOW = (OVERLAPPED | CAPTION | SYSMENU | THICKFRAME | MINIMIZEBOX | MAXIMIZEBOX),
		/// <summary>
		/// The window is initially visible.
		/// This style can be turned on and off by using the ShowWindow or SetWindowPos function.
		/// </summary>
		VISIBLE = 0x10000000,
		/// <summary>The window has a vertical scroll bar.</summary>
		VSCROLL = 0x00200000,
	}

	/// <summary>Extended window styles.</summary>
	[Flags]
	enum WS_EX : uint
	{
		/// <summary>The window accepts drag-drop files.</summary>
		ACCEPTFILES = 0x00000010,
		/// <summary>Forces a top-level window onto the taskbar when the window is visible.</summary>
		APPWINDOW = 0x00040000,
		/// <summary>The window has a border with a sunken edge.</summary>
		CLIENTEDGE = 0x00000200,
		/// <summary>
		/// Paints all descendants of a window in bottom-to-top painting order using double-buffering.
		/// This cannot be used if the window has a class style of either CS_OWNDC or CS_CLASSDC.
		/// </summary>
		/// <remarks>Windows 2000:  This style is not supported.</remarks>
		COMPOSITED = 0x02000000,
		/// <summary>
		/// The title bar of the window includes a question mark.
		/// When the user clicks the question mark, the cursor changes to a question mark with a pointer.
		/// If the user then clicks a child window, the child receives a <see cref="M:WM.HELP"/> message.
		/// The child window should pass the message to the parent window procedure, which should call the WinHelp function using the HELP_WM_HELP command.
		/// The Help application displays a pop-up window that typically contains help for the child window.
		/// <see cref="CONTEXTHELP"/> cannot be used with the <see cref="M:WS.MAXIMIZEBOX"/> or  <see cref="M:WS.MINIMIZEBOX"/> styles.
		/// </summary>
		CONTEXTHELP = 0x00000400,
		/// <summary>
		/// The window itself contains child windows that should take part in dialog box navigation.
		/// If this style is specified, the dialog manager recurses into children of this window when performing
		/// navigation operations such as handling the TAB key, an arrow key, or a keyboard mnemonic.
		/// </summary>
		CONTROLPARENT = 0x00010000,
		/// <summary>
		/// The window has a double border; the window can, optionally, be created with a title bar
		/// by specifying the <see cref="M:WS.CAPTION"/> style in the dwStyle parameter.
		/// </summary>
		DLGMODALFRAME = 0x00000001,
		/// <summary>
		/// The window is a layered window.
		/// This style cannot be used if the window has a class style of either <see cref="M:CS.OWNDC"/> or <see cref="M:CS.CLASSDC"/>.
		/// </summary>
		LAYERED = 0x00080000,
		/// <summary>
		/// If the shell language is Hebrew, Arabic, or another language that supports reading order alignment,
		/// the horizontal origin of the window is on the right edge.
		/// Increasing horizontal values advance to the left. 
		/// </summary>
		LAYOUTRTL = 0x00400000,
		/// <summary>The window has generic left-aligned properties. This is the default.</summary>
		LEFT = 0x00000000,
		/// <summary>
		/// If the shell language is Hebrew, Arabic, or another language that supports reading order alignment,
		/// the vertical scroll bar (if present) is to the left of the client area.
		/// For other languages, the style is ignored.
		/// </summary>
		LEFTSCROLLBAR = 0x00004000,
		/// <summary>The window text is displayed using left-to-right reading-order properties. This is the default.</summary>
		LTRREADING = 0x00000000,
		/// <summary>The window is a MDI child window.</summary>
		MDICHILD = 0x00000040,
		/// <summary>
		/// A top-level window created with this style does not become the foreground window when the user clicks it.
		/// The system does not bring this window to the foreground when the user minimizes or closes the foreground window.
		/// </summary>
		NOACTIVATE = 0x08000000,
		/// <summary>The window does not pass its window layout to its child windows.</summary>
		NOINHERITLAYOUT = 0x00100000,
		/// <summary>
		/// The child window created with this style does not send the <see cref="WM.PARENTNOTIFY"/> message
		/// to its parent window when it is created or destroyed.
		/// </summary>
		NOPARENTNOTIFY = 0x00000004,
		/// <summary>
		/// The window does not render to a redirection surface. This is for windows that do not have visible content
		/// or that use mechanisms other than surfaces to provide their visual.
		/// </summary>
		NOREDIRECTIONBITMAP = 0x00200000,
		/// <summary>The window is an overlapped window.</summary>
		OVERLAPPEDWINDOW = (WINDOWEDGE | CLIENTEDGE),
		/// <summary>The window is palette window, which is a modeless dialog box that presents an array of commands.</summary>
		PALETTEWINDOW = (WINDOWEDGE | TOOLWINDOW | TOPMOST),
		/// <summary>
		/// The window has generic "right-aligned" properties. This depends on the window class.
		/// This style has an effect only if the shell language is Hebrew, Arabic, or another language that supports
		/// reading-order alignment; otherwise, the style is ignored.
		/// </summary>
		RIGHT = 0x00001000,
		/// <summary>The vertical scroll bar (if present) is to the right of the client area. This is the default.</summary>
		RIGHTSCROLLBAR = 0x00000000,
		/// <summary>
		/// If the shell language is Hebrew, Arabic, or another language that supports reading-order alignment,
		/// the window text is displayed using right-to-left reading-order properties.
		/// For other languages, the style is ignored.
		/// </summary>
		RTLREADING = 0x00002000,
		/// <summary>
		/// The window has a three-dimensional border style intended to be used for items that do not accept user input.
		/// </summary>
		STATICEDGE = 0x00020000,
		/// <summary>
		/// The window is intended to be used as a floating toolbar. A tool window has a title bar that is shorter than
		/// a normal title bar, and the window title is drawn using a smaller font. A tool window does not appear
		/// in the taskbar or in the dialog that appears when the user presses ALT+TAB. If a tool window has a system menu,
		/// its icon is not displayed on the title bar. However, you can display the system menu by right-clicking or by typing ALT+SPACE. 
		/// </summary>
		TOOLWINDOW = 0x00000080,
		/// <summary>
		/// The window should be placed above all non-topmost windows and should stay above them, even when the window is deactivated.
		/// To add or remove this style, use the SetWindowPos function.
		/// </summary>
		TOPMOST = 0x00000008,
		/// <summary>
		/// The window should not be painted until siblings beneath the window (that were created by the same thread) have been painted.
		/// The window appears transparent because the bits of underlying sibling windows have already been painted.
		/// To achieve transparency without these restrictions, use the SetWindowRgn function.
		/// </summary>
		TRANSPARENT = 0x00000020,
		/// <summary>The window has a border with a raised edge.</summary>
		WINDOWEDGE = 0x00000100,
	}

	/// <summary>Флаги для функции <see cref="User32.ChildWindowFromPointEx"/>.</summary>
	[Flags]
	enum CWP
	{
		/// <summary>Does not skip any child windows.</summary>
		ALL = 0x0000,
		/// <summary>Skips disabled child windows.</summary>
		SKIPDISABLED = 0x0002,
		/// <summary>Skips invisible child windows.</summary>
		SKIPINVISIBLE = 0x0001,
		/// <summary>Skips transparent child windows.</summary>
		SKIPTRANSPARENT = 0x0004,
	}

	/// <summary>System metric.</summary>
	/// <seealso>http://msdn.microsoft.com/en-us/library/windows/desktop/ms724385.aspx</seealso>
	enum SM
	{
		/// <summary>The flags that specify how the system arranged minimized windows. </summary>
		ARRANGE = 0x38,
		/// <summary>
		/// The value that specifies how the system is started:
		///		0 = Normal boot
		///		1 = Fail-safe boot
		///		2 = Fail-safe with network boot
		/// A fail-safe boot (also called SafeBoot, Safe Mode, or Clean Boot) bypasses the user startup files.
		/// </summary>
		CLEANBOOT = 0x43,
		/// <summary>The number of display monitors on a desktop.</summary>
		CMONITORS = 80,
		/// <summary>The number of buttons on a mouse, or zero if no mouse is installed.</summary>
		CMOUSEBUTTONS = 0x2b,
		/// <summary>
		/// The width of a window border, in pixels.
		/// This is equivalent to the <see cref="CXEDGE"/> value for windows with the 3-D look.
		/// </summary>
		CXBORDER = 5,
		/// <summary>The width of a cursor, in pixels. The system cannot create cursors of other sizes.</summary>
		CXCURSOR = 13,
		/// <summary>This value is the same as <see cref="CXFIXEDFRAME"/>.</summary>
		CXDLGFRAME = 7,
		/// <summary>
		/// The width of the rectangle around the location of a first click in a double-click sequence, in pixels.
		/// The second click must occur within the rectangle that is defined by <see cref="CXDOUBLECLK"/> and <see cref="CYDOUBLECLK"/>
		/// for the system to consider the two clicks a double-click.
		/// The two clicks must also occur within a specified time. 
		/// </summary>
		CXDOUBLECLK = 0x24,
		/// <summary>
		/// The amount of border padding for captioned windows, in pixels. Windows XP/2000:  This value is not supported.
		/// </summary>
		CXPADDEDBORDER = 92,
		/// <summary>
		/// The number of pixels on either side of a mouse-down point that the mouse pointer can move before a drag operation begins.
		/// This allows the user to click and release the mouse button easily without unintentionally starting a drag operation.
		/// If this value is negative, it is subtracted from the left of the mouse-down point and added to the right of it.
		/// </summary>
		CXDRAG = 0x44,
		/// <summary>
		/// The width of a 3-D border, in pixels. This metric is the 3-D counterpart of <see cref="CXBORDER"/>.
		/// </summary>
		CXEDGE = 0x2d,
		/// <summary>
		/// The thickness of the frame around the perimeter of a window that has a caption but is not sizable, in pixels.
		/// <see cref="CXFIXEDFRAME"/> is the height of the horizontal border, and <see cref="CYFIXEDFRAME"/> is the width of the vertical border.
		/// This value is the same as SM_CXDLGFRAME.
		/// </summary>
		CXFIXEDFRAME = 7,
		/// <summary>
		/// The width of the left and right edges of the focus rectangle that the DrawFocusRect draws. This value is in pixels. 
		/// </summary>
		CXFOCUSBORDER = 0x53,
		/// <summary>
		/// This value is the same as <see cref="CXSIZEFRAME"/>.
		/// </summary>
		CXFRAME = 0x20,
		/// <summary>
		/// The width of the client area for a full-screen window on the primary display monitor, in pixels.
		/// To get the coordinates of the portion of the screen that is not obscured by the system taskbar or by application desktop toolbars,
		/// call the SystemParametersInfo function with the SPI_GETWORKAREA value.
		/// </summary>
		CXFULLSCREEN = 0x10,
		/// <summary>The width of the arrow bitmap on a horizontal scroll bar, in pixels.</summary>
		CXHSCROLL = 0x15,
		/// <summary>
		/// The width of the thumb box in a horizontal scroll bar, in pixels.
		/// </summary>
		CXHTHUMB = 10,
		/// <summary>
		/// The default width of an icon, in pixels. The LoadIcon function can load only icons with the dimensions that
		/// <see cref="CXICON"/> and <see cref="CYICON"/> specifies.
		/// </summary>
		CXICON = 11,
		/// <summary>
		/// The width of a grid cell for items in large icon view, in pixels.
		/// Each item fits into a rectangle of size <see cref="CXICONSPACING"/> by <see cref="CYICONSPACING"/> when arranged.
		/// This value is always greater than or equal to <see cref="CXICON"/>.
		/// </summary>
		CXICONSPACING = 0x26,
		/// <summary>The default width, in pixels, of a maximized top-level window on the primary display monitor.</summary>
		CXMAXIMIZED = 0x3d,
		/// <summary>
		/// The default maximum width of a window that has a caption and sizing borders, in pixels.
		/// This metric refers to the entire desktop. The user cannot drag the window frame to a size larger than these dimensions.
		/// A window can override this value by processing the <see cref="M:WM.GETMINMAXINFO"/> message.
		/// </summary>
		CXMAXTRACK = 0x3b,
		/// <summary>The width of the default menu check-mark bitmap, in pixels.</summary>
		CXMENUCHECK = 0x47,
		/// <summary>
		/// The width of menu bar buttons, such as the child window close button that is used in the multiple document interface, in pixels.
		/// </summary>
		CXMENUSIZE = 0x36,
		/// <summary>The minimum width of a window, in pixels.</summary>
		CXMIN = 0x1c,
		/// <summary>The width of a minimized window, in pixels.</summary>
		CXMINIMIZED = 0x39,
		/// <summary>
		/// The width of a grid cell for a minimized window, in pixels. Each minimized window fits into a rectangle this size when arranged.
		/// This value is always greater than or equal to <see cref="CXMINIMIZED"/>.
		/// </summary>
		CXMINSPACING = 0x2f,
		/// <summary>
		/// The minimum tracking width of a window, in pixels. The user cannot drag the window frame to a size smaller than these dimensions.
		/// A window can override this value by processing the <see cref="M:WM.GETMINMAXINFO"/> message.
		/// </summary>
		CXMINTRACK = 0x22,
		/// <summary>
		/// The width of the screen of the primary display monitor, in pixels. This is the same value obtained by calling GetDeviceCaps
		/// as follows: GetDeviceCaps( hdcPrimaryMonitor, HORZRES).
		/// </summary>
		CXSCREEN = 0,
		/// <summary>The width of a button in a window caption or title bar, in pixels.</summary>
		CXSIZE = 30,
		/// <summary>
		/// The thickness of the sizing border around the perimeter of a window that can be resized, in pixels.
		/// <see cref="CXSIZEFRAME"/> is the width of the horizontal border, and <see cref="CYSIZEFRAME"/> is the height of the vertical border.
		/// This value is the same as SM_CXFRAME.
		/// </summary>
		CXSIZEFRAME = 0x20,
		/// <summary>
		/// The recommended width of a small icon, in pixels. Small icons typically appear in window captions and in small icon view.
		/// </summary>
		CXSMICON = 0x31,
		/// <summary>The width of small caption buttons, in pixels.</summary>
		CXSMSIZE = 0x34,
		/// <summary>
		/// The width of the virtual screen, in pixels. The virtual screen is the bounding rectangle of all display monitors.
		/// The <see cref="CXVIRTUALSCREEN"/> metric is the coordinates for the left side of the virtual screen. 
		/// </summary>
		CXVIRTUALSCREEN = 0x4e,
		/// <summary>The width of a vertical scroll bar, in pixels.</summary>
		CXVSCROLL = 2,
		/// <summary>
		/// The height of a window border, in pixels. This is equivalent to the <see cref="CYEDGE"/> value for windows with the 3-D look.
		/// </summary>
		CYBORDER = 6,
		/// <summary>The height of a caption area, in pixels.</summary>
		CYCAPTION = 4,
		/// <summary>The height of a cursor, in pixels. The system cannot create cursors of other sizes.</summary>
		CYCURSOR = 14,
		/// <summary>This value is the same as <see cref="CYFIXEDFRAME"/>.</summary>
		CYDLGFRAME = 8,
		/// <summary>
		/// The height of the rectangle around the location of a first click in a double-click sequence, in pixels.
		/// The second click must occur within the rectangle defined by <see cref="CXDOUBLECLK"/> and <see cref="CYDOUBLECLK"/>
		/// for the system to consider the two clicks a double-click. The two clicks must also occur within a specified time. 
		/// </summary>
		CYDOUBLECLK = 0x25,
		/// <summary>
		/// The number of pixels above and below a mouse-down point that the mouse pointer can move before a drag operation begins.
		/// This allows the user to click and release the mouse button easily without unintentionally starting a drag operation.
		/// If this value is negative, it is subtracted from above the mouse-down point and added below it. 
		/// </summary>
		CYDRAG = 0x45,
		/// <summary>
		/// The height of a 3-D border, in pixels. This is the 3-D counterpart of <see cref="CYBORDER"/>.
		/// </summary>
		CYEDGE = 0x2e,
		/// <summary>
		/// The thickness of the frame around the perimeter of a window that has a caption but is not sizable, in pixels.
		/// <see cref="CXFIXEDFRAME"/> is the height of the horizontal border, and <see cref="CYFIXEDFRAME"/> is the width of the vertical border.
		/// This value is the same as <see cref="CYDLGFRAME"/>.
		/// </summary>
		CYFIXEDFRAME = 8,
		/// <summary>The height of the top and bottom edges of the focus rectangle drawn by DrawFocusRect. This value is in pixels.</summary>
		CYFOCUSBORDER = 0x54,
		/// <summary>
		/// This value is the same as <see cref="CYSIZEFRAME"/>.
		/// </summary>
		CYFRAME = 0x21,
		/// <summary>
		/// The height of the client area for a full-screen window on the primary display monitor, in pixels.
		/// To get the coordinates of the portion of the screen not obscured by the system taskbar or by application desktop toolbars,
		/// call the SystemParametersInfo function with the SPI_GETWORKAREA value.
		/// </summary>
		CYFULLSCREEN = 0x11,
		/// <summary>The height of a horizontal scroll bar, in pixels.</summary>
		CYHSCROLL = 3,
		/// <summary>
		/// The default height of an icon, in pixels.
		/// The LoadIcon function can load only icons with the dimensions <see cref="CXICON"/> and <see cref="CYICON"/>.
		/// </summary>
		CYICON = 12,
		/// <summary>
		/// The height of a grid cell for items in large icon view, in pixels.
		/// Each item fits into a rectangle of size <see cref="CXICONSPACING"/> by <see cref="CYICONSPACING"/> when arranged.
		/// This value is always greater than or equal to <see cref="CYICON"/>.
		/// </summary>
		CYICONSPACING = 0x27,
		/// <summary>
		/// For double byte character set versions of the system, this is the height of the Kanji window at the bottom of the screen, in pixels.
		/// </summary>
		CYKANJIWINDOW = 0x12,
		/// <summary>
		/// The default height, in pixels, of a maximized top-level window on the primary display monitor.
		/// </summary>
		CYMAXIMIZED = 0x3e,
		/// <summary>
		/// The default maximum height of a window that has a caption and sizing borders, in pixels.
		/// This metric refers to the entire desktop. The user cannot drag the window frame to a size larger than these dimensions.
		/// A window can override this value by processing the <see cref="M:WM.GETMINMAXINFO"/> message.
		/// </summary>
		CYMAXTRACK = 60,
		/// <summary>The height of a single-line menu bar, in pixels.</summary>
		CYMENU = 15,
		/// <summary>The height of the default menu check-mark bitmap, in pixels.</summary>
		CYMENUCHECK = 0x48,
		/// <summary>
		/// The height of menu bar buttons, such as the child window close button that is used in the multiple document interface, in pixels.
		/// </summary>
		CYMENUSIZE = 0x37,
		/// <summary>The minimum height of a window, in pixels.</summary>
		CYMIN = 0x1d,
		/// <summary>The height of a minimized window, in pixels.</summary>
		CYMINIMIZED = 0x3a,
		/// <summary>
		/// The height of a grid cell for a minimized window, in pixels.
		/// Each minimized window fits into a rectangle this size when arranged.
		/// This value is always greater than or equal to <see cref="CYMINIMIZED"/>.
		/// </summary>
		CYMINSPACING = 0x30,
		/// <summary>
		/// The minimum tracking height of a window, in pixels.
		/// The user cannot drag the window frame to a size smaller than these dimensions.
		/// A window can override this value by processing the <see cref="M:WM.GETMINMAXINFO"/> message.
		/// </summary>
		CYMINTRACK = 0x23,
		/// <summary>
		/// The height of the screen of the primary display monitor, in pixels.
		/// This is the same value obtained by calling GetDeviceCaps as follows: GetDeviceCaps( hdcPrimaryMonitor, VERTRES).
		/// </summary>
		CYSCREEN = 1,
		/// <summary>The height of a button in a window caption or title bar, in pixels.</summary>
		CYSIZE = 0x1f,
		/// <summary>
		/// The thickness of the sizing border around the perimeter of a window that can be resized, in pixels.
		/// <see cref="CXSIZEFRAME"/> is the width of the horizontal border, and <see cref="CYSIZEFRAME"/> is the height of the vertical border.
		/// This value is the same as <see cref="CYFRAME"/>.
		/// </summary>
		CYSIZEFRAME = 0x21,
		/// <summary>The height of a small caption, in pixels.</summary>
		CYSMCAPTION = 0x33,
		/// <summary>The recommended height of a small icon, in pixels. Small icons typically appear in window captions and in small icon view.</summary>
		CYSMICON = 50,
		/// <summary>The height of small caption buttons, in pixels.</summary>
		CYSMSIZE = 0x35,
		/// <summary>
		/// The height of the virtual screen, in pixels. The virtual screen is the bounding rectangle of all display monitors.
		/// The <see cref="YVIRTUALSCREEN"/> metric is the coordinates for the top of the virtual screen.
		/// </summary>
		CYVIRTUALSCREEN = 0x4f,
		/// <summary>The height of the arrow bitmap on a vertical scroll bar, in pixels.</summary>
		CYVSCROLL = 20,
		/// <summary>The height of the thumb box in a vertical scroll bar, in pixels.</summary>
		CYVTHUMB = 9,
		/// <summary>Nonzero if User32.dll supports DBCS; otherwise, 0.</summary>
		DBCSENABLED = 0x2a,
		/// <summary>Nonzero if the debug version of User.exe is installed; otherwise, 0.</summary>
		DEBUG = 0x16,
		/// <summary>
		/// Nonzero if the current operating system is Windows 7 or Windows Server 2008 R2 and the Tablet PC Input service is started; otherwise, 0.
		/// The return value is a bitmask that specifies the type of digitizer input supported by the device.
		/// </summary>
		DIGITIZER = 94,
		/// <summary>Nonzero if Input Method Manager/Input Method Editor features are enabled; otherwise, 0.</summary>
		IMMENABLED = 0x52,
		/// <summary>Nonzero if there are digitizers in the system; otherwise, 0.</summary>
		MAXIMUMTOUCHES = 95,
		/// <summary>Nonzero if the current operating system is the Windows XP, Media Center Edition, 0 if not.</summary>
		MEDIACENTER = 0x57,
		/// <summary>Nonzero if drop-down menus are right-aligned with the corresponding menu-bar item; 0 if the menus are left-aligned.</summary>
		MENUDROPALIGNMENT = 40,
		/// <summary>Nonzero if the system is enabled for Hebrew and Arabic languages, 0 if not.</summary>
		MIDEASTENABLED = 0x4a,
		/// <summary>
		/// Nonzero if a mouse is installed; otherwise, 0. This value is rarely zero, because of support for virtual mice
		/// and because some systems detect the presence of the port instead of the presence of a mouse.
		/// </summary>
		MOUSEPRESENT = 0x13,
		/// <summary>Nonzero if a mouse with a horizontal scroll wheel is installed; otherwise 0.</summary>
		MOUSEHORIZONTALWHEELPRESENT = 91,
		/// <summary>Nonzero if a mouse with a vertical scroll wheel is installed; otherwise 0.</summary>
		MOUSEWHEELPRESENT = 0x4b,
		/// <summary>
		/// The least significant bit is set if a network is present; otherwise, it is cleared.
		/// The other bits are reserved for future use.
		/// </summary>
		NETWORK = 0x3f,
		/// <summary>
		/// Nonzero if the Microsoft Windows for Pen computing extensions are installed; zero otherwise.
		/// </summary>
		PENWINDOWS = 0x29,
		/// <summary>
		/// This system metric is used in a Terminal Services environment to determine if the current
		/// Terminal Server session is being remotely controlled.
		/// Its value is nonzero if the current session is remotely controlled; otherwise, 0.
		/// </summary>
		REMOTECONTROL = 0x2001,
		/// <summary>
		/// This system metric is used in a Terminal Services environment.
		/// If the calling process is associated with a Terminal Services client session, the return value is nonzero.
		/// If the calling process is associated with the Terminal Services console session, the return value is 0. 
		/// </summary>
		REMOTESESSION = 0x1000,
		/// <summary>
		/// Nonzero if all the display monitors have the same color format, otherwise, 0.
		/// Two displays can have the same bit depth, but different color formats.
		/// For example, the red, green, and blue pixels can be encoded with different numbers of bits,
		/// or those bits can be located in different places in a pixel color value.
		/// </summary>
		SAMEDISPLAYFORMAT = 0x51,
		/// <summary>This system metric should be ignored; it always returns 0.</summary>
		SECURE = 0x2c,
		/// <summary>
		/// The build number if the system is Windows Server 2003 R2; otherwise, 0.
		/// </summary>
		SERVERR2 = 89,
		/// <summary>
		/// Nonzero if the user requires an application to present information visually in
		/// situations where it would otherwise present the information only in audible form; otherwise, 0.
		/// </summary>
		SHOWSOUNDS = 70,
		/// <summary>Nonzero if the current session is shutting down; otherwise, 0.</summary>
		/// <remarks>Windows 2000:  This value is not supported.</remarks>
		SHUTTINGDOWN = 0x2000,
		/// <summary>
		/// Nonzero if the computer has a low-end (slow) processor; otherwise, 0.
		/// </summary>
		SLOWMACHINE = 0x49,
		/// <summary>
		/// Nonzero if the meanings of the left and right mouse buttons are swapped; otherwise, 0.
		/// </summary>
		SWAPBUTTON = 0x17,
		/// <summary>
		/// Nonzero if the current operating system is Windows 7 Starter Edition, Windows Vista Starter,
		/// or Windows XP Starter Edition; otherwise, 0.
		/// </summary>
		STARTER = 88,
		/// <summary>
		/// Nonzero if the current operating system is the Windows XP Tablet PC edition or if the current
		/// operating system is Windows Vista or Windows 7 and the Tablet PC Input service is started; otherwise, 0.
		/// The <see cref="DIGITIZER"/> setting indicates the type of digitizer input supported by a device running Windows 7
		/// or Windows Server 2008 R2.
		/// </summary>
		TABLETPC = 0x56,
		/// <summary>
		/// The coordinates for the left side of the virtual screen.
		/// The virtual screen is the bounding rectangle of all display monitors.
		/// The <see cref="CXVIRTUALSCREEN"/> metric is the width of the virtual screen.
		/// </summary>
		XVIRTUALSCREEN = 0x4c,
		/// <summary>
		/// The coordinates for the top of the virtual screen.
		/// The virtual screen is the bounding rectangle of all display monitors.
		/// The <see cref="CYVIRTUALSCREEN"/> metric is the height of the virtual screen.
		/// </summary>
		YVIRTUALSCREEN = 0x4d
	}

	/// <summary>Device caps.</summary>
	/// <seealso>http://msdn.microsoft.com/en-us/library/windows/desktop/dd144877.aspx</seealso>
	enum DeviceCaps
	{
		/// <summary>Device driver version.</summary>
		DRIVERVERSION = 0,
		/// <summary>Device classification.</summary>
		TECHNOLOGY = 2,
		/// <summary>Horizontal size in millimeters.</summary>
		HORZSIZE = 4,
		/// <summary>Vertical size in millimeters.</summary>
		VERTSIZE = 6,
		/// <summary>Horizontal width in pixels.</summary>
		HORZRES = 8,
		/// <summary>Vertical height in pixels.</summary>
		VERTRES = 10,
		/// <summary>Number of bits per pixel.</summary>
		BITSPIXEL = 12,
		/// <summary>Number of planes.</summary>
		PLANES = 14,
		/// <summary>Number of brushes the device has.</summary>
		NUMBRUSHES = 16,
		/// <summary>Number of pens the device has.</summary>
		NUMPENS = 18,
		/// <summary>Number of markers the device has.</summary>
		NUMMARKERS = 20,
		/// <summary>Number of fonts the device has.</summary>
		NUMFONTS = 22,
		/// <summary>Number of colors the device supports.</summary>
		NUMCOLORS = 24,
		/// <summary>Size required for device descriptor.</summary>
		PDEVICESIZE = 26,
		/// <summary>Curve capabilities.</summary>
		CURVECAPS = 28,
		/// <summary>Line capabilities.</summary>
		LINECAPS = 30,
		/// <summary>Polygonal capabilities.</summary>
		POLYGONALCAPS = 32,
		/// <summary>Text capabilities.</summary>
		TEXTCAPS = 34,
		/// <summary>Clipping capabilities.</summary>
		CLIPCAPS = 36,
		/// <summary>Bitblt capabilities.</summary>
		RASTERCAPS = 38,
		/// <summary>Length of the X leg.</summary>
		ASPECTX = 40,
		/// <summary>Length of the Y leg.</summary>
		ASPECTY = 42,
		/// <summary>Length of the hypotenuse.</summary>
		ASPECTXY = 44,
		/// <summary>Shading and Blending caps.</summary>
		SHADEBLENDCAPS = 45,

		/// <summary>Logical pixels inch in X.</summary>
		LOGPIXELSX = 88,
		/// <summary>Logical pixels inch in Y.</summary>
		LOGPIXELSY = 90,

		/// <summary>Number of entries in physical palette.</summary>
		SIZEPALETTE = 104,
		/// <summary>Number of reserved entries in palette.</summary>
		NUMRESERVED = 106,
		/// <summary>Actual color resolution.</summary>
		COLORRES = 108,

		/// <summary>Physical Width in device units.</summary>
		PHYSICALWIDTH = 110,
		/// <summary>Physical Height in device units.</summary>
		PHYSICALHEIGHT = 111,
		/// <summary>Physical Printable Area x margin.</summary>
		PHYSICALOFFSETX = 112,
		/// <summary>Physical Printable Area y margin.</summary>
		PHYSICALOFFSETY = 113,
		/// <summary>Scaling factor x.</summary>
		SCALINGFACTORX = 114,
		/// <summary>Scaling factor y.</summary>
		SCALINGFACTORY = 115,

		/// <summary>Current vertical refresh rate of the display device (for displays only) in Hz.</summary>
		VREFRESH = 116,
		/// <summary>Horizontal width of entire desktop in pixels.</summary>
		DESKTOPVERTRES = 117,
		/// <summary>Vertical height of entire desktop in pixels.</summary>
		DESKTOPHORZRES = 118,
		/// <summary>Preferred blt alignment.</summary>
		BLTALIGNMENT = 119
	}

	/// <summary>Flags for <see cref="User32.SetWindowPos"/>.</summary>
	[Flags]
	enum SWP
	{
		/// <summary>Retains the current size (ignores the cx and cy parameters).</summary>
		NOSIZE = 0x0001,
		/// <summary>Retains the current position (ignores x and y parameters).</summary>
		NOMOVE = 0x0002,
		/// <summary>Retains the current Z order (ignores the hWndInsertAfter parameter).</summary>
		NOZORDER = 0x0004,
		/// <summary>
		/// Does not redraw changes. If this flag is set, no repainting of any kind occurs.
		/// This applies to the client area, the nonclient area (including the title bar and scroll bars),
		/// and any part of the parent window uncovered as a result of the window being moved. When this flag is set,
		/// the application must explicitly invalidate or redraw any parts of the window and parent window that need redrawing.
		/// </summary>
		NOREDRAW = 0x0008,
		/// <summary>
		/// Does not activate the window. If this flag is not set, the window is activated and moved to the
		/// top of either the topmost or non-topmost group (depending on the setting of the hWndInsertAfter parameter).
		/// </summary>
		NOACTIVATE = 0x0010,
		/// <summary>Draws a frame (defined in the window's class description) around the window.</summary>
		DRAWFRAME = 0x0020,
		/// <summary>
		/// Applies new frame styles set using the <see cref="User32.SetWindowLongPtr"/> function.
		/// Sends a <see cref="M:WM.NCCALCSIZE"/> message to the window, even if the window's size is not being changed.
		/// If this flag is not specified, <see cref="M:WM.NCCALCSIZE"/> is sent only when the window's size is being changed.
		/// </summary>
		FRAMECHANGED = 0x0020,
		/// <summary>Displays the window.</summary>
		SHOWWINDOW = 0x0040,
		/// <summary>Hides the window.</summary>
		HIDEWINDOW = 0x0080,
		/// <summary>
		/// Discards the entire contents of the client area.
		/// If this flag is not specified, the valid contents of the client area are saved and copied back
		/// into the client area after the window is sized or repositioned.
		/// </summary>
		NOCOPYBITS = 0x0100,
		/// <summary>Does not change the owner window's position in the Z order.</summary>
		NOOWNERZORDER = 0x0200,
		/// <summary>Same as the <see cref="NOOWNERZORDER"/> flag.</summary>
		NOREPOSITION = 0x0200,
		/// <summary>Prevents the window from receiving the <see cref="M:WM.WINDOWPOSCHANGING"/> message.</summary>
		NOSENDCHANGING = 0x0400,
		/// <summary>Prevents generation of the <see cref="M:WM.SYNCPAINT"/> message.</summary>
		DEFERERASE = 0x2000,
		/// <summary>
		/// If the calling thread and the thread that owns the window are attached to different input queues,
		/// the system posts the request to the thread that owns the window.
		/// This prevents the calling thread from blocking its execution while other threads process the request. 
		/// </summary>
		ASYNCWINDOWPOS = 0x4000,
	}

	/// <summary>DWM Window Attributes.</summary>
	enum DWMWA
	{
		/// <summary>[get] Is non-client rendering enabled/disabled.</summary>
		NCRENDERING_ENABLED = 1,
		/// <summary>[set] Non-client rendering policy.</summary>
		NCRENDERING_POLICY,
		/// <summary>[set] Potentially enable/forcibly disable transitions.</summary>
		TRANSITIONS_FORCEDISABLED,
		/// <summary>[set] Allow contents rendered in the non-client area to be visible on the DWM-drawn frame.</summary>
		ALLOW_NCPAINT,
		/// <summary>[get] Bounds of the caption button area in window-relative space.</summary>
		CAPTION_BUTTON_BOUNDS,
		/// <summary>[set] Is non-client content RTL mirrored.</summary>
		NONCLIENT_RTL_LAYOUT,
		/// <summary>[set] Force this window to display iconic thumbnails.</summary>
		FORCE_ICONIC_REPRESENTATION,
		/// <summary>[set] Designates how Flip3D will treat the window.</summary>
		FLIP3D_POLICY,
		/// <summary>[get] Gets the extended frame bounds rectangle in screen space.</summary>
		EXTENDED_FRAME_BOUNDS,
		/// <summary>[set] Indicates an available bitmap when there is no better thumbnail representation.</summary>
		/// <remarks>Windows Vista and earlier:  This value is not supported.</remarks>
		HAS_ICONIC_BITMAP,
		/// <summary>[set] Don't invoke Peek on the window.</summary>
		/// <remarks>Windows Vista and earlier:  This value is not supported.</remarks>
		DISALLOW_PEEK,
		/// <summary>[set] LivePreview exclusion information.</summary>
		/// <remarks>Windows Vista and earlier:  This value is not supported.</remarks>
		EXCLUDED_FROM_PEEK,
		/// <summary>[set] Cloak or uncloak the window.</summary>
		/// <remarks>Do not use.</remarks>
		CLOAK,
		/// <summary>[get] Gets the cloaked state of the window.</summary>
		/// <remarks>Windows 7 and earlier:  This value is not supported.</remarks>
		CLOAKED,
		/// <summary>[set] Force this window to freeze the thumbnail without live update.</summary>
		/// <remarks>Windows 7 and earlier:  This value is not supported.</remarks>
		FREEZE_REPRESENTATION,
	};

	/// <summary>Non-client rendering policy attribute values.</summary>
	enum DWMNCRP
	{
		/// <summary>Enable/disable non-client rendering based on window style.</summary>
		USEWINDOWSTYLE,
		/// <summary>Disabled non-client rendering; window style is ignored.</summary>
		DISABLED,
		/// <summary>Enabled non-client rendering; window style is ignored.</summary>
		ENABLED,
	};

	/// <summary>Values designating how Flip3D treats a given window.</summary>
	enum DWMFLIP3D
	{
		/// <summary>Hide or include the window in Flip3D based on window style and visibility.</summary>
		DEFAULT,
		/// <summary>Display the window under Flip3D and disabled.</summary>
		EXCLUDEBELOW,
		/// <summary>Display the window above Flip3D and enabled.</summary>
		EXCLUDEABOVE,
	};

	/// <summary>Cloaked flags describing why a window is cloaked.</summary>
	[Flags]
	enum DWM_CLOAKED
	{
		/// <summary>The window was cloaked by its owner application.</summary>
		APP = 0x00000001,
		/// <summary>The window was cloaked by the Shell.</summary>
		SHELL = 0x00000002,
		/// <summary>The cloak value was inherited from its owner window.</summary>
		INHERITED = 0x00000004,
	}

	[Flags]
	enum DWM_TNP
	{
		RECTDESTINATION = 0x00000001,
		RECTSOURCE = 0x00000002,
		OPACITY = 0x00000004,
		VISIBLE = 0x00000008,
		SOURCECLIENTAREAONLY = 0x00000010,
	}

	[Flags]
	enum DWM_BB
	{
		/// <summary>A value for the fEnable member has been specified.</summary>
		ENABLE = 0x00000001,
		/// <summary>A value for the hRgnBlur member has been specified.</summary>
		BLURREGION = 0x00000002,
		/// <summary>A value for the fTransitionOnMaximized member has been specified.</summary>
		TRANSITIONONMAXIMIZED = 0x00000004,
	}

	enum DWM_EC
	{
		DISABLECOMPOSITION = 0,
		ENABLECOMPOSITION = 1,
	}

	enum LSFW
	{
		LOCK = 1,
		UNLOCK = 2,
	}

	/// <summary>Flags for <see cref="T:FLASHWINFO"/>.</summary>
	[Flags]
	enum FLASHW
	{
		/// <summary>
		/// Flash both the window caption and taskbar button.
		/// This is equivalent to setting the CAPTION | TRAY flags.
		/// </summary>
		ALL = 0x00000003,
		/// <summary>Flash the window caption.</summary>
		CAPTION = 0x00000001,
		/// <summary>Stop flashing. The system restores the window to its original state.</summary>
		STOP = 0x00000000,
		/// <summary>Flash continuously, until the STOP flag is set.</summary>
		TIMER = 0x00000004,
		/// <summary>Flash continuously until the window comes to the foreground.</summary>
		TIMERNOFG = 0x0000000C,
		/// <summary>Flash the taskbar button.</summary>
		TRAY = 0x00000002,
	}

	/// <summary>
	/// Flags for <see cref="M:User32.MonitorFromWindow()"/>.
	/// Determines the function's return value if the window does not intersect any display monitor.
	/// </summary>
	enum MONITOR
	{
		/// <summary>
		/// Returns IntPtr.Zero.
		/// </summary>
		DEFAULTTONULL = 0,
		/// <summary>
		/// Returns a handle to the primary display monitor.
		/// </summary>
		DEFAULTTOPRIMARY = 1,
		/// <summary>
		/// Returns a handle to the display monitor that is nearest to the window.
		/// </summary>
		DEFAULTTONEAREST = 2,
	}

	enum ULW
	{
		/// <summary>
		/// Use pblend as the blend function.
		/// If the display mode is 256 colors or less, the effect of this value is the same as the effect of ULW_OPAQUE.
		/// </summary>
		ALPHA = 0x00000002,
		/// <summary>Use crKey as the transparency color.</summary>
		COLORKEY = 0x00000001,
		/// <summary>Draw an opaque layered window.</summary>
		ULW_OPAQUE = 0x00000004,
	}

	/// <summary>Flags for <see cref="M:User32.AnimateWindow"/>.</summary>
	[Flags]
	enum AW
	{
		/// <summary>Activates the window. Do not use this value with <see cref="M:HIDE"/>.</summary>
		ACTIVATE = 0x00020000,
		/// <summary>Uses a fade effect. This flag can be used only if hwnd is a top-level window.</summary>
		BLEND = 0x00080000,
		/// <summary>
		/// Makes the window appear to collapse inward if <see cref="M:HIDE"/> is used or expand outward
		/// if the <see cref="M:HIDE"/> is not used. The various direction flags have no effect.
		/// </summary>
		CENTER = 0x00000010,
		/// <summary>Hides the window. By default, the window is shown.</summary>
		HIDE = 0x00010000,
		/// <summary>
		/// Animates the window from left to right.
		/// This flag can be used with roll or slide animation.
		/// It is ignored when used with <see cref="M:CENTER"/> or <see cref="M:BLEND"/>.
		/// </summary>
		HOR_POSITIVE = 0x00000001,
		/// <summary>
		/// Animates the window from right to left.
		/// This flag can be used with roll or slide animation.
		/// It is ignored when used with <see cref="M:CENTER"/> or <see cref="M:BLEND"/>.
		/// </summary>
		HOR_NEGATIVE = 0x00000002,
		/// <summary>
		/// Uses slide animation. By default, roll animation is used.
		/// This flag is ignored when used with <see cref="M:CENTER"/>.
		/// </summary>
		SLIDE = 0x00040000,
		/// <summary>
		/// Animates the window from top to bottom.
		/// This flag can be used with roll or slide animation.
		/// It is ignored when used with <see cref="M:CENTER"/> or <see cref="M:BLEND"/>.
		/// </summary>
		VER_POSITIVE = 0x00000004,
		/// <summary>
		/// Animates the window from bottom to top.
		/// This flag can be used with roll or slide animation.
		/// It is ignored when used with <see cref="M:CENTER"/> or <see cref="M:BLEND"/>.
		/// </summary>
		VER_NEGATIVE = 0x00000008,
	}

	/// <summary>Flags for <see cref="M:User32.RedrawWindow"/>.</summary>
	[Flags]
	enum RDW : uint
	{
		/// <summary>
		/// Invalidates the rectangle or region that you specify in lprcUpdate or hrgnUpdate.
		/// You can set only one of these parameters to a non-NULL value. If both are NULL,
		/// RDW_INVALIDATE invalidates the entire window.
		/// </summary>
		INVALIDATE = 0x1,

		/// <summary>
		/// Causes the OS to post a WM_PAINT message to the window regardless of whether a portion of the window is invalid.
		/// </summary>
		INTERNALPAINT = 0x2,

		/// <summary>
		/// Causes the window to receive a WM_ERASEBKGND message when the window is repainted.
		/// Specify this value in combination with the RDW_INVALIDATE value; otherwise, RDW_ERASE has no effect.
		/// </summary>
		ERASE = 0x4,

		/// <summary>
		/// Validates the rectangle or region that you specify in lprcUpdate or hrgnUpdate.
		/// You can set only one of these parameters to a non-NULL value. If both are NULL, RDW_VALIDATE validates the entire window.
		/// This value does not affect internal WM_PAINT messages.
		/// </summary>
		VALIDATE = 0x8,

		/// <summary>
		/// Suppresses any pending internal WM_PAINT messages. This flag does not affect WM_PAINT
		/// messages resulting from a non-NULL update area.
		/// </summary>
		NOINTERNALPAINT = 0x10,

		/// <summary>Suppresses any pending WM_ERASEBKGND messages.</summary>
		NOERASE = 0x20,

		/// <summary>Excludes child windows, if any, from the repainting operation.</summary>
		NOCHILDREN = 0x40,

		/// <summary>Includes child windows, if any, in the repainting operation.</summary>
		ALLCHILDREN = 0x80,

		/// <summary>
		/// Causes the affected windows, which you specify by setting the RDW_ALLCHILDREN and RDW_NOCHILDREN values,
		/// to receive WM_ERASEBKGND and WM_PAINT messages before the RedrawWindow returns, if necessary.
		/// </summary>
		UPDATENOW = 0x100,

		/// <summary>
		/// Causes the affected windows, which you specify by setting the RDW_ALLCHILDREN and RDW_NOCHILDREN values,
		/// to receive WM_ERASEBKGND messages before RedrawWindow returns, if necessary.
		/// The affected windows receive WM_PAINT messages at the ordinary time.
		/// </summary>
		ERASENOW = 0x200,

		/// <summary>
		/// Causes any part of the nonclient area of the window that intersects the update region to receive a WM_NCPAINT message.
		/// The RDW_INVALIDATE flag must also be specified; otherwise, RDW_FRAME has no effect. The WM_NCPAINT message is typically
		/// not sent during the execution of RedrawWindow unless either RDW_UPDATENOW or RDW_ERASENOW is specified.
		/// </summary>
		FRAME = 0x400,

		/// <summary>
		/// Suppresses any pending WM_NCPAINT messages. This flag must be used with RDW_VALIDATE and is typically
		/// used with RDW_NOCHILDREN. RDW_NOFRAME should be used with care, as it could cause parts of a window to be painted improperly.
		/// </summary>
		NOFRAME = 0x800
	}

	/// <summary>Stock objects.</summary>
	enum StockObject : int
	{
		/// <summary>White brush.</summary>
		WHITE_BRUSH = 0,
		/// <summary>Light gray brush.</summary>
		LTGRAY_BRUSH = 1,
		/// <summary>Gray brush.</summary>
		GRAY_BRUSH = 2,
		/// <summary>Dark gray brush.</summary>
		DKGRAY_BRUSH = 3,
		/// <summary>Black brush.</summary>
		BLACK_BRUSH = 4,
		/// <summary>Null brush (equivalent to HOLLOW_BRUSH).</summary>
		NULL_BRUSH = 5,
		/// <summary>Hollow brush (equivalent to NULL_BRUSH).</summary>
		HOLLOW_BRUSH = NULL_BRUSH,
		/// <summary>White pen.</summary>
		WHITE_PEN = 6,
		/// <summary>Black pen.</summary>
		BLACK_PEN = 7,
		/// <summary>Null pen. The null pen draws nothing.</summary>
		NULL_PEN = 8,
		/// <summary>Original equipment manufacturer (OEM) dependent fixed-pitch (monospace) font.</summary>
		OEM_FIXED_FONT = 10,
		/// <summary>Windows fixed-pitch (monospace) system font.</summary>
		ANSI_FIXED_FONT = 11,
		/// <summary>Windows variable-pitch (proportional space) system font.</summary>
		ANSI_VAR_FONT = 12,
		/// <summary>
		/// System font. By default, the system uses the system font to draw menus, dialog box controls, and text.
		/// It is not recommended that you use DEFAULT_GUI_FONT or SYSTEM_FONT to obtain the font used by dialogs and windows.
		/// The default system font is Tahoma.
		/// </summary>
		SYSTEM_FONT = 13,
		/// <summary> Device-dependent font.</summary>
		DEVICE_DEFAULT_FONT = 14,
		/// <summary>Default palette. This palette consists of the static colors in the system palette.</summary>
		DEFAULT_PALETTE = 15,
		/// <summary></summary>
		SYSTEM_FIXED_FONT = 16,
		/// <summary>
		/// Default font for user interface objects such as menus and dialog boxes.
		/// It is not recommended that you use DEFAULT_GUI_FONT or SYSTEM_FONT to obtain the font used by dialogs and windows.
		/// The default font is Tahoma.
		/// </summary>
		DEFAULT_GUI_FONT = 17,
		/// <summary>
		/// Solid color brush. The default color is white.
		/// The color can be changed by using the SetDCBrushColor function.
		/// For more information, see the Remarks section.
		/// </summary>
		DC_BRUSH = 18,
		/// <summary>
		/// Solid pen color. The default color is white.
		/// The color can be changed by using the SetDCPenColor function.
		/// For more information, see the Remarks section.
		/// </summary>
		DC_PEN = 19,
	}

	/// <summary>Flags for <see cref="M:WM.TABLET_QUERYSYSTEMGESTURESTATUS"/>.</summary>
	/// <seealso>https://msdn.microsoft.com/en-us/library/bb969148%28v=vs.85%29.aspx</seealso>
	[Flags]
	enum TABLET : uint
	{
		DISABLE_PRESSANDHOLD      = 0x00000001,
		DISABLE_PENTAPFEEDBACK    = 0x00000008,
		DISABLE_PENBARRELFEEDBACK = 0x00000010,
		DISABLE_TOUCHUIFORCEON    = 0x00000100,
		DISABLE_TOUCHUIFORCEOFF   = 0x00000200,
		DISABLE_TOUCHSWITCH       = 0x00008000,
		DISABLE_FLICKS            = 0x00010000,
		ENABLE_FLICKSONCONTEXT    = 0x00020000,
		ENABLE_FLICKLEARNINGMODE  = 0x00040000,
		DISABLE_SMOOTHSCROLLING   = 0x00080000,
		DISABLE_FLICKFALLBACKKEYS = 0x00100000,
		ENABLE_MULTITOUCHDATA     = 0x01000000,
	}

	/// <summary>Pointer message flags.</summary>
	[Flags]
	enum POINTER_MESSAGE_FLAG
	{
		/// <summary>New pointer.</summary>
		NEW = 0x00000001,
		/// <summary>Pointer has not departed.</summary>
		INRANGE = 0x00000002,
		/// <summary>Pointer is in contact.</summary>
		INCONTACT = 0x00000004,
		/// <summary>Primary action.</summary>
		FIRSTBUTTON = 0x00000010,
		/// <summary>Secondary action.</summary>
		SECONDBUTTON = 0x00000020,
		/// <summary>Third button</summary>
		THIRDBUTTON = 0x00000040,
		/// <summary>Fourth button</summary>
		FOURTHBUTTON = 0x00000080,
		/// <summary>Fifth button.</summary>
		FIFTHBUTTON = 0x00000100,
		/// <summary>Pointer is primary.</summary>
		PRIMARY = 0x00002000,
		/// <summary>Pointer is considered unlikely to be accidental.</summary>
		CONFIDENCE = 0x00004000,
		/// <summary>Pointer is departing in an abnormal manner.</summary>
		CANCELED = 0x00008000,
	}

	[Flags]
	enum LoadLibraryFlags
	{
		/// <summary>
		/// If this value is used, and the executable module is a DLL, the system does not call DllMain for process and thread initialization and termination. Also, the system does not load additional executable modules that are referenced by the specified module.
		/// Note Do not use this value; it is provided only for backward compatibility. If you are planning to access only data or resources in the DLL, use LOAD_LIBRARY_AS_DATAFILE_EXCLUSIVE or LOAD_LIBRARY_AS_IMAGE_RESOURCE or both. Otherwise, load the library as a DLL or executable module using the LoadLibrary function.
		/// </summary>
		DONT_RESOLVE_DLL_REFERENCES = 0x00000001,

		/// <summary>
		/// If this value is used, the system does not check AppLocker rules or apply Software Restriction Policies for the DLL. This action applies only to the DLL being loaded and not to its dependencies. This value is recommended for use in setup programs that must run extracted DLLs during installation.
		/// Windows Server 2008 R2 and Windows 7:  On systems with KB2532445 installed, the caller must be running as "LocalSystem" or "TrustedInstaller"; otherwise the system ignores this flag.For more information, see "You can circumvent AppLocker rules by using an Office macro on a computer that is running Windows 7 or Windows Server 2008 R2" in the Help and Support Knowledge Base at http://support.microsoft.com/kb/2532445.
		/// Windows Server 2008, Windows Vista, Windows Server 2003, and Windows XP:  AppLocker was introduced in Windows 7 and Windows Server 2008 R2.
		/// </summary>
		LOAD_IGNORE_CODE_AUTHZ_LEVEL = 0x00000010,

		/// <summary>
		/// If this value is used, the system maps the file into the calling process's virtual address space as if it were a data file. Nothing is done to execute or prepare to execute the mapped file. Therefore, you cannot call functions like GetModuleFileName, GetModuleHandle or GetProcAddress with this DLL. Using this value causes writes to read-only memory to raise an access violation. Use this flag when you want to load a DLL only to extract messages or resources from it.
		/// This value can be used with LOAD_LIBRARY_AS_IMAGE_RESOURCE.For more information, see Remarks.
		/// </summary>
		LOAD_LIBRARY_AS_DATAFILE = 0x00000002,

		/// <summary>
		/// Similar to LOAD_LIBRARY_AS_DATAFILE, except that the DLL file is opened with exclusive write access for the calling process.Other processes cannot open the DLL file for write access while it is in use.However, the DLL can still be opened by other processes.
		/// This value can be used with LOAD_LIBRARY_AS_IMAGE_RESOURCE.For more information, see Remarks.
		/// Windows Server 2003 and Windows XP:  This value is not supported until Windows Vista.
		/// </summary>
		LOAD_LIBRARY_AS_DATAFILE_EXCLUSIVE = 0x00000040,

		/// <summary>
		/// If this value is used, the system maps the file into the process's virtual address space as an image file. However, the loader does not load the static imports or perform the other usual initialization steps. Use this flag when you want to load a DLL only to extract messages or resources from it.
		/// Unless the application depends on the file having the in-memory layout of an image, this value should be used with either LOAD_LIBRARY_AS_DATAFILE_EXCLUSIVE or LOAD_LIBRARY_AS_DATAFILE.For more information, see the Remarks section.
		/// Windows Server 2003 and Windows XP:  This value is not supported until Windows Vista.
		/// </summary>
		LOAD_LIBRARY_AS_IMAGE_RESOURCE = 0x00000020,

		/// <summary>
		/// If this value is used, the application's installation directory is searched for the DLL and its dependencies. Directories in the standard search path are not searched. This value cannot be combined with LOAD_WITH_ALTERED_SEARCH_PATH.
		/// Windows 7, Windows Server 2008 R2, Windows Vista, and Windows Server 2008:  This value requires KB2533623 to be installed.
		/// Windows Server 2003 and Windows XP:  This value is not supported.
		/// </summary>
		LOAD_LIBRARY_SEARCH_APPLICATION_DIR = 0x00000200,

		/// <summary>
		/// This value is a combination of LOAD_LIBRARY_SEARCH_APPLICATION_DIR, LOAD_LIBRARY_SEARCH_SYSTEM32, and LOAD_LIBRARY_SEARCH_USER_DIRS. Directories in the standard search path are not searched.This value cannot be combined with LOAD_WITH_ALTERED_SEARCH_PATH.
		/// This value represents the recommended maximum number of directories an application should include in its DLL search path.
		/// Windows 7, Windows Server 2008 R2, Windows Vista, and Windows Server 2008:  This value requires KB2533623 to be installed.
		/// Windows Server 2003 and Windows XP:  This value is not supported.
		/// </summary>
		LOAD_LIBRARY_SEARCH_DEFAULT_DIRS = 0x00001000,

		/// <summary>
		/// If this value is used, the directory that contains the DLL is temporarily added to the beginning of the list of directories that are searched for the DLL's dependencies. Directories in the standard search path are not searched.
		/// The lpFileName parameter must specify a fully qualified path.This value cannot be combined with LOAD_WITH_ALTERED_SEARCH_PATH.
		/// For example, if Lib2.dll is a dependency of C:\Dir1\Lib1.dll, loading Lib1.dll with this value causes the system to search for Lib2.dll only in C:\Dir1.To search for Lib2.dll in C:\Dir1 and all of the directories in the DLL search path, combine this value with LOAD_LIBRARY_DEFAULT_DIRS.
		/// Windows 7, Windows Server 2008 R2, Windows Vista, and Windows Server 2008:  This value requires KB2533623 to be installed.
		/// Windows Server 2003 and Windows XP:  This value is not supported.
		/// </summary>
		LOAD_LIBRARY_SEARCH_DLL_LOAD_DIR = 0x00000100,

		/// <summary>
		/// If this value is used, %windows%\system32 is searched for the DLL and its dependencies.Directories in the standard search path are not searched.This value cannot be combined with LOAD_WITH_ALTERED_SEARCH_PATH.
		/// Windows 7, Windows Server 2008 R2, Windows Vista, and Windows Server 2008:  This value requires KB2533623 to be installed.
		/// Windows Server 2003 and Windows XP:  This value is not supported.
		/// </summary>
		LOAD_LIBRARY_SEARCH_SYSTEM32 = 0x00000800,

		/// <summary>
		/// If this value is used, directories added using the AddDllDirectory or the SetDllDirectory function are searched for the DLL and its dependencies.If more than one directory has been added, the order in which the directories are searched is unspecified.Directories in the standard search path are not searched.This value cannot be combined with LOAD_WITH_ALTERED_SEARCH_PATH.
		/// Windows 7, Windows Server 2008 R2, Windows Vista, and Windows Server 2008:  This value requires KB2533623 to be installed.
		/// Windows Server 2003 and Windows XP:  This value is not supported.
		/// </summary>
		LOAD_LIBRARY_SEARCH_USER_DIRS = 0x00000400,

		/// <summary>
		/// If this value is used and lpFileName specifies an absolute path, the system uses the alternate file search strategy discussed in the Remarks section to find associated executable modules that the specified module causes to be loaded. If this value is used and lpFileName specifies a relative path, the behavior is undefined.
		/// If this value is not used, or if lpFileName does not specify a path, the system uses the standard search strategy discussed in the Remarks section to find associated executable modules that the specified module causes to be loaded.
		/// This value cannot be combined with any LOAD_LIBRARY_SEARCH flag.
		/// </summary>
		LOAD_WITH_ALTERED_SEARCH_PATH = 0x00000008,
	}

	/// <summary>Flags for <see cref="Kernel32.SetErrorMode"/>.</summary>
	[Flags]
	enum SEM
	{
		/// <summary>Use the system default, which is to display all error dialog boxes.</summary>
		Default = 0,
		/// <summary>
		/// The system does not display the critical-error-handler message box.
		/// Instead, the system sends the error to the calling process.
		/// </summary>
		/// <remarks>
		/// Best practice is that all applications call the process-wide SetErrorMode function with a parameter of <see cref="M:SEM.FAILCRITICALERRORS"/> at startup.
		/// This is to prevent error mode dialogs from hanging the application.
		/// </remarks>
		FAILCRITICALERRORS = 0x0001,
		/// <summary>
		/// The system automatically fixes memory alignment faults and makes them invisible to the application.
		/// It does this for the calling process and any descendant processes.
		/// This feature is only supported by certain processor architectures.
		/// After this value is set for a process, subsequent attempts to clear the value are ignored.
		/// </summary>
		NOALIGNMENTFAULTEXCEPT = 0x0004,
		/// <summary>
		/// The system does not display the Windows Error Reporting dialog.
		/// </summary>
		NOGPFAULTERRORBOX = 0x0002,
		/// <summary>
		/// The system does not display a message box when it fails to find a file.
		/// Instead, the error is returned to the calling process.
		/// </summary>
		NOOPENFILEERRORBOX = 0x8000,
	}

	[Flags]
	enum HeapAllocFlags : uint
	{
		None = 0,

		NO_SERIALIZE = 0x00000001,
		GENERATE_EXCEPTIONS = 0x00000004,
		ZERO_MEMORY = 0x00000008,
	}

	[Flags]
	enum HeapFreeFlags : uint
	{
		None = 0,

		NO_SERIALIZE = 0x00000001,
	}

	[Flags]
	enum HeapReAllocFlags : uint
	{
		None = 0,

		NO_SERIALIZE = 0x00000001,
		GENERATE_EXCEPTIONS = 0x00000004,
		ZERO_MEMORY = 0x00000008,
		REALLOC_IN_PLACE_ONLY = 0x00000010,
	}

	[Flags]
	enum HeapCompactFlags : uint
	{
		None = 0,

		NO_SERIALIZE = 0x00000001,
	}

	/// <summary>File attribute flags.</summary>
	[Flags]
	enum FILE_ATTRIBUTE : int
	{
		/// <summary>Error.</summary>
		INVALID = -1,
		/// <summary>
		/// A file or directory that is an archive file or directory.
		/// Applications typically use this attribute to mark files for backup or removal.
		/// </summary>
		ARCHIVE = 0x20,
		/// <summary>
		/// A file or directory that is compressed.
		/// For a file, all of the data in the file is compressed.
		/// For a directory, compression is the default for newly created files and subdirectories.
		/// </summary>
		COMPRESSED = 0x800,
		/// <summary>This value is reserved for system use.</summary>
		DEVICE = 0x40,
		/// <summary>The handle that identifies a directory.</summary>
		DIRECTORY = 0x10,
		/// <summary>
		/// A file or directory that is encrypted.
		/// For a file, all data streams in the file are encrypted.
		/// For a directory, encryption is the default for newly created files and subdirectories.
		/// </summary>
		ENCRYPTED = 0x4000,
		/// <summary>
		/// The file or directory is hidden.
		/// It is not included in an ordinary directory listing.
		/// </summary>
		HIDDEN = 0x2,
		/// <summary>
		/// The directory or user data stream is configured with integrity (only supported on ReFS volumes).
		/// It is not included in an ordinary directory listing.
		/// The integrity setting persists with the file if it's renamed.
		/// If a file is copied the destination file will have integrity set if either the source
		/// file or destination directory have integrity set.
		/// </summary>
		/// <remarks>
		/// Windows Server 2008 R2, Windows 7, Windows Server 2008, Windows Vista,
		/// Windows Server 2003, and Windows XP: This flag is not supported until Windows Server 2012.
		/// </remarks>
		INTEGRITY_STREAM = 0x8000,
		/// <summary>
		/// A file that does not have other attributes set.
		/// This attribute is valid only when used alone.
		/// </summary>
		NORMAL = 0x80,
		/// <summary>
		/// The file or directory is not to be indexed by the content indexing service.
		/// </summary>
		NOT_CONTENT_INDEXED = 0x2000,
		/// <summary>
		/// The user data stream not to be read by the background data integrity scanner (AKA scrubber).
		/// When set on a directory it only provides inheritance. This flag is only supported on
		/// Storage Spaces and ReFS volumes.It is not included in an ordinary directory listing.
		/// </summary>
		/// <remarks>
		/// Windows Server 2008 R2, Windows 7, Windows Server 2008, Windows Vista,
		/// Windows Server 2003, and Windows XP: This flag is not supported until Windows 8 and Windows Server 2012.
		/// </remarks>
		NO_SCRUB_DATA = 0x20000,
		/// <summary>
		/// The data of a file is not available immediately.
		/// This attribute indicates that the file data is physically moved to offline storage.
		/// This attribute is used by Remote Storage, which is the hierarchical storage management software.
		/// Applications should not arbitrarily change this attribute.
		/// </summary>
		OFFLINE = 0x1000,
		/// <summary>
		/// A file that is read-only.
		/// Applications can read the file, but cannot write to it or delete it.
		/// This attribute is not honored on directories.
		/// </summary>
		READONLY = 0x1,
		/// <summary>
		/// A file or directory that has an associated reparse point, or a file that is a symbolic link.
		/// </summary>
		REPARSE_POINT = 0x400,
		/// <summary>A file that is a sparse file.</summary>
		SPARSE_FILE = 0x200,
		/// <summary>
		/// A file or directory that the operating system uses a part of, or uses exclusively.
		/// </summary>
		SYSTEM = 0x4,
		/// <summary>
		/// A file that is being used for temporary storage.
		/// File systems avoid writing data back to mass storage if sufficient
		/// cache memory is available, because typically, an application deletes a temporary
		/// file after the handle is closed.
		/// In that scenario, the system can entirely avoid writing the data.
		/// Otherwise, the data is written after the handle is closed.
		/// </summary>
		TEMPORARY = 0x100,
		/// <summary>This value is reserved for system use.</summary>
		VIRTUAL = 0x10000,
	}

	/// <summary>File type.</summary>
	enum FILE_TYPE : int
	{
		/// <summary>
		/// The specified file is a character file, typically an LPT device or a console.
		/// </summary>
		CHAR = 0x0002,
		/// <summary>
		/// The specified file is a disk file.
		/// </summary>
		DISK = 0x0001,
		/// <summary>
		/// The specified file is a socket, a named pipe, or an anonymous pipe.
		/// </summary>
		PIPE = 0x0003,
		/// <summary>
		/// Unused.
		/// </summary>
		REMOTE = 0x8000,
		/// <summary>
		/// Either the type of the specified file is unknown, or the function failed.
		/// </summary>
		UNKNOWN = 0x0000,
	}

	enum BACKUP : int
	{
		/// <summary>
		/// Alternative data streams.This corresponds to the
		/// NTFS $DATA stream type on a named data stream.
		/// </summary>
		ALTERNATE_DATA = 0x00000004,
		/// <summary>
		/// Standard data. This corresponds to the NTFS $DATA stream type on the default (unnamed) data stream.
		/// </summary>
		DATA = 0x00000001,
		/// <summary>
		/// Extended attribute data.
		/// This corresponds to the NTFS $EA stream type.
		/// </summary>
		EA_DATA = 0x00000002,
		/// <summary>
		/// Hard link information.
		/// This corresponds to the NTFS $FILE_NAME stream type.
		/// </summary>
		LINK = 0x00000005,
		/// <summary>
		/// Objects identifiers. This corresponds to the NTFS $OBJECT_ID stream type.
		/// </summary>
		OBJECT_ID = 0x00000007,
		/// <summary>
		/// Property data.
		/// </summary>
		PROPERTY_DATA = 0x00000006,
		/// <summary>
		/// Reparse points. This corresponds to the NTFS $REPARSE_POINT stream type.
		/// </summary>
		REPARSE_DATA = 0x00000008,
		/// <summary>
		/// Security descriptor data.
		/// </summary>
		SECURITY_DATA = 0x00000003,
		/// <summary>
		/// Sparse file. This corresponds to the NTFS $DATA stream type for a sparse file.
		/// </summary>
		SPARSE_BLOCK = 0x00000009,
		/// <summary>
		/// Transactional NTFS (TxF) data stream.
		/// This corresponds to the NTFS $TXF_DATA stream type.
		/// </summary>
		/// <remarks>Windows Server 2003 and Windows XP:  This value is not supported.</remarks>
		TXFS_DATA = 0x0000000A,
	}

	/// <summary>Stream attributes.</summary>
	[Flags]
	enum STREAM : int
	{
		NORMAL = 0x00000000,
		/// <summary>
		/// Attribute set if the stream contains data that is modified when read. Allows the backup application to know that verification of data will fail.
		/// </summary>
		MODIFIED_WHEN_READ = 0x00000001,
		/// <summary>
		/// Stream contains security data (general attributes). Allows the stream to be ignored on cross-operations restore.
		/// </summary>
		CONTAINS_SECURITY = 0x00000002,
		CONTAINS_PROPERTIES  = 0x00000004,
		SPARSE  = 0x00000008,
	}

	/// <summary>
	/// Any additional information about the system.
	/// This member can be one of the following values.
	/// </summary>
	enum ProductType : byte
	{
		/// <summary>
		/// The system is a domain controller and the operating system is Windows Server 2012, Windows Server 2008 R2, Windows Server 2008, Windows Server 2003, or Windows 2000 Server.
		/// </summary>
		VER_NT_DOMAIN_CONTROLLER = 0x0000002,
		/// <summary>
		/// The operating system is Windows Server 2012, Windows Server 2008 R2, Windows Server 2008, Windows Server 2003, or Windows 2000 Server.
		/// Note that a server that is also a domain controller is reported as VER_NT_DOMAIN_CONTROLLER, not VER_NT_SERVER.
		/// </summary>
		VER_NT_SERVER = 0x0000003,
		/// <summary>
		/// The operating system is Windows 8, Windows 7, Windows Vista, Windows XP Professional, Windows XP Home Edition, or Windows 2000 Professional.
		/// </summary>
		VER_NT_WORKSTATION = 0x0000001,
	}

	/// <summary>
	/// A bit mask that identifies the product suites available on the system.
	/// This member can be a combination of the following values.
	/// </summary>
	[Flags]
	enum SuiteMask : ushort
	{
		/// <summary>
		/// Microsoft BackOffice components are installed.
		/// </summary>
		VER_SUITE_BACKOFFICE = 0x00000004,
		/// <summary>
		/// Windows Server 2003, Web Edition is installed.
		/// </summary>
		VER_SUITE_BLADE = 0x00000400,
		/// <summary>
		/// Windows Server 2003, Compute Cluster Edition is installed.
		/// </summary>
		VER_SUITE_COMPUTE_SERVER = 0x00004000,
		/// <summary>
		/// Windows Server 2008 Datacenter, Windows Server 2003, Datacenter Edition, or Windows 2000 Datacenter Server is installed.
		/// </summary>
		VER_SUITE_DATACENTER = 0x00000080,
		/// <summary>
		/// Windows Server 2008 Enterprise, Windows Server 2003, Enterprise Edition, or Windows 2000 Advanced Server is installed.
		/// Refer to the Remarks section for more information about this bit flag.
		/// </summary>
		VER_SUITE_ENTERPRISE = 0x00000002,
		/// <summary>
		/// Windows XP Embedded is installed.
		/// </summary>
		VER_SUITE_EMBEDDEDNT = 0x00000040,
		/// <summary>
		/// Windows Vista Home Premium, Windows Vista Home Basic, or Windows XP Home Edition is installed.
		/// </summary>
		VER_SUITE_PERSONAL = 0x00000200,
		/// <summary>
		/// Remote Desktop is supported, but only one interactive session is supported.
		/// This value is set unless the system is running in application server mode.
		/// </summary>
		VER_SUITE_SINGLEUSERTS = 0x00000100,
		/// <summary>
		/// Microsoft Small Business Server was once installed on the system, but may have been upgraded to another version of Windows.
		/// Refer to the Remarks section for more information about this bit flag.
		/// </summary>
		VER_SUITE_SMALLBUSINESS = 0x00000001,
		/// <summary>
		/// Microsoft Small Business Server is installed with the restrictive client license in force.
		/// Refer to the Remarks section for more information about this bit flag.
		/// </summary>
		VER_SUITE_SMALLBUSINESS_RESTRICTED = 0x00000020,
		/// <summary>
		/// Windows Storage Server 2003 R2 or Windows Storage Server 2003is installed.
		/// </summary>
		VER_SUITE_STORAGE_SERVER = 0x00002000,
		/// <summary>
		/// Terminal Services is installed. This value is always set.
		/// </summary>
		VER_SUITE_TERMINAL = 0x00000010,
		/// <summary>
		/// Windows Home Server is installed.
		/// If VER_SUITE_TERMINAL is set but VER_SUITE_SINGLEUSERTS is not set, the system is running in application server mode.
		/// </summary>
		VER_SUITE_WH_SERVER = 0x00008000,
	}

	#endregion

	#region Structs

	/// <summary>
	/// Contains operating system version information. The information includes major and minor version numbers, a build number, a platform identifier, and descriptive text about the operating system. This structure is used with the <see cref="Kernel32.GetVersionEx(ref OSVERSIONINFO)"/> function.
	/// To obtain additional version information, use the <see cref="OSVERSIONINFOEX"/> structure with <see cref="Kernel32.GetVersionEx(ref OSVERSIONINFOEX)"/> instead.
	/// </summary>
	/// <seealso>https://msdn.microsoft.com/en-us/library/ms724834(v=vs.85).aspx</seealso>
	[StructLayout(LayoutKind.Sequential)]
	struct OSVERSIONINFO
	{
		#region Data

		/// <summary>
		/// The size of this data structure, in bytes. Set this member to sizeof(OSVERSIONINFO).
		/// </summary>
		public uint dwOSVersionInfoSize;
		/// <summary>
		/// The major version number of the operating system. For more information, see Remarks.
		/// </summary>
		public uint dwMajorVersion;
		/// <summary>
		/// The minor version number of the operating system. For more information, see Remarks.
		/// </summary>
		public uint dwMinorVersion;
		/// <summary>
		/// The build number of the operating system.
		/// </summary>
		public uint dwBuildNumber;
		/// <summary>
		/// The operating system platform. This member can be the following value.
		/// </summary>
		public uint dwPlatformId;
		/// <summary>
		/// A null-terminated string, such as "Service Pack 3", that indicates the latest Service Pack installed on the system.
		/// If no Service Pack has been installed, the string is empty.
		/// </summary>
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
		public string szCSDVersion;

		#endregion
	}

	/// <summary>
	/// Contains operating system version information.
	/// The information includes major and minor version numbers, a build number, a platform identifier, and information about product suites and the latest Service Pack installed on the system.
	/// This structure is used with the <see cref="Kernel32.GetVersionEx(ref OSVERSIONINFOEX)"/> and VerifyVersionInfo functions.
	/// </summary>
	/// <seealso>https://msdn.microsoft.com/en-us/library/ms724833(v=vs.85).aspx</seealso>
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
	struct OSVERSIONINFOEX
	{
		#region Data

		/// <summary>
		/// The size of this data structure, in bytes. Set this member to sizeof(OSVERSIONINFOEX).
		/// </summary>
		public uint dwOSVersionInfoSize;
		/// <summary>
		/// The major version number of the operating system. For more information, see Remarks.
		/// </summary>
		public uint dwMajorVersion;
		/// <summary>
		/// The minor version number of the operating system. For more information, see Remarks.
		/// </summary>
		public uint dwMinorVersion;
		/// <summary>
		/// The build number of the operating system.
		/// </summary>
		public uint dwBuildNumber;
		/// <summary>
		/// The operating system platform. This member can be VER_PLATFORM_WIN32_NT (2).
		/// </summary>
		public uint dwPlatformId;
		/// <summary>
		/// A null-terminated string, such as "Service Pack 3", that indicates the latest Service Pack installed on the system.
		/// If no Service Pack has been installed, the string is empty.
		/// </summary>
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
		public string szCSDVersion;
		/// <summary>
		/// The major version number of the latest Service Pack installed on the system.
		/// For example, for Service Pack 3, the major version number is 3.
		/// If no Service Pack has been installed, the value is zero.
		/// </summary>
		public ushort wServicePackMajor;
		/// <summary>
		/// The minor version number of the latest Service Pack installed on the system.
		/// For example, for Service Pack 3, the minor version number is 0.
		/// </summary>
		public ushort wServicePackMinor;
		/// <summary>
		/// A bit mask that identifies the product suites available on the system.
		/// This member can be a combination of the following values.
		/// </summary>
		public SuiteMask wSuiteMask;
		/// <summary>
		/// Any additional information about the system.
		/// This member can be one of the following values.
		/// </summary>
		public ProductType wProductType;
		/// <summary>
		/// Reserved for future use.
		/// </summary>
		public byte wReserved;

		#endregion
	}

	/// <summary>
	/// The BITMAPINFOHEADER structure contains information about the dimensions and color format of a DIB.
	/// </summary>
	/// <seealso>http://msdn.microsoft.com/en-us/library/dd183376.aspx</seealso>
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	struct BITMAPINFOHEADER
	{
		#region Data

		/// <summary>The number of bytes required by the structure.</summary>
		public uint biSize;
		/// <summary>The width of the bitmap, in pixels.</summary>
		/// <remarks>
		/// If biCompression is BI_JPEG or BI_PNG, the biWidth member specifies the width of the
		/// decompressed JPEG or PNG image file, respectively.
		/// </remarks>
		public int biWidth;
		/// <summary>
		/// The height of the bitmap, in pixels.
		/// If biHeight is positive, the bitmap is a bottom-up DIB and its origin is the lower-left corner.
		/// If biHeight is negative, the bitmap is a top-down DIB and its origin is the upper-left corner.
		/// </summary>
		/// <remarks>
		/// If biHeight is negative, indicating a top-down DIB, biCompression must be either BI_RGB or BI_BITFIELDS. Top-down DIBs cannot be compressed.
		///  If biCompression is BI_JPEG or BI_PNG, the biHeight member specifies the height of the decompressed JPEG or PNG image file, respectively.
		/// </remarks>
		public int biHeight;
		/// <summary>The number of planes for the target device. This value must be set to 1.</summary>
		public short biPlanes;
		/// <summary>
		/// The number of bits-per-pixel. The biBitCount member of the BITMAPINFOHEADER structure determines the number of bits
		/// that define each pixel and the maximum number of colors in the bitmap. This member must be one of the following values:
		/// 0, 1, 4, 8, 16, 24, 32
		/// </summary>
		public short biBitCount;
		/// <summary>
		/// The type of compression for a compressed bottom-up bitmap (top-down DIBs cannot be compressed).
		/// </summary>
		public DibCompression biCompression;
		/// <summary>
		/// The size, in bytes, of the image. This may be set to zero for BI_RGB bitmaps.
		/// </summary>
		public uint biSizeImage;
		/// <summary>
		/// The horizontal resolution, in pixels-per-meter, of the target device for the bitmap. An application
		/// can use this value to select a bitmap from a resource group that best matches the characteristics of the current device.
		/// </summary>
		public int biXPelsPerMeter;
		/// <summary>The vertical resolution, in pixels-per-meter, of the target device for the bitmap.</summary>
		public int biYPelsPerMeter;
		/// <summary>
		/// The number of color indexes in the color table that are actually used by the bitmap.
		/// If this value is zero, the bitmap uses the maximum number of colors corresponding to the value
		/// of the biBitCount member for the compression mode specified by biCompression.
		/// </summary>
		public uint biClrUsed;
		/// <summary>
		/// The number of color indexes that are required for displaying the bitmap. If this value is zero, all colors are required.
		/// </summary>
		public uint biClrImportant;

		#endregion

		/// <summary>Creates <see cref="BITMAPINFOHEADER"/>.</summary>
		/// <param name="binaryReader">Reader to read from.</param>
		public BITMAPINFOHEADER(BinaryReader binaryReader)
		{
			biSize = binaryReader.ReadUInt32();
			biWidth = binaryReader.ReadInt32();
			biHeight = binaryReader.ReadInt32();
			biPlanes = binaryReader.ReadInt16();
			biBitCount = binaryReader.ReadInt16();
			biCompression = (DibCompression)binaryReader.ReadUInt32();
			biSizeImage = binaryReader.ReadUInt32();
			biXPelsPerMeter = binaryReader.ReadInt32();
			biYPelsPerMeter = binaryReader.ReadInt32();
			biClrUsed = binaryReader.ReadUInt32();
			biClrImportant = binaryReader.ReadUInt32();
		}

		/// <summary>Writes this <see cref="BITMAPINFOHEADER"/> to a stream.</summary>
		/// <param name="binaryWriter">Writer to write to.</param>
		public void Write(BinaryWriter binaryWriter)
		{
			binaryWriter.Write(biSize);
			binaryWriter.Write(biWidth);
			binaryWriter.Write(biHeight);
			binaryWriter.Write(biPlanes);
			binaryWriter.Write(biBitCount);
			binaryWriter.Write((uint)biCompression);
			binaryWriter.Write(biSizeImage);
			binaryWriter.Write(biXPelsPerMeter);
			binaryWriter.Write(biYPelsPerMeter);
			binaryWriter.Write(biClrUsed);
			binaryWriter.Write(biClrImportant);
		}
	}

	/// <summary>
	/// The BITMAPFILEHEADER structure contains information about the type, size, and layout of a file that contains a DIB.
	/// </summary>
	/// <seealso>http://msdn.microsoft.com/en-us/library/dd183374.aspx</seealso>
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	struct BITMAPFILEHEADER
	{
		#region Static

		/// <summary>"BM".</summary>
		public static readonly short BMP_MAGIC_COOKIE = 19778;

		#endregion

		#region Data

		/// <summary>The file type; must be <see cref="BMP_MAGIC_COOKIE"/>.</summary>
		public short bfType;
		/// <summary>The size, in bytes, of the bitmap file.</summary>
		public int bfSize;
		/// <summary>Reserved; must be zero.</summary>
		public short bfReserved1;
		/// <summary>Reserved; must be zero.</summary>
		public short bfReserved2;
		/// <summary>
		/// The offset, in bytes, from the beginning of the BITMAPFILEHEADER structure to the bitmap bits.
		/// </summary>
		public int bfOffBits;

		#endregion

		/// <summary>Creates <see cref="BITMAPFILEHEADER"/>.</summary>
		/// <param name="binaryReader">Reader to read from.</param>
		public BITMAPFILEHEADER(BinaryReader binaryReader)
		{
			bfType = binaryReader.ReadInt16();
			bfSize = binaryReader.ReadInt32();
			bfReserved1 = binaryReader.ReadInt16();
			bfReserved2 = binaryReader.ReadInt16();
			bfOffBits = binaryReader.ReadInt32();
		}

		/// <summary>Writes this <see cref="BITMAPFILEHEADER"/> to a stream.</summary>
		/// <param name="binaryWriter">Writer to write to.</param>
		public void Write(BinaryWriter binaryWriter)
		{
			binaryWriter.Write(bfType);
			binaryWriter.Write(bfSize);
			binaryWriter.Write(bfReserved1);
			binaryWriter.Write(bfReserved2);
			binaryWriter.Write(bfOffBits);
		}
	}

	/// <summary>
	/// The RGBQUAD structure describes a color consisting of relative intensities of red, green, and blue.
	/// </summary>
	/// <seealso>http://msdn.microsoft.com/en-us/library/vstudio/dd162938.aspx</seealso>
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	struct RGBQUAD
	{
		#region Data

		/// <summary>The intensity of blue in the color.</summary>
		public readonly byte rgbBlue;
		/// <summary>The intensity of green in the color.</summary>
		public readonly byte rgbGreen;
		/// <summary>The intensity of red in the color.</summary>
		public readonly byte rgbRed;
		/// <summary>This member is reserved and must be zero.</summary>
		public readonly byte rgbReserved;

		#endregion

		/// <summary>Creates <see cref="RGBQUAD"/>.</summary>
		/// <param name="stream">Stream to read from.</param>
		public RGBQUAD(Stream stream)
		{
			rgbBlue = (byte)stream.ReadByte();
			rgbGreen = (byte)stream.ReadByte();
			rgbRed = (byte)stream.ReadByte();
			rgbReserved = (byte)stream.ReadByte();
		}

		/// <summary>Creates <see cref="RGBQUAD"/>.</summary>
		/// <param name="br">Reader to read from.</param>
		public RGBQUAD(BinaryReader br)
		{
			uint rgb_quad = br.ReadUInt32();
			rgbBlue = (byte)(rgb_quad >> 0);
			rgbGreen = (byte)(rgb_quad >> 8);
			rgbRed = (byte)(rgb_quad >> 16);
			rgbReserved = (byte)(rgb_quad >> 24);
		}
	}

#if ALLOW_UNSAFE
	/// <summary>BITMAPINFOHEADER + palette.</summary>
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	unsafe struct BITMAPINFO256
	{
		/// <summary><see cref="BITMAPINFOHEADER"/>.</summary>
		public BITMAPINFOHEADER bmiHeader;
		/// <summary>Palette.</summary>
		public fixed byte bmiColors [4 * 256];
	}
#endif

	/// <summary>
	/// The <see cref="BLENDFUNCTION"/> structure controls blending by specifying the blending
	/// functions for source and destination bitmaps.
	/// </summary>
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	struct BLENDFUNCTION
	{
		/// <summary>
		/// The source blend operation. Currently, the only source and destination
		/// blend operation that has been defined is AC_SRC_OVER.
		/// </summary>
		public byte BlendOp;

		/// <summary>Must be zero.</summary>
		public byte BlendFlags;

		/// <summary>
		/// Specifies an alpha transparency value to be used on the entire source bitmap.
		/// The SourceConstantAlpha value is combined with any per-pixel alpha values in the source bitmap.
		/// If you set SourceConstantAlpha to 0, it is assumed that your image is transparent.
		/// Set the SourceConstantAlpha value to 255 (opaque) when you only want to use per-pixel alpha values.
		/// </summary>
		public byte SourceConstantAlpha;

		/// <summary>
		/// This member controls the way the source and destination bitmaps are interpreted.
		/// </summary>
		public byte AlphaFormat;
	}

	/// <summary>The POINT structure defines the x- and y- coordinates of a point.</summary>
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	struct POINT
	{
		#region Data

		/// <summary>The x-coordinate of the point.</summary>
		public int X;
		/// <summary>The y-coordinate of the point.</summary>
		public int Y;

		#endregion

		/// <summary>Creates <see cref="POINT"/>.</summary>
		/// <param name="x">The x-coordinate of the point.</param>
		/// <param name="y">The y-coordinate of the point.</param>
		public POINT(int x, int y)
		{
			X = x;
			Y = y;
		}
	}

	/// <summary>The SIZE structure specifies the width and height of a rectangle.</summary>
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	struct SIZE
	{
		#region Data

		/// <summary>Specifies the rectangle's width. The units depend on which function uses this.</summary>
		public int Width;

		/// <summary>Specifies the rectangle's height. The units depend on which function uses this.</summary>
		public int Height;

		#endregion

		#region .ctor

		/// <summary>Creates <see cref="SIZE"/> struct.</summary>
		/// <param name="width">Specifies the rectangle's width.</param>
		/// <param name="height">Specifies the rectangle's height.</param>
		public SIZE(int width, int height)
		{
			Width  = width;
			Height = height;
		}

		#endregion
	}

	/// <summary>The RECT structure defines the coordinates of the upper-left and lower-right corners of a rectangle.</summary>
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	struct RECT
	{
		#region Data

		/// <summary>The x-coordinate of the upper-left corner of the rectangle.</summary>
		public int Left;
		/// <summary>The y-coordinate of the upper-left corner of the rectangle.</summary>
		public int Top;
		/// <summary>The x-coordinate of the lower-right corner of the rectangle.</summary>
		public int Right;
		/// <summary>The y-coordinate of the lower-right corner of the rectangle.</summary>
		public int Bottom;

		#endregion

		/// <summary>Creates <see cref="RECT"/>.</summary>
		/// <param name="left">The x-coordinate of the upper-left corner of the rectangle.</param>
		/// <param name="top">The y-coordinate of the upper-left corner of the rectangle.</param>
		/// <param name="right">The x-coordinate of the lower-right corner of the rectangle.</param>
		/// <param name="bottom">The y-coordinate of the lower-right corner of the rectangle.</param>
		public RECT(int left, int top, int right, int bottom)
		{
			Left = left;
			Top = top;
			Right = right;
			Bottom = bottom;
		}
	}

	/// <summary>
	/// The PAINTSTRUCT structure contains information for an application.
	/// This information can be used to paint the client area of a window owned by that application.
	/// </summary>
	[StructLayout(LayoutKind.Sequential)]
	struct PAINTSTRUCT
	{
		#region Data

		/// <summary>A handle to the display DC to be used for painting.</summary>
		public IntPtr hdc;

		/// <summary>
		/// Indicates whether the background must be erased.
		/// This value is nonzero if the application should erase the background.
		/// The application is responsible for erasing the background if a window class is created without a background brush.
		/// For more information, see the description of the hbrBackground member of the WNDCLASS structure.
		/// </summary>
		public int fErase;

		/// <summary>
		/// A <see cref="RECT"/> structure that specifies the upper left and lower right corners of the rectangle in
		/// which the painting is requested, in device units relative to the upper-left corner of the client area.
		/// </summary>
		public RECT rcPaint;

		#endregion

		#region Reserved

		/// <summary>Reserved; used internally by the system.</summary>
		public int fRestore;

		/// <summary>Reserved; used internally by the system.</summary>
		public int fIncUpdate;

		/// <summary>Reserved; used internally by the system.</summary>
		public int _Reserved1;
		/// <summary>Reserved; used internally by the system.</summary>
		public int _Reserved2;
		/// <summary>Reserved; used internally by the system.</summary>
		public int _Reserved3;
		/// <summary>Reserved; used internally by the system.</summary>
		public int _Reserved4;
		/// <summary>Reserved; used internally by the system.</summary>
		public int _Reserved5;
		/// <summary>Reserved; used internally by the system.</summary>
		public int _Reserved6;
		/// <summary>Reserved; used internally by the system.</summary>
		public int _Reserved7;
		/// <summary>Reserved; used internally by the system.</summary>
		public int _Reserved8;

		#endregion
	}

	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	struct MARGINS
	{
		#region Data

		public int Left;
		public int Top;
		public int Right;
		public int Bottom;

		#endregion

		public MARGINS(int left, int top, int right, int bottom)
		{
			Left = left;
			Top = top;
			Right = right;
			Bottom = bottom;
		}

		public MARGINS(int horizontal, int vertical)
		{
			Left = horizontal;
			Top = vertical;
			Right = horizontal;
			Bottom = vertical;
		}

		public MARGINS(int value)
		{
			Left = value;
			Top = value;
			Right = value;
			Bottom = value;
		}
	}

	/// <summary>Contains information about the size and position of a window.</summary>
	[StructLayout(LayoutKind.Sequential)]
	struct WINDOWPOS
	{
		/// <summary>A handle to the window.</summary>
		public IntPtr hwnd;
		/// <summary>
		/// The position of the window in Z order (front-to-back position).
		/// This member can be a handle to the window behind which this window is placed,
		/// or can be one of the special values listed with the SetWindowPos function.
		/// </summary>
		public IntPtr hwndInsertAfter;
		/// <summary>The position of the left edge of the window.</summary>
		public int x;
		/// <summary>The position of the top edge of the window.</summary>
		public int y;
		/// <summary>The window width, in pixels.</summary>
		public int cx;
		/// <summary>The window height, in pixels.</summary>
		public int cy;
		/// <summary>
		/// The window position. This member can be one or more of the following values:
		/// <see cref="M:SWP.DRAWFRAME"/>
		/// <see cref="M:SWP.FRAMECHANGED"/>
		/// <see cref="M:SWP.HIDEWINDOW"/>
		/// <see cref="M:SWP.NOACTIVATE"/>
		/// <see cref="M:SWP.NOCOPYBITS"/>
		/// <see cref="M:SWP.NOMOVE"/>
		/// <see cref="M:SWP.NOOWNERZORDER"/>
		/// <see cref="M:SWP.NOREDRAW"/>
		/// <see cref="M:SWP.NOREPOSITION"/>
		/// <see cref="M:SWP.NOSENDCHANGING"/>
		/// <see cref="M:SWP.NOSIZE"/>
		/// <see cref="M:SWP.NOZORDER"/>
		/// <see cref="M:SWP.SHOWWINDOW"/>
		/// </summary>
		public SWP flags;
	}

	/// <summary>
	/// Specifies Desktop Window Manager (DWM) blur-behind properties.
	/// Used by the <see cref="M:Dwmapi.DwmEnableBlurBehindWindow"/> function.
	/// </summary>
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	struct DWM_BLURBEHIND
	{
		/// <summary>
		/// A bitwise combination of <see cref="T:DWM_BB"/> constant values that indicates
		/// which of the members of this structure have been set.
		/// </summary>
		public DWM_BB dwFlags;
		/// <summary>
		/// <c>true</c> to register the window handle to DWM blur behind;
		/// <c>false</c> to unregister the window handle from DWM blur behind.
		/// </summary>
		[MarshalAs(UnmanagedType.Bool)]
		public bool fEnable;
		/// <summary>
		/// The region within the client area where the blur behind will be applied.
		/// A <c>IntPtr.Zero</c> value will apply the blur behind the entire client area.
		/// </summary>
		public IntPtr hRgnBlur;
		/// <summary>
		/// <c>true</c> if the window's colorization should transition to match the maximized windows;
		/// otherwise, <c>false</c>.
		/// </summary>
		[MarshalAs(UnmanagedType.Bool)]
		public bool fTransitionOnMaximized;
	}

	/// <summary>Contains the flash status for a window and the number of times the system should flash the window.</summary>
	[StructLayout(LayoutKind.Sequential)]
	struct FLASHWINFO
	{
		/// <summary>The size of the structure, in bytes.</summary>
		public uint cbSize;
		/// <summary>A handle to the window to be flashed. The window can be either opened or minimized.</summary>
		public IntPtr hwnd;
		/// <summary>The flash status.</summary>
		public FLASHW dwFlags;
		/// <summary>The number of times to flash the window.</summary>
		public uint uCount;
		/// <summary>
		/// The rate at which the window is to be flashed, in milliseconds.
		/// If dwTimeout is zero, the function uses the default cursor blink rate.</summary>
		public uint dwTimeout;

		public FLASHWINFO(IntPtr hwnd, FLASHW flags, uint count, uint timeout)
		{
			this.cbSize    = (uint)Marshal.SizeOf(typeof(FLASHWINFO));
			this.hwnd      = hwnd;
			this.dwFlags   = flags;
			this.uCount    = count;
			this.dwTimeout = timeout;
		}
	}

	/// <summary>
	/// Contains information about a window's maximized size and position 
	/// and its minimum and maximum tracking size.
	/// </summary>
	/// <remarks>
	/// For systems with multiple monitors, the ptMaxSize and ptMaxPosition members describe the maximized size
	/// and position of the window on the primary monitor, even if the window ultimately maximizes onto a secondary monitor.
	/// In that case, the window manager adjusts these values to compensate for differences between the primary monitor and the
	/// monitor that displays the window. Thus, if the user leaves ptMaxSize untouched, a window on a monitor larger than the
	/// primary monitor maximizes to the size of the larger monitor.
	/// </remarks>
	[StructLayout(LayoutKind.Sequential)]
	struct MINMAXINFO
	{
		/// <summary>Reserved; do not use.</summary>
		public POINT ptReserved;
		/// <summary>
		/// The maximized width (x member) and the maximized height (y member) of the window.
		/// For top-level windows, this value is based on the width of the primary monitor.
		/// </summary>
		public POINT ptMaxSize;
		/// <summary>
		/// The position of the left side of the maximized window (x member) and the position
		/// of the top of the maximized window (y member). For top-level windows, this value
		/// is based on the position of the primary monitor.
		/// </summary>
		public POINT ptMaxPosition;
		/// <summary>
		/// The minimum tracking width (x member) and the minimum tracking height (y member) of the window.
		/// This value can be obtained programmatically from the system metrics SM_CXMINTRACK and SM_CYMINTRACK (see the GetSystemMetrics function).
		/// </summary>
		public POINT ptMinTrackSize;
		/// <summary>
		/// The maximum tracking width (x member) and the maximum tracking height (y member) of the window.
		/// This value is based on the size of the virtual screen and can be obtained programmatically from the system metrics SM_CXMAXTRACK and SM_CYMAXTRACK (see the GetSystemMetrics function).
		/// </summary>
		public POINT ptMaxTrackSize;
	}

	/// <summary>Contains information about a display monitor.</summary>
	/// <seealso>http://msdn.microsoft.com/en-us/library/dd145065%28v=vs.85%29.aspx</seealso>
	[StructLayout(LayoutKind.Sequential)]
	struct MONITORINFO
	{
		/// <summary>The size of the structure, in bytes.</summary>
		public uint cbSize;
		/// <summary>
		/// A <see cref="RECT"/> structure that specifies the display monitor rectangle, expressed in
		/// virtual-screen coordinates. Note that if the monitor is not the primary display monitor,
		/// some of the rectangle's coordinates may be negative values.
		/// </summary>
		public RECT rcMonitor;
		/// <summary>
		/// A <see cref="RECT"/> structure that specifies the work area rectangle of the display monitor,
		/// expressed in virtual-screen coordinates. Note that if the monitor is not the primary display
		/// monitor, some of the rectangle's coordinates may be negative values.
		/// </summary>
		public RECT rcWork;
		/// <summary>
		/// A set of flags that represent attributes of the display monitor.
		/// </summary>
		public uint dwFlags;
	}

	/// <summary>Contains stream data.</summary>
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	struct WIN32_STREAM_ID
	{
		/// <summary>Type of data.</summary>
		public BACKUP dwStreamId;
		/// <summary>Attributes of data to facilitate cross-operating system transfer.</summary>
		public STREAM dwStreamAttributes;
		/// <summary>Size of data, in bytes.</summary>
		public ulong Size;
		/// <summary>Length of the name of the alternative data stream, in bytes.</summary>
		public uint dwStreamNameSize;
	}

	#endregion

	#region Handles

	sealed class ModuleHandle : SafeHandle
	{
		public ModuleHandle()
			: base(IntPtr.Zero, ownsHandle: true)
		{
		}

		public ModuleHandle(IntPtr handle)
			: this()
		{
			SetHandle(handle);
		}

		public bool Preserve { get; set; }

		public override bool IsInvalid => handle == IntPtr.Zero;

		protected override bool ReleaseHandle()
		{
			if(!Preserve)
			{
				return Kernel32.FreeLibrary(handle);
			}
			return true;
		}
	}

	#endregion

	#region Helpers

	sealed class NativeLibrary : IDisposable
	{
		private ModuleHandle _handle;

		public static NativeLibrary Load(string fileName)
		{
			Verify.Argument.IsNeitherNullNorWhitespace(fileName, nameof(fileName));

			var handle = Kernel32.LoadLibrary(fileName);
			if(handle == null || handle.IsInvalid)
			{
				throw new InvalidOperationException($"Failed to load library '{fileName}'.");
			}
			return new NativeLibrary(handle);
		}

		public static NativeLibrary Load(string fileName, LoadLibraryFlags flags)
		{
			Verify.Argument.IsNeitherNullNorWhitespace(fileName, nameof(fileName));

			var handle = Kernel32.LoadLibraryEx(fileName, IntPtr.Zero, flags);
			if(handle == null || handle.IsInvalid)
			{
				throw new InvalidOperationException($"Failed to load library '{fileName}'.");
			}
			return new NativeLibrary(handle);
		}

		private NativeLibrary(ModuleHandle handle)
		{
			Verify.Argument.IsNotNull(handle, nameof(handle));

			_handle = handle;
		}

		public bool Preserve
		{
			get { return _handle.Preserve; }
			set { _handle.Preserve = value; }
		}

		public bool AddRef()
		{
			bool success = false;
			if(_handle != null && !_handle.IsInvalid)
			{
				_handle.DangerousAddRef(ref success);
			}
			return success;
		}

		public void Release()
		{
			if(_handle != null && !_handle.IsInvalid)
			{
				_handle.DangerousRelease();
			}
		}

		public T GetMethod<T>(string methodName)
			where T : class
		{
			Verify.Argument.IsNotNull(methodName, nameof(methodName));
			Verify.State.IsNotDisposed(this, IsDisposed);

			var procAddress = Kernel32.GetProcAddress(_handle, methodName);
			if(procAddress == IntPtr.Zero)
			{
				return default(T);
			}
			return (T)(object)Marshal.GetDelegateForFunctionPointer(procAddress, typeof(T));
		}

		public Delegate GetMethod(string methodName, Type delegateType)
		{
			Verify.Argument.IsNotNull(methodName, nameof(methodName));
			Verify.Argument.IsNotNull(delegateType, nameof(delegateType));
			Verify.State.IsNotDisposed(this, IsDisposed);

			var procAddress = Kernel32.GetProcAddress(_handle, methodName);
			if(procAddress == IntPtr.Zero)
			{
				return default(Delegate);
			}
			return Marshal.GetDelegateForFunctionPointer(procAddress, delegateType);
		}

		public bool IsDisposed
		{
			get
			{
				var handle = _handle;
				return handle == null || handle.IsInvalid || handle.IsClosed;
			}
		}

		public void Dispose()
		{
			if(_handle != null)
			{
				_handle.Dispose();
				_handle = null;
			}
		}
	}

	#endregion
}
