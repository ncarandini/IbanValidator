using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace TPCWare.IbanValidator
{
	public static class IbanValidator
	{
		public static (bool isValid, int errorCode, string errorMsg) Check(string iban)
		{
			bool isValid;
			int errorCode;
			string errorMsg;

			string ibanCountryIsoCode = iban.Substring(0, 2);
			string ibanWithoutspaces = iban.Replace(" ", "");

			// Check if the country is supported
			var ibanCountryFormat = IbanCountryFormats.FirstOrDefault(icf => icf.CountryIsoCode == ibanCountryIsoCode);
			if (ibanCountryFormat == null)
			{
				isValid = false;
				errorCode = 1;
				errorMsg = "Country not supported.";
			}
			else
			{
				// Check if format is valid
				bool ibanFormatIsValid = Regex.IsMatch(ibanWithoutspaces, ibanCountryFormat.IbanRegex);
				if (!ibanFormatIsValid)
				{
					isValid = false;
					errorCode = 2;
					errorMsg = "Country format check failed.";
				}
				else
				{
					// Validate the IBAN
					string ibanValidationSource = ibanWithoutspaces.Substring(4) + ibanWithoutspaces.Substring(0, 4);

					string ibanValidationNumbers = "";
					foreach (char c in ibanValidationSource)
					{
						if (c >= '0' && c <= '9')
						{
							ibanValidationNumbers += (c - 48).ToString();
						}
						else if (c >= 'A' && c <= 'Z')
						{
							ibanValidationNumbers += (c - 55).ToString();
						}
					}

					int i = 0;
					int len = ibanValidationNumbers.Length;
					string d = ibanValidationNumbers.Substring(i, 9); i += 9;
					int r = Convert.ToInt32(d) % 97;
					while (i < len)
					{
						d = r.ToString("00") + ibanValidationNumbers.Substring(i, Math.Min(len-i, 7)); i += 7;
						r = Convert.ToInt32(d) % 97;
					}
					if (r == 1)
					{
						isValid = true;
						errorCode = 0;
						errorMsg = string.Empty;
					}
					else
					{
						isValid = false;
						errorCode = 1;
						errorMsg = "Checksum failed.";
					}
				}
			}

			return (isValid, errorCode, errorMsg);
		}

		public static List<IbanCountryFormat> IbanCountryFormats = new List<IbanCountryFormat>
		{
			new IbanCountryFormat
			{
				CountryName = "Italy",
				CountryIsoCode = "IT",
				IbanRegex = @"IT\d\d[A-Z]\d{10}[0-9A-Z]{12}"
			}
		};
	}
}
