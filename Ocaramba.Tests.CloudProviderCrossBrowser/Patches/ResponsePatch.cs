// <copyright file="ResponsePatch.cs" company="Accenture">
// Copyright (c) Objectivity Bespoke Software Specialists. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using HarmonyLib;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;

namespace Ocaramba.Tests.CloudProviderCrossBrowser.Patches
{
    /// <summary>
    /// Patch for OpenQA.Selenium.Response to handle BrowserStack specific behavior.
    /// </summary>
    [HarmonyPatch(typeof(Response), "Value", MethodType.Getter)]
    public static class ResponsePatch
    {
        private static readonly Dictionary<string, Type> TypeMap = new Dictionary<string, Type>
        {
            { "response", typeof(Response) },
            { "session", typeof(Response) },
            { "script", typeof(object) },
            { "screenshot", typeof(string) }
        };

        /// <summary>
        /// Postfix patch to modify Response.Value behavior for BrowserStack integration.
        /// </summary>
        /// <param name="__instance">The Response instance being patched.</param>
        public static void Postfix(Response __instance)
        {
            if (__instance?.Value == null || !(__instance is Response response))
            {
                return;
            }

            // Fix BrowserStack Response value mapping
            if (response.Value is Dictionary<string, object> responseDict)
            {
                foreach (var kvp in TypeMap)
                {
                    if (responseDict.ContainsKey(kvp.Key) && responseDict[kvp.Key] != null)
                    {
                        if (kvp.Value == typeof(Response) && responseDict[kvp.Key] is Dictionary<string, object> nestedDict)
                        {
                            typeof(Response).GetProperty("Value").SetValue(response, nestedDict);
                            return;
                        }
                    }
                }
            }
        }
    }
}
