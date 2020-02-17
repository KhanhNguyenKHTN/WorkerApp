using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace WorkerModel.Menu
{
    public class Food : BaseModel
    {
        public Food()
        {

        }

        [JsonProperty("name")]
        public string LabName { get; set; }
        public int Likes { get; set; }

        [JsonProperty("image")]
        public string ImagesSource { get; set; }

        [JsonProperty("cost")]
        public int RealCost { get; set; }

        public string Unit = "VND";

        public string Cost { get { return RealCost.ToString("C0").Remove(RealCost.ToString("C0").Length - 2, 2) + " " + Unit; } set { } } // temp for food menu
        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("dishCategory")]
        public Category Category { get; set; }

        private bool _IsSelected;
        public bool IsSelected { get => _IsSelected; set { _IsSelected = value; OnPropertyChanged(); } }


        private int _OrderQuantity = 1;
        public int OrderQuantity
        {
            get { return _OrderQuantity; }
            set { _OrderQuantity = value; OnPropertyChanged(); }
        }

        private int _Quantity;

        public int Quantity
        {
            get { return _Quantity; }
            set { _Quantity = value; OnPropertyChanged(); }
        }

        public int TotalQuantity { get { return OrderQuantity * RealCost; } }
    }
    public class Category
    {
        //"dishCategoryId": 3,
        //"code": "D-0003",
        //"name": "Món gỏi",
        //"description": "Món gỏi"

        [JsonProperty("dishCategoryId")]
        public int Id { get; set; }

        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

    }
}
