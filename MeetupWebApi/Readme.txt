This Web Api implements REST-style functionality for working with meetups.
Before running add or create configuration file appsetting.json to MeetupWebApi.WEB project.
In this file must be the following sections:

1.	"ConnectionStrings": {
    		"DefaultConnection": "Host=custom_host;Port=1111;Database=custom_db;Username=custom_name;Password=custom_password"
  	}

to connect to the PostgreSQL database. To create a new database use migration. If the database exists, application will
create a new table with 3 test meetups.

2.	"Serilog": {
    		"Using": [ "Serilog.Sinks.Console", "Serilog.Sinks.File" ],
    		"MinimumLevel": "Debug",
    		"WriteTo": [
      		{
        			"Name": "Console",
        			"Args": {
          			"outputTemplate": "[{Timestamp:HH:mm:ss} {SourceContext} [{Level}] {Message}{NewLine}{Exception}"
        			}
      		},
      		{
        			"Name": "File",
        			"Args": {
          			"path": "Logs/logs.txt",
          			"outputTemplate": "[{Timestamp:HH:mm:ss} {SourceContext} [{Level}] {Message}{NewLine}{Exception}",
          			"formatter": {
            		"type": "Serilog.Formatting.Compact.CompactJsonFormatter, Serilog.Formatting.Compact"
          			}
        		}	
      	}
    		],
    		"Enrich": [
      		"FromLogContext",
      		"WithMachineName",
      		"WithThreadId"
    		],
    		"Properties": {
      	"Application": "Serilog Demo"
    	}

to configure logging using Serilog

3.	"IdentityServer": {
    		"Authority": "custom_identity_host",
    		"ApiName": "custom_api",
    		"RequireHttpsMetadata": true or false (true to use SSL)
  	}

to connect to the IdentityServer.

4. Section with allowed hosts

"AllowedHosts": "*"

Then run the project (may need to install nuget-packages)

