# MAC Address API

**NOTE:** The API requires a mysql server to run
_Request examples_: [mac-api.http](./mac-api.http)

## Endpoints

> The {id} field is a number

### Fetch all addresses

```http
GET http://localhost:5160/mac
```

### Fetch a single address

```http
GET http://localhost:5160/{id}
```

### Create a new address

> _address_ must be a valid MAC address separated by either **colons** or **dashes**
>
> _validUntil_ must be a string that can be parsed as a **DateTime**
>
> _email_ must be a valid **email address**

```http
POST http://localhost:5160/mac
Content-Type: application/json

{
  "address": "00:00:00:00:00:00",
  "validUntil": "2050-12-31T23:59:59Z",
  "email": "test.address@gmail.com"
}
```

### Update an address

> The same validation rules apply for this endpoint as for creating a new address

```http
PATCH http://localhost:5160/mac/{id}
Content-Type: application/json

{
  "email": "address.test@gmail.com"
}
```

### Delete an address

```http
DELETE http://localhost:5160/{id}
```
