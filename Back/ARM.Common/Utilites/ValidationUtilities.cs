using static ARM.Core.Constants.ValidationConstants;
using static System.Text.RegularExpressions.Regex;

namespace ARM.Common.Utilites;

public static class ValidationUtilities
{
    public static bool IsValidName(string name) => IsMatch(name, NameRegex);
    public static bool IsValidPhone(string phone) => IsMatch(phone, PhoneRegex);
    public static bool IsValidEmail(string email) => IsMatch(email, EmailRegex);
    public static bool IsValidAddress(string address) => IsMatch(address, AddressRegex);
    public static bool IsValidTaxNumber(string taxNumber) => IsMatch(taxNumber, TaxNumberRegex);
    public static bool IsValidBankAccount(string bankAccount) => IsMatch(bankAccount, BankAccountRegex);
    public static bool IsValidCarPlate(string carPlate) => IsMatch(carPlate, CarPlateRegex);
    public static bool IsValidDate(string date) => IsMatch(date, DateRegex);
    public static bool IsValidDecimal(string value) => IsMatch(value, DecimalRegex);
    public static bool IsValidDiagnosisResults(string diagnosis) => diagnosis.Length <= MaxDiagnosisResultsLength;
    public static bool IsValidCancellationReason(string reason) => reason.Length <= MaxCancellationReasonLength;
    public static bool IsValidCustomerComments(string comments) => comments.Length <= MaxCommentLength;
    public static bool IsValidComment(string comment) => comment.Length <= MaxCommentLength;
}