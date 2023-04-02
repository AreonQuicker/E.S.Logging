using System;

namespace E.S.Logging.Models;

public class LogSearchRequest
{
    public string Level { get; set; }
    public string System { get; set; }
    public string SubSystem { get; set; }
    public string Operation { get; set; }
    public string LoggedInUser { get; set; }
    public string Key { get; set; }
    public string Status { get; set; }
    public DateTime End { get; set; }
    public DateTime Start { get; set; }
}