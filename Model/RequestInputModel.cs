using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Model
{
    public class RequestInputModel
    {
        public class Data
        {
            public string contractNumber { get; set; }
            public string dateContract { get; set; }
            public string branchName { get; set; }
            public string branchNumber { get; set; }
            public string branchAddress { get; set; }
            public string firstName { get; set; }
            public string lastName { get; set; }
            public string fatherName { get; set; }
            public string birthDate { get; set; }
            public string nationalNumber { get; set; }
            public string birthIssuePlace { get; set; }
            public string nationalSerial { get; set; }
            public string nationalCode { get; set; }
            public string customerForeignId { get; set; }
            public string customerForeignDocumentNumber { get; set; }
            public string economicCode { get; set; }
            public string postalCode { get; set; }
            public string address { get; set; }
            public string tellphone { get; set; }
            public string tellNumber { get; set; }
            public string email { get; set; }
            public string companyNamePrefix { get; set; }
            public string companyNamePostfix { get; set; }
            public string companyRegistrationId { get; set; }
            public string registrationDate { get; set; }
            public string companyEconomicCode { get; set; }
            public string companyNationalId { get; set; }
            public string guarantorName { get; set; }
            public string guarantorFatherName { get; set; }
            public string guarantorBirthDate { get; set; }
            public string guarantorNationalNumber { get; set; }
            public string guarantorBirthIssuPlace { get; set; }
            public string guarantorNationalSerial { get; set; }
            public string guarantorNationalCode { get; set; }
            public string foreignPassportNumber { get; set; }
            public string foreignId { get; set; }
            public string guarantorEconomicCode { get; set; }
            public string guarantorPostalCode { get; set; }
            public string guarantorAddress { get; set; }
            public string guarantorTellphone { get; set; }
            public string guarantorTellNumber { get; set; }
            public string guarantorEmail { get; set; }
            public string guarantorCompanyName { get; set; }
            public string guarantorCompanyRegistrationId { get; set; }
            public string guarantorRegistrationDate { get; set; }
            public string guarantorCompanyEconomicCode { get; set; }
            public string guarantorCompanyNationalId { get; set; }
            public string contractDuration { get; set; }
            public string facilityAmount { get; set; }
            public string fee { get; set; }
            public string paymentCount { get; set; }
            public string minusPaymentCount { get; set; }
            public string paymentAmount { get; set; }
            public string lastMonthPaymentAmount { get; set; }
            public string firstPaymentDate { get; set; }
            public string paybackPeriod { get; set; }
            public string commitmentRate { get; set; }
        }

        public class Meta
        {
            public string transactionId { get; set; }
        }

        public class Result
        {
            public Data data { get; set; }
            public Status status { get; set; }
        }

        public class Root
        {
            public Result result { get; set; }
            public Status status { get; set; }
            public Meta meta { get; set; }
        }

        public class Status
        {
            public string code { get; set; }
            public string message { get; set; }
        }
    }
}
