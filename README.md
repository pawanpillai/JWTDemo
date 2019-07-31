# JWTDemo
A sample project on how to refresh JWT token using ASP.NET Core 2.0 API.

![alt text](https://github.com/pawanpillai/JWTDemo/blob/master/architecture.png)

This is adaptation project from the original project here: http://www.c-sharpcorner.com/article/handle-refresh-token-using-asp-net-core-2-0-and-json-web-token/. 

--------------------------------

# How to use:

1. Initial call with username and password. This will generate Access Token and Refresh Token:

```javascript
HTTP POST Call to: http://localhost:7000/v1/api/auth

    Sample JSON Data in POST:
    {
	    "grant_type": "password",
	    "client_id": "100",
	    "client_secret": "0000",
	    "username": "appsu",
	    "password": "123456",
	    "usertype": "user"
    }

    //Response Format:
    {
	"status": true,
	"code": "999",
	"message": "Ok",
	"data": "{\n    \"access_token\": \"eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMDAiLCJqdGkiOiI5NmFmMTVlMC1mMmMyLTRkY2ItYjFmYy04MWQ0OWEyZjBmNTYiLCJpYXQiOiIwNy8zMS8yMDE5IDE1OjMzOjQyIiwiTmFtZSI6ImFwcHN1IiwiVXNlclR5cGUiOiJ1c2VyIiwibmJmIjoxNTY0NTg3MjIyLCJleHAiOjE1NjQ1ODcyODIsImlzcyI6Imh0dHA6Ly9sb2NhbGhvc3Q6NzAwMCIsImF1ZCI6IlJlc291cmNlU2VydmVyIn0.Rzl4ne2J6_4gpNI_KNJZ54pb5S0-kvB1nM_O9PW1dWk\",\n
			\"expires_in\": 60,\n
			\"refresh_token\": \"3f7759ffe87f4b7e985072f1b6acb4b8\"
		 \n}"
    }
```              

2. Future calls to Token Server to generate new Refresh Token:
```javascript
HTTP POST Call to: http://localhost:7000/v1/api/auth

    Sample JSON Data in POST:
    {
	    "grant_type": "refresh_token",
	    "client_id": "100",
	    "client_secret": "0000",
	    "refresh_token": "3f7759ffe87f4b7e985072f1b6acb4b8"     //use the refresh_token from above response for 1st time, then use refresh_token from it's own response below
    }

    //Response Format:
    {
	"status": true,
	"code": "999",
	"message": "Ok",
	"data": "{\n    \"access_token\": \"eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMDAiLCJqdGkiOiI5NmFmMTVlMC1mMmMyLTRkY2ItYjFmYy04MWQ0OWEyZjBmNTYiLCJpYXQiOiIwNy8zMS8yMDE5IDE1OjMzOjQyIiwiTmFtZSI6ImFwcHN1IiwiVXNlclR5cGUiOiJ1c2VyIiwibmJmIjoxNTY0NTg3MjIyLCJleHAiOjE1NjQ1ODcyODIsImlzcyI6Imh0dHA6Ly9sb2NhbGhvc3Q6NzAwMCIsImF1ZCI6IlJlc291cmNlU2VydmVyIn0.Rzl4ne2J6_4gpNI_KNJZ54pb5S0-kvB1nM_O9PW1dWk\",\n
			\"expires_in\": 60,\n
			\"refresh_token\": \"e6817614bcab4aafa41259cd48029c20\"
		 \n}"
    }
 ```     

3. And add all controller with [Authorize] decorator to restrict unwanted access. Like:
```javascript
Route("v1/api/[controller]")]
[Authorize]
public class SomeController : Controller, ISomeController
{
	Response response;
	//.....
}
```

4. From client end, you have to send refresh_token before each HTTP call. Best way is to implemnent HTTP Interceptor. See /client/src/app/common/http-error-filter.ts and client/src/app/common/http-filter.ts. These are registered in app.module.ts.
