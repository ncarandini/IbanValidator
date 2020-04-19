# IBAN validator
Simple IBAN Validator

This library contains s static class `IbanValidator` with a method `Check`and an `IbanCountryFormat` list that describe the lenght and format of the IBAN code for each supported country.

Actually I've managed to add support for IBAN codes of the UE countries and the United Kingdom.
Please feel free to contribute to add more countries, deriving the Regex expression from the [Wikipedia IBAN formats by country](https://en.wikipedia.org/wiki/International_Bank_Account_Number) table.

### Excerpt from the code

``` csharp
public static class IbanValidator
{
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
             CountryName = "France",
             CountryIsoCode = "FR",
             IbanRegex = @"FR\d{2}\d{10}[0-9A-Z]{11}\d{2}"
         },
         new IbanCountryFormat
         {
             CountryName = "Germany",
             CountryIsoCode = "DE",
             IbanRegex = @"DE\d{2}\d{18}"
         },
         new IbanCountryFormat
         {
             CountryName = "Italy",
             CountryIsoCode = "IT",
             IbanRegex = @"IT\d{2}[A-Z]\d{10}[0-9A-Z]{12}"
         },
         new IbanCountryFormat
         {
             CountryName = "Spain",
             CountryIsoCode = "SP",
             IbanRegex = @"SP\d{2}\d{20}"
         },
         new IbanCountryFormat
         {
             CountryName = "United Kingdom of Great Britain and Northern Ireland (the)",
             CountryIsoCode = "GB",
             IbanRegex = @"GB\d{2}[A-Z]{4}\d{14}"
         }
         
        // Other country formats
        ...

    };
}
```
