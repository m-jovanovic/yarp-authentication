# YARP Authentication

There are a few sample requests you can send from the `gateway.http` file.
The gateway is configured to use token authentication, so you can easily test this. 

```
@BaseUrl =
@AccessToken =

### Login - pass `isVip=true` to add the respective claim

GET {{BaseUrl}}/login?isVip=true

### Proxy api/hello

GET {{BaseUrl}}/api/hello
Authorization: Bearer {{AccessToken}}

### Proxy api/hello-vip

GET {{BaseUrl}}/api/hello-vip
Authorization: Bearer {{AccessToken}}
```
