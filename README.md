# IBAN validator
Simple IBAN Validator

This library contains s static class `IbanValidator` with a method `Check`and an `IbanCountryFormat` list that describe the lenght and format of the IBAN code for each supported country.

Actually I've managed to add suppurt for those countries: Italy, France, Germany, United Kindom, Spain.
Please contribute to add more countries, deriving the Regex expression from the [Wikipedia IBAN formats by country](https://en.wikipedia.org/wiki/International_Bank_Account_Number) table.

### Exceprt from the code

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
			CountryName = "Italy",
			CountryIsoCode = "IT",
			IbanRegex = @"IT\d\d[A-Z]\d{10}[0-9A-Z]{12}"
		},
    
    ...
    
	};
}
```
