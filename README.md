## Summary
This started as the basic coding challenge below but has been extended to investigate various .Net (6) core Web Api features, patterns etc.

#### The Challenge
Assume currently, we require our customers to provide their financial data for risk evaluation purpose every 6 months via email. We would like to speed up this process by connecting to their accounting software and pull down their sales records. 

#### Background: 
Let’s assume, we want to support Xero to start with. 

#### Requirements:
- Pull down last 6 months of invoices with critical data points
    - Invoice customer name
    - Invoice issue date
    - Invoice original amount
    - Invoice outstanding amount

- Analyse the data, present the 'healthiness' result plus the latest 10 sample invoices to user’s screen.
- A healthy account is:
    - No outstanding invoice is older than 90 days
    - The sum of all invoices’ original amount for the last 90 days is greater than 100k 


Mocked data or fake services including Xero api are freely to be used for this test purpose. It is an open solution, but we would like to see a working solution at least.
