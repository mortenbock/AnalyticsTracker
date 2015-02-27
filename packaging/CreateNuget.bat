if not exist Release mkdir Release
..\tools\NuGet\NuGet.exe pack ..\src\AnalyticsTracker\AnalyticsTracker.csproj -Build -Properties Configuration=Release -OutputDirectory Release -Verbosity detailed