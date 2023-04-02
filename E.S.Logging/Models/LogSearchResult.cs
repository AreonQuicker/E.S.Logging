using System;

namespace E.S.Logging.Models;

public class LogSearchResult
{
    public string Level { get; set; }
    public string System { get; set; }
    public string SubSystem { get; set; }
    public string Operation { get; set; }
    public string LoggedInUser { get; set; }
    public string Key { get; set; }
    public string Status { get; set; }
    public DateTime Date { get; set; }
    public string Message { get; set; }
    public string RequestPath { get; set; }
    public string Exception { get; set; }
}