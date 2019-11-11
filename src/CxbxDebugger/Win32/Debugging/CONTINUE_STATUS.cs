namespace VsChromium.Core.Win32.Debugging {
  public enum CONTINUE_STATUS : uint {
    DBG_CONTINUE = 0x00010002,
    DBG_REPLY_LATER = 0x40010001, // Win10 1507+
    DBG_EXCEPTION_NOT_HANDLED = 0x80010001,
  }
}