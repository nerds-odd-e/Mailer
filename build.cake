#addin "Cake.Powershell"    
#tool "nuget:?package=NUnit.ConsoleRunner"
//////////////////////////////////////////////////////////////////////
// ARGUMENTS
//////////////////////////////////////////////////////////////////////

var target = Argument("target", "Default");
var configuration = Argument("configuration", "Debug");

//////////////////////////////////////////////////////////////////////
// PREPARATION
//////////////////////////////////////////////////////////////////////

// Define directories.
var buildDir = Directory("./Mailer/bin/") + Directory(configuration);

//////////////////////////////////////////////////////////////////////
// TASKS
//////////////////////////////////////////////////////////////////////

Task("Clean")
    .Does(() =>
{
    CleanDirectory(buildDir);
});

Task("Restore-NuGet-Packages")
    .IsDependentOn("Clean")
    .Does(() =>
{
    NuGetRestore("./Mailer.sln");
});

Task("Build")
    .IsDependentOn("Restore-NuGet-Packages")
    .Does(() =>
{
    if(IsRunningOnWindows())
    {
      // Use MSBuild
      MSBuild("./Mailer.sln", settings =>
        settings.SetConfiguration(configuration));
    }
    else
    {
      // Use XBuild
      XBuild("./Mailer.sln", settings =>
        settings.SetConfiguration(configuration));
    }
});

Task("Db-Script")
    .IsDependentOn("Build")
    .Description("Run an example powershell command with parameters")
    .Does(() =>
{
    StartPowershellFile("DbCreation.ps1");
});

Task("Run-Unit-Tests")
    .IsDependentOn("Build")
    .IsDependentOn("Db-Script")
    .Does(() =>
{
    NUnit3("./Mailer.Tests/bin/" + configuration + "/*.Tests.dll", new NUnit3Settings {
        NoResults = true,
        ToolPath="./tools/NUnit.ConsoleRunner.3.8.0/tools/nunit3-console.exe"
        });
});

Task("Run-Acceptance-Tests")
    .IsDependentOn("Build")
    .IsDependentOn("Db-Script")
    .Does(() =>
{
    NUnit3("./Mailer.AcceptanceTests/bin/" + configuration + "/*.AcceptanceTests.dll", new NUnit3Settings {
        NoResults = true,
        ToolPath="./tools/NUnit.ConsoleRunner.3.8.0/tools/nunit3-console.exe"
        });
});


Task("Spec")
    //.IsDependentOn("Build")
    //.IsDependentOn("Db-Script")
    .Does(() =>
{
    NUnit3("./Mailer.AcceptanceTests/bin/" + configuration + "/*.AcceptanceTests.dll", new NUnit3Settings {
        NoResults = true
        });
});

//////////////////////////////////////////////////////////////////////
// TASK TARGETS
//////////////////////////////////////////////////////////////////////

Task("Default")
    .IsDependentOn("Run-Unit-Tests");

//////////////////////////////////////////////////////////////////////
// EXECUTION
//////////////////////////////////////////////////////////////////////

RunTarget(target);