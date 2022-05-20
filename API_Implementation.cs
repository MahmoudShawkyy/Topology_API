using System;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
namespace API
{
    public class Component
    {
        public string type { get; set; }
        public string id { get; set; }
        public Resistance resistance { get; set; } 
        public Netlist netlist { get; set; }
        [JsonProperty("m(l)")] public ML ML { get; set; }
    }

    public class ML
    {
        public double @default { get; set; }
        public int min { get; set; }
        public int max { get; set; }
    }

    public class Netlist
    {
        public string t1 { get; set; }
        public string t2 { get; set; }
        public string drain { get; set; }
        public string gate { get; set; }
        public string source { get; set; }
    }

    public class Resistance
    {
        public int @default { get; set; }
        public int min { get; set; }
        public int max { get; set; }
    }

    public class Topology
    {
        public string id { get; set; }
        public List<Component> components { get; set; }
    }


    public class Request
    {

        public List<Topology> topologies = new List<Topology>();
        public Topology topology1;
        public JObject topologyObject;

        public void readJson(string file)
        {
            topologyObject = JObject.Parse(File.ReadAllText(@file));
            topology1 = JsonConvert.DeserializeObject<Topology>(topologyObject.ToString());
            topologies.Add(topology1);
        }

        public void writeJson(string jsonInmemory, string path)
        {
            File.WriteAllText(@path, jsonInmemory.ToString());
        }

        public void deleteTopology(Topology topologyID)
        {
            topologies.Remove(topologyID);
        }

        public List<Topology> queryTopologies()
        {
            return topologies;
        }

        public JObject queryDevices(Topology topologyID)
        {
            JObject tempObject = JObject.Parse(topologyID.ToString());
            return JObject.Parse(tempObject.Last.ToString());
        }

        public List<Component> test(Topology topologyID, Netlist netlistID)
        {
            List<Component> devices = new List<Component>();
            var result = from device in topologyID.components
                where device.netlist == netlistID
                select device;
            foreach (var device in result)
                devices.Add(device);
            return devices;

        }
    }
}
