using System.Text;

namespace IISExpressMagic
{
    public enum TraceLevel
    {
        None,
        Info,
        Warning,
        Error
    }

    public class Arguments
    {
        private Arguments()
        {
        }

        public string Config { get; private set; }
        public TraceLevel Trace { get; private set; }
        public string Site { get; private set; }
        public int? SiteId { get; private set; }
        public bool SysTray { get; private set; }

        public static Arguments UseConfigFile(string configFile, string siteName, bool sysTray = true, TraceLevel traceLevel = TraceLevel.None)
        {
            return new Arguments
            {
                Config = configFile,
                Site = siteName,
                SysTray = sysTray,
                Trace = traceLevel
            };
        }

        public static Arguments UseConfigFile(string configFile, int siteId, bool sysTray = true, TraceLevel traceLevel = TraceLevel.None)
        {
            return new Arguments
            {
                Config = configFile,
                SiteId = siteId,
                SysTray = sysTray,
                Trace = traceLevel
            };
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            if (!string.IsNullOrEmpty(Config))
                sb.Append($" /config:{Config}");

            if (Trace != TraceLevel.None)
                sb.Append($" /trace:{Trace.ToString().ToLower()}");

            if (!string.IsNullOrEmpty(Site))
                sb.Append($" /site:{Site}");

            if (SiteId.HasValue)
                sb.Append($" /siteid:{SiteId}");

            if (SysTray)
                sb.Append(" /systray");

            return sb.ToString().Trim();
        }
    }
}