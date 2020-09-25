using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SportsPro.Models
{
    public class RegistrationViewModel
    {
		public List<Customer> Customers { get; set; }

		public Customer ActiveCustomer { get; set; }

		public List<Registration> Registrations { get; set; }

		public List<Product> Products { get; set; }
		
		public int CustomerID { get; set; }

		public int ProductID { get; set; }


	}
}