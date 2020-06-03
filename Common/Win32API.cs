using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace Common
{
	public class Win32API
	{

		/// <summary> 
		/// 该函数设置由不同线程产生的窗口的显示状态。 
		/// </summary> 
		/// <param name="hWnd">窗口句柄</param> 
		/// <param name="cmdShow">指定窗口如何显示。查看允许值列表，请查阅ShowWlndow函数的说明部分。</param> 
		/// <returns>如果函数原来可见，返回值为非零；如果函数原来被隐藏，返回值为零。</returns> 
		[DllImport("User32.dll")]
		public static extern bool ShowWindowAsync(IntPtr hWnd, int cmdShow);
		/// <summary> 
		/// 该函数将创建指定窗口的线程设置到前台，并且激活该窗口。键盘输入转向该窗口，并为用户改各种可视的记号。系统给创建前台窗口的线程分配的权限稍高于其他线程。 
		/// </summary> 
		/// <param name="hWnd">将被激活并被调入前台的窗口句柄。</param> 
		/// <returns>如果窗口设入了前台，返回值为非零；如果窗口未被设入前台，返回值为零。</returns> 
		[DllImport("User32.dll")]
		public static extern bool SetForegroundWindow(IntPtr hWnd);

		public const int WS_SHOWNORMAL = 1;

		/// <summary>
		/// 注册系统热键
		/// </summary>
		/// <param name="hWnd">窗口句柄</param>
		/// <param name="id">热键ID</param>
		/// <param name="fsModifiers">热键功能键</param>
		/// <param name="vk">键值</param>
		/// <returns></returns>
		[DllImport("user32.dll", SetLastError = true)]
		public static extern bool RegisterHotKey(IntPtr hWnd, int id, KeyModifiers fsModifiers, System.Windows.Forms.Keys vk);

		/// <summary>
		/// 设置剪贴板监听
		/// </summary>
		/// <param name="hWndNewViewer">监听窗口句柄</param>
		/// <returns>下一个监听窗口句柄</returns>
		[DllImport("User32.dll")]
		public static extern int SetClipboardViewer(int hWndNewViewer);

		/// <summary>
		/// 设置系统热键
		/// </summary>
		/// <param name="hWnd">窗口句柄</param>
		/// <param name="id">热键ID</param>
		/// <param name="bCtrl">是否包含功能键Ctrl</param>
		/// <param name="bShift">是否包含功能键Shift</param>
		/// <param name="bAlt">是否包含功能键Alt</param>
		/// <param name="bWindows">是否包含功能键Win</param>
		/// <param name="nowKey">其他键</param>
		/// <returns></returns>
		public static bool SetHotKey(IntPtr hWnd, int id, bool bCtrl, bool bShift, bool bAlt, bool bWindows, System.Windows.Forms.Keys nowKey)
		{
			KeyModifiers modifier = KeyModifiers.None;
			if (bCtrl)
				modifier |= KeyModifiers.Control;
			if (bAlt)
				modifier |= KeyModifiers.Alt;
			if (bShift)
				modifier |= KeyModifiers.Shift;
			if (bWindows)
				modifier |= KeyModifiers.Windows;
			return RegisterHotKey(hWnd, id, modifier, nowKey);
		}
		/// <summary>
		/// 获取前台激活窗口句柄
		/// </summary>
		/// <returns>窗口句柄</returns>
		[DllImport("user32.dll")]
		public static extern IntPtr GetForegroundWindow();

		/// <summary>
		/// 将窗口坐标转换成屏幕坐标
		/// </summary>
		/// <param name="hWnd">窗口句柄</param>
		/// <param name="p">坐标</param>
		/// <returns>是否成功</returns>
		[DllImport("user32.dll")]
		public static extern bool ClientToScreen(IntPtr hWnd, out System.Drawing.Point p);


		/// <summary>
		/// 获取指定窗口的GUI信息
		/// </summary>
		/// <param name="hwnd"></param>
		/// <param name="lpgui"></param>
		/// <returns></returns>
		public static bool GetGUIInfo(IntPtr hwnd, out GUITHREADINFO lpgui)
		{
			uint lpdwProcessId;
			uint threadId = GetWindowThreadProcessId(hwnd, out lpdwProcessId);
			lpgui = new GUITHREADINFO();
			lpgui.cbSize = Marshal.SizeOf(lpgui);
			return GetGUIThreadInfo(threadId, ref lpgui);
		}

		/// <summary>
		/// 发送消息
		/// </summary>
		/// <param name="hwnd">句柄</param>
		/// <param name="wMsg">消息</param>
		/// <param name="wParam">参数</param>
		/// <param name="lParam">参数</param>
		/// <returns></returns>
		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		public static extern int SendMessage(IntPtr hwnd, int wMsg, IntPtr wParam, IntPtr lParam);
		[DllImport("user32.dll")]
		public static extern bool SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);
		[DllImport("user32.dll")]
		public static extern bool SendMessage(IntPtr hwnd, int wMsg, string wParam, string lParam);

		/// <summary>
		/// 获取指定线程GUI信息
		/// </summary>
		/// <param name="hTreadID">线程ID</param>
		/// <param name="lpgui">GUI信息</param>
		/// <returns>是否成功</returns>
		[DllImport("user32.dll", SetLastError = true)]
		public static extern bool GetGUIThreadInfo(uint hTreadID, ref GUITHREADINFO lpgui);

		/// <summary>
		/// 获取线程ID
		/// </summary>
		/// <param name="hwnd">窗口句柄</param>
		/// <param name="lpdwProcessId">线程ID</param>
		/// <returns>线程ID</returns>
		[DllImport("user32.dll")]
		public static extern uint GetWindowThreadProcessId(IntPtr hwnd, out uint lpdwProcessId);


		/// <summary>
		/// 热键中的功能键
		/// </summary>
		[Flags()]
		public enum KeyModifiers
		{
			None = 0,
			Alt = 1,
			Control = 2,
			Shift = 4,
			Windows = 8
		}
		/// <summary>
		/// 系统消息
		/// </summary>
		public enum WinMsg
		{
			WM_USER = 1024,
			WM_DRAWCLIPBOARD = 0x308,
			WM_CHANGECBCHAIN = 0x030D,
			WM_HOTKEY = 0x0312,

			WM_CUT = 0x0300,
			WM_COPY = 0x0301,
			WM_PASTE = 0x0302,
			WM_GETTEXT = 0x000D,

			WM_SYSCOMMAND = 0x0112,
		}

		/// <summary>
		/// 线程GUI信息
		/// </summary>
		[StructLayout(LayoutKind.Sequential)]
		public struct GUITHREADINFO
		{
			public int cbSize;
			public int flags;
			public IntPtr hwndActive;
			public IntPtr hwndFocus;
			public IntPtr hwndCapture;
			public IntPtr hwndMenuOwner;
			public IntPtr hwndMoveSize;
			public IntPtr hwndCaret;
			public RECT rectCaret;
		}
		/// <summary>
		/// 矩形区域
		/// </summary>
		[StructLayout(LayoutKind.Sequential)]
		public struct RECT
		{
			public int iLeft;
			public int iTop;
			public int iRight;
			public int iBottom;
		}


	}
}
