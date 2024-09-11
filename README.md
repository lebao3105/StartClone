# StartClone

A proof-of-concept MetroUI Start Screen, made for Windows 10 and higher.

Very limited at functionalities.

## Why limited?

This is a Store app. Period.

That means what? Limited API. Even calling functions from Shell32.dll didn't work well.

Deprecated API. You can't use Windows.System.UserProfile on Windows 10.

You can't make the window top-most (at least for now).

## Give it a try

Install Visual Studio 2015 (2017 is not tested) and Windows 8.1 SDK.

Use msbuild or run directly in Visual Studio.
