# How to test

Set up the connection string to application insights

```bash
dotnet user-secrets set "ApplicationInsights:ConnectionString" "<connection string coming from azure>"
```

Run the project and in the output window of visual studio an error is displayed

```
OpenTelemetry-AzureMonitor-Exporter - EventId: [2], EventName: [WriteError], Message: [FailedToExport - System.ObjectDisposedException: IFeatureCollection has been disposed.
Object name: 'Collection'.
   at Microsoft.AspNetCore.Http.Features.FeatureReferences`1.ThrowContextDisposed()
   at Microsoft.AspNetCore.Http.Features.FeatureReferences`1.ContextDisposed()
   at Microsoft.AspNetCore.Http.Features.FeatureReferences`1.Fetch[TFeature](TFeature& cached, Func`2 factory)
   at Microsoft.AspNetCore.Http.DefaultHttpRequest.get_Protocol()
   at Microsoft.AspNetCore.Hosting.HostingRequestStartingLog.get_Item(Int32 index)
   at Microsoft.AspNetCore.Hosting.HostingRequestStartingLog.GetEnumerator()+MoveNext()
   at Azure.Monitor.OpenTelemetry.Exporter.Internals.LogsHelper.ExtractProperties(String& message, IDictionary`2 properties, IReadOnlyCollection`1 stateDictionary)
   at Azure.Monitor.OpenTelemetry.Exporter.Internals.LogsHelper.GetMessageAndSetProperties(LogRecord logRecord, IDictionary`2 properties)
   at Azure.Monitor.OpenTelemetry.Exporter.Models.MessageData..ctor(Int32 version, LogRecord logRecord)
   at Azure.Monitor.OpenTelemetry.Exporter.Internals.LogsHelper.OtelToAzureMonitorLogs(Batch`1 batchLogRecord, AzureMonitorResource resource, String instrumentationKey)
   at Azure.Monitor.OpenTelemetry.Exporter.AzureMonitorLogExporter.Export(Batch`1& batch)]
   ```
   
The log message produced by the following code is not sent to azure monitor.
```csharp
logger.LogInformation("Sample log message");
```
 
   
   ## Change log level of Microsoft.AspNetCore
   
   In the `appsettings.json` file, change the log level of Microsoft.AspNetCore to `warning` and run again the project. This time there is no error and the log is sent to azure monitor.
