package hk.Util.Network;

import java.util.concurrent.Executor;
import java.util.concurrent.Executors;


public class NetworkManager {
	public static NetworkManager networkManager;
	private Executor networkExecutor;
	private int THREADPOOL_COUNT = 10;
	
	public static NetworkManager getInstance(){
		if(networkManager == null){
			networkManager = new NetworkManager();
		}
		return networkManager;
	}
	
	public NetworkManager(){
		networkExecutor = Executors.newFixedThreadPool(THREADPOOL_COUNT); 
	}
	
	
	public void addJob(NetworkJob networkJob){
		NetworkWorker networkWorker = new NetworkWorker(networkJob);
		networkExecutor.execute(networkWorker);
	}
}
