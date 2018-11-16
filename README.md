# checkout-order-total-kata

[Development exercise](https://github.com/PillarTechnology/kata-checkout-order-total) for Pillar Technology.

### Build and run tests

From root directory:
```
dotnet restore
dotnet build
dotnet test .\GroceryPointOfSale.Test\GroceryPointOfSale.Test.csproj
```

### Run the API and test with Postman

1. Run the API.  From root directory:
```
dotnet run -p .\GroceryPointOfSale.WebApi\
```

2. Execute [Postman](https://www.getpostman.com/).

3. Click the "Import" button, then select "Import From Link."

4. Paste `https://www.getpostman.com/collections/b2fb6c86712425c203bc` in the textbox and press the "Import" button.  This will create a preconfigured collection of API requests.

5. Modify and send requests as desired.
