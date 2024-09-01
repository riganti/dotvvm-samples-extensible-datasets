using DotVVM.Framework.Configuration;

public class DotvvmStartup : IDotvvmStartup, IDotvvmServiceConfigurator
{
    public void Configure(DotvvmConfiguration config, string applicationPath)
    {
        config.RouteTable.Add("Default", "", "Views/Default.dothtml");
        config.RouteTable.Add("DefaultStaticCommands", "static-commands", "Views/DefaultStaticCommands.dothtml");
        config.RouteTable.Add("NextToken", "next-token", "Views/NextToken.dothtml");
        config.RouteTable.Add("NextTokenHistory", "next-token-history", "Views/NextTokenHistory.dothtml");
        config.RouteTable.Add("MultiSort", "multi-sort", "Views/MultiSort.dothtml");
        config.RouteTable.Add("AppendablePager", "appendable-pager", "Views/AppendablePager.dothtml");
    }

    public void ConfigureServices(IDotvvmServiceCollection services)
    {
        services.AddDefaultTempStorages("temp");
        services.AddHotReload();
    }
}