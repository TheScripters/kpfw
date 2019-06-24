using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for RedirectTable
/// </summary>
public static class RedirectTable
{
    public static Dictionary<string, string> CapsList = new Dictionary<string, string>
    {
        { "LL&S_Cross_Over", "ls-rufus" },
        { "graduation.php", "graduation-pt-1" },
        { "robots.txt", "/robots.txt" },
        { "bundle/css", "/bundle/css" }
    };
}