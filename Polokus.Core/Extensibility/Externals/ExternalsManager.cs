﻿using Polokus.Core.Extensibility.Externals.Models;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text.Json;

namespace Polokus.Core.Extensibility.Externals
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
            var options = new JsonSerializerOptions();

            return JsonSerializer.Deserialize<Externals>(jsonString, options)
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

        public static ISettingsProvider InstantiateSettingsProvider(ExternalSettingsProvider externalSettingsProvider)
        {
            return Instantiate<ISettingsProvider>(externalSettingsProvider.Assembly, externalSettingsProvider.ClassName);
        }

        public static IHooksProvider InstantiateHooksProvider(ExternalHooksProvider externalHooksProvider)
        {
            return Instantiate<IHooksProvider>(externalHooksProvider.Assembly, externalHooksProvider.ClassName);
        }

        public static IMonitor InstantiateMonitor(IPolokusMaster master, ExternalMonitor externalMonitor)
        {
            foreach (var dependency in externalMonitor.Dependencies)
            {
                Assembly.LoadFrom(dependency);
            }

            object?[]? args = externalMonitor.Arguments.ToList<object>().Prepend(master).ToArray();
            return Instantiate<IMonitor>(externalMonitor.Assembly, externalMonitor.ClassName, args);
        }

        private static T Instantiate<T>(string assemblyPath, string className, object?[]? arguments = null)
        {
            Type type = GetType(assemblyPath, className);

            object? obj = Activator.CreateInstance(type, arguments);
            if (obj == null)
            {
                throw new Exception("Failed to instantiate object.");
            }
            if (obj is T t)
            {
                return t;
            }
            else
            {
                throw new Exception($"Unable to cast object of type {obj?.GetType().FullName} to {typeof(T).FullName}");
            }
        }

        public static Type GetType(string assemblyPath, string className)
        {
            if (assemblyPath.StartsWith("./"))
            {
                assemblyPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, assemblyPath);
            }

            var asm = Assembly.LoadFile(assemblyPath);
            var type = asm.GetType(className);
            if (type == null)
            {
                throw new Exception($"Cannot find type {className} in assembly: {assemblyPath}");
            }

            return type;
        }


    }
}
