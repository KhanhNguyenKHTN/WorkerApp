using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using WorkerModel.Menu;

namespace WorkerModel.Order
{
    public class Role : BaseModel
    {
        private string _Code;

        [JsonProperty("code")]
        public string Code { get => _Code; set { _Code = value; OnPropertyChanged(); } }


        private string _Description;
        [JsonProperty("description")]
        public string Description { get => _Description; set { _Description = value; OnPropertyChanged(); } }

        private string _Name;
        [JsonProperty("name")]
        public string Name { get => _Name; set { _Name = value; OnPropertyChanged(); } }

        private int _RoleId;
        [JsonProperty("roleId")]
        public int RoleId { get => _RoleId; set { _RoleId = value; OnPropertyChanged(); } }

    }

    public class UserInfo : BaseModel
    {
        private string _Address;
        [JsonProperty("address")]
        public string Address { get => _Address; set { _Address = value; OnPropertyChanged(); } }

        private string _AvatarUrl;
        [JsonProperty("avatarUrl")]
        public string AvatarUrl { get => _AvatarUrl; set { _AvatarUrl = value; OnPropertyChanged(); } }

        private string _DisplayName;
        [JsonProperty("displayName")]
        public string DisplayName { get => _DisplayName; set { _DisplayName = value; OnPropertyChanged(); } }


        //private DateTime _Dob;
        //[JsonProperty("dob")]
        //public DateTime Dob { get => _Dob; set { _Dob = value; OnPropertyChanged(); } }

        private string _Mail;
        [JsonProperty("email")]
        public string Mail { get => _Mail; set { _Mail = value; OnPropertyChanged(); } }

        private string _Phone;
        [JsonProperty("phone")]
        public string Phone { get => _Phone; set { _Phone = value; OnPropertyChanged(); } }


        private string _Status;
        [JsonProperty("status")]
        public string Status { get => _Status; set { _Status = value; OnPropertyChanged(); } }


        private int _UserId;
        [JsonProperty("userId")]
        public int UserId { get => _UserId; set { _UserId = value; OnPropertyChanged(); } }

    }

    public class Pic : BaseModel
    {
        private string _Code;
        [JsonProperty("code")]
        public string Code { get => _Code; set { _Code = value; OnPropertyChanged(); } }

        private string _Description;
        [JsonProperty("desciption")]
        public string Description { get => _Description; set { _Description = value; OnPropertyChanged(); } }


        private int _EmployeeId;
        [JsonProperty("employeeId")]
        public int EmployeeId { get => _EmployeeId; set { _EmployeeId = value; OnPropertyChanged(); } }

        private string _PassWord;
        [JsonProperty("password")]
        public string PassWord { get => _PassWord; set { _PassWord = value; OnPropertyChanged(); } }

        private Role _Role;
        [JsonProperty("role")]
        public Role Role { get => _Role; set { _Role = value; OnPropertyChanged(); } }

        private string _Status;
        public string Status { get => _Status; set { _Status = value; OnPropertyChanged(); } }

        private UserInfo _UserInfo;
        [JsonProperty("userInfo")]
        public UserInfo UserInfo { get => _UserInfo; set { _UserInfo = value; OnPropertyChanged(); } }


        private string _UserName;
        [JsonProperty("userName")]
        public string UserName { get => _UserName; set { _UserName = value; OnPropertyChanged(); } }

    }

    public class OrderDetail : BaseModel
    {
        private string _Description;
        [JsonProperty("description")]
        public string Description { get => _Description; set { _Description = value; OnPropertyChanged(); OnPropertyChanged("IsHasDescription"); } }

        private Food _Dish;
        [JsonProperty("dish")]
        public Food Dish { get => _Dish; set { _Dish = value; OnPropertyChanged(); } }

        private int _OrderDetailId;
        [JsonProperty("orderDetailId")]
        public int OrderDetailId { get => _OrderDetailId; set { _OrderDetailId = value; OnPropertyChanged(); } }

        private Pic _Pic;
        [JsonProperty("pic")]
        public Pic Pic { get => _Pic; set { _Pic = value; OnPropertyChanged(); } }

        private int _Quantity;
        [JsonProperty("quantity")]
        public int Quantity { get => _Quantity; set { _Quantity = value; OnPropertyChanged(); } }

        private string _Status;
        [JsonProperty("status")]
        public string Status { get => _Status; set { _Status = value; OnPropertyChanged(); } }

        private string _Note;
        [JsonProperty("note")]
        public string Note { get => _Note; set { _Note = value; OnPropertyChanged(); } }

        public bool IsHasDescription { get { return !string.IsNullOrEmpty(Description); } }


    }

    public class Customer : BaseModel
    {
        private string _Password;
        [JsonProperty("password")]
        public string Password { get => _Password; set { _Password = value; OnPropertyChanged(); } }

        private UserInfo _Info;
        [JsonProperty("userInfo")]
        public UserInfo Info { get => _Info; set { _Info = value; OnPropertyChanged(); } }

        private string _UserName;
        [JsonProperty("userName")]
        public string UserName { get => _UserName; set { _UserName = value; OnPropertyChanged(); } }
    }
    public class Order : BaseModel
    {
        private string _Code;
        [JsonProperty("code")]
        public string Code { get => _Code; set { _Code = value; OnPropertyChanged(); } }

        private Customer _Customer;
        [JsonProperty("customer")]
        public Customer Customer { get => _Customer; set { _Customer = value; OnPropertyChanged(); } }

        private string _DateCheckin;
        [JsonProperty("dateCheckin")]
        public string DateCheckin { get => _DateCheckin; set { _DateCheckin = value; OnPropertyChanged(); } }

        private DateTime _DateTimeCheckin;
        public DateTime DateTimeCheckin
        {
            get => _DateTimeCheckin; set
            {
                _DateTimeCheckin = value; DateCheckin = value.ToString("yyyy-MM-dd HH:mm:ss");
                timeCheckin = value.ToString("yyyy-MM-dd HH:mm:ss"); OnPropertyChanged();
            }
        }



        public string dateCheckin { get; set; }
        public string description { get; set; }
        public string name { get; set; }
        public List<OrderDetail> orderDetail { get; set; }

        public int orderId { get; set; }
        public int price { get; set; }
        public int quantity { get; set; }
        public string status { get; set; }
        //public List<Table> tables { get; set; }
        public string timeCheckin { get; set; }
        public string type { get; set; }
    }
}
