# C# Samples

These samples are setup to guide C++ programmers into C# beginning with the classic "Hello World!" and then introducing additional concepts one at a time.

## Requirements

These samples will include some language features as new as C# 10. You should have (minimum) Visual Studio 2022 installed on your system supporting .NET 7.0.

## Prerequisites

It is assumed that the reader has some experience with programming languages and, more specifically, experience with C++. Several concepts that are already present in a language like C++ won't be explained here and you need to be familiar with them. Some of those concepts are (but are not limited to):

1. Basic programming constructs (variables, arrays, loops, io).
2. Class types (*class* and *struct*)
3. Class vs instance members (*static*)
4. Basic access (*public*, *private*, *protected*)
5. Namespaces

## How to proceed

All of the details are in the code. Go through the numbered samples one at a time opening the solution file in **Visual Studio** or opening the folder in **Visual Studio Code**. Then read through the comments in the source file(s) thoroughly.

### Visual Studio

Each folder contains a solution file and a project file. Open the solution file in Visual Studio (2022 or later), build, and then run. Note that these are all console apps. That means that if you run any of the apps from within Visual Studio, the console could disappear before you have a chance to see anything.

By default, Visual Studio 2022 pauses at the end of a console application and prompts the user to hit any key to close the console. If you're not seeing this, you need to open up your options dialog *Tools|Options*, go to the *General* section under *Debugging*, and make sure that the option *Automatically close the console when debugging stops* is **NOT** selected. 

### Visual Studio Code

You can build/run these samples in Visual Studio Code. Simply open the desired folder in Code. The first time you do so, you'll likely see a prompt stating *Required assets to build and debug are missing. Add them?*. Select *Yes* and note that Code creates a folder named *.vscode* in your project folder. To run the same, do the following:

1. Select *Terminal|New Terminal* from the menu or type *Ctrl+Shift+'*.
2. In the new terminal, type *dotnet run*. At that point the sample should run and you should see the results.

### At the end

You can run *clean.cmd* from the top level folder to loop through all of the sample folders and clean up build artifacts/outputs/etc.