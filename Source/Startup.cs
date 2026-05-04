using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using Verse;

namespace AteCorpseMood

{
    [StaticConstructorOnStartup]
    public static class Startup
    {
        public readonly static string MODID = "RightAir.AteCorpseMood";
        static Startup()
        {
            Log.Message($"[{MODID}] startup...");
            new Harmony(MODID).PatchAll(Assembly.GetExecutingAssembly());
        }
    }
}
