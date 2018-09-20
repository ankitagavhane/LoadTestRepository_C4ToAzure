using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Twinfield_NewFramework
{
    public static class CommonCode
    {
        public static T Random<T>(this IEnumerable<T> list)
        {
            if (list == null || list.Count() == 0) return default(T);
            return list.ElementAt(new Random().Next(0, list.Count() - 1));
        }
    }

    public class View
    {
        public string href { get; set; }
    }

    public class Delete
    {
        public string href { get; set; }
    }

    public class Links
    {
        public View view { get; set; }
        public Delete delete { get; set; }
    }

    public class Item
    {
        public string id { get; set; }
        public string name { get; set; }
        public string number { get; set; }
        public string addressLine1 { get; set; }
        public string addressLine2 { get; set; }
        public string postalCode { get; set; }
        public string city { get; set; }
        public object accountCreationError { get; set; }
        public bool nameIsNotUnique { get; set; }
        public int version { get; set; }
        public Links _links { get; set; }

        public string created { get; set; }
        public float total { get; set; }
    }
    public class Item3
    {
        public string description { get; set; }
        public float quantity { get; set; }
        public float pricePerUnit { get; set; }
        public string kind { get; set; }
        public int quantityPrecision { get; set; }
        public bool quantityHidden { get; set; }
        public string vatId { get; set; }
        public object articleId { get; set; }
        public object articleName { get; set; }
        public object articleCode { get; set; }
        public object ictInfo { get; set; }

    }


    public class Self
    {
        public string href { get; set; }
    }
    public class Bulkprint
    {
        public string href { get; set; }
    }
    public class Timeline
    {
        public string href { get; set; }
    }

    public class Export
    {
        public string href { get; set; }
    }

    public class Links2
    {
        public Self self { get; set; }
        public Timeline timeline { get; set; }
        public Export export { get; set; }
        public Bulkprint bulkprint { get; set; }
    }

    public class RootObject
    {
        public int pageIndex { get; set; }
        public int pageSize { get; set; }
        public List<Page> page { get; set; }
        public int total { get; set; }
        public int filteredTotal { get; set; }
        public int remaining { get; set; }
        public List<Item> items { get; set; }
        public Links2 _links { get; set; }
        public Data data { get; set; }
        public List<Address> addresses { get; set; }
        public string culture { get; set; }
        public FromPeriod fromPeriod { get; set; }


    }

    public class Page
    {
        public bool isOpen { get; set; }
        public int year { get; set; }
        public int periodNumber { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
    }

    public class FromPeriod
    {
        public int periodNumber { get; set; }
        public int year { get; set; }
    }

    public class Data
    {
        public string paymentReference { get; set; }
    }

    public class Address
    {
        public string subsectionId { get; set; }
        public string addressType { get; set; }
        public string addressLine1 { get; set; }
        public string addressLine2 { get; set; }
        public string postalCode { get; set; }
        public string city { get; set; }
        public string countryCode { get; set; }
        public string forAttentionOf { get; set; }
        public string phone { get; set; }
        public string fax { get; set; }
        public string vatNumber { get; set; }
        public string commerceNumber { get; set; }
        public string email { get; set; }
        public bool isDefault { get; set; }
    }


    public class Depricate
    {
        public bool depreciateAll { get; set; }
        public List<object> fixedAssetIds { get; set; }
        public List<object> groupIds { get; set; }
        public Tillperiod tillPeriod { get; set; }
        public bool postInFirstOpenPeriod { get; set; }
    }

    public class Tillperiod
    {
        public bool isOpen { get; set; }
        public int year { get; set; }
        public string periodNumber { get; set; }
        public string startDate { get; set; }
        public string endDate { get; set; }
    }


    #region Customer Details class logic

    public class CustomerDetails
    {
        public _Links _links { get; set; }
        public Data1 data { get; set; }
        public string templateCulture { get; set; }
        public object templateId { get; set; }
        public Invoicedata invoiceData { get; set; }
        public object recurrenceInformation { get; set; }
    }
    public class CustomerDetails1
    {
        public string id { get; set; }
        public object originalInvoiceReference { get; set; }
        public object originalInvoiceNumber { get; set; }
        public string type { get; set; }
        public string customerId { get; set; }
        public string customerCode { get; set; }
        public string customerName { get; set; }
        public string customerContactPerson { get; set; }
        public string customerAddressLine1 { get; set; }
        public string customerAddressLine2 { get; set; }
        public string customerPostalCode { get; set; }
        public string customerCity { get; set; }
        public string customerCountry { get; set; }
        public string customerVatNumber { get; set; }
        public object companyInfo { get; set; }
        public Invoicedate invoiceDate { get; set; }
        public Duedate dueDate { get; set; }
        public Deliverydate deliveryDate { get; set; }
        public object reference { get; set; }
        public string currencyCode { get; set; }
        public object intro { get; set; }
        public object outro { get; set; }
        public object footer { get; set; }
        public object projectId { get; set; }
        public object projectCode { get; set; }
        public float grossTotal { get; set; }
        public float netTotal { get; set; }
        public float earlyPaymentTotal { get; set; }
        public float netTotalWithDiscount { get; set; }
        public float totalDiscount { get; set; }
        public float totalTurnover { get; set; }
        public float totalVatAmount { get; set; }
        public object baseCurrencyCode { get; set; }
        public object totalVatAmountInBaseCurrency { get; set; }
        public object totalTurnoverInBaseCurrency { get; set; }
        public string legalVatInfo { get; set; }
        public object[] lines { get; set; }
        public object[] vatLines { get; set; }
        public object templateId { get; set; }
        public string templateCulture { get; set; }
        public object recurrenceInformation { get; set; }
        public object originalRecurrenceId { get; set; }
        public Item3[] items { get; set; }
        public object discount { get; set; }
        public string discountType { get; set; }
        public object[] attachmentIds { get; set; }
        public bool isVatInclusive { get; set; }
        public int version { get; set; }
        public _Links _links { get; set; }

        public object logoFileName { get; set; }
        public object backgroundFileName { get; set; }
        public object secondaryBackgroundFileName { get; set; }
        public string paymentReference { get; set; }
        public string customerCultureCode { get; set; }
        public string publishingEmail { get; set; }
        public object customerDiscount { get; set; }
        public bool itemsHasICT { get; set; }
        public string sendInvoicesAs { get; set; }
        public int dueDays { get; set; }
        public bool initCustomer { get; set; }
    }


    public class Invoicedate
    {
        public int year { get; set; }
        public int month { get; set; }
        public int day { get; set; }
    }

    public class Duedate
    {
        public int year { get; set; }
        public int month { get; set; }
        public int day { get; set; }
    }

    public class Deliverydate
    {
        public int year { get; set; }
        public int month { get; set; }
        public int day { get; set; }
    }

    public class _Links
    {
        public Self self { get; set; }
        public Send send { get; set; }
        public Sendasattachment sendasattachment { get; set; }
        public Sendtoprinter sendtoprinter { get; set; }
        public Print print { get; set; }
        public Delete delete { get; set; }
        public Update update { get; set; }
        public Copy copy { get; set; }
        public Timeline timeline { get; set; }
        public Template template { get; set; }
        public Comments comments { get; set; }
    }

    public class Send
    {
        public string href { get; set; }
    }

    public class Sendasattachment
    {
        public string href { get; set; }
    }

    public class Sendtoprinter
    {
        public string href { get; set; }
    }

    public class Print
    {
        public string href { get; set; }
    }

    public class Update
    {
        public string href { get; set; }
    }

    public class Template
    {
        public string href { get; set; }
    }

    public class Comments
    {
        public string href { get; set; }
    }

    public class Data1
    {
        public string customerId { get; set; }
        public string customerCode { get; set; }
        public string customerName { get; set; }
        public string customerContactPerson { get; set; }
        public string customerAddressLine1 { get; set; }
        public string customerAddressLine2 { get; set; }
        public string customerPostalCode { get; set; }
        public string customerCity { get; set; }
        public string customerCountry { get; set; }
        public string customerVatNumber { get; set; }
        public object companyInfo { get; set; }
        public Invoicedate invoiceDate { get; set; }
        public Duedate dueDate { get; set; }
        public Deliverydate deliveryDate { get; set; }
        public object reference { get; set; }
        public string currencyCode { get; set; }
        public object intro { get; set; }
        public object outro { get; set; }
        public object footer { get; set; }
        public object logoFileName { get; set; }
        public object projectId { get; set; }
        public object projectCode { get; set; }
        public Item1[] items { get; set; }
        public object discount { get; set; }
        public string discountType { get; set; }
        // public object[] attachmentIds { get; set; }
        public List<object> attachmentIds { get; set; }
        public bool isVatInclusive { get; set; }
        public object backgroundFileName { get; set; }
        public object secondaryBackgroundFileName { get; set; }
        public string paymentReference { get; set; }
        public string customerCultureCode { get; set; }
        public string publishingEmail { get; set; }
        public object customerDiscount { get; set; }
        public bool itemsHasICT { get; set; }
        public string legalVatInfo { get; set; }
        public string sendInvoicesAs { get; set; }
        public int dueDays { get; set; }
        public bool initCustomer { get; set; }
    }

    public class Item1
    {
        public int quantity { get; set; }
        public int quantityPrecision { get; set; }
        public bool quantityHidden { get; set; }
        public string description { get; set; }
        public int pricePerUnit { get; set; }
        public string vatId { get; set; }
        public object articleId { get; set; }
        public object articleName { get; set; }
        public object articleCode { get; set; }
        public object ictInfo { get; set; }
        public string kind { get; set; }
    }

    public class Invoicedata
    {
        public string customerId { get; set; }
        public string customerCode { get; set; }
        public string customerName { get; set; }
        public string customerContactPerson { get; set; }
        public string customerAddressLine1 { get; set; }
        public string customerAddressLine2 { get; set; }
        public string customerPostalCode { get; set; }
        public string customerCity { get; set; }
        public string customerCountry { get; set; }
        public string customerVatNumber { get; set; }
        public object companyInfo { get; set; }
        public Invoicedate invoiceDate { get; set; }
        public Duedate dueDate { get; set; }
        public Deliverydate deliveryDate { get; set; }
        public object reference { get; set; }
        public string currencyCode { get; set; }
        public object intro { get; set; }
        public object outro { get; set; }
        public object footer { get; set; }
        public object logoFileName { get; set; }
        public object projectId { get; set; }
        public object projectCode { get; set; }
        public Item2[] items { get; set; }
        public object discount { get; set; }
        public string discountType { get; set; }
        // public object[] attachmentIds { get; set; }
        public List<object> attachmentIds { get; set; }
        public bool isVatInclusive { get; set; }
        public object backgroundFileName { get; set; }
        public object secondaryBackgroundFileName { get; set; }
        public string paymentReference { get; set; }
        public string customerCultureCode { get; set; }
        public string publishingEmail { get; set; }
        public object customerDiscount { get; set; }
        public bool itemsHasICT { get; set; }
        public string legalVatInfo { get; set; }
        public string sendInvoicesAs { get; set; }
        public int dueDays { get; set; }
        public bool initCustomer { get; set; }
    }

    public class Item2
    {
        public int quantity { get; set; }
        public int quantityPrecision { get; set; }
        public bool quantityHidden { get; set; }
        public string description { get; set; }
        public int pricePerUnit { get; set; }
        public string vatId { get; set; }
        public object articleId { get; set; }
        public object articleName { get; set; }
        public object articleCode { get; set; }
        public object ictInfo { get; set; }
        public string kind { get; set; }
    }


    #endregion


    #region Company Details

    public class CompanyObject
    {
        public User user { get; set; }
        public string securityToken { get; set; }
        public string loginUrl { get; set; }
        public string organizationName { get; set; }
        public object supportOrganization { get; set; }
        public string companyName { get; set; }
        public string companyCode { get; set; }
        public string companyCountry { get; set; }
        public string companyCity { get; set; }
        public string companyDefaultCulture { get; set; }
        public string culture { get; set; }
        public string numberGroupSeparator { get; set; }
        public string numberDecimalSeparator { get; set; }
        public string dateFormat { get; set; }
        public string currencyCode { get; set; }
        public string currencySymbol { get; set; }
        public Officecurrency[] officeCurrencies { get; set; }
        public string applicationVersion { get; set; }
        public bool hasCompanySelected { get; set; }
        public bool hasAnonymousAccess { get; set; }
        public object[] contextSwitchApplications { get; set; }
        public _Links2 _links { get; set; }
    }

    public class User
    {
        public string name { get; set; }
        public string code { get; set; }
        public bool hasAccessToClassic { get; set; }
        public bool hasAccessToSwitchOrganisation { get; set; }
        public bool hasAccessToTwinfieldId { get; set; }
        public bool hasAccessToWkId { get; set; }
        public bool hasAccessToHomepageInsights { get; set; }
        public string systemRole { get; set; }
    }

    public class _Links2
    {
        public Self self { get; set; }
        public Home home { get; set; }
        public Tasks tasks { get; set; }
        public Bookkeeping bookkeeping { get; set; }
        public Sales sales { get; set; }
        public Creditmanagement creditmanagement { get; set; }
        public Banks banks { get; set; }
        public Purchase purchase { get; set; }
        public Reports reports { get; set; }
        public Basecone basecone { get; set; }
        public Provisioning provisioning { get; set; }
        public Blobstorage blobstorage { get; set; }
        public Documentshare documentshare { get; set; }
        public Users users { get; set; }
        public Companies companies { get; set; }
        public Officemanagement officemanagement { get; set; }
        public Notifications notifications { get; set; }
        public Organisation organisation { get; set; }
        public Companyimport companyimport { get; set; }
        public Appstore appstore { get; set; }
        public Accessoverview accessoverview { get; set; }
        public Fixedassets fixedassets { get; set; }
        public Vatmanagement vatmanagement { get; set; }
        public Sitemapfavourites sitemapfavourites { get; set; }
        public Geo geo { get; set; }
    }


    public class Home
    {
        public string href { get; set; }
    }

    public class Tasks
    {
        public string href { get; set; }
    }

    public class Bookkeeping
    {
        public string href { get; set; }
    }

    public class Sales
    {
        public string href { get; set; }
    }

    public class Creditmanagement
    {
        public string href { get; set; }
    }

    public class Banks
    {
        public string href { get; set; }
    }

    public class Purchase
    {
        public string href { get; set; }
    }

    public class Reports
    {
        public string href { get; set; }
    }

    public class Basecone
    {
        public string href { get; set; }
    }

    public class Provisioning
    {
        public string href { get; set; }
    }

    public class Blobstorage
    {
        public string href { get; set; }
    }

    public class Documentshare
    {
        public string href { get; set; }
    }

    public class Users
    {
        public string href { get; set; }
    }

    public class Companies
    {
        public string href { get; set; }
    }

    public class Officemanagement
    {
        public string href { get; set; }
    }

    public class Notifications
    {
        public string href { get; set; }
    }

    public class Organisation
    {
        public string href { get; set; }
    }

    public class Companyimport
    {
        public string href { get; set; }
    }

    public class Appstore
    {
        public string href { get; set; }
    }

    public class Accessoverview
    {
        public string href { get; set; }
    }

    public class Fixedassets
    {
        public string href { get; set; }
    }

    public class Vatmanagement
    {
        public string href { get; set; }
    }

    public class Sitemapfavourites
    {
        public string href { get; set; }
    }

    public class Geo
    {
        public string href { get; set; }
    }

    public class Officecurrency
    {
        public string id { get; set; }
        public string code { get; set; }
        public string name { get; set; }
        public string sign { get; set; }
    }
    #endregion
    public class CompanyDetailsObject
    {
        public string id { get; set; }
        public int version { get; set; }
        public Officeinformation officeInformation { get; set; }
        public Financialinformation financialInformation { get; set; }
        public Systemaccounts systemAccounts { get; set; }
        public Taxsettings taxSettings { get; set; }
        public General general { get; set; }
        public Fixedassetssettings fixedAssetsSettings { get; set; }
        public Purchasesettings purchaseSettings { get; set; }
        public Salessettings salesSettings { get; set; }
        public Bankssettings banksSettings { get; set; }
        public Company_Links _links { get; set; }
    }

    public class Officeinformation
    {
        public string code { get; set; }
        public string name { get; set; }
        public string shortName { get; set; }
        public string chamberOfCommerceNumber { get; set; }
        public string officeGroupCode { get; set; }
        public object officeGroupDesc { get; set; }
        public string type { get; set; }
        public bool isDemo { get; set; }
        public bool isDemoChanged { get; set; }
        public bool isDemoChangeable { get; set; }
        public object industrialClassificationMajorGroupId { get; set; }
        public string defaultCultureCode { get; set; }
        public string addressLine1 { get; set; }
        public string postalCode { get; set; }
        public string city { get; set; }
        public string countryCode { get; set; }
        public string phone { get; set; }
        public string fax { get; set; }
        public string addressLine2 { get; set; }
        public string addressLine3 { get; set; }
        public string addressLine4 { get; set; }
        public string addressLine5 { get; set; }
        public string addressLine6 { get; set; }
    }

    public class Financialinformation
    {
        public string baseCurrencyCode { get; set; }
        public string reportingCurrencyCode { get; set; }
        public string creditorIdentifier { get; set; }
        public object templateOfficeId { get; set; }
        public bool isTemplate { get; set; }
        public bool accessibleByNotTemplateUserTypes { get; set; }
        public string defaultBankCode { get; set; }
        public object defaultReportingStructureCode { get; set; }
        public string defaultBankDesc { get; set; }
        public string baseCurrencyDesc { get; set; }
        public string reportingCurrencyDesc { get; set; }
        public object templateOfficeDesc { get; set; }
        public object defaultReportingStructureDesc { get; set; }
    }

    public class Systemaccounts
    {
        public Accountsreceivable accountsReceivable { get; set; }
        public Accountspayable accountsPayable { get; set; }
        public Profitandlossbroughtforward profitAndLossBroughtForward { get; set; }
        public Suspenseaccount suspenseAccount { get; set; }
        public Workinprogress workInProgress { get; set; }
        public Projectmarkup projectMarkup { get; set; }
        public Defaultrevenuetype defaultRevenueType { get; set; }
        public Timeandexpensescostcenter timeAndExpensesCostCenter { get; set; }
        public Fixedassetscostcenter fixedAssetsCostCenter { get; set; }
        public Yearendprofitorloss yearEndProfitOrLoss { get; set; }
        public Foreignbalancesyearend foreignBalancesYearEnd { get; set; }
        public Exchangeratedifferencesdebit exchangeRateDifferencesDebit { get; set; }
        public Exchangeratedifferencescredit exchangeRateDifferencesCredit { get; set; }
        public Writeoffdebitaccount writeOffDebitAccount { get; set; }
        public Writeoffcreditaccount writeOffCreditAccount { get; set; }
        public Deductiondebitaccount deductionDebitAccount { get; set; }
        public Deductioncreditaccount deductionCreditAccount { get; set; }
    }

    public class Accountsreceivable
    {
        public string id { get; set; }
        public string description { get; set; }
    }

    public class Accountspayable
    {
        public string id { get; set; }
        public string description { get; set; }
    }

    public class Profitandlossbroughtforward
    {
        public string id { get; set; }
        public string description { get; set; }
    }

    public class Suspenseaccount
    {
        public string id { get; set; }
        public string description { get; set; }
    }

    public class Workinprogress
    {
        public string id { get; set; }
        public string description { get; set; }
    }

    public class Projectmarkup
    {
        public string id { get; set; }
        public string description { get; set; }
    }

    public class Defaultrevenuetype
    {
        public string id { get; set; }
        public string description { get; set; }
    }

    public class Timeandexpensescostcenter
    {
        public string id { get; set; }
        public string description { get; set; }
    }

    public class Fixedassetscostcenter
    {
        public object id { get; set; }
        public object description { get; set; }
    }

    public class Yearendprofitorloss
    {
        public string id { get; set; }
        public string description { get; set; }
    }

    public class Foreignbalancesyearend
    {
        public object id { get; set; }
        public object description { get; set; }
    }

    public class Exchangeratedifferencesdebit
    {
        public string id { get; set; }
        public string description { get; set; }
    }

    public class Exchangeratedifferencescredit
    {
        public string id { get; set; }
        public string description { get; set; }
    }

    public class Writeoffdebitaccount
    {
        public string id { get; set; }
        public string description { get; set; }
    }

    public class Writeoffcreditaccount
    {
        public string id { get; set; }
        public string description { get; set; }
    }

    public class Deductiondebitaccount
    {
        public string id { get; set; }
        public string description { get; set; }
    }

    public class Deductioncreditaccount
    {
        public string id { get; set; }
        public string description { get; set; }
    }

    public class Taxsettings
    {
        public string vatNumber { get; set; }
        public string liableEntityName { get; set; }
        public string liableEntityPhone { get; set; }
        public string beconNumber { get; set; }
        public string intermediaryName { get; set; }
        public string intermediaryPhone { get; set; }
        public string serviceBureauNumber { get; set; }
        public string serviceBureauName { get; set; }
        public string serviceBureauPhone { get; set; }
        public object defaultGatewayType { get; set; }
        public object defaultGatewayCode { get; set; }
        public object defaultGatewayDesc { get; set; }
        public bool mayGiveEstimate { get; set; }
        public bool includePurchaseOrSalesProvisionalTransactions { get; set; }
        public bool includeCashOrBankProvisionalTransactions { get; set; }
        public bool includeJournalProvisionalTransactions { get; set; }
        public bool includeProvisionalTransactionsEnabled { get; set; }
        public string paymentDiscount { get; set; }
        public string vatReturnTimeframe { get; set; }
        public object vatReturnTimeframeStart { get; set; }
        public string ictReturnTimeframe { get; set; }
        public string accountingScheme { get; set; }
        public bool showVatInBaseCurrency { get; set; }
        public object taxGroupCode { get; set; }
        public object taxGroupName { get; set; }
    }

    public class General
    {
        public bool isMultipleControlAccountsAllowed { get; set; }
        public bool isBankStatementImportAllowed { get; set; }
        public bool isDimensionNameAdjustable { get; set; }
        public bool areWordInvoicesReadOnly { get; set; }
        public string dateFormat { get; set; }
        public string groupSeparator { get; set; }
        public string decimalSeparator { get; set; }
        public int expirationInDays { get; set; }
        public float defaultReminderAmount { get; set; }
        public int expirationInDaysAfterPartialPayment { get; set; }
        public int defaultToDoList { get; set; }
        public object defaultToDoListDesc { get; set; }
    }

    public class Fixedassetssettings
    {
        public bool autoCreateNewFixedAsset { get; set; }
        public object[] fixedAssetAccounts { get; set; }
        public Regimessettings regimesSettings { get; set; }
    }

    public class Regimessettings
    {
        public bool useRegimes { get; set; }
        public Regime[] regimes { get; set; }
        public bool editable { get; set; }
    }

    public class Regime
    {
        public string name { get; set; }
        public bool enabled { get; set; }
        public bool used { get; set; }
    }

    public class Purchasesettings
    {
        public string invoicePostingSettings { get; set; }
        public string invoiceValidationMode { get; set; }
    }

    public class Salessettings
    {
        public bool revenueTransferEnabled { get; set; }
        public Revenuetransferaccount revenueTransferAccount { get; set; }
        public bool ublGenerationEnabled { get; set; }
        public bool twinfieldAccountingEnabled { get; set; }
    }

    public class Revenuetransferaccount
    {
        public object id { get; set; }
        public object description { get; set; }
    }

    public class Bankssettings
    {
        public string bankAccountLedger { get; set; }
    }

    public class Company_Links
    {
        public Company_Self self { get; set; }
        public Company_Update update { get; set; }
        public Company_Delete delete { get; set; }
        public Banks banks { get; set; }
        public Reportingstructure reportingstructure { get; set; }
        public Taxgroup taxgroup { get; set; }
        public Generalledgeraccounts generalledgeraccounts { get; set; }
        public Balanceledgeraccounts balanceledgeraccounts { get; set; }
        public Balanceledgeraccountsforfixedasset balanceledgeraccountsforfixedasset { get; set; }
        public Balanceledgeraccountsforrevenuetransfer balanceledgeraccountsforrevenuetransfer { get; set; }
        public Profitandlossaccounts profitandlossaccounts { get; set; }
        public Costcenters costcenters { get; set; }
        public Accessiblegateways accessiblegateways { get; set; }
        public Company_Timeline timeline { get; set; }
        public Copy copy { get; set; }
        public Switch _switch { get; set; }
    }

    public class Company_Self
    {
        public string href { get; set; }
    }

    public class Company_Update
    {
        public string href { get; set; }
    }

    public class Company_Delete
    {
        public string href { get; set; }
    }

    public class Company_Banks
    {
        public string href { get; set; }
    }

    public class Reportingstructure
    {
        public string href { get; set; }
    }

    public class Taxgroup
    {
        public string href { get; set; }
    }

    public class Generalledgeraccounts
    {
        public string href { get; set; }
    }

    public class Balanceledgeraccounts
    {
        public string href { get; set; }
    }

    public class Balanceledgeraccountsforfixedasset
    {
        public string href { get; set; }
    }

    public class Balanceledgeraccountsforrevenuetransfer
    {
        public string href { get; set; }
    }

    public class Profitandlossaccounts
    {
        public string href { get; set; }
    }

    public class Costcenters
    {
        public string href { get; set; }
    }

    public class Accessiblegateways
    {
        public string href { get; set; }
    }

    public class Company_Timeline
    {
        public string href { get; set; }
    }

    public class Copy
    {
        public string href { get; set; }
    }

    public class Switch
    {
        public string href { get; set; }
    }

    public class CompaniesDetails
    {
        public int total { get; set; }
        public int filteredTotal { get; set; }
        public List<Item5> items { get; set; }
        public _Links3 _links { get; set; }
    }

    public class _Links3
    {
        public Self self { get; set; }
        public Next next { get; set; }
        public Current current { get; set; }
        public Summary summary { get; set; }
        public All all { get; set; }
        public Portfolio portfolio { get; set; }
    }


    public class Next
    {
        public string href { get; set; }
    }

    public class Current
    {
        public string href { get; set; }
    }

    public class Summary
    {
        public string href { get; set; }
    }

    public class All
    {
        public string href { get; set; }
    }

    public class Portfolio
    {
        public string href { get; set; }
    }

    public class Item5
    {
        public string id { get; set; }
        public string code { get; set; }
        public string name { get; set; }
        public int provisionalTransactionTotal { get; set; }
        public int statementsToBeProcessedTotal { get; set; }
        public bool partOfPortfolio { get; set; }
        public _Links12 _links { get; set; }
    }

    public class _Links12
    {
        public Switch _switch { get; set; }
        public Addtoportfolio addtoportfolio { get; set; }
        public Removefromportfolio removefromportfolio { get; set; }
    }


    public class Addtoportfolio
    {
        public string href { get; set; }
    }

    public class Removefromportfolio
    {
        public string href { get; set; }
    }






    public class sSearchObject
    {
        public List<Class1> Property1 { get; set; }
    }

    public class Class1
    {
        public string name { get; set; }
        public string label { get; set; }
        public bool collapsed { get; set; }
        public string id { get; set; }
        public List<Option> options { get; set; }
        public string onClickJSHandler { get; set; }
        public string onExpandToggleJSHandler { get; set; }
    }

    public class Option
    {
        public string label { get; set; }
        public string value { get; set; }
        public int count { get; set; }
        public bool _checked { get; set; }
        public string toolTip { get; set; }
    }
}
