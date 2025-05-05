using Microsoft.Office.Interop.Word;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Documents;
using System.Xml.Linq;
using Xceed.Words.NET;
using static ConsoleApp1.Model.RequestInputModel;

namespace ConsoleApp1
{
    class Program
    {
        public static void FormatDatesInObject(object obj, List<string> dateFields)
        {
            var props = obj.GetType().GetProperties();
            foreach (var prop in props)
            {
                if (prop.PropertyType == typeof(string) &&
                    //dateFields.Contains(prop.Name))
                    dateFields.Any(df => string.Equals(df, prop.Name, StringComparison.OrdinalIgnoreCase)))
                {
                    var value = prop.GetValue(obj) as string;
                    if (!string.IsNullOrEmpty(value) && value.Length == 8 && value.All(char.IsDigit))
                    {
                        string formatted = $"{value.Substring(0, 4)}/{value.Substring(4, 2)}/{value.Substring(6, 2)}";
                        formatted = ConvertToPersianNumbers(formatted);
                        prop.SetValue(obj, formatted);
                    }   
                }
            }
        }
        public static string ConvertToPersianNumbers(string input)
        {
            if (string.IsNullOrEmpty(input)) return input;

            var englishDigits = "0123456789";
            var persianDigits = "۰۱۲۳۴۵۶۷۸۹";

            var builder = new StringBuilder(input.Length);
            foreach (char c in input)
            {
                int index = englishDigits.IndexOf(c);
                if (index >= 0)
                    builder.Append(persianDigits[index]);
                else
                    builder.Append(c);
            }
            return builder.ToString();
        }

        public static string WordProcess(string inputpathjson, Root deeserializedobject, string outputPathWord, string outputPathPdf)
        {
            try
            {
                var data = deeserializedobject.result.data;
                var datefields = new List<string>
                {
                    "dateContract",
                    "birthDate",
                    "registrationDate",
                    "guarantorBirthDate",
                    "guarantorRegistrationDate",
                    "firstPaymentDate"
                };
                FormatDatesInObject(data, datefields);
                var replacements = new Dictionary<string, string>
                {
                { "%25%", data.contractNumber },
                { "%13%", data.dateContract },
                { "%10%", data.branchName },
                { "%1%", data.branchNumber }, // Verify no conflict
                { "%52%", data.branchAddress },
                { "%2%", data.firstName },
                { "%3%", data.lastName },
                { "%4%", data.fatherName },
                { "%40%", data.birthDate },
                { "%5%", data.nationalNumber },
                { "%6%", data.birthIssuePlace },
                { "%83%", data.nationalSerial },
                { "%7%", data.nationalCode },
                { "%85%", data.customerForeignId },
                { "%86%", data.customerForeignDocumentNumber },
                { "%82%", data.economicCode },
                { "%9%", data.postalCode },
                { "%8%", data.address },
                { "%84%", data.tellphone },
                { "%41%", data.tellNumber },
                { "%87%", data.email },
                { "%93%", data.companyNamePrefix },
                { "%98%", data.companyNamePostfix },
                { "%94%", data.companyRegistrationId },
                { "%95%", data.registrationDate },
                { "%96%", data.companyEconomicCode },
                { "%97%", data.companyNationalId },
                { "%55%", data.guarantorName },
                { "%56%", data.guarantorFatherName },
                { "%58%", data.guarantorBirthDate },
                { "%57%", data.guarantorNationalNumber },
                { "%59%", data.guarantorBirthIssuPlace },
                { "%105%", data.guarantorNationalSerial },
                { "%60%", data.guarantorNationalCode },
                { "%106%", data.foreignPassportNumber },
                { "%107%", data.foreignId },
                { "%103%", data.guarantorEconomicCode },
                { "%62%", data.guarantorPostalCode },
                { "%61%", data.guarantorAddress },
                { "%102%", data.guarantorTellphone },
                { "%63%", data.guarantorTellphone },
                { "%104%", data.guarantorEmail },
                { "%88%", data.guarantorCompanyName },
                { "%92%", data.guarantorCompanyRegistrationId },
                { "%89%", data.guarantorRegistrationDate },
                { "%90%", data.guarantorCompanyEconomicCode },
                { "%91%", data.guarantorCompanyNationalId },
                { "%17%", data.contractDuration },
                { "%20%", data.facilityAmount },
              //{ "%115%", data. }, // مبلغ به حروف فارسی
                { "%44%", data.fee },
                { "%111%", data.paymentCount },
                { "%65%", data.minusPaymentCount },
                { "%19%", data.paymentAmount },
                { "%27%", data.lastMonthPaymentAmount },
                { "%18%", data.firstPaymentDate },
                { "%77%", data.paybackPeriod },
                { "%45%", data.commitmentRate }
                };

                using (var doc = DocX.Load(inputpathjson))
                {
                    foreach (var pair in replacements)
                    {
                        doc.ReplaceText(pair.Key, pair.Value ?? "");
                    }
                    doc.SaveAs(outputPathWord);
                }
                var wordApp = new Microsoft.Office.Interop.Word.Application();
                var wordDoc = wordApp.Documents.Open(outputPathWord);
                wordDoc.ExportAsFixedFormat(outputPathPdf, Microsoft.Office.Interop.Word.WdExportFormat.wdExportFormatPDF);
                wordDoc.Close(false);
                wordApp.Quit();
                string base64Result = Convert.ToBase64String(File.ReadAllBytes(outputPathPdf));
                return base64Result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        static void Main(string[] args)
        {
            {
                string inputpathjson = File.ReadAllText(@"C:\Users\Emad\PressForm\JsonInput.txt");
                var deserializedobject = JsonConvert.DeserializeObject<Root>(inputpathjson);
                string inputWordTemplate = "C:\\Users\\Emad\\PressForm\\newForm131.docx";
                string outputWordPath = "C:\\Users\\Emad\\PressForm\\Form131Update.docx";
                string outputPdfPath = @"C:\Users\Emad\PressForm\Form131Update2.pdf";
                var wordApp = new Microsoft.Office.Interop.Word.Application();
                var wordDoc =
                    wordApp.Documents.Open(@"C:\Users\Emad\PressForm\Form131Update.docx");
                string pdfPath = @"C:\Users\Emad\PressForm\Form131Update2.pdf";
                wordDoc.ExportAsFixedFormat(pdfPath, WdExportFormat.wdExportFormatPDF);

                wordDoc.Close();
                wordApp.Quit();
                string base64Result = WordProcess(inputWordTemplate, deserializedobject, outputWordPath, outputPdfPath);
                Console.WriteLine(base64Result);
                Console.ReadLine();

            }
        }
    }
}



