   Request request = new Request();
     request.readJson(filePath);
     request.writeJson();
     request.deleteTopology(topologyID);
     request.queryTopologies();
     request.queryDevices(topologyID);
     request.queryDevicesWithNetlistNode(topologyID, netlistID);
