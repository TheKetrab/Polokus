using Polokus.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Polokus.Core.Externals
{
    public static class ExternalsManager
    {
        public static Externals LoadExternalsFromFile(string jsonPath)
        {
            if (File.Exists(jsonPath))
            {
                string jsonString = File.ReadAllText(jsonPath);
                return LoadExternals(jsonString);
            }

            throw new FileNotFoundException(jsonPath);
        }

        public static Externals LoadExternals(string jsonString)
        {
            return JsonSerializer.Deserialize<Externals>(jsonString)
                ?? throw new SerializationException("Failed to load externals.");
        }

        public static Externals? TryLoadExternals(string jsonPath)
        {
            try
            {
                Externals externals = LoadExternalsFromFile(jsonPath);
                return externals;
            }
            catch (FileNotFoundException)
            {
                return null;
            }
            catch (SerializationException)
            {
                return null;
            }
        }

        public static IHooksProvider InstantiateHooksProvider(Externals externals)
        {
            if (externals.HooksProvider == null)
            {
                throw new Exception("Cannot instantiate hooks provider because external definitions are empty.");
            }

            var asm = Assembly.LoadFile(externals.HooksProvider.Assembly);
            var type = asm.GetType(externals.HooksProvider.ClassName);
            if (type == null)
            {
                throw new Exception("Cannot find type in assembly.");
            }

            return Activator.CreateInstance(type) as IHooksProvider
                ?? throw new Exception("Unable to instantiate settings provider.");
        }

        public static ISettingsProvider InstantiateSettingsProvider(Externals externals)
        {
            if (externals.SettingsProvider == null)
            {
                throw new Exception("Cannot instantiate settings provider because external definitions are empty.");
            }

            var asm = Assembly.LoadFile(externals.SettingsProvider.Assembly);
            var type = asm.GetType(externals.SettingsProvider.ClassName);
            if (type == null)
            {
                throw new Exception("Cannot find type in assembly.");
            }

            return Activator.CreateInstance(type) as ISettingsProvider
                ?? throw new Exception("Unable to instantiate settings provider.");
        }
    }
}
