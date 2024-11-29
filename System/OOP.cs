using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
namespace classes
{

    [XmlInclude(typeof(House))]
    [XmlInclude(typeof(OfficeBuilding))]
    public class Rations
    {
        [XmlElement("type")]
        public string Type { get; set; }
        
        [XmlElement("population")]
        public int Population { get; set; }

        [XmlElement("address")]
        public string Address { get; set; }
        
        [XmlElement("square")]
        public int Square { get; set; }
        
        public Rations() {} 

        public Rations(string address, int square)
        {
            Address = address;
            Square = square;
        }
    }

    public class House : Rations
    {
        private int roomCount;

        public House() {} 

        public House(string address, int square, int roomCount) : base(address, square)
        {
            Type = "House";
            this.roomCount = roomCount;
            CalculatePopulation();
        }

        private void CalculatePopulation()
        {
            Population = Convert.ToInt32(Square * roomCount * 1.3);
        }
    }

    public class OfficeBuilding : Rations
    {
        public OfficeBuilding() {} 

        public OfficeBuilding(string address, int square) : base(address, square)
        {
            Type = "Office";
            CalculatePopulation();
        }

        private void CalculatePopulation()
        {
            Population = Convert.ToInt32(Square * 0.2);
        }
    }

    public class ManagementCompany
    {
        public List<Rations> quarters = new List<Rations>();

        public int average
        {
            get
            {
                if (quarters.Count == 0)
                    return 0;
                    
                int result = 0;
                int i = 0;
                foreach (var n in quarters)
                {
                    result += n.Population;
                    i++;
                }

                return result/i;
            }

            set
            {
                average = value;
            }
        }
        
        public List<string> names{
            get
            {
                List<string> _names = new List<string>();
                foreach (var quater in quarters)
                {
                    _names.Add(quater.Address);
                }

                return _names;
            }
        }

        public void AddQuarters(Rations quarter)
        {
            quarters.Add(quarter);
            names.Add(quarter.Address);
        }

        public void SaveToXml(string filePath)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<Rations>));
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                serializer.Serialize(writer, quarters);
            }
        }
        
        public void LoadFromXml(string filePath)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<Rations>), new Type[] { typeof(House), typeof(OfficeBuilding) });
            using (StreamReader reader = new StreamReader(filePath))
            {
                List<Rations> loadedQuarters = (List<Rations>)serializer.Deserialize(reader);
                quarters = loadedQuarters;
            }
        }

        public void Sorted()
        {
            int quartersLength = quarters.Count;
            for (int i = 0; i < quartersLength - 1; i++)
            {
                for (int j = 0; j < quartersLength - i - 1; j++)
                {
                    if (quarters[j].Population < quarters[j + 1].Population)
                        (quarters[j], quarters[j + 1]) = (quarters[j + 1], quarters[j]);
                    else if (quarters[j].Population == quarters[j + 1].Population)
                    {
                        if (quarters[j] is OfficeBuilding && quarters[j + 1] is House)
                        {
                            (quarters[j], quarters[j + 1]) = (quarters[j + 1], quarters[j]);
                        }
                    }
                }
            }
        }

        public void Print3one()
        {
            int i = 0;
            while (i < 3 && i < quarters.Count)
            {
                Console.WriteLine($"{quarters[i].Address} \t {quarters[i].Population}");
                i++;
            }
        }
        
        public void Print4last()
        {
            int n = quarters.Count;
            int i = Math.Max(n - 4, 0);
            while (i < n)
            {
                Console.WriteLine($"{quarters[i].Address} \t {quarters[i].Population}");
                i++;
            }
        }

        public int CountM()
        {
            int s = 0;
            foreach (var i in quarters)
            {
                s++;
            }
            return s;
        }
        
    }
}