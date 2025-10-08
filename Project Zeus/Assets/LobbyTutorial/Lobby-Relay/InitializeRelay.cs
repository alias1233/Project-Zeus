using UnityEngine;
using Unity.Services.Relay;
using Unity.Services.Relay.Models;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using Unity.Networking.Transport.Relay;
using System.Threading.Tasks;

public class InitializeRelay : MonoBehaviour
{
    public static InitializeRelay Instance { get; private set; }

    [SerializeField]
    LobbyUI lobbyUI;

    private void Awake()
    {
        Instance = this;
    }

    /*
     *  Gets all available regions for Relay servers, and sends the data over to the lobby to allow players to choose the region of the server.
     */
    public async void GetRegions()
    {
        var regionsTask = await RelayService.Instance.ListRegionsAsync();

        lobbyUI.InitRegions(regionsTask);
    }

    /*
     *  Creates Relay server with parameter region as the region of the server.
     */
    public async Task<string> CreateRelay(string region)
    {
        try
        {
            Allocation allocation = await RelayService.Instance.CreateAllocationAsync(6, region);
            string joincode = await RelayService.Instance.GetJoinCodeAsync(allocation.AllocationId);

            UnityTransport unityTransport = NetworkManager.Singleton.GetComponent<UnityTransport>();
            RelayServerData serverData = AllocationUtils.ToRelayServerData(allocation, "wss");
            unityTransport.SetRelayServerData(serverData);
            unityTransport.UseWebSockets = true;

            NetworkManager.Singleton.StartHost();
            return joincode;
        }

        catch (RelayServiceException e)
        {
            Debug.Log(e);
            return null;
        }
    }

    /*
     *  Attempts to join Relay server with the given joincode.
     */
    public async void joinrelay(string joincode)
    {
        try
        {
            JoinAllocation joinAllocation = await RelayService.Instance.JoinAllocationAsync(joincode);
            var unityTransport = NetworkManager.Singleton.GetComponent<UnityTransport>();

            RelayServerData serverData = AllocationUtils.ToRelayServerData(joinAllocation, "wss");
            unityTransport.SetRelayServerData(serverData);
            unityTransport.UseWebSockets = true;

            NetworkManager.Singleton.StartClient();
        }

        catch (RelayServiceException e)
        {
            Debug.Log(e);
        }
    }
}