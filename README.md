# Cxbx Reloaded Debugger

The Cxbx Debugger is a tool to provide debugging capability to Xbox executables emulated by Cxbx Reloaded.

*Note: This repo is a fork of the original project that aims to expand on the original tool as a drop-in replacement*

![Cxbx-Debugger screenshot at breakpoint](https://i.imgur.com/DaNEbBC.png)

![Cxbx-Debugger screenshot at memory editor](https://i.imgur.com/zd076l7.png)


## How it works

The debugger is a C# application that is launched through Cxbx Reloaded after opening an XBE file. The launch option is `Emulation -> Start Debugger`.

Cxbx Reloaded will launch `cxbxr-debugger.exe` and pass on a specific set of arguments.

Cxbx Debugger will start a new instance of Cxbx Reloaded (passing on the arguments to `cxbx.exe`) with debug privileges

The debugger makes use of specific exception messages that the emulator will trigger (such as file events)


## Links

* The C# implementation of the Windows debugger API is taken from [@VsChromium](https://github.com/chromium/vs-chromium)
* Sebastian Solnica ([@lowleveldesign](https://github.com/lowleveldesign)) for [Process Governor](https://github.com/lowleveldesign/process-governor) and [MinDBG](https://github.com/lowleveldesign/mindbg)
* Alexander Bothe ([@aBothe](https://github.com/aBothe)) for [DDebugger](https://github.com/aBothe/DDebugger)
