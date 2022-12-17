using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Polokus.Core.Externals
{
    public class ExternalsManager
    {
        public Externals LoadExternalsFromFile(string jsonPath)
        {
            if (File.Exists(jsonPath))
            {
                string jsonString = File.ReadAllText(jsonPath);
                return LoadExternals(jsonString);
            }

            throw new FileNotFoundException(jsonPath);
        }

        public Externals LoadExternals(string jsonString)
        {
            return JsonSerializer.Deserialize<Externals>(jsonString)
                ?? throw new Exception("Failed to load externals.");
        }
    }
}
