param($installPath, $toolsPath, $package, $project)
$file = "NLog.config"
$handle = $project.ProjectItems.Item($file) 
#set 'Copy To Output Directory' to 'Copy Always' 
$copyToOutput = $handle.Properties.Item("CopyToOutputDirectory") 
$copyToOutput.Value = 1
