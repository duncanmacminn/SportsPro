using Microsoft.AspNetCore.Mvc.ViewFeatures;
using SportsPro.Models;

namespace SportsPro.DataLayer

{
    public class Validate
    {
        private const string CustomerKey = "validCustomer";
        private const string EmailKey = "validEmail";
        

        private ITempDataDictionary tempData { get; set; }
        public Validate(ITempDataDictionary temp) => tempData = temp;

        public bool IsValid { get; private set; }
        public string ErrorMessage { get; private set; }

        public void CheckEmail(string email, Repository<Customer> data, int duplication)
        {
            QueryOptions<Customer> emailCheck = new QueryOptions<Customer>
            {
                WhereClauses = new WhereClauses<Customer>
                {
                    {c => c.Email == email }
                }
            };
            Customer entity = data.Get(emailCheck);
            if (duplication == 0)
            {
                IsValid = (entity == null) ? true : false;
            }
            else
            {
                IsValid = true;
            }

            //foreach (Customer cust in data.List(new QueryOptions<Customer>()))
            //{
            //    if (cust.Email == email && duplication == 0)
            //    {
            //            IsValid = false;
            //    }
            //    IsValid = true;
            //}
            ErrorMessage = (IsValid) ? "" : $"Customer email : {email} is already in the database.";
        }
        public void MarkEmailChecked() => tempData[EmailKey] = true;
        public void ClearEmail() => tempData.Remove(EmailKey);
        public bool IsEmailChecked => tempData.Keys.Contains(EmailKey);



    }
}
