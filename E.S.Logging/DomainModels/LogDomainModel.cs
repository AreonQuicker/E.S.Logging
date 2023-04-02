using System;
using E.S.Data.Query.Context.Attributes;
using E.S.Data.Query.Context.Enums;

namespace E.S.Logging.DomainModels;

[DataQueryContext("Logs", "Logging")]
public class LogDomainModel
{
    [DataQueryContextIdProperty]
    [DataQueryContextProperty(DataQueryContextPropertyFlags.None)]
    public int Id { get; set; }

    public string Message { get; set; }
    public string Level { get; set; }
    public DateTime TimeStamp { get; set; }
    public string Exception { get; set; }
    public string RequestPath { get; set; }
    public string LoggedInUser { get; set; }
    public string System { get; set; }
    public string SubSystem { get; set; }
    public string Operation { get; set; }
    public string Key { get; set; }
    public string UserMessage { get; set; }
    public string Status { get; set; }
}