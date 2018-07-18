# Kim Possible Fan World

This project is the open source development arm of the website [https://www.kpfanworld.com](https://www.kpfanworld.com). Those wanting to help in development are encouraged to contribute.

## Building/Running

This project is written in ASP.NET Webforms in C#. You will need Visual Studio running on Windows to run it.

The database backup file is SQL Server 2016 (Developer Edition can be downloaded for free or Express can be installed)

### Requirements

* .NET Framework 4.7.1
* Visual Studio
* SQL Server 2016 Developer or Express (or newer)

## Notes

The kpfanworld.com domain is preloaded in the HSTS lists and so you will need to run it on localhost or get redirected to a secure version automatically.

There are rules in the web.config to handle localhost and not redirect.