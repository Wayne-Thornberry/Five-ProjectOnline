
var target = Argument("target", "Deploy");
var platform = "bin";
var deployment = "Release"; 
var configuration = Argument("configuration", $"{deployment}");
var version = Argument("packageVersion", "0.0.1");
var prerelease = Argument("prerelease", "");

var commonDir = "./code/common";
var resourceDir = "./code/resources";
var componentDir = "./code/components";
var libDir = "./../../libs";
var dataDir = "./../../../data/resources";
var toolsDir = "./code/tools";
var resourceFramework = "Proline.Component.Framework";

// a full build would be to build the common first, libs second, resources third, components fourth, tools fifth

var deployDir = "E:/servers/Game_Servers/FiveM/core";
var componentOutputDir = $"{deployDir}/components";
var resourceOutputDir = $"{deployDir}/resources";
var artificatsOutputDir = "./../../../artifacts";


class ProjectInformation
{
    public int ProjectType {get; set;}
    public string OutputDir {get ;set;}
    public string Name { get; set; }
    public string FullPath { get; set; }
    public bool IsTestProject { get; set; }
}

string packageVersion;
ProjectInformation resource;

///////////////////////////////////////////////////////////////////////////////
// SETUP / TEARDOWN
///////////////////////////////////////////////////////////////////////////////

Setup(ctx =>
{
	// Executed BEFORE the first task.
	Information("Running tasks...");

    packageVersion = $"{version}{prerelease}"; 
    var dir = Context.Environment.WorkingDirectory;

    resource = new ProjectInformation
    {
        OutputDir = $"{artificatsOutputDir}/"+dir.GetDirectoryName(),
        Name = dir.GetDirectoryName(),
        FullPath = dir.FullPath,
        ProjectType = 2,
        //IsTestProject = p.GetFilenameWithoutExtension().ToString().EndsWith("Tests")
    };
    

	    Information(resource.Name);
	    Information(resource.FullPath);
        Information(resource.OutputDir);
});

Teardown(ctx =>
{
	// Executed AFTER the last task.
	Information("Finished running tasks.");
});

//////////////////////////////////////////////////////////////////////
// TASKS
//////////////////////////////////////////////////////////////////////
 
Task("Clean") 
    //.WithCriteria(c => HasArgument("rebuild"))
    .Does(() =>
{
	    Information("Cleaning " + resource.OutputDir);
        CleanDirectory(resource.OutputDir);
});

Task("Restore") 
    .IsDependentOn("Clean")
    .Does(() =>
{
        DotNetRestore(resource.FullPath + "/src");
});

Task("Build")
    .IsDependentOn("Restore")
    .ContinueOnError()
    .Does(() =>
{
        DotNetBuild(resource.FullPath + "/src", new DotNetBuildSettings
        {
            Configuration = configuration,  
            OutputDirectory = resource.OutputDir,
            NoRestore = true
        });
})
.DeferOnError();

Task("Test")
    .IsDependentOn("Build")
    .Does(() =>
{
      DotNetTest(resource.FullPath + "/src", new DotNetTestSettings
        {
            Configuration = configuration,
            NoBuild = true,
        });
});

Task("Deploy")
    .IsDependentOn("Build")
    .Does(() =>
{ 
    if(resource.ProjectType == 2)
        { 
            var resourceDeployDir = $"{componentOutputDir}/{resource.Name}";
          
            CopyDirectory($"{artificatsOutputDir}/{resource.Name}", resourceDeployDir); 
	        Information($"Copied {artificatsOutputDir}/{resource.Name}" + " To " + resourceDeployDir); 
        }
});

//////////////////////////////////////////////////////////////////////
// EXECUTION
//////////////////////////////////////////////////////////////////////

RunTarget(target);