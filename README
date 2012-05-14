What is DistAppLog?
DistAppLog it's a distributed application log, for when you need to log some action or info in your application, but you want to centralize this information in a database to further analisys.

DistAppLog can use a Redis database or Sqlite database to store the logs.

How can i use?
The app are divided in the server lib and the client lib.
First, you need to setup the server, the server it's a dll that can be hosted inside a windows service,a console app or in another app.
In the host application you need to configure if the server will use a redis database or a relational database(Sql server, sqlite, postgre,etc) ,also the communication port with the clients and the web server port.

In the client apps, configure the client to use the server ip and port, and a name for your app(to identify in the server side) and you can start to send some loggin info.

In your class, add a variable like this:

	private static DistALClient.ILog log = DistALClient.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString());

To log some data in the server:
	log.Info("info log of the app");
Other way it's using the static method in the logclient:
	DistALClient.AppLogClient.Instance.SendInfoMessage("Mymodule","Message");

You can log events like error, fatal, warn, debug and info.
Check the example in the solution and give at try in your app.

New in this version:
-Major perfomance improvement implementing a socket pool to send the data and reuse the socket connections
-Now using fluent Nhibernate to access to relational databases

Dependencies:
ZeroMq- To communicate the clients with the server, you need to configure an open tcp port in the server, and have this port open in the client in order to communicate with the server.
Fluent Nhibernate- To abstract the database access,can use any database that have compatibility with nhibernate
ServiceStack.Redis -To save data in a Redis Server, for fast logging
Nancy + Kayak -To serving a simple page to view the logs 

TODO:
-Improve web frontend for administration,log view,search and a REST API for logging
-Maybe create an provider for log4net.

Contact:
twitter: @majimenezp
email: majimenezp@taan.com.mx