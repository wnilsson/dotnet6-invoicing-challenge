using System;
using System.Collections.Generic;
using InvoicingService.RestClients.Xero.Entities;

namespace InvoicingService.RestClients.Xero
{
    internal class XeroMockData
    {
        internal static Dictionary<int, XeroInvoice[]> Data { get; }

        static XeroMockData()
        {
            Data = new Dictionary<int, XeroInvoice[]>();

            // Healthy customer - no outstanding invoices older than 90 days and total > 100k
            Data.Add(1, new[]
            {
                new XeroInvoice
                {
                    InvoiceNumber = 1, CustomerId = 1, CustomerName = "Will Wilson", Date = DateTime.Now.AddDays(-200),
                    AmountPaid = 150, AmountDue = 0
                },
                new XeroInvoice
                {
                    InvoiceNumber = 2, CustomerId = 1, CustomerName = "Will Wilson", Date = DateTime.Now.AddDays(-130),
                    AmountPaid = 2000, AmountDue = 0
                },
                new XeroInvoice
                {
                    InvoiceNumber = 3, CustomerId = 2, CustomerName = "Donald Duck", Date = DateTime.Now.AddDays(-100),
                    AmountPaid = 1500, AmountDue = 0
                },
                new XeroInvoice
                {
                    InvoiceNumber = 4, CustomerId = 2, CustomerName = "Donald Duck", Date = DateTime.Now.AddDays(-60),
                    AmountPaid = 55000, AmountDue = 5000
                },
                new XeroInvoice
                {
                    InvoiceNumber = 5, CustomerId = 3, CustomerName = "Rodger Rabbit", Date = DateTime.Now.AddDays(-50),
                    AmountPaid = 5000.5m, AmountDue = 0
                },
                new XeroInvoice
                {
                    InvoiceNumber = 6, CustomerId = 4, CustomerName = "Tom Cruise", Date = DateTime.Now.AddDays(-40),
                    AmountPaid = 800, AmountDue = 0
                },
                new XeroInvoice
                {
                    InvoiceNumber = 7, CustomerId = 5, CustomerName = "Kate Rivers", Date = DateTime.Now.AddDays(-32),
                    AmountPaid = 7000, AmountDue = 0
                },
                new XeroInvoice
                {
                    InvoiceNumber = 8, CustomerId = 6, CustomerName = "Donald Duck", Date = DateTime.Now.AddDays(-28),
                    AmountPaid = 10000, AmountDue = 20000
                },
                new XeroInvoice
                {
                    InvoiceNumber = 9, CustomerId = 6, CustomerName = "Donald Duck", Date = DateTime.Now.AddDays(-21),
                    AmountPaid = 350, AmountDue = 0
                },
                new XeroInvoice
                {
                    InvoiceNumber = 10, CustomerId = 7, CustomerName = "Rodger Rabbit",
                    Date = DateTime.Now.AddDays(-14), AmountPaid = 500, AmountDue = 1500
                },
                new XeroInvoice
                {
                    InvoiceNumber = 11, CustomerId = 8, CustomerName = "Tom Cruise", Date = DateTime.Now.AddDays(-7),
                    AmountPaid = 423.5m, AmountDue = 0
                },
                new XeroInvoice
                {
                    InvoiceNumber = 12, CustomerId = 9, CustomerName = "Kate Rivers", Date = DateTime.Now.AddDays(-3),
                    AmountPaid = 0, AmountDue = 2000
                },
                new XeroInvoice
                {
                    InvoiceNumber = 13, CustomerId = 9, CustomerName = "Kate Rivers", Date = DateTime.Now,
                    AmountPaid = 0, AmountDue = 100
                }
            });
            
            // Unhealthy customer - unpaid invoice over 90 days
            Data.Add(2, new[]
            {
                new XeroInvoice
                {
                    InvoiceNumber = 1, CustomerId = 1, CustomerName = "John Johnson", Date = DateTime.Now.AddDays(-200),
                    AmountPaid = 167, AmountDue = 0
                },
                new XeroInvoice
                {
                    InvoiceNumber = 2, CustomerId = 1, CustomerName = "John Johnson", Date = DateTime.Now.AddDays(-140),
                    AmountPaid = 1988, AmountDue = 0
                },
                new XeroInvoice
                {
                    InvoiceNumber = 3, CustomerId = 2, CustomerName = "Daffy Duck", Date = DateTime.Now.AddDays(-97),
                    AmountPaid = 0, AmountDue = 1500
                },
                new XeroInvoice
                {
                    InvoiceNumber = 4, CustomerId = 3, CustomerName = "Micky Mouse", Date = DateTime.Now.AddDays(-64),
                    AmountPaid = 100000, AmountDue = 20000
                },
                new XeroInvoice
                {
                    InvoiceNumber = 5, CustomerId = 4, CustomerName = "Ben Smith", Date = DateTime.Now.AddDays(-55),
                    AmountPaid = 5000.5m, AmountDue = 0
                },
                new XeroInvoice
                {
                    InvoiceNumber = 6, CustomerId = 4, CustomerName = "Ben Smith", Date = DateTime.Now.AddDays(-37),
                    AmountPaid = 756.3m, AmountDue = 0
                },
                new XeroInvoice
                {
                    InvoiceNumber = 7, CustomerId = 5, CustomerName = "Jane Jones", Date = DateTime.Now.AddDays(-30),
                    AmountPaid = 8965.21m, AmountDue = 0
                },
                new XeroInvoice
                {
                    InvoiceNumber = 8, CustomerId = 6, CustomerName = "Helen Keller", Date = DateTime.Now.AddDays(-26),
                    AmountPaid = 0, AmountDue = 3000
                },
                new XeroInvoice
                {
                    InvoiceNumber = 9, CustomerId = 6, CustomerName = "Helen Keller", Date = DateTime.Now.AddDays(-21),
                    AmountPaid = 397, AmountDue = 0
                },
                new XeroInvoice
                {
                    InvoiceNumber = 10, CustomerId = 7, CustomerName = "Will Smith", Date = DateTime.Now.AddDays(-13),
                    AmountPaid = 670, AmountDue = 1350
                },
                new XeroInvoice
                {
                    InvoiceNumber = 11, CustomerId = 8, CustomerName = "Bruce Burton", Date = DateTime.Now.AddDays(-9),
                    AmountPaid = 597.5m, AmountDue = 0
                },
                new XeroInvoice
                {
                    InvoiceNumber = 12, CustomerId = 9, CustomerName = "Tom Cruise", Date = DateTime.Now.AddDays(-4),
                    AmountPaid = 0, AmountDue = 3587.20m
                },
                new XeroInvoice
                {
                    InvoiceNumber = 13, CustomerId = 10, CustomerName = "Mary Smith", Date = DateTime.Now,
                    AmountPaid = 0, AmountDue = 596
                }
            });
            
            // Unhealthy customer - sum of invoices orig amount < 100000
            Data.Add(3, new[]
            {
                new XeroInvoice
                {
                    InvoiceNumber = 1, CustomerId = 1, CustomerName = "Lewis Neil", Date = DateTime.Now.AddDays(-200),
                    AmountPaid = 150, AmountDue = 0
                },
                new XeroInvoice
                {
                    InvoiceNumber = 2, CustomerId = 1, CustomerName = "Lewis Neil", Date = DateTime.Now.AddDays(-126),
                    AmountPaid = 2000, AmountDue = 0
                },
                new XeroInvoice
                {
                    InvoiceNumber = 3, CustomerId = 2, CustomerName = "Violet	Zellweger",
                    Date = DateTime.Now.AddDays(-86), AmountPaid = 1500, AmountDue = 0
                },
                new XeroInvoice
                {
                    InvoiceNumber = 4, CustomerId = 2, CustomerName = "Violet	Zellweger",
                    Date = DateTime.Now.AddDays(-40), AmountPaid = 10000, AmountDue = 0
                },
                new XeroInvoice
                {
                    InvoiceNumber = 5, CustomerId = 3, CustomerName = "Angela Harding",
                    Date = DateTime.Now.AddDays(-30), AmountPaid = 5000.5m, AmountDue = 0
                },
                new XeroInvoice
                {
                    InvoiceNumber = 6, CustomerId = 4, CustomerName = "Ruth Armstrong",
                    Date = DateTime.Now.AddDays(-22), AmountPaid = 800, AmountDue = 0
                },
                new XeroInvoice
                {
                    InvoiceNumber = 7, CustomerId = 5, CustomerName = "Brian Reeve", Date = DateTime.Now.AddDays(-15),
                    AmountPaid = 7000, AmountDue = 0
                },
                new XeroInvoice
                {
                    InvoiceNumber = 8, CustomerId = 6, CustomerName = "Fay Wilton", Date = DateTime.Now.AddDays(-11),
                    AmountPaid = 0, AmountDue = 3000
                },
                new XeroInvoice
                {
                    InvoiceNumber = 9, CustomerId = 6, CustomerName = "Fay Wilton", Date = DateTime.Now.AddDays(-10),
                    AmountPaid = 350, AmountDue = 0
                },
                new XeroInvoice
                {
                    InvoiceNumber = 10, CustomerId = 7, CustomerName = "Lloyd Bell", Date = DateTime.Now.AddDays(-6),
                    AmountPaid = 500, AmountDue = 1500
                },
                new XeroInvoice
                {
                    InvoiceNumber = 11, CustomerId = 8, CustomerName = "Tom Cruise", Date = DateTime.Now.AddDays(-4),
                    AmountPaid = 423.5m, AmountDue = 0
                },
                new XeroInvoice
                {
                    InvoiceNumber = 12, CustomerId = 9, CustomerName = "Wayne Marsh", Date = DateTime.Now.AddDays(-2),
                    AmountPaid = 0, AmountDue = 2000
                },
                new XeroInvoice
                {
                    InvoiceNumber = 13, CustomerId = 9, CustomerName = "Wayne Marsh", Date = DateTime.Now.AddDays(-1),
                    AmountPaid = 0, AmountDue = 100
                }
            });
        }
    }
}
