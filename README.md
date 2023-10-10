

![cbimage (2)](https://github.com/jil1710/readmedemo/assets/125335932/dcea5a56-7d8f-429e-b3f7-778f4b5f3118)

# Stripe Integration

- Stripe is a payment service provider that lets merchants accept credit and debit cards or other payments. Its payment processing solution, Stripe Payments, is best suited for businesses that make most of their sales online, as most of its unique features are primarily geared toward online sales.
  
# How does Stripe payment processing work?
- Stripe processes payments in six steps.

  - The customer provides their card information, either online or in person.
  - Those card details enter Stripe’s payment gateway, which encrypts the data.
  - Stripe sends that data to the acquirer, which is a bank that will process the transaction on the merchant’s behalf. In this step, Stripe serves as the merchant (with the business owner as a submerchant). This means Stripe users don’t have to set up a merchant account,     which can be cumbersome.
  - The payment passes through a credit card network, such as Visa or Mastercard, to the cardholder’s issuing bank.
  - The issuing bank approves or denies the transaction.
  - That signal travels from the issuing bank through the card network to the acquirer, then through the gateway to the customer — who sees a message telling them the payment has been accepted or declined.
 
    ![image](https://github.com/jil1710/readmedemo/assets/125335932/90ab2d4d-0751-4494-900c-9536847ce7bf)

# Let's implement one checkout prototype using stripe integration

- **Step 1 :** Create stripe account by clicking [stripe dashboard](https://dashboard.stripe.com/)

- **Step 2 : Get Stripe keys**

   - At the top of your Stripe Dashboard, you got a link named Developers. Click that and also enable Test mode by switching the button next to Developers.

    ![image](https://github.com/jil1710/readmedemo/assets/125335932/0cf97f8d-c911-4551-bc17-f3e4a3c109b5)

    ![image](https://github.com/jil1710/readmedemo/assets/125335932/93347d95-ac39-4bcd-9b4e-12f77d875c44)

   - What are the keys used for?

      - The Publishable key is used by the client application. This could be a web or mobile app making connections.
      - The Secret key is used in the authentication process at the backend server (API Server). By default, we use this key to perform any of the requests between our Web API and Stripe.

- **Step 3 : Create Asp.Net Core Web app and in `appsettings.json` add public key and secret key. After that install below nuget package**

  Install through Package Manager Console:

    ```csharp
        Install-Package Stripe.net
    ```

- **Step 4 : Add Stript Apikey in Program.cs file to interact with stripe account**

    ![image](https://github.com/jil1710/Stripe/assets/125335932/72f4c33c-33e9-4fc3-b400-3da92b866c71)


- **Step 5 : Let's create checkout functionality when user click on checkout the we redirect to stripe payment page as shown in figure.**

  - Here is our checkout UI :
  
    ![image](https://github.com/jil1710/readmedemo/assets/125335932/964e1736-6bc2-4fa7-971a-bf92e83be657)

  - After Clicking the `Checkout` button ajax call to `Checkout Action Method` and After Configuring the stripe UI Page and create checkout session we redirect to stripe payment page

    ![image](https://github.com/jil1710/readmedemo/assets/125335932/f8ff618f-4d62-4022-bc8f-95a7d2c61a5f)
    ![image](https://github.com/jil1710/readmedemo/assets/125335932/9e5b5557-2c3c-4a66-89e0-fa2eb05c755d)
    ![image](https://github.com/jil1710/readmedemo/assets/125335932/4fcbfb5d-9409-4da4-b775-05b8a671925e)

  - Here is the stripe UI look like that we setup using above options

    ![image](https://github.com/jil1710/readmedemo/assets/125335932/27f3ae61-03fa-468e-aba5-3313cde24db1)

  - After Applying the Promocode, here i use `JILPATEL` as promocode and off 30% amount that i already configure in my stript dashboard.
 
      ![image](https://github.com/jil1710/readmedemo/assets/125335932/a3c723b5-4468-415d-a446-405f6b86673f)
      ![image](https://github.com/jil1710/readmedemo/assets/125335932/45489e1a-472f-45f3-9900-ca26cce9795b)  
      ![image](https://github.com/jil1710/readmedemo/assets/125335932/074543a7-864d-4261-964e-834440e56e68)

  - After filling the card details, we can place the order. For testing [click here to get stripe test card](https://stripe.com/docs/testing).

    ![image](https://github.com/jil1710/readmedemo/assets/125335932/e43b7c68-93d2-4ac8-bfbd-909dc09d89ff)
    ![image](https://github.com/jil1710/readmedemo/assets/125335932/dbb75899-5180-4802-9b50-75a285e9d72b)


















