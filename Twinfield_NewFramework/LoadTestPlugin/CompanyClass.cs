
using System.Collections.Generic;

public class Company
{
    public _Links _links { get; set; }
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
}

public class _Links
{
    public Self self { get; set; }
    public Update update { get; set; }
    public Delete delete { get; set; }
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
    public Timeline timeline { get; set; }
    public Copy copy { get; set; }
    public Switch _switch { get; set; }
}

public class Self
{
    public string href { get; set; }
}

public class Update
{
    public string href { get; set; }
}

public class Delete
{
    public string href { get; set; }
}

public class Banks
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

public class Timeline
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
    public bool isPostalCodeCityCatalogAvailable { get; set; }
    public string countryName { get; set; }
    public Countrybind countryBind { get; set; }
    public string companyTypeDescription { get; set; }
}

public class Countrybind
{
    public string value { get; set; }
    public string caption { get; set; }
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
    public string defaultReportingStructureCode { get; set; }
    public string defaultBankDesc { get; set; }
    public string baseCurrencyDesc { get; set; }
    public string reportingCurrencyDesc { get; set; }
    public object templateOfficeDesc { get; set; }
    public object defaultReportingStructureDesc { get; set; }
    public Defaultbankbind defaultBankBind { get; set; }
}

public class Defaultbankbind
{
    public string code { get; set; }
    public string name { get; set; }
    public string description { get; set; }
    public string caption { get; set; }
    public string value { get; set; }
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
    public bool invalid { get; set; }
    public string value { get; set; }
    public string caption { get; set; }
}

public class Accountspayable
{
    public string id { get; set; }
    public string description { get; set; }
    public bool invalid { get; set; }
    public string value { get; set; }
    public string caption { get; set; }
}

public class Profitandlossbroughtforward
{
    public string id { get; set; }
    public string description { get; set; }
    public bool invalid { get; set; }
    public string value { get; set; }
    public string caption { get; set; }
}

public class Suspenseaccount
{
    public string id { get; set; }
    public string description { get; set; }
    public bool invalid { get; set; }
    public string value { get; set; }
    public string caption { get; set; }
}

public class Workinprogress
{
    public string id { get; set; }
    public string description { get; set; }
    public bool invalid { get; set; }
    public string value { get; set; }
    public string caption { get; set; }
}

public class Projectmarkup
{
    public string id { get; set; }
    public string description { get; set; }
    public bool invalid { get; set; }
    public string value { get; set; }
    public string caption { get; set; }
}

public class Defaultrevenuetype
{
    public string id { get; set; }
    public string description { get; set; }
    public bool invalid { get; set; }
    public string value { get; set; }
    public string caption { get; set; }
}

public class Timeandexpensescostcenter
{
    public object id { get; set; }
    public object description { get; set; }
    public bool invalid { get; set; }
    public object value { get; set; }
    public object caption { get; set; }
}

public class Fixedassetscostcenter
{
    public object id { get; set; }
    public object description { get; set; }
    public bool invalid { get; set; }
    public object value { get; set; }
    public object caption { get; set; }
}

public class Yearendprofitorloss
{
    public string id { get; set; }
    public string description { get; set; }
    public bool invalid { get; set; }
    public string value { get; set; }
    public string caption { get; set; }
}

public class Foreignbalancesyearend
{
    public object id { get; set; }
    public object description { get; set; }
    public bool invalid { get; set; }
    public object value { get; set; }
    public object caption { get; set; }
}

public class Exchangeratedifferencesdebit
{
    public string id { get; set; }
    public string description { get; set; }
    public bool invalid { get; set; }
    public string value { get; set; }
    public string caption { get; set; }
}

public class Exchangeratedifferencescredit
{
    public string id { get; set; }
    public string description { get; set; }
    public bool invalid { get; set; }
    public string value { get; set; }
    public string caption { get; set; }
}

public class Writeoffdebitaccount
{
    public string id { get; set; }
    public string description { get; set; }
    public bool invalid { get; set; }
    public string value { get; set; }
    public string caption { get; set; }
}

public class Writeoffcreditaccount
{
    public string id { get; set; }
    public string description { get; set; }
    public bool invalid { get; set; }
    public string value { get; set; }
    public string caption { get; set; }
}

public class Deductiondebitaccount
{
    public string id { get; set; }
    public string description { get; set; }
    public bool invalid { get; set; }
    public string value { get; set; }
    public string caption { get; set; }
}

public class Deductioncreditaccount
{
    public string id { get; set; }
    public string description { get; set; }
    public bool invalid { get; set; }
    public string value { get; set; }
    public string caption { get; set; }
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
    public bool mayGiveEstimate { get; set; }
    public bool includePurchaseOrSalesProvisionalTransactions { get; set; }
    public bool includeCashOrBankProvisionalTransactions { get; set; }
    public bool includeJournalProvisionalTransactions { get; set; }
    public bool includeProvisionalTransactionsEnabled { get; set; }
    public string paymentDiscount { get; set; }
    public string vatReturnTimeframe { get; set; }
    public string vatReturnTimeframeStart { get; set; }
    public string ictReturnTimeframe { get; set; }
    public string accountingScheme { get; set; }
    public bool showVatInBaseCurrency { get; set; }
    public object taxGroupCode { get; set; }
    public object taxGroupName { get; set; }
    public bool hasTaxGroup { get; set; }
    public string taxGroupCodeAndName { get; set; }
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
    public object defaultToDoList { get; set; }
    public object defaultToDoListDesc { get; set; }
}

public class Fixedassetssettings
{
    public bool autoCreateNewFixedAsset { get; set; }
    public Fixedassetaccount[] fixedAssetAccounts { get; set; }
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

public class Fixedassetaccount
{
    public string id { get; set; }
    public string description { get; set; }
    public Fixedassetaccountbind fixedAssetAccountBind { get; set; }
}

public class Fixedassetaccountbind
{
    public string id { get; set; }
    public string code { get; set; }
    public string name { get; set; }
    public string subanalysis { get; set; }
    public object defaultVatCode { get; set; }
    public bool isFixedVatCode { get; set; }
    public object substitutionAccountId { get; set; }
    public bool matchable { get; set; }
    public string description { get; set; }
    public string caption { get; set; }
    public string value { get; set; }
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
    public object value { get; set; }
    public object caption { get; set; }
}

public class Bankssettings
{
    public string bankAccountLedger { get; set; }
}

[System.Serializable]


public class InitializationAndTranslation
{
    public Datepicker datePicker { get; set; }
    public Countrycatalog countryCatalog { get; set; }
    public string[] monthNames { get; set; }
    public string[] shortMonthNames { get; set; }
    public string[] shortDayNames { get; set; }
    public string finderInfoText { get; set; }
    public bool isDebug { get; set; }
    public Commentspubdatefilter commentsPubdateFilter { get; set; }
    public Document document { get; set; }
    public Dragndrop dragndrop { get; set; }
    public Systemmessages systemMessages { get; set; }
    public Assignments assignments { get; set; }
}

public class Datepicker
{
    public string closeText { get; set; }
    public string currentText { get; set; }
    public string[] dayNames { get; set; }
    public string[] dayNamesShort { get; set; }
    public string[] dayNamesMin { get; set; }
    public string[] monthNames { get; set; }
    public string[] monthNamesShort { get; set; }
    public int firstDay { get; set; }
    public bool isRTL { get; set; }
    public string prevText { get; set; }
    public string nextText { get; set; }
    public string weekHeader { get; set; }
    public string dateFormat { get; set; }
}

public class Countrycatalog
{
    //public List<CommonCountry> commonCountry {get; set;}
    public Af af { get; set; }
    public Ax ax { get; set; }
    public Al al { get; set; }
    public Dz dz { get; set; }
    public As _as { get; set; }
    public Ad ad { get; set; }
    public Ao ao { get; set; }
    public Ai ai { get; set; }
    public Aq aq { get; set; }
    public Ag ag { get; set; }
    public Ar ar { get; set; }
    public Am am { get; set; }
    public Aw aw { get; set; }
    public Au au { get; set; }
    public At at { get; set; }
    public Az az { get; set; }
    public Bs bs { get; set; }
    public Bh bh { get; set; }
    public Bd bd { get; set; }
    public Bb bb { get; set; }
    public By by { get; set; }
    public Be be { get; set; }
    public Bz bz { get; set; }
    public Bj bj { get; set; }
    public Bm bm { get; set; }
    public Bt bt { get; set; }
    public Bo bo { get; set; }
    public Bq bq { get; set; }
    public Ba ba { get; set; }
    public Bw bw { get; set; }
    public Bv bv { get; set; }
    public Br br { get; set; }
    public Bn bn { get; set; }
    public Bg bg { get; set; }
    public Bf bf { get; set; }
    public Bi bi { get; set; }
    public Kh kh { get; set; }
    public Cm cm { get; set; }
    public Ca ca { get; set; }
    public Cv cv { get; set; }
    public Ky ky { get; set; }
    public Cf cf { get; set; }
    public Td td { get; set; }
    public Cl cl { get; set; }
    public Cn cn { get; set; }
    public Cx cx { get; set; }
    public Cc cc { get; set; }
    public Co co { get; set; }
    public Km km { get; set; }
    public Cg cg { get; set; }
    public Cd cd { get; set; }
    public Ck ck { get; set; }
    public Cr cr { get; set; }
    public Ci ci { get; set; }
    public Hr hr { get; set; }
    public Cu cu { get; set; }
    public Cw cw { get; set; }
    public Cy cy { get; set; }
    public Cz cz { get; set; }
    public Dk dk { get; set; }
    public Dj dj { get; set; }
    public Dm dm { get; set; }
    public Do _do { get; set; }
    public Tl tl { get; set; }
    public Ec ec { get; set; }
    public Eg eg { get; set; }
    public Sv sv { get; set; }
    public Gq gq { get; set; }
    public Er er { get; set; }
    public Ee ee { get; set; }
    public Et et { get; set; }
    public Fk fk { get; set; }
    public Fo fo { get; set; }
    public Fj fj { get; set; }
    public Fi fi { get; set; }
    public Fr fr { get; set; }
    public Gf gf { get; set; }
    public Pf pf { get; set; }
    public Tf tf { get; set; }
    public Ga ga { get; set; }
    public Gm gm { get; set; }
    public Ge ge { get; set; }
    public De de { get; set; }
    public Gh gh { get; set; }
    public Gi gi { get; set; }
    public Gr gr { get; set; }
    public Gl gl { get; set; }
    public Gd gd { get; set; }
    public Gp gp { get; set; }
    public Gu gu { get; set; }
    public Gt gt { get; set; }
    public Gg gg { get; set; }
    public Gn gn { get; set; }
    public Gw gw { get; set; }
    public Gy gy { get; set; }
    public Ht ht { get; set; }
    public Hm hm { get; set; }
    public Va va { get; set; }
    public Hn hn { get; set; }
    public Hk hk { get; set; }
    public Hu hu { get; set; }
    public Is _is { get; set; }
    public In _in { get; set; }
    public Id id { get; set; }
    public Ir ir { get; set; }
    public Iq iq { get; set; }
    public Ie ie { get; set; }
    public Im im { get; set; }
    public Il il { get; set; }
    public It it { get; set; }
    public Jm jm { get; set; }
    public Jp jp { get; set; }
    public Je je { get; set; }
    public Jo jo { get; set; }
    public Kz kz { get; set; }
    public Ke ke { get; set; }
    public Ki ki { get; set; }
    public Kp kp { get; set; }
    public Kr kr { get; set; }
    public Kw kw { get; set; }
    public Kg kg { get; set; }
    public La la { get; set; }
    public Lv lv { get; set; }
    public Lb lb { get; set; }
    public Ls ls { get; set; }
    public Lr lr { get; set; }
    public Ly ly { get; set; }
    public Li li { get; set; }
    public Lt lt { get; set; }
    public Lu lu { get; set; }
    public Mo mo { get; set; }
    public Mk mk { get; set; }
    public Mg mg { get; set; }
    public Mw mw { get; set; }
    public My my { get; set; }
    public Mv mv { get; set; }
    public Ml ml { get; set; }
    public Mt mt { get; set; }
    public Mh mh { get; set; }
    public Mq mq { get; set; }
    public Mr mr { get; set; }
    public Mu mu { get; set; }
    public Yt yt { get; set; }
    public Mx mx { get; set; }
    public Fm fm { get; set; }
    public Md md { get; set; }
    public Mc mc { get; set; }
    public Mn mn { get; set; }
    public Me me { get; set; }
    public Ms ms { get; set; }
    public Ma ma { get; set; }
    public Mz mz { get; set; }
    public Mm mm { get; set; }
    public Na na { get; set; }
    public Nr nr { get; set; }
    public Np np { get; set; }
    public Nl nl { get; set; }
    public An an { get; set; }
    public Nc nc { get; set; }
    public Nz nz { get; set; }
    public Ni ni { get; set; }
    public Ne ne { get; set; }
    public Ng ng { get; set; }
    public Nu nu { get; set; }
    public Nf nf { get; set; }
    public Mp mp { get; set; }
    public No no { get; set; }
    public Om om { get; set; }
    public Pk pk { get; set; }
    public Pw pw { get; set; }
    public Ps ps { get; set; }
    public Pa pa { get; set; }
    public Pg pg { get; set; }
    public Py py { get; set; }
    public Pe pe { get; set; }
    public Ph ph { get; set; }
    public Pn pn { get; set; }
    public Pl pl { get; set; }
    public Pt pt { get; set; }
    public Pr pr { get; set; }
    public Qa qa { get; set; }
    public Re re { get; set; }
    public Ro ro { get; set; }
    public Ru ru { get; set; }
    public Rw rw { get; set; }
    public Bl bl { get; set; }
    public Kn kn { get; set; }
    public Lc lc { get; set; }
    public Mf mf { get; set; }
    public Vc vc { get; set; }
    public Ws ws { get; set; }
    public Sm sm { get; set; }
    public St st { get; set; }
    public Sa sa { get; set; }
    public Sn sn { get; set; }
    public Rs rs { get; set; }
    public Sc sc { get; set; }
    public Sl sl { get; set; }
    public Sg sg { get; set; }
    public Sx sx { get; set; }
    public Sk sk { get; set; }
    public Si si { get; set; }
    public Sb sb { get; set; }
    public So so { get; set; }
    public Za za { get; set; }
    public Es es { get; set; }
    public Lk lk { get; set; }
    public Sh sh { get; set; }
    public Pm pm { get; set; }
    public Sd sd { get; set; }
    public Sr sr { get; set; }
    public Sj sj { get; set; }
    public Sz sz { get; set; }
    public Se se { get; set; }
    public Ch ch { get; set; }
    public Sy sy { get; set; }
    public Tw tw { get; set; }
    public Tj tj { get; set; }
    public Tz tz { get; set; }
    public Th th { get; set; }
    public Tg tg { get; set; }
    public Tk tk { get; set; }
    public To to { get; set; }
    public Tt tt { get; set; }
    public Tn tn { get; set; }
    public Tr tr { get; set; }
    public Tm tm { get; set; }
    public Tc tc { get; set; }
    public Tv tv { get; set; }
    public Ug ug { get; set; }
    public Ua ua { get; set; }
    public Ae ae { get; set; }
    public Gb gb { get; set; }
    public Us us { get; set; }
    public Um um { get; set; }
    public Uy uy { get; set; }
    public Uz uz { get; set; }
    public Vu vu { get; set; }
    public Ve ve { get; set; }
    public Vn vn { get; set; }
    public Vg vg { get; set; }
    public Vi vi { get; set; }
    public Wf wf { get; set; }
    public Eh eh { get; set; }
    public Ye ye { get; set; }
    public Zm zm { get; set; }
    public Zw zw { get; set; }
}

public class CommonCountry
{
    public string localisedName { get; set; }
    public string threeLetterISORegionName { get; set; }
    public string twoLetterISORegionName { get; set; }
    public object ictCode { get; set; }
}

/// <summary>
/// added baseclasss
/// </summary>
/// 
interface ItempBase
{
    string localisedName { get; set; }
    string threeLetterISORegionName { get; set; }
    string twoLetterISORegionName { get; set; }
    object ictCode { get; set; }

}
public class Af 
{
    public string localisedName { get; set; }
    public string threeLetterISORegionName { get; set; }
    public string twoLetterISORegionName { get; set; }
    public object ictCode { get; set; }
}

public class Ax 
{
    public string localisedName { get; set; }
    public string threeLetterISORegionName { get; set; }
    public string twoLetterISORegionName { get; set; }
    public object ictCode { get; set; }
}

public class Al
{
    public string localisedName { get; set; }
    public string threeLetterISORegionName { get; set; }
    public string twoLetterISORegionName { get; set; }
    public object ictCode { get; set; }
}

public class Dz
{
    public string localisedName { get; set; }
    public string threeLetterISORegionName { get; set; }
    public string twoLetterISORegionName { get; set; }
    public object ictCode { get; set; }
}

public class As
{
    public string localisedName { get; set; }
    public string threeLetterISORegionName { get; set; }
    public string twoLetterISORegionName { get; set; }
    public object ictCode { get; set; }
}

public class Ad
{
    public string localisedName { get; set; }
    public string threeLetterISORegionName { get; set; }
    public string twoLetterISORegionName { get; set; }
    public object ictCode { get; set; }
}

public class Ao
{
    public string localisedName { get; set; }
    public string threeLetterISORegionName { get; set; }
    public string twoLetterISORegionName { get; set; }
    public object ictCode { get; set; }
}

public class Ai
{
    public string localisedName { get; set; }
    public string threeLetterISORegionName { get; set; }
    public string twoLetterISORegionName { get; set; }
    public object ictCode { get; set; }
}

public class Aq
{
    public string localisedName { get; set; }
    public string threeLetterISORegionName { get; set; }
    public string twoLetterISORegionName { get; set; }
    public object ictCode { get; set; }
}

public class Ag
{
    public string localisedName { get; set; }
    public string threeLetterISORegionName { get; set; }
    public string twoLetterISORegionName { get; set; }
    public object ictCode { get; set; }
}

public class Ar
{
    public string localisedName { get; set; }
    public string threeLetterISORegionName { get; set; }
    public string twoLetterISORegionName { get; set; }
    public object ictCode { get; set; }
}

public class Am
{
    public string localisedName { get; set; }
    public string threeLetterISORegionName { get; set; }
    public string twoLetterISORegionName { get; set; }
    public object ictCode { get; set; }
}

public class Aw
{
    public string localisedName { get; set; }
    public string threeLetterISORegionName { get; set; }
    public string twoLetterISORegionName { get; set; }
    public object ictCode { get; set; }
}

public class Au
{
    public string localisedName { get; set; }
    public string threeLetterISORegionName { get; set; }
    public string twoLetterISORegionName { get; set; }
    public object ictCode { get; set; }
}

public class At
{
    public string localisedName { get; set; }
    public string threeLetterISORegionName { get; set; }
    public string twoLetterISORegionName { get; set; }
    public string ictCode { get; set; }
}

public class Az
{
    public string localisedName { get; set; }
    public string threeLetterISORegionName { get; set; }
    public string twoLetterISORegionName { get; set; }
    public object ictCode { get; set; }
}

public class Bs
{
    public string localisedName { get; set; }
    public string threeLetterISORegionName { get; set; }
    public string twoLetterISORegionName { get; set; }
    public object ictCode { get; set; }
}

public class Bh
{
    public string localisedName { get; set; }
    public string threeLetterISORegionName { get; set; }
    public string twoLetterISORegionName { get; set; }
    public object ictCode { get; set; }
}

public class Bd
{
    public string localisedName { get; set; }
    public string threeLetterISORegionName { get; set; }
    public string twoLetterISORegionName { get; set; }
    public object ictCode { get; set; }
}

public class Bb
{
    public string localisedName { get; set; }
    public string threeLetterISORegionName { get; set; }
    public string twoLetterISORegionName { get; set; }
    public object ictCode { get; set; }
}

public class By
{
    public string localisedName { get; set; }
    public string threeLetterISORegionName { get; set; }
    public string twoLetterISORegionName { get; set; }
    public object ictCode { get; set; }
}

public class Be
{
    public string localisedName { get; set; }
    public string threeLetterISORegionName { get; set; }
    public string twoLetterISORegionName { get; set; }
    public string ictCode { get; set; }
}

public class Bz
{
    public string localisedName { get; set; }
    public string threeLetterISORegionName { get; set; }
    public string twoLetterISORegionName { get; set; }
    public object ictCode { get; set; }
}

public class Bj
{
    public string localisedName { get; set; }
    public string threeLetterISORegionName { get; set; }
    public string twoLetterISORegionName { get; set; }
    public object ictCode { get; set; }
}

public class Bm
{
    public string localisedName { get; set; }
    public string threeLetterISORegionName { get; set; }
    public string twoLetterISORegionName { get; set; }
    public object ictCode { get; set; }
}

public class Bt
{
    public string localisedName { get; set; }
    public string threeLetterISORegionName { get; set; }
    public string twoLetterISORegionName { get; set; }
    public object ictCode { get; set; }
}

public class Bo
{
    public string localisedName { get; set; }
    public string threeLetterISORegionName { get; set; }
    public string twoLetterISORegionName { get; set; }
    public object ictCode { get; set; }
}

public class Bq
{
    public string localisedName { get; set; }
    public string threeLetterISORegionName { get; set; }
    public string twoLetterISORegionName { get; set; }
    public object ictCode { get; set; }
}

public class Ba
{
    public string localisedName { get; set; }
    public string threeLetterISORegionName { get; set; }
    public string twoLetterISORegionName { get; set; }
    public object ictCode { get; set; }
}

public class Bw
{
    public string localisedName { get; set; }
    public string threeLetterISORegionName { get; set; }
    public string twoLetterISORegionName { get; set; }
    public object ictCode { get; set; }
}

public class Bv
{
    public string localisedName { get; set; }
    public string threeLetterISORegionName { get; set; }
    public string twoLetterISORegionName { get; set; }
    public object ictCode { get; set; }
}

public class Br
{
    public string localisedName { get; set; }
    public string threeLetterISORegionName { get; set; }
    public string twoLetterISORegionName { get; set; }
    public object ictCode { get; set; }
}

public class Bn
{
    public string localisedName { get; set; }
    public string threeLetterISORegionName { get; set; }
    public string twoLetterISORegionName { get; set; }
    public object ictCode { get; set; }
}

public class Bg
{
    public string localisedName { get; set; }
    public string threeLetterISORegionName { get; set; }
    public string twoLetterISORegionName { get; set; }
    public string ictCode { get; set; }
}

public class Bf
{
    public string localisedName { get; set; }
    public string threeLetterISORegionName { get; set; }
    public string twoLetterISORegionName { get; set; }
    public object ictCode { get; set; }
}

public class Bi
{
    public string localisedName { get; set; }
    public string threeLetterISORegionName { get; set; }
    public string twoLetterISORegionName { get; set; }
    public object ictCode { get; set; }
}

public class Kh
{
    public string localisedName { get; set; }
    public string threeLetterISORegionName { get; set; }
    public string twoLetterISORegionName { get; set; }
    public object ictCode { get; set; }
}

public class Cm
{
    public string localisedName { get; set; }
    public string threeLetterISORegionName { get; set; }
    public string twoLetterISORegionName { get; set; }
    public object ictCode { get; set; }
}

public class Ca
{
    public string localisedName { get; set; }
    public string threeLetterISORegionName { get; set; }
    public string twoLetterISORegionName { get; set; }
    public object ictCode { get; set; }
}

public class Cv
{
    public string localisedName { get; set; }
    public string threeLetterISORegionName { get; set; }
    public string twoLetterISORegionName { get; set; }
    public object ictCode { get; set; }
}

public class Ky
{
    public string localisedName { get; set; }
    public string threeLetterISORegionName { get; set; }
    public string twoLetterISORegionName { get; set; }
    public object ictCode { get; set; }
}

public class Cf
{
    public string localisedName { get; set; }
    public string threeLetterISORegionName { get; set; }
    public string twoLetterISORegionName { get; set; }
    public object ictCode { get; set; }
}

public class Td
{
    public string localisedName { get; set; }
    public string threeLetterISORegionName { get; set; }
    public string twoLetterISORegionName { get; set; }
    public object ictCode { get; set; }
}

public class Cl
{
    public string localisedName { get; set; }
    public string threeLetterISORegionName { get; set; }
    public string twoLetterISORegionName { get; set; }
    public object ictCode { get; set; }
}

public class Cn
{
    public string localisedName { get; set; }
    public string threeLetterISORegionName { get; set; }
    public string twoLetterISORegionName { get; set; }
    public object ictCode { get; set; }
}

public class Cx
{
    public string localisedName { get; set; }
    public string threeLetterISORegionName { get; set; }
    public string twoLetterISORegionName { get; set; }
    public object ictCode { get; set; }
}

public class Cc
{
    public string localisedName { get; set; }
    public string threeLetterISORegionName { get; set; }
    public string twoLetterISORegionName { get; set; }
    public object ictCode { get; set; }
}

public class Co
{
    public string localisedName { get; set; }
    public string threeLetterISORegionName { get; set; }
    public string twoLetterISORegionName { get; set; }
    public object ictCode { get; set; }
}

public class Km
{
    public string localisedName { get; set; }
    public string threeLetterISORegionName { get; set; }
    public string twoLetterISORegionName { get; set; }
    public object ictCode { get; set; }
}

public class Cg
{
    public string localisedName { get; set; }
    public string threeLetterISORegionName { get; set; }
    public string twoLetterISORegionName { get; set; }
    public object ictCode { get; set; }
}

public class Cd
{
    public string localisedName { get; set; }
    public string threeLetterISORegionName { get; set; }
    public string twoLetterISORegionName { get; set; }
    public object ictCode { get; set; }
}

public class Ck
{
    public string localisedName { get; set; }
    public string threeLetterISORegionName { get; set; }
    public string twoLetterISORegionName { get; set; }
    public object ictCode { get; set; }
}

public class Cr
{
    public string localisedName { get; set; }
    public string threeLetterISORegionName { get; set; }
    public string twoLetterISORegionName { get; set; }
    public object ictCode { get; set; }
}

public class Ci
{
    public string localisedName { get; set; }
    public string threeLetterISORegionName { get; set; }
    public string twoLetterISORegionName { get; set; }
    public object ictCode { get; set; }
}

public class Hr
{
    public string localisedName { get; set; }
    public string threeLetterISORegionName { get; set; }
    public string twoLetterISORegionName { get; set; }
    public string ictCode { get; set; }
}

public class Cu
{
    public string localisedName { get; set; }
    public string threeLetterISORegionName { get; set; }
    public string twoLetterISORegionName { get; set; }
    public object ictCode { get; set; }
}

public class Cw
{
    public string localisedName { get; set; }
    public string threeLetterISORegionName { get; set; }
    public string twoLetterISORegionName { get; set; }
    public object ictCode { get; set; }
}

public class Cy
{
    public string localisedName { get; set; }
    public string threeLetterISORegionName { get; set; }
    public string twoLetterISORegionName { get; set; }
    public string ictCode { get; set; }
}

public class Cz
{
    public string localisedName { get; set; }
    public string threeLetterISORegionName { get; set; }
    public string twoLetterISORegionName { get; set; }
    public string ictCode { get; set; }
}

public class Dk
{
    public string localisedName { get; set; }
    public string threeLetterISORegionName { get; set; }
    public string twoLetterISORegionName { get; set; }
    public string ictCode { get; set; }
}

public class Dj
{
    public string localisedName { get; set; }
    public string threeLetterISORegionName { get; set; }
    public string twoLetterISORegionName { get; set; }
    public object ictCode { get; set; }
}

public class Dm
{
    public string localisedName { get; set; }
    public string threeLetterISORegionName { get; set; }
    public string twoLetterISORegionName { get; set; }
    public object ictCode { get; set; }
}

public class Do
{
    public string localisedName { get; set; }
    public string threeLetterISORegionName { get; set; }
    public string twoLetterISORegionName { get; set; }
    public object ictCode { get; set; }
}

public class Tl
{
    public string localisedName { get; set; }
    public string threeLetterISORegionName { get; set; }
    public string twoLetterISORegionName { get; set; }
    public object ictCode { get; set; }
}

public class Ec
{
    public string localisedName { get; set; }
    public string threeLetterISORegionName { get; set; }
    public string twoLetterISORegionName { get; set; }
    public object ictCode { get; set; }
}

public class Eg
{
    public string localisedName { get; set; }
    public string threeLetterISORegionName { get; set; }
    public string twoLetterISORegionName { get; set; }
    public object ictCode { get; set; }
}

public class Sv
{
    public string localisedName { get; set; }
    public string threeLetterISORegionName { get; set; }
    public string twoLetterISORegionName { get; set; }
    public object ictCode { get; set; }
}

public class Gq
{
    public string localisedName { get; set; }
    public string threeLetterISORegionName { get; set; }
    public string twoLetterISORegionName { get; set; }
    public object ictCode { get; set; }
}

public class Er
{
    public string localisedName { get; set; }
    public string threeLetterISORegionName { get; set; }
    public string twoLetterISORegionName { get; set; }
    public object ictCode { get; set; }
}

public class Ee
{
    public string localisedName { get; set; }
    public string threeLetterISORegionName { get; set; }
    public string twoLetterISORegionName { get; set; }
    public string ictCode { get; set; }
}

public class Et
{
    public string localisedName { get; set; }
    public string threeLetterISORegionName { get; set; }
    public string twoLetterISORegionName { get; set; }
    public object ictCode { get; set; }
}

public class Fk
{
    public string localisedName { get; set; }
    public string threeLetterISORegionName { get; set; }
    public string twoLetterISORegionName { get; set; }
    public object ictCode { get; set; }
}

public class Fo
{
    public string localisedName { get; set; }
    public string threeLetterISORegionName { get; set; }
    public string twoLetterISORegionName { get; set; }
    public object ictCode { get; set; }
}

public class Fj
{
    public string localisedName { get; set; }
    public string threeLetterISORegionName { get; set; }
    public string twoLetterISORegionName { get; set; }
    public object ictCode { get; set; }
}

public class Fi
{
    public string localisedName { get; set; }
    public string threeLetterISORegionName { get; set; }
    public string twoLetterISORegionName { get; set; }
    public string ictCode { get; set; }
}

public class Fr
{
    public string localisedName { get; set; }
    public string threeLetterISORegionName { get; set; }
    public string twoLetterISORegionName { get; set; }
    public string ictCode { get; set; }
}

public class Gf
{
    public string localisedName { get; set; }
    public string threeLetterISORegionName { get; set; }
    public string twoLetterISORegionName { get; set; }
    public object ictCode { get; set; }
}

public class Pf
{
    public string localisedName { get; set; }
    public string threeLetterISORegionName { get; set; }
    public string twoLetterISORegionName { get; set; }
    public object ictCode { get; set; }
}

public class Tf
{
    public string localisedName { get; set; }
    public string threeLetterISORegionName { get; set; }
    public string twoLetterISORegionName { get; set; }
    public object ictCode { get; set; }
}

public class Ga
{
    public string localisedName { get; set; }
    public string threeLetterISORegionName { get; set; }
    public string twoLetterISORegionName { get; set; }
    public object ictCode { get; set; }
}

public class Gm
{
    public string localisedName { get; set; }
    public string threeLetterISORegionName { get; set; }
    public string twoLetterISORegionName { get; set; }
    public object ictCode { get; set; }
}

public class Ge
{
    public string localisedName { get; set; }
    public string threeLetterISORegionName { get; set; }
    public string twoLetterISORegionName { get; set; }
    public object ictCode { get; set; }
}

public class De
{
    public string localisedName { get; set; }
    public string threeLetterISORegionName { get; set; }
    public string twoLetterISORegionName { get; set; }
    public string ictCode { get; set; }
}

public class Gh
{
    public string localisedName { get; set; }
    public string threeLetterISORegionName { get; set; }
    public string twoLetterISORegionName { get; set; }
    public object ictCode { get; set; }
}

public class Gi
{
    public string localisedName { get; set; }
    public string threeLetterISORegionName { get; set; }
    public string twoLetterISORegionName { get; set; }
    public object ictCode { get; set; }
}

public class Gr
{
    public string localisedName { get; set; }
    public string threeLetterISORegionName { get; set; }
    public string twoLetterISORegionName { get; set; }
    public string ictCode { get; set; }
}

public class Gl
{
    public string localisedName { get; set; }
    public string threeLetterISORegionName { get; set; }
    public string twoLetterISORegionName { get; set; }
    public object ictCode { get; set; }
}

public class Gd
{
    public string localisedName { get; set; }
    public string threeLetterISORegionName { get; set; }
    public string twoLetterISORegionName { get; set; }
    public object ictCode { get; set; }
}

public class Gp
{
    public string localisedName { get; set; }
    public string threeLetterISORegionName { get; set; }
    public string twoLetterISORegionName { get; set; }
    public object ictCode { get; set; }
}

public class Gu
{
    public string localisedName { get; set; }
    public string threeLetterISORegionName { get; set; }
    public string twoLetterISORegionName { get; set; }
    public object ictCode { get; set; }
}

public class Gt
{
    public string localisedName { get; set; }
    public string threeLetterISORegionName { get; set; }
    public string twoLetterISORegionName { get; set; }
    public object ictCode { get; set; }
}

public class Gg
{
    public string localisedName { get; set; }
    public string threeLetterISORegionName { get; set; }
    public string twoLetterISORegionName { get; set; }
    public object ictCode { get; set; }
}

public class Gn
{
    public string localisedName { get; set; }
    public string threeLetterISORegionName { get; set; }
    public string twoLetterISORegionName { get; set; }
    public object ictCode { get; set; }
}

public class Gw
{
    public string localisedName { get; set; }
    public string threeLetterISORegionName { get; set; }
    public string twoLetterISORegionName { get; set; }
    public object ictCode { get; set; }
}

public class Gy
{
    public string localisedName { get; set; }
    public string threeLetterISORegionName { get; set; }
    public string twoLetterISORegionName { get; set; }
    public object ictCode { get; set; }
}

public class Ht
{
    public string localisedName { get; set; }
    public string threeLetterISORegionName { get; set; }
    public string twoLetterISORegionName { get; set; }
    public object ictCode { get; set; }
}

public class Hm
{
    public string localisedName { get; set; }
    public string threeLetterISORegionName { get; set; }
    public string twoLetterISORegionName { get; set; }
    public object ictCode { get; set; }
}

public class Va
{
    public string localisedName { get; set; }
    public string threeLetterISORegionName { get; set; }
    public string twoLetterISORegionName { get; set; }
    public object ictCode { get; set; }
}

public class Hn
{
    public string localisedName { get; set; }
    public string threeLetterISORegionName { get; set; }
    public string twoLetterISORegionName { get; set; }
    public object ictCode { get; set; }
}

public class Hk
{
    public string localisedName { get; set; }
    public string threeLetterISORegionName { get; set; }
    public string twoLetterISORegionName { get; set; }
    public object ictCode { get; set; }
}

public class Hu
{
    public string localisedName { get; set; }
    public string threeLetterISORegionName { get; set; }
    public string twoLetterISORegionName { get; set; }
    public string ictCode { get; set; }
}

public class Is
{
    public string localisedName { get; set; }
    public string threeLetterISORegionName { get; set; }
    public string twoLetterISORegionName { get; set; }
    public object ictCode { get; set; }
}

public class In
{
    public string localisedName { get; set; }
    public string threeLetterISORegionName { get; set; }
    public string twoLetterISORegionName { get; set; }
    public object ictCode { get; set; }
}

public class Id
{
    public string localisedName { get; set; }
    public string threeLetterISORegionName { get; set; }
    public string twoLetterISORegionName { get; set; }
    public object ictCode { get; set; }
}

public class Ir
{
    public string localisedName { get; set; }
    public string threeLetterISORegionName { get; set; }
    public string twoLetterISORegionName { get; set; }
    public object ictCode { get; set; }
}

public class Iq
{
    public string localisedName { get; set; }
    public string threeLetterISORegionName { get; set; }
    public string twoLetterISORegionName { get; set; }
    public object ictCode { get; set; }
}

public class Ie
{
    public string localisedName { get; set; }
    public string threeLetterISORegionName { get; set; }
    public string twoLetterISORegionName { get; set; }
    public string ictCode { get; set; }
}

public class Im
{
    public string localisedName { get; set; }
    public string threeLetterISORegionName { get; set; }
    public string twoLetterISORegionName { get; set; }
    public object ictCode { get; set; }
}

public class Il
{
    public string localisedName { get; set; }
    public string threeLetterISORegionName { get; set; }
    public string twoLetterISORegionName { get; set; }
    public object ictCode { get; set; }
}

public class It
{
    public string localisedName { get; set; }
    public string threeLetterISORegionName { get; set; }
    public string twoLetterISORegionName { get; set; }
    public string ictCode { get; set; }
}

public class Jm
{
    public string localisedName { get; set; }
    public string threeLetterISORegionName { get; set; }
    public string twoLetterISORegionName { get; set; }
    public object ictCode { get; set; }
}

public class Jp
{
    public string localisedName { get; set; }
    public string threeLetterISORegionName { get; set; }
    public string twoLetterISORegionName { get; set; }
    public object ictCode { get; set; }
}

public class Je
{
    public string localisedName { get; set; }
    public string threeLetterISORegionName { get; set; }
    public string twoLetterISORegionName { get; set; }
    public object ictCode { get; set; }
}

public class Jo
{
    public string localisedName { get; set; }
    public string threeLetterISORegionName { get; set; }
    public string twoLetterISORegionName { get; set; }
    public object ictCode { get; set; }
}

public class Kz
{
    public string localisedName { get; set; }
    public string threeLetterISORegionName { get; set; }
    public string twoLetterISORegionName { get; set; }
    public object ictCode { get; set; }
}

public class Ke
{
    public string localisedName { get; set; }
    public string threeLetterISORegionName { get; set; }
    public string twoLetterISORegionName { get; set; }
    public object ictCode { get; set; }
}

public class Ki
{
    public string localisedName { get; set; }
    public string threeLetterISORegionName { get; set; }
    public string twoLetterISORegionName { get; set; }
    public object ictCode { get; set; }
}

public class Kp
{
    public string localisedName { get; set; }
    public string threeLetterISORegionName { get; set; }
    public string twoLetterISORegionName { get; set; }
    public object ictCode { get; set; }
}

public class Kr
{
    public string localisedName { get; set; }
    public string threeLetterISORegionName { get; set; }
    public string twoLetterISORegionName { get; set; }
    public object ictCode { get; set; }
}

public class Kw
{
    public string localisedName { get; set; }
    public string threeLetterISORegionName { get; set; }
    public string twoLetterISORegionName { get; set; }
    public object ictCode { get; set; }
}

public class Kg
{
    public string localisedName { get; set; }
    public string threeLetterISORegionName { get; set; }
    public string twoLetterISORegionName { get; set; }
    public object ictCode { get; set; }
}

public class La
{
    public string localisedName { get; set; }
    public string threeLetterISORegionName { get; set; }
    public string twoLetterISORegionName { get; set; }
    public object ictCode { get; set; }
}

public class Lv
{
    public string localisedName { get; set; }
    public string threeLetterISORegionName { get; set; }
    public string twoLetterISORegionName { get; set; }
    public string ictCode { get; set; }
}

public class Lb
{
    public string localisedName { get; set; }
    public string threeLetterISORegionName { get; set; }
    public string twoLetterISORegionName { get; set; }
    public object ictCode { get; set; }
}

public class Ls
{
    public string localisedName { get; set; }
    public string threeLetterISORegionName { get; set; }
    public string twoLetterISORegionName { get; set; }
    public object ictCode { get; set; }
}

public class Lr
{
    public string localisedName { get; set; }
    public string threeLetterISORegionName { get; set; }
    public string twoLetterISORegionName { get; set; }
    public object ictCode { get; set; }
}

public class Ly
{
    public string localisedName { get; set; }
    public string threeLetterISORegionName { get; set; }
    public string twoLetterISORegionName { get; set; }
    public object ictCode { get; set; }
}

public class Li
{
    public string localisedName { get; set; }
    public string threeLetterISORegionName { get; set; }
    public string twoLetterISORegionName { get; set; }
    public object ictCode { get; set; }
}

public class Lt
{
    public string localisedName { get; set; }
    public string threeLetterISORegionName { get; set; }
    public string twoLetterISORegionName { get; set; }
    public string ictCode { get; set; }
}

public class Lu
{
    public string localisedName { get; set; }
    public string threeLetterISORegionName { get; set; }
    public string twoLetterISORegionName { get; set; }
    public string ictCode { get; set; }
}

public class Mo
{
    public string localisedName { get; set; }
    public string threeLetterISORegionName { get; set; }
    public string twoLetterISORegionName { get; set; }
    public object ictCode { get; set; }
}

public class Mk
{
    public string localisedName { get; set; }
    public string threeLetterISORegionName { get; set; }
    public string twoLetterISORegionName { get; set; }
    public object ictCode { get; set; }
}

public class Mg
{
    public string localisedName { get; set; }
    public string threeLetterISORegionName { get; set; }
    public string twoLetterISORegionName { get; set; }
    public object ictCode { get; set; }
}

public class Mw
{
    public string localisedName { get; set; }
    public string threeLetterISORegionName { get; set; }
    public string twoLetterISORegionName { get; set; }
    public object ictCode { get; set; }
}

public class My
{
    public string localisedName { get; set; }
    public string threeLetterISORegionName { get; set; }
    public string twoLetterISORegionName { get; set; }
    public object ictCode { get; set; }
}

public class Mv
{
    public string localisedName { get; set; }
    public string threeLetterISORegionName { get; set; }
    public string twoLetterISORegionName { get; set; }
    public object ictCode { get; set; }
}

public class Ml
{
    public string localisedName { get; set; }
    public string threeLetterISORegionName { get; set; }
    public string twoLetterISORegionName { get; set; }
    public object ictCode { get; set; }
}

public class Mt
{
    public string localisedName { get; set; }
    public string threeLetterISORegionName { get; set; }
    public string twoLetterISORegionName { get; set; }
    public string ictCode { get; set; }
}

public class Mh
{
    public string localisedName { get; set; }
    public string threeLetterISORegionName { get; set; }
    public string twoLetterISORegionName { get; set; }
    public object ictCode { get; set; }
}

public class Mq
{
    public string localisedName { get; set; }
    public string threeLetterISORegionName { get; set; }
    public string twoLetterISORegionName { get; set; }
    public object ictCode { get; set; }
}

public class Mr
{
    public string localisedName { get; set; }
    public string threeLetterISORegionName { get; set; }
    public string twoLetterISORegionName { get; set; }
    public object ictCode { get; set; }
}

public class Mu
{
    public string localisedName { get; set; }
    public string threeLetterISORegionName { get; set; }
    public string twoLetterISORegionName { get; set; }
    public object ictCode { get; set; }
}

public class Yt
{
    public string localisedName { get; set; }
    public string threeLetterISORegionName { get; set; }
    public string twoLetterISORegionName { get; set; }
    public object ictCode { get; set; }
}

public class Mx
{
    public string localisedName { get; set; }
    public string threeLetterISORegionName { get; set; }
    public string twoLetterISORegionName { get; set; }
    public object ictCode { get; set; }
}

public class Fm
{
    public string localisedName { get; set; }
    public string threeLetterISORegionName { get; set; }
    public string twoLetterISORegionName { get; set; }
    public object ictCode { get; set; }
}

public class Md
{
    public string localisedName { get; set; }
    public string threeLetterISORegionName { get; set; }
    public string twoLetterISORegionName { get; set; }
    public object ictCode { get; set; }
}

public class Mc
{
    public string localisedName { get; set; }
    public string threeLetterISORegionName { get; set; }
    public string twoLetterISORegionName { get; set; }
    public object ictCode { get; set; }
}

public class Mn
{
    public string localisedName { get; set; }
    public string threeLetterISORegionName { get; set; }
    public string twoLetterISORegionName { get; set; }
    public object ictCode { get; set; }
}

public class Me
{
    public string localisedName { get; set; }
    public string threeLetterISORegionName { get; set; }
    public string twoLetterISORegionName { get; set; }
    public object ictCode { get; set; }
}

public class Ms
{
    public string localisedName { get; set; }
    public string threeLetterISORegionName { get; set; }
    public string twoLetterISORegionName { get; set; }
    public object ictCode { get; set; }
}

public class Ma
{
    public string localisedName { get; set; }
    public string threeLetterISORegionName { get; set; }
    public string twoLetterISORegionName { get; set; }
    public object ictCode { get; set; }
}

public class Mz
{
    public string localisedName { get; set; }
    public string threeLetterISORegionName { get; set; }
    public string twoLetterISORegionName { get; set; }
    public object ictCode { get; set; }
}

public class Mm
{
    public string localisedName { get; set; }
    public string threeLetterISORegionName { get; set; }
    public string twoLetterISORegionName { get; set; }
    public object ictCode { get; set; }
}

public class Na
{
    public string localisedName { get; set; }
    public string threeLetterISORegionName { get; set; }
    public string twoLetterISORegionName { get; set; }
    public object ictCode { get; set; }
}

public class Nr
{
    public string localisedName { get; set; }
    public string threeLetterISORegionName { get; set; }
    public string twoLetterISORegionName { get; set; }
    public object ictCode { get; set; }
}

public class Np
{
    public string localisedName { get; set; }
    public string threeLetterISORegionName { get; set; }
    public string twoLetterISORegionName { get; set; }
    public object ictCode { get; set; }
}

public class Nl
{
    public string localisedName { get; set; }
    public string threeLetterISORegionName { get; set; }
    public string twoLetterISORegionName { get; set; }
    public string ictCode { get; set; }
}

public class An
{
    public string localisedName { get; set; }
    public string threeLetterISORegionName { get; set; }
    public string twoLetterISORegionName { get; set; }
    public object ictCode { get; set; }
}

public class Nc
{
    public string localisedName { get; set; }
    public string threeLetterISORegionName { get; set; }
    public string twoLetterISORegionName { get; set; }
    public object ictCode { get; set; }
}

public class Nz
{
    public string localisedName { get; set; }
    public string threeLetterISORegionName { get; set; }
    public string twoLetterISORegionName { get; set; }
    public object ictCode { get; set; }
}

public class Ni
{
    public string localisedName { get; set; }
    public string threeLetterISORegionName { get; set; }
    public string twoLetterISORegionName { get; set; }
    public object ictCode { get; set; }
}

public class Ne
{
    public string localisedName { get; set; }
    public string threeLetterISORegionName { get; set; }
    public string twoLetterISORegionName { get; set; }
    public object ictCode { get; set; }
}

public class Ng
{
    public string localisedName { get; set; }
    public string threeLetterISORegionName { get; set; }
    public string twoLetterISORegionName { get; set; }
    public object ictCode { get; set; }
}

public class Nu
{
    public string localisedName { get; set; }
    public string threeLetterISORegionName { get; set; }
    public string twoLetterISORegionName { get; set; }
    public object ictCode { get; set; }
}

public class Nf
{
    public string localisedName { get; set; }
    public string threeLetterISORegionName { get; set; }
    public string twoLetterISORegionName { get; set; }
    public object ictCode { get; set; }
}

public class Mp
{
    public string localisedName { get; set; }
    public string threeLetterISORegionName { get; set; }
    public string twoLetterISORegionName { get; set; }
    public object ictCode { get; set; }
}

public class No
{
    public string localisedName { get; set; }
    public string threeLetterISORegionName { get; set; }
    public string twoLetterISORegionName { get; set; }
    public object ictCode { get; set; }
}

public class Om
{
    public string localisedName { get; set; }
    public string threeLetterISORegionName { get; set; }
    public string twoLetterISORegionName { get; set; }
    public object ictCode { get; set; }
}

public class Pk
{
    public string localisedName { get; set; }
    public string threeLetterISORegionName { get; set; }
    public string twoLetterISORegionName { get; set; }
    public object ictCode { get; set; }
}

public class Pw
{
    public string localisedName { get; set; }
    public string threeLetterISORegionName { get; set; }
    public string twoLetterISORegionName { get; set; }
    public object ictCode { get; set; }
}

public class Ps
{
    public string localisedName { get; set; }
    public string threeLetterISORegionName { get; set; }
    public string twoLetterISORegionName { get; set; }
    public object ictCode { get; set; }
}

public class Pa
{
    public string localisedName { get; set; }
    public string threeLetterISORegionName { get; set; }
    public string twoLetterISORegionName { get; set; }
    public object ictCode { get; set; }
}

public class Pg
{
    public string localisedName { get; set; }
    public string threeLetterISORegionName { get; set; }
    public string twoLetterISORegionName { get; set; }
    public object ictCode { get; set; }
}

public class Py
{
    public string localisedName { get; set; }
    public string threeLetterISORegionName { get; set; }
    public string twoLetterISORegionName { get; set; }
    public object ictCode { get; set; }
}

public class Pe
{
    public string localisedName { get; set; }
    public string threeLetterISORegionName { get; set; }
    public string twoLetterISORegionName { get; set; }
    public object ictCode { get; set; }
}

public class Ph
{
    public string localisedName { get; set; }
    public string threeLetterISORegionName { get; set; }
    public string twoLetterISORegionName { get; set; }
    public object ictCode { get; set; }
}

public class Pn
{
    public string localisedName { get; set; }
    public string threeLetterISORegionName { get; set; }
    public string twoLetterISORegionName { get; set; }
    public object ictCode { get; set; }
}

public class Pl
{
    public string localisedName { get; set; }
    public string threeLetterISORegionName { get; set; }
    public string twoLetterISORegionName { get; set; }
    public string ictCode { get; set; }
}

public class Pt
{
    public string localisedName { get; set; }
    public string threeLetterISORegionName { get; set; }
    public string twoLetterISORegionName { get; set; }
    public string ictCode { get; set; }
}

public class Pr
{
    public string localisedName { get; set; }
    public string threeLetterISORegionName { get; set; }
    public string twoLetterISORegionName { get; set; }
    public object ictCode { get; set; }
}

public class Qa
{
    public string localisedName { get; set; }
    public string threeLetterISORegionName { get; set; }
    public string twoLetterISORegionName { get; set; }
    public object ictCode { get; set; }
}

public class Re
{
    public string localisedName { get; set; }
    public string threeLetterISORegionName { get; set; }
    public string twoLetterISORegionName { get; set; }
    public object ictCode { get; set; }
}

public class Ro
{
    public string localisedName { get; set; }
    public string threeLetterISORegionName { get; set; }
    public string twoLetterISORegionName { get; set; }
    public string ictCode { get; set; }
}

public class Ru
{
    public string localisedName { get; set; }
    public string threeLetterISORegionName { get; set; }
    public string twoLetterISORegionName { get; set; }
    public object ictCode { get; set; }
}

public class Rw
{
    public string localisedName { get; set; }
    public string threeLetterISORegionName { get; set; }
    public string twoLetterISORegionName { get; set; }
    public object ictCode { get; set; }
}

public class Bl
{
    public string localisedName { get; set; }
    public string threeLetterISORegionName { get; set; }
    public string twoLetterISORegionName { get; set; }
    public object ictCode { get; set; }
}

public class Kn
{
    public string localisedName { get; set; }
    public string threeLetterISORegionName { get; set; }
    public string twoLetterISORegionName { get; set; }
    public object ictCode { get; set; }
}

public class Lc
{
    public string localisedName { get; set; }
    public string threeLetterISORegionName { get; set; }
    public string twoLetterISORegionName { get; set; }
    public object ictCode { get; set; }
}

public class Mf
{
    public string localisedName { get; set; }
    public string threeLetterISORegionName { get; set; }
    public string twoLetterISORegionName { get; set; }
    public object ictCode { get; set; }
}

public class Vc
{
    public string localisedName { get; set; }
    public string threeLetterISORegionName { get; set; }
    public string twoLetterISORegionName { get; set; }
    public object ictCode { get; set; }
}

public class Ws
{
    public string localisedName { get; set; }
    public string threeLetterISORegionName { get; set; }
    public string twoLetterISORegionName { get; set; }
    public object ictCode { get; set; }
}

public class Sm
{
    public string localisedName { get; set; }
    public string threeLetterISORegionName { get; set; }
    public string twoLetterISORegionName { get; set; }
    public object ictCode { get; set; }
}

public class St
{
    public string localisedName { get; set; }
    public string threeLetterISORegionName { get; set; }
    public string twoLetterISORegionName { get; set; }
    public object ictCode { get; set; }
}

public class Sa
{
    public string localisedName { get; set; }
    public string threeLetterISORegionName { get; set; }
    public string twoLetterISORegionName { get; set; }
    public object ictCode { get; set; }
}

public class Sn
{
    public string localisedName { get; set; }
    public string threeLetterISORegionName { get; set; }
    public string twoLetterISORegionName { get; set; }
    public object ictCode { get; set; }
}

public class Rs
{
    public string localisedName { get; set; }
    public string threeLetterISORegionName { get; set; }
    public string twoLetterISORegionName { get; set; }
    public object ictCode { get; set; }
}

public class Sc
{
    public string localisedName { get; set; }
    public string threeLetterISORegionName { get; set; }
    public string twoLetterISORegionName { get; set; }
    public object ictCode { get; set; }
}

public class Sl
{
    public string localisedName { get; set; }
    public string threeLetterISORegionName { get; set; }
    public string twoLetterISORegionName { get; set; }
    public object ictCode { get; set; }
}

public class Sg
{
    public string localisedName { get; set; }
    public string threeLetterISORegionName { get; set; }
    public string twoLetterISORegionName { get; set; }
    public object ictCode { get; set; }
}

public class Sx
{
    public string localisedName { get; set; }
    public string threeLetterISORegionName { get; set; }
    public string twoLetterISORegionName { get; set; }
    public object ictCode { get; set; }
}

public class Sk
{
    public string localisedName { get; set; }
    public string threeLetterISORegionName { get; set; }
    public string twoLetterISORegionName { get; set; }
    public string ictCode { get; set; }
}

public class Si
{
    public string localisedName { get; set; }
    public string threeLetterISORegionName { get; set; }
    public string twoLetterISORegionName { get; set; }
    public string ictCode { get; set; }
}

public class Sb
{
    public string localisedName { get; set; }
    public string threeLetterISORegionName { get; set; }
    public string twoLetterISORegionName { get; set; }
    public object ictCode { get; set; }
}

public class So
{
    public string localisedName { get; set; }
    public string threeLetterISORegionName { get; set; }
    public string twoLetterISORegionName { get; set; }
    public object ictCode { get; set; }
}

public class Za
{
    public string localisedName { get; set; }
    public string threeLetterISORegionName { get; set; }
    public string twoLetterISORegionName { get; set; }
    public object ictCode { get; set; }
}

public class Es
{
    public string localisedName { get; set; }
    public string threeLetterISORegionName { get; set; }
    public string twoLetterISORegionName { get; set; }
    public string ictCode { get; set; }
}

public class Lk
{
    public string localisedName { get; set; }
    public string threeLetterISORegionName { get; set; }
    public string twoLetterISORegionName { get; set; }
    public object ictCode { get; set; }
}

public class Sh
{
    public string localisedName { get; set; }
    public string threeLetterISORegionName { get; set; }
    public string twoLetterISORegionName { get; set; }
    public object ictCode { get; set; }
}

public class Pm
{
    public string localisedName { get; set; }
    public string threeLetterISORegionName { get; set; }
    public string twoLetterISORegionName { get; set; }
    public object ictCode { get; set; }
}

public class Sd
{
    public string localisedName { get; set; }
    public string threeLetterISORegionName { get; set; }
    public string twoLetterISORegionName { get; set; }
    public object ictCode { get; set; }
}

public class Sr
{
    public string localisedName { get; set; }
    public string threeLetterISORegionName { get; set; }
    public string twoLetterISORegionName { get; set; }
    public object ictCode { get; set; }
}

public class Sj
{
    public string localisedName { get; set; }
    public string threeLetterISORegionName { get; set; }
    public string twoLetterISORegionName { get; set; }
    public object ictCode { get; set; }
}

public class Sz
{
    public string localisedName { get; set; }
    public string threeLetterISORegionName { get; set; }
    public string twoLetterISORegionName { get; set; }
    public object ictCode { get; set; }
}

public class Se
{
    public string localisedName { get; set; }
    public string threeLetterISORegionName { get; set; }
    public string twoLetterISORegionName { get; set; }
    public string ictCode { get; set; }
}

public class Ch
{
    public string localisedName { get; set; }
    public string threeLetterISORegionName { get; set; }
    public string twoLetterISORegionName { get; set; }
    public object ictCode { get; set; }
}

public class Sy
{
    public string localisedName { get; set; }
    public string threeLetterISORegionName { get; set; }
    public string twoLetterISORegionName { get; set; }
    public object ictCode { get; set; }
}

public class Tw
{
    public string localisedName { get; set; }
    public string threeLetterISORegionName { get; set; }
    public string twoLetterISORegionName { get; set; }
    public object ictCode { get; set; }
}

public class Tj
{
    public string localisedName { get; set; }
    public string threeLetterISORegionName { get; set; }
    public string twoLetterISORegionName { get; set; }
    public object ictCode { get; set; }
}

public class Tz
{
    public string localisedName { get; set; }
    public string threeLetterISORegionName { get; set; }
    public string twoLetterISORegionName { get; set; }
    public object ictCode { get; set; }
}

public class Th
{
    public string localisedName { get; set; }
    public string threeLetterISORegionName { get; set; }
    public string twoLetterISORegionName { get; set; }
    public object ictCode { get; set; }
}

public class Tg
{
    public string localisedName { get; set; }
    public string threeLetterISORegionName { get; set; }
    public string twoLetterISORegionName { get; set; }
    public object ictCode { get; set; }
}

public class Tk
{
    public string localisedName { get; set; }
    public string threeLetterISORegionName { get; set; }
    public string twoLetterISORegionName { get; set; }
    public object ictCode { get; set; }
}

public class To
{
    public string localisedName { get; set; }
    public string threeLetterISORegionName { get; set; }
    public string twoLetterISORegionName { get; set; }
    public object ictCode { get; set; }
}

public class Tt
{
    public string localisedName { get; set; }
    public string threeLetterISORegionName { get; set; }
    public string twoLetterISORegionName { get; set; }
    public object ictCode { get; set; }
}

public class Tn
{
    public string localisedName { get; set; }
    public string threeLetterISORegionName { get; set; }
    public string twoLetterISORegionName { get; set; }
    public object ictCode { get; set; }
}

public class Tr
{
    public string localisedName { get; set; }
    public string threeLetterISORegionName { get; set; }
    public string twoLetterISORegionName { get; set; }
    public object ictCode { get; set; }
}

public class Tm
{
    public string localisedName { get; set; }
    public string threeLetterISORegionName { get; set; }
    public string twoLetterISORegionName { get; set; }
    public object ictCode { get; set; }
}

public class Tc
{
    public string localisedName { get; set; }
    public string threeLetterISORegionName { get; set; }
    public string twoLetterISORegionName { get; set; }
    public object ictCode { get; set; }
}

public class Tv
{
    public string localisedName { get; set; }
    public string threeLetterISORegionName { get; set; }
    public string twoLetterISORegionName { get; set; }
    public object ictCode { get; set; }
}

public class Ug
{
    public string localisedName { get; set; }
    public string threeLetterISORegionName { get; set; }
    public string twoLetterISORegionName { get; set; }
    public object ictCode { get; set; }
}

public class Ua
{
    public string localisedName { get; set; }
    public string threeLetterISORegionName { get; set; }
    public string twoLetterISORegionName { get; set; }
    public object ictCode { get; set; }
}

public class Ae
{
    public string localisedName { get; set; }
    public string threeLetterISORegionName { get; set; }
    public string twoLetterISORegionName { get; set; }
    public object ictCode { get; set; }
}

public class Gb
{
    public string localisedName { get; set; }
    public string threeLetterISORegionName { get; set; }
    public string twoLetterISORegionName { get; set; }
    public string ictCode { get; set; }
}

public class Us
{
    public string localisedName { get; set; }
    public string threeLetterISORegionName { get; set; }
    public string twoLetterISORegionName { get; set; }
    public object ictCode { get; set; }
}

public class Um
{
    public string localisedName { get; set; }
    public string threeLetterISORegionName { get; set; }
    public string twoLetterISORegionName { get; set; }
    public object ictCode { get; set; }
}

public class Uy
{
    public string localisedName { get; set; }
    public string threeLetterISORegionName { get; set; }
    public string twoLetterISORegionName { get; set; }
    public object ictCode { get; set; }
}

public class Uz
{
    public string localisedName { get; set; }
    public string threeLetterISORegionName { get; set; }
    public string twoLetterISORegionName { get; set; }
    public object ictCode { get; set; }
}

public class Vu
{
    public string localisedName { get; set; }
    public string threeLetterISORegionName { get; set; }
    public string twoLetterISORegionName { get; set; }
    public object ictCode { get; set; }
}

public class Ve
{
    public string localisedName { get; set; }
    public string threeLetterISORegionName { get; set; }
    public string twoLetterISORegionName { get; set; }
    public object ictCode { get; set; }
}

public class Vn
{
    public string localisedName { get; set; }
    public string threeLetterISORegionName { get; set; }
    public string twoLetterISORegionName { get; set; }
    public object ictCode { get; set; }
}

public class Vg
{
    public string localisedName { get; set; }
    public string threeLetterISORegionName { get; set; }
    public string twoLetterISORegionName { get; set; }
    public object ictCode { get; set; }
}

public class Vi
{
    public string localisedName { get; set; }
    public string threeLetterISORegionName { get; set; }
    public string twoLetterISORegionName { get; set; }
    public object ictCode { get; set; }
}

public class Wf
{
    public string localisedName { get; set; }
    public string threeLetterISORegionName { get; set; }
    public string twoLetterISORegionName { get; set; }
    public object ictCode { get; set; }
}

public class Eh
{
    public string localisedName { get; set; }
    public string threeLetterISORegionName { get; set; }
    public string twoLetterISORegionName { get; set; }
    public object ictCode { get; set; }
}

public class Ye
{
    public string localisedName { get; set; }
    public string threeLetterISORegionName { get; set; }
    public string twoLetterISORegionName { get; set; }
    public object ictCode { get; set; }
}

public class Zm
{
    public string localisedName { get; set; }
    public string threeLetterISORegionName { get; set; }
    public string twoLetterISORegionName { get; set; }
    public object ictCode { get; set; }
}

public class Zw
{
    public string localisedName { get; set; }
    public string threeLetterISORegionName { get; set; }
    public string twoLetterISORegionName { get; set; }
    public object ictCode { get; set; }
}

public class Commentspubdatefilter
{
    public string today { get; set; }
    public string yesterday { get; set; }
    public string just { get; set; }
}

public class Document
{
    public string templateSavedMessage { get; set; }
    public Reminder reminder { get; set; }
    public Invoice invoice { get; set; }
    public Creditnote creditnote { get; set; }
    public Quote quote { get; set; }
    public string defaultTemplate { get; set; }
    public Variables variables { get; set; }
}

public class Reminder
{
    public string templateName { get; set; }
}

public class Invoice
{
    public string templateName { get; set; }
}

public class Creditnote
{
    public string templateName { get; set; }
}

public class Quote
{
    public string templateName { get; set; }
}

public class Variables
{
    public string PageNumber { get; set; }
    public string PageTotal { get; set; }
    public string PaymentReference { get; set; }
}

public class Dragndrop
{
    public string items { get; set; }
}

public class Systemmessages
{
    public string unsavedData { get; set; }
    public string leaveAnyway { get; set; }
    public string stay { get; set; }
    public string leavePageQuestion { get; set; }
}

public class Assignments
{
    public string noVat { get; set; }
}


