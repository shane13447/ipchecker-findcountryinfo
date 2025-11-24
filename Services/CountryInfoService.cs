namespace ipchecker_findcountryinfo.Services;

public class CountryInfoService
{
    public Dictionary<string, string> GetCountryInfo(string items)
    {
        if (string.IsNullOrWhiteSpace(items))
        {
            return new Dictionary<string, string>();
        }
        // Split by comma and process each IP
        string[] ipArray = items.Split(',')
            .Select(ip => ip.Trim())
            .Where(ip => !string.IsNullOrEmpty(ip))
            .ToArray();

        var countryInfo = new Dictionary<string, string>();

        foreach (string ip in ipArray)
        {
            string country = FindCountryInfo(ip);
            countryInfo[ip] = country;
        }

        return countryInfo;
    }
    private string FindCountryInfo(string ip)
    {
        if (string.IsNullOrWhiteSpace(ip))
        {
            return "Unknown";
        }
        // Extract the first group
        int firstDotIndex = ip.IndexOf('.');
        if (firstDotIndex <= 0)
        {
            return "Unknown";
        }
        string firstGroup = ip.Substring(0, firstDotIndex);
        // Check first 3 digits if 100 = US, 101 = UK, 102 = China
        if (firstGroup.StartsWith("100"))
        {
            return "US";
        }
        else if (firstGroup.StartsWith("101"))
        {
            return "UK";
        }
        else if (firstGroup.StartsWith("102"))
        {
            return "China";
        }
        return "Unknown";
    }
}

