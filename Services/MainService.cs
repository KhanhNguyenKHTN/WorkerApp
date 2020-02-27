using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WorkerModel.Order;

namespace Services
{
    class SendObject
    {
        [JsonProperty("id")]
        public long Id { get; set; }
        [JsonProperty("status")]
        public string Status { get; set; }
    }
    class JsonDetach
    {
        [JsonProperty("message")]
        public string Message { get; set; }
        [JsonProperty("data")]
        public ObservableCollection<OrderDetail> Data { get; set; }
    }
    public class MainService
    {
        private HttpClient client;

        public MainService()
        {
            client = new HttpClient();
        }

        public async Task<ObservableCollection<OrderDetail>> GetListPicAndStatus(Pic cus, string status)
        {
            try
            {
                string url = Global.GlobalInfo.URL + @"/order-detail/pic/" + cus.EmployeeId + @"/type/" + status;

                var response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    //JsonDetach det = new JsonDetach();
                    var Items = JsonConvert.DeserializeObject<JsonDetach>(content);

                    return Items.Data;
                }
                else
                {
                    return new ObservableCollection<OrderDetail>();
                }
            }
            catch (Exception ex)
            {
                return new ObservableCollection<OrderDetail>();
            }
        }

        public async Task ChangeStatus(OrderDetail e, string v)
        {
            try
            {
                string url = Global.GlobalInfo.URL + @"/order-detail/status/id/" + e.OrderDetailId + @"?status=" + v;
                var json = JsonConvert.SerializeObject(new SendObject() { Id =e.OrderDetailId, Status = v });
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PutAsync(url,content);

                if (response.IsSuccessStatusCode)
                {

                }
                else
                {
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}
