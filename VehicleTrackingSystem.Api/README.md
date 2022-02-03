# Introduction 
The solutions is to track vehicles position using GPS navigation. A device emboarded in a vehicle, communicates with this API to register the vehicle and update its position.

# Getting Started
The VehicleTrackingSystem.zip contains following file.
- "VehicleTrackingSystem" folder which is net6.0 project
- "dump-vehicle-tracking-system-202202040117" - a db dump file to create db
- "plain-dump vehicle-tracking-system" - a plain db dump where you can read the queries if needed
- "ERD - Vehicle-Tracking-System" - db ERD diagram



Code -> 
Open code in Visual Studio 2022 as it uses net6.0. 
There are few newGet dependencies, which are mentioned in "VehicleTrackingSystem.Api > Properties > libman.json". I have kept this dependencies in project for easy access.
Set VehicleTrackingSystem.Api as starting project and run the project it will open a swagger page where you can access all the api endpoints.


DB ->
To run this project, one need to setup database for which dump (dump-vehicle-tracking-system-202202040117) is given in the zip file. Postgresql (14.1) DB is used for this project.
Link to download postgresql -> https://www.postgresql.org/download/
To restore given script please follow doc link -> https://www.postgresql.org/docs/8.1/backup.html


Swagger -> 
Swagger is configured and can be located at /swagger/index.html


Authentication -> 
All endpoints are secured by JWT token authentication (except api/token).
api/location/get - To access this particular endpoint Admin authentication is required.



# Build and Test
There are 2 unit test project.
Unit tests are created using NUnit.