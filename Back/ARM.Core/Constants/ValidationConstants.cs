namespace ARM.Core.Constants;

public static class ValidationConstants
{
    public const int MaxNameLength = 100;
    public const int MaxDescriptionLength = 500;
    public const int MaxEmailLength = 254;
    public const int MaxPhoneNumberLength = 15;
    public const int MaxAddressLength = 200;
    public const int MaxTaxNumberLength = 20;
    public const int MaxBankAccountLength = 34;
    public const int MaxCarMakeLength = 50;
    public const int MaxCarModelLength = 50;
    public const int MaxCarPlateLength = 15;
    public const int MaxCarColorLength = 30;
    public const int MaxVINLength = 17;
    public const int MaxEngineTypeLength = 30;
    public const int MaxTransmissionLength = 20;
    public const int MaxRepairDescriptionLength = 500;
    public const int MaxRepairDiagnosisLength = 300;
    public const int MaxRepairWorkPerformedLength = 400;
    public const int MaxPartsReplacedLength = 300;
    public const int MaxRepairRecommendationsLength = 250;
    public const int MinYear = 1886; 
    public const int MaxCommentLength = 500;
    public const int MaxDiagnosisResultsLength = 300;
    public const int MaxCancellationReasonLength = 250;
    public const int MaxPermissionNameLength = 100;
    public const int MaxPermissionDescriptionLength = 500;
    public const int MaxServiceRequestDescriptionLength = 1000;
    public const int MaxServiceRequestNotesLength = 500;
    public const int MaxProblemDescriptionLength = 1000;
    
    public static readonly string NameRegex = @"^[a-zA-Z0-9\s\-']{2,100}$";
    public static readonly string PhoneRegex = @"^\+?[0-9\s\-]{8,15}$";
    public static readonly string EmailRegex = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
    public static readonly string AddressRegex = @"^[a-zA-Z0-9\s,.'-]{3,200}$";
    public static readonly string TaxNumberRegex = @"^[A-Z0-9]{5,20}$";
    public static readonly string BankAccountRegex = @"^[A-Z0-9]{15,34}$";
    public static readonly string VINRegex = @"^[A-HJ-NPR-Z0-9]{17}$";
    public static readonly string CarPlateRegex = @"^[A-Z0-9\-]{4,15}$";
    public static readonly string DateRegex = @"^\d{4}-\d{2}-\d{2}$";
    public static readonly string DecimalRegex = @"^\d+(\.\d{1,2})?$";
    
    
}