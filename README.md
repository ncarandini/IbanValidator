# IBAN validator
Simple IBAN Validator

This library contains a static class `IbanValidator` with two static methods:
- `bool IsValid(string iban)`
- `(bool isValid, int errorCode, string errorMsg) Check(string iban)`

In the static class an internally used list `IbanCountryFormats` of `IbanCountryFormat` items describe the lenght and format of the IBAN code for each supported country.

### Contribution

Actually I've managed to add support for IBAN codes of the UE countries and the United Kingdom.
Please feel free to contribute to add more countries, deriving the Regex expression from the [Wikipedia IBAN formats by country](https://en.wikipedia.org/wiki/International_Bank_Account_Number) table.

### Excerpt from the code

``` csharp
public static class IbanValidator
{
    public static bool IsValid(string iban)
    {
        (bool isValid, _, _) = Check(iban);
        return isValid;
    }

    public static (bool isValid, int errorCode, string errorMsg) Check(string iban)
    {
        // Check IBAN code format
        ...
    
        // Calculate and verify IBAN code checksum 
        ...
    
        return (isValid, errorCode, errorMsg);
    }

    public static List<IbanCountryFormat> IbanCountryFormats = new List<IbanCountryFormat>
    {
        new IbanCountryFormat
        {
            CountryName = "Austria",
            CountryIsoCode = "AT",
            IbanRegex = @"AT\d{2}\d{16}" // 16n
        },
        
        // Other country formats
        ...
    };
}
```
