# CampfireTraceListener

CampfireTraceListener is an ASP.NET TraceListener for sending trace messages to a campfire room. 

This project includes a demo program that shows basic usage

Code for the CustomTraceListener base class is from Ukadc.Diagnostics: http://ukadcdiagnostics.codeplex.com 

## Configuration

Configuration example can be found in the demo project, under CampfireTraceListener.Demo\app.config


## Usage

Using the TraceSource class:

     private static readonly TraceSource _traceSource = new TraceSource("CampfireTraceListenerDemo");

     _traceSource.TraceEvent(TraceEventType.Error, 0, "Error message sent through Campfire");

Or 

     Trace.Write("Trace message sent through Campfire");
