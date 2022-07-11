package hk.Util.Network;


public interface NetworkInterface {
	void didCompleteNetworkJob(NetworkJob networkJob);
	void didFailNetworkJob(NetworkJob networkJob);
}
