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
				CountryName = "Austria",
				CountryIsoCode = "AT",
				IbanRegex = @"AT\d{2}\d{16}" // 16n
			},
			new IbanCountryFormat
			{
				CountryName = "Belgium",
				CountryIsoCode = "BE",
				IbanRegex = @"BE\d{2}\d{12}" // 12n
			},
			new IbanCountryFormat
			{
				CountryName = "Bulgaria",
				CountryIsoCode = "BG",
				IbanRegex = @"BG\d{2}[A-Z]{4}\d{6}[0-9A-Z]{8}"  // 4a,6n,8c
			},
			new IbanCountryFormat
			{
				CountryName = "Cyprus",
				CountryIsoCode = "CY",
				IbanRegex = @"CY\d{2}\d{8}[0-9A-Z]{16}"  // 8n,16c	
			},
			new IbanCountryFormat
			{
				CountryName = "Croatia",
				CountryIsoCode = "HR",
				IbanRegex = @"HR\d{2}\d{17}"  // 17n	
			},
			new IbanCountryFormat
			{
				CountryName = "Denmark",
				CountryIsoCode = "DK",
				IbanRegex = @"DK\d{2}\d{14}" // 14n
			},
			new IbanCountryFormat
			{
				CountryName = "Estonia",
				CountryIsoCode = "EE",
				IbanRegex = @"EE\d{2}\d{16}" // 16n
			},
			new IbanCountryFormat
			{
				CountryName = "Finland",
				CountryIsoCode = "FI",
				IbanRegex = @"FI\d{2}\d{14}" // 14n
			},
			new IbanCountryFormat
			{
				CountryName = "France",
				CountryIsoCode = "FR",
				IbanRegex = @"FR\d{2}\d{10}[0-9A-Z]{11}\d{2}" // 10n,11c,2n
			},
			new IbanCountryFormat
			{
				CountryName = "Germany",
				CountryIsoCode = "DE",
				IbanRegex = @"DE\d{2}\d{18}" // 18n
			},
			new IbanCountryFormat
			{
				CountryName = "Greece",
				CountryIsoCode = "GR",
				IbanRegex = @"GR\d{2}\d{7}[0-9A-Z]{16}" // 7n,16c
			},
			new IbanCountryFormat
			{
				CountryName = "Ireland",
				CountryIsoCode = "IE",
				IbanRegex = @"IE\d{2}[0-9A-Z]{4}\d{14}" // 4c,14n
			},
			new IbanCountryFormat
			{
				CountryName = "Italy",
				CountryIsoCode = "IT",
				IbanRegex = @"IT\d{2}[A-Z]\d{10}[0-9A-Z]{12}" // 1a,10n,12c
			},
			new IbanCountryFormat
			{
				CountryName = "Latvia",
				CountryIsoCode = "LV",
				IbanRegex = @"LV\d{2}[A-Z]{4}[0-9A-Z]{13}" // 4a,13c
			},
			new IbanCountryFormat
			{
				CountryName = "Lithuania",
				CountryIsoCode = "LT",
				IbanRegex = @"LT\d{2}\d{16}" // 16n
			},
			new IbanCountryFormat
			{
				CountryName = "Luxembourg",
				CountryIsoCode = "LU",
				IbanRegex = @"LU\d{2}\d{3}[0-9A-Z]{13}" // 3n,13c
			},
			new IbanCountryFormat
			{
				CountryName = "Malta",
				CountryIsoCode = "MT",
				IbanRegex = @"MT\d{2}[A-Z]{4}\d{5}[0-9A-Z]{18}" // 4a,5n,18c
			},
			new IbanCountryFormat
			{
				CountryName = "Netherlands",
				CountryIsoCode = "NL",
				IbanRegex = @"NL\d{2}[A-Z]{4}\d{10}" // 4a,10n
			},
			new IbanCountryFormat
			{
				CountryName = "Poland",
				CountryIsoCode = "PL",
				IbanRegex = @"PL\d{2}\d{24}" // 24n
			},
			new IbanCountryFormat
			{
				CountryName = "Portugal",
				CountryIsoCode = "PT",
				IbanRegex = @"PT\d{2}\d{21}" // 21n
			},
			new IbanCountryFormat
			{
				CountryName = "Czechia",
				CountryIsoCode = "CZ",
				IbanRegex = @"CZ\d{2}\d{20}" // 20n
			},
			new IbanCountryFormat
			{
				CountryName = "Romania",
				CountryIsoCode = "RO",
				IbanRegex = @"RO\d{2}[A-Z]{4}[0-9A-Z]{16}" // 4a,16c
			},
			new IbanCountryFormat
			{
				CountryName = "Slovakia",
				CountryIsoCode = "SK",
				IbanRegex = @"SK\d{2}\d{20}" // 20n
			},
			new IbanCountryFormat
			{
				CountryName = "Slovenia",
				CountryIsoCode = "SI",
				IbanRegex = @"SI\d{2}\d{15}" // 15n
			},
			new IbanCountryFormat
			{
				CountryName = "Spain",
				CountryIsoCode = "SP",
				IbanRegex = @"SP\d{2}\d{20}" // 20n
			},
			new IbanCountryFormat
			{
				CountryName = "Sweden",
				CountryIsoCode = "SE",
				IbanRegex = @"SE\d{2}\d{20}" // 20n
			},
			new IbanCountryFormat
			{
				CountryName = "Hungary",
				CountryIsoCode = "HU",
				IbanRegex = @"HU\d{2}\d{24}" // 24n
			},
			new IbanCountryFormat
			{
				CountryName = "United Kingdom of Great Britain and Northern Ireland (the)",
				CountryIsoCode = "GB",
				IbanRegex = @"GB\d{2}[A-Z]{4}\d{14}" // 4a,14n
			}
		};
	}
}
