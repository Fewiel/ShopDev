using ShopDev.DAL.Repositories;
using System.Net;
using System.Net.Sockets;

namespace ShopDev.Server.Services;

public class NodeHost : IHostedService
{
    private bool Running;
    private int Port;
    private SettingRepository _setting;
    private Dictionary<Guid, NodeClient> _nodes = new();
    public IReadOnlyDictionary<Guid, NodeClient> Nodes => _nodes;

    public NodeHost(SettingRepository setting)
    {
        _setting = setting;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        Running = true;

        var port = await _setting.GetByKeyAsync("node_server_port");

        if (port == null || port.Value == null)
            return;

        Port = int.Parse(port.Value);

        new Thread(Worker).Start();        
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        Running = false;
        return Task.CompletedTask;
    }

    private void Worker()
    {
        var server = new TcpListener(IPAddress.Any, Port);

        while (Running)
        {
            TcpClient client = server.AcceptTcpClient();
            NetworkStream ns = client.GetStream();

            Thread.Sleep(100);
        }
    }

    public void OnNodeConnected(NodeClient nodeClient)
    {
        
    }
}