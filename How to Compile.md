# Build Landtory

In the following list, you will get all the libs and things you require.

## Requirements

These are the library you require.

|          Name          |                            Factor                            |   Included   |
| :--------------------: | :----------------------------------------------------------: | :----------: |
|  ScriptHookDotNet.dll  |                    The basic of Landtory                     |   INCLUDED   |
| NativeFunctionHook.dll | Download at [here](http://github.com/RelaperCrystal/NativeFunctionHook) | NOT INCLUDED |
|  Newtonsoft.Json.dll   |                    Used to make settings                     | NOT INCLUDED |

You also require these tools:

|             Name              |              Factor              | Included | Required |
| :---------------------------: | :------------------------------: | :------: | :------: |
|  Visual Studio 2017 or later  |  IDE to build the Landtory code  |    NO    |   Yes    |
| Git for Windows / TortoiseGit | The tools to acquire latest code |    NO    |   Yes    |
|        Github Plug-in         |        Recommended to use        |    NO    |    No    |

## How to build

First, open the **Git Bash** or **Git CMD**. Then, type the following code:

`git clone https://github.com/RelaperCrystal/Landtory.git`

The command will clone the latest code into your machine.

![Make sure you are getting these files properly](Res-Files.png)

Then, open the `Landtory.sln` using Visual Studio 2017 or later. You may notified the Solution version is for 2010, because the project was created by that but moved to 2013 Later, then 2017, and now using .NET Framework 4.5. **Do not open the Solution using Visual Studio 2010 / 2013 at any rate. It may broke the code. If you do that, Please re-clone a copy of codes and do not make any commits, push.**

Open Your Visual Studio, click Build in your menu bar. Build the whole solution.

After building Landtory, please make sure you copied the files into GTAIV like This:

![Landtory Structure Main](Res-FinalCopy.png)

In The Scripts Folder, rename `Landtory.dll` into `Landtory.net.dll` to make sure .NET Script Hook load the Landtory. The Main Logic inside the `Landtory.dll` is to make some script-only functions and load the `Landtory.Engine.dll`, `NativeFunctionHook.dll`.