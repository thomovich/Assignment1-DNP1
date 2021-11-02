using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Assignment1_DNP1.Models;
using FileData;
using Microsoft.AspNetCore.Components;
using Models;

namespace Assignment1_DNP1.Data
{
    public class WebsiteData : IAdultdata
    {
        private IList<Adult> adults;
        private FileContext _fileContext;
        public string AdultFile = "adults.json";

        public WebsiteData() {
            if (!File.Exists(AdultFile))
            {
                
                WriteAdultFile();
            }
            else
            {
                string content = File.ReadAllText(AdultFile);
                adults = JsonSerializer.Deserialize<List<Adult>>(content);
            }
            
        }


        public IList<Adult> GetAdults()
        {
            List<Adult> tmp = new List<Adult>(adults);
            return tmp;
        }

        public async Task<Adult> AddAdult(Adult adult)
        {
            adults.Add(adult);
            WriteAdultFile();
        }

        

        public async Task<Adult> RemoveAdult(int adultId)
        {
            Adult toRemove = adults.First(t => t.Id == adultId);
            adults.Remove(toRemove);
            WriteAdultFile();
            return adults;
        }

        private void WriteAdultFile()
        {
            string todoAsJson = JsonSerializer.Serialize(adults);
            File.WriteAllText(AdultFile, todoAsJson);
        }

        private void WriteAdultsToFile()
        {
            string adultasJson = JsonSerializer.Serialize(adults);
            File.WriteAllText(AdultFile, adultasJson);
            _fileContext.SaveChanges();
        }

        

        public async Task<Adult?> Get(int id)
        {
            return adults.FirstOrDefault(t => t.Id == id);
        }

        public Task<IList<Adult>> GetAdultsAsync()
        {
            throw new System.NotImplementedException();
        }

        public async Update(Adult adult)
        {
            Adult toUpdate = adults.First(t => t.Id == adult.Id);
            toUpdate.FirstName = adult.FirstName;
            WriteAdultFile();
            
        }
        
        
    }
}