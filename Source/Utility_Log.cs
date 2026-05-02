
using Verse;
namespace AteCorpseMood
{
    public static class L
    {
        public static readonly string PREFIX = "[Rt's AteCorpseMood] ";
        public static void W(string txt) => Log.Warning(PREFIX + txt);
        public static void E(string txt) => Log.Error(PREFIX + txt);
        public static void M(string txt) => Log.Message(PREFIX + txt);
        public static void M(object obj) => L.M(obj.ToString());
    }
}
