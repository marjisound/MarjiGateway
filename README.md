# MarjiGateway
A checkout gateway API to handle payment requests 

API Endpoints:

### 1- POST /marjigateway/v1/payment
Request Body:
```
{

    "Payment":{
		"CardNumber": "4242424242424242",
		"ExpiryMonth": 5,
		"ExpiryYear": 2021,
		"Amount": 400,
		"Currency": "GBP",
		"Cvv": 434
    }
}
```

Card Number: Must be a valid debit or credit card number
Expiry Year: Greater than current year
Amount: Must be a whole number
Currency: Must be one of (EUR, USD, GBP)
CVV: Must be 3 or 4 digit only

### 2- GET /marjigateway/v1/payment?identifier={id}
