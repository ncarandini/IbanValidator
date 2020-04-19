using System;
using System.Collections.Generic;
using System.Text;

namespace TPCWare.IbanValidator
{
	public class IbanCountryFormat
	{
		public string CountryName { get; set; }

		public string CountryIsoCode { get; set; }

		public string IbanRegex { get; set; }
	}
}
