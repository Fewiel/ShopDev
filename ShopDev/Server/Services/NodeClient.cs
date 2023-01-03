using ShopDev.NodeModels;
using System.Net.Sockets;
using System.Text.Json;

namespace ShopDev.Server.Services;

public class NodeClient
{
	private TcpClient TcpClient;
    private StreamReader StreamReader;
    private StreamWriter StreamWriter;    
    private NodeHost NodeHost;
    public Guid NodeId { get; private set; }

	public NodeClient(TcpClient client, NodeHost host)
	{
        NodeHost = host;
		TcpClient = client;
        new Thread(ClientWorker).Start();
        var ns = TcpClient.GetStream();

        StreamReader = new StreamReader(ns);
        StreamWriter = new StreamWriter(ns);
    }

    private void ClientWorker()
	{
        while (TcpClient.Connected)
        {
            var msg = StreamReader.ReadLine();
            if (string.IsNullOrEmpty(msg))
                throw new InvalidOperationException();

            var request = JsonSerializer.Deserialize<NodeRequest>(msg);
            if (request == null)
                throw new InvalidOperationException();

            HandleRequest(request);
        }
    }

    private void HandleRequest(NodeRequest request)
    {
        if (request is InitialNodeRequest inr)
        {
            NodeId = inr.NodeId;
            NodeHost.OnNodeConnected(this);
        }
    }

    public void Disconnect()
    {
        TcpClient.Close();
        TcpClient.Dispose();
    }
}