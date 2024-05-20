# WalletTopUp

WalletTopUp is a .NET web application for topping up wallets and retrieving active beneficiaries of a user.

## ExternalUserApi

ExternalUserApi is an external web API to handle account credit, debit, and balance inquiries.

## Steps to Run

1. Start both the WalletTopUp and ExternalUserApi projects simultaneously.
2. Open the Swagger UI for WalletTopUp.
3. Use the endpoint `/api/Wallet/topup` with the following payload:

```json
{
  "topUpOption": 100,
  "beneficiaryId": 1,
  "userId": 1
}

4. It will call external httpClient app to get the account balance and debit the amount.
Thanks


