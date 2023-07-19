using Common.Autofac;
using OpenSteamworks;

namespace Common.Managers;

public class AppsManager : IHasStartupTasks
{
    private SteamClient steamClient;
    public AppsManager(SteamClient client) {
        steamClient = client;
    }
    public void RunStartup()
    {
        Console.WriteLine("AppsManager startup");
        //steamClient.LogClientState();
    }
}