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

        public void CheckEmail(int customerID, Repository<Customer> data, int duplication)
        {
            Customer entity = data.Get(customerID);
            if(duplication == 0) 
            { 
                IsValid = (entity == null) ? true : false;
               
            }
            else 
            {
                IsValid = true ;

            }

            ErrorMessage = (IsValid) ? "" :
                 $"Customer email : {entity.Email} is already in the database.";

        }
        public void MarkEmailChecked() => tempData[EmailKey] = true;
        public void ClearEmail() => tempData.Remove(EmailKey);
        public bool IsEmailChecked => tempData.Keys.Contains(EmailKey);



    }
}
