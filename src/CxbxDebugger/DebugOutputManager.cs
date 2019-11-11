// Written by x1nixmzeng for the Cxbx-Reloaded project
//

using System.Collections.Generic;
using System.Windows.Forms;

namespace CxbxDebugger
{
    class DebugOutputManager
    {
        string LastString = string.Empty;
        uint LastStringDupes = 1;
        ListBox Handler;

        public DebugOutputManager(ListBox Target)
        {
            Handler = Target;
        }

        private void RefreshUI()
        {
            // TODO: filter
        }

        public List<string> Lines { get; private set; } = new List<string>();

        public void AddLine(string Message)
        {
            bool immediateDupe = false;

            if (LastString == Message)
            {
                // Bump the number of times this was duplicated
                ++LastStringDupes;

                immediateDupe = true;
            }
            else
            {
                LastString = Message;
                LastStringDupes = 1;

                Lines.Add(Message);
            }

            if (Handler.InvokeRequired)
            {
                Handler.Invoke(new MethodInvoker(delegate ()
                {
                    if (immediateDupe)
                        HandleDupe();
                    else
                        Handler.Items.Insert(0, Message);
                }));
            }
            else
            {
                if (immediateDupe)
                    HandleDupe();
                else
                    Handler.Items.Insert(0, Message);
            }
        }

        private void HandleDupe()
        {
            Handler.Items[0] = $"{LastString} ({LastStringDupes} dupes)";
        }
    }
}
