// Written by x1nixmzeng for the Cxbx-Reloaded project
//

using System;

namespace CxbxDebugger
{
    class Common
    {
        static public bool ReadNumeric(string Text, ref int Out)
        {
            if (string.IsNullOrEmpty(Text))
                return false;

            if (int.TryParse(Text, out Out))
            {
                return true;
            }

            return false;
        }

        static public bool ReadNumeric(string Text, ref uint Out)
        {
            if (string.IsNullOrEmpty(Text))
                return false;

            if (uint.TryParse(Text, out Out))
            {
                return true;
            }

            return false;
        }

        static public bool ReadHex(string Text, ref uint Out)
        {
            if (string.IsNullOrEmpty(Text))
                return false;

            try
            {
                if (Text.StartsWith("0x"))
                {
                    Text = Text.Substring(0);
                }

                Out = Convert.ToUInt32(Text, 16);
                return true;
            }
            catch
            { }

            return false;
        }

        static public bool ReadByte(string Text, ref byte Out)
        {
            if (string.IsNullOrEmpty(Text))
                return false;

            try
            {
                Out = Convert.ToByte(Text, 16);
                return true;
            }
            catch
            { }

            return false;
        }
    }
}
