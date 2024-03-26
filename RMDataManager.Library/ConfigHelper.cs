using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Protocols.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMDataManager.Library
{
    public class ConfigHelper : IConfigHelper
    {
        private readonly IConfiguration config;
        public ConfigHelper(IConfiguration config)
        {
            this.config = config;
        }

        // TODO: Move this from config to the API, using database for tax rates
        public decimal GetTaxRate()
        {
            string rateText = config["TaxRate"];

            bool IsValidTaxRate = Decimal.TryParse(rateText, out decimal output);

            if (IsValidTaxRate == false)
            {
                throw new InvalidConfigurationException("Tax rate is not set up properly");
            }
            return output;
        }
    }
}
