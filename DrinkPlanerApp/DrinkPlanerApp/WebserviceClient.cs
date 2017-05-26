using DrinkPlaner.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DrinkPlanerApp
{
    public class WebserviceClient
    {
        private HttpClient client;
        //TODO: IP/URL anpassen
        private readonly string serviceURL = @"http://169.254.80.80:49516/api/{0}/{1}";
        private Person selfPerson;

        //TODO: ID des "eigenen" Users eintragen
        public int SelfPeopleId { get { return 1; } }

        public Person SelfPerson
        {
            get
            {
                if (this.selfPerson == null)
                {
                    this.CacheSelfPerson();
                }
                return this.selfPerson;
            }
        }

        public WebserviceClient()
        {
            client = new HttpClient();
        }

        public async void CacheSelfPerson()
        {
            this.selfPerson = await CallWebservice<Person>("People", string.Empty + this.SelfPeopleId);
        }

        public async Task<List<Party>> LoadPartiesAsync()
        {
            return await CallWebservice<List<Party>>("Parties", "?forPersonId=" + SelfPeopleId);
        }

        internal async Task<List<Drink>> LoadDrinksAsync()
        {
            return await this.CallWebservice<List<Drink>>("drinks", string.Empty);
        }

        internal async Task<List<Person>> LoadPeopleAsync()
        {
            return await CallWebservice<List<Person>>("People", string.Empty);
        }

        internal async Task<Party> SavePartyAsync(Party party)
        {
            if (party.Id > 0)
                return await SaveObjectToWebservice("Parties", party, party.Id);
            else
                return await SaveObjectToWebservice("Parties", party);
        }

        internal async Task SaveSuppliesAsync(ICollection<Supply> supplies, Party party)
        {
            List<Task> tasks = new List<Task>();
            foreach (var supply in supplies)
            {
                supply.DrinkId = supply.Drink.Id;
                if (supply.Id > 0)
                    tasks.Add(this.SaveObjectToWebservice("supplies", supply, supply.Id));
                else
                    tasks.Add(this.SaveObjectToWebservice("supplies", supply, parameters: "party?forPartyId=" + party.Id));
            }
            await Task.WhenAll(tasks.ToArray());
        }

        internal async Task SaveSuppliesAsync(ICollection<Supply> supplies, Participation participation)
        {
            List<Task> tasks = new List<Task>();
            foreach (var supply in supplies)
            {
                supply.DrinkId = supply.Drink.Id;
                if (supply.Id > 0)
                    tasks.Add(this.SaveObjectToWebservice("supplies", supply, supply.Id));
                else
                    tasks.Add(this.SaveObjectToWebservice("supplies", supply, parameters: "participation?forParticipationId=" + participation.Id));
            }
            await Task.WhenAll(tasks.ToArray());
        }

        internal async Task DeleteSupplyAsync(Supply supply)
        {
            if (supply.Id > 0)
            {
                await this.DeleteObjectOnWebservice("supplies", supply.Id);
            }
        }

        internal async Task SaveParticipationsAsync(ICollection<Participation> participations, Party party)
        {
            List<Task> tasks = new List<Task>();
            foreach (var participation in participations)
            {
                if (participation.Id > 0)
                    tasks.Add(this.SaveObjectToWebservice("participations", participation, participation.Id));
                else
                    tasks.Add(this.SaveObjectToWebservice("participations", participation));
            }
            foreach (var participation in party.Guests)
            {
                if (!participations.Contains(participation))
                    tasks.Add(this.DeleteObjectOnWebservice("participations", participation.Id));
            }
            await Task.WhenAll(tasks.ToArray());
        }

        private async Task<bool> DeleteObjectOnWebservice(string controller, int id)
        {
            var uri = new Uri(string.Format(serviceURL, controller, id));
            var response = await this.client.DeleteAsync(uri);
            if (!response.IsSuccessStatusCode)
            {
                Debug.WriteLine("Errorcode: " + response.StatusCode);
                return false;
            }
            else
            {
                return true;
            }
        }

        private async Task<T> SaveObjectToWebservice<T>(string controller, T entity, int id = -1, string parameters = "") where T : class
        {
            try
            {
                var json = JsonConvert.SerializeObject(
                    entity,
                    new JsonSerializerSettings()
                    {
                        ContractResolver = new NoReferencesContractResolver()
                    });

                var content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response;

                if (id == -1)
                {
                    var uri = new Uri(string.Format(serviceURL, controller, parameters));
                    response = await this.client.PostAsync(uri, content);
                }
                else
                {
                    var uri = new Uri(string.Format(serviceURL, controller, id + parameters));
                    response = await this.client.PutAsync(uri, content);
                }

                if (!response.IsSuccessStatusCode)
                {
                    Debug.WriteLine("Errorcode: " + response.StatusCode);
                    return null;
                }
                else
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<T>(responseContent);
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);
                return null;
            }
        }

        private async Task<T> CallWebservice<T>(string controller, string parameter) where T : class
        {
            Task<HttpResponseMessage> responsTask;
            try
            {
                var uri = new Uri(string.Format(serviceURL, controller, parameter));

                responsTask = client.GetAsync(uri);
                var response = await responsTask;
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<T>(content);
                }
                Debug.WriteLine("Errorcode: " + response.StatusCode);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
            return null;
        }
    }
}
