## Table of Contents

1. [Project Requirements](#project-requirements)
2. [How to run](#how-to-run)
3. [Infinite loop detection](#infinite-loop-detection)

# Project Requirements

Click [here](REQUIREMENTS.md) to read the project requirements

# How to run

### Prerequisite

* .NET 6 - https://dotnet.microsoft.com/en-us/download
* Node.js - https://nodejs.org/en/
* PostgreSQL (I used version 15, but 14 probably works) - https://postgresapp.com/

### Configurations
* PostgreSQL host is configured to be `Host=localhost;Database=postgres;Username=postgres;`
but that can be easily changed in file `/api/Data/SimpleFinderDbContext.cs`

* Default API port is http://localhost:5225
* Default Web-client port is http://localhost:3000

### How to Run
There are 2 servers to be run (api and web-client):
* api is run by going into api folder and executing command `dotnet run`
* web-client is run by going into web-client folder and executing command `npm start`


# Infinite loop detection

I was thinking of all possible scenarios what would be important in a production environment for this app and a very scary thing came to my mind. Infinite loop / Stackoverflow exception in SQL.

I can happen data is being manually touched in files_node table or with bad code.

To avoid that I wrote this small sql query that returns all rows that are in infinite loop and my solution would be to add a trigger on insert that would run this and see if the new entry would make a infinite loop.

### Query (checks for infinite loops in file_node table)

```sql
with  recursive detect_infinites as (
	select fn.parent_id, id as child, 1  as depth
	from file_nodes fn

	union all
  
	select di.parent_id, rn.id as child, di.depth + 1  AS depth
	from detect_infinites di
	join file_nodes rn on di.child = rn.parent_id
	WHERE di.depth < 10
)

SELECT DISTINCT parent_id
FROM detect_infinites
WHERE parent_id = child;
```

<img width="585" alt="image" src="https://user-images.githubusercontent.com/6454831/198977822-1d96f01d-ec54-4ac6-8a0a-a0e85db20c1a.png">
