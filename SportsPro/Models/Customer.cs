﻿using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace SportsPro.Models
{
    public class Customer
    {
		public int CustomerID { get; set; }

		[Required(ErrorMessage ="First Name Required.")]
		[StringLength(51, ErrorMessage = "Must be 51 characters or less")]
		public string FirstName { get; set; }

		[Required(ErrorMessage = "Last Name Required.")]
		[StringLength(51, ErrorMessage = "Must be 51 characters or less")]
		public string LastName { get; set; }

		[Required(ErrorMessage = "Address Required.")]
		[StringLength(51, ErrorMessage = "Must be 51 characters or less")]
		public string Address { get; set; }

		[Required(ErrorMessage = "City Required.")]
		[StringLength(51, ErrorMessage = "Must be 51 characters or less")]
		public string City { get; set; }

		[Required(ErrorMessage = "State/Province Required.")]
		[StringLength(51, ErrorMessage = "Must be 51 characters or less")]
		public string State { get; set; }

		[Required(ErrorMessage = "Postal Code Required.")]
		[StringLength(21, ErrorMessage = "Must be 51 characters or less")]
		public string PostalCode { get; set; }

		[Required(ErrorMessage = "Country Required.")]
		public string CountryID { get; set; }
		public Country Country { get; set; }

		[RegularExpression(@"^\(([0-9]{3})\)[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Phone number must be in (999) 999-9999 format.")]
		public string Phone { get; set; }

		[Required(ErrorMessage = "Email Required.")]
		[StringLength(51, ErrorMessage = "Must be 51 characters or less")]
		[DataType(DataType.EmailAddress)]
		[Remote("CheckEmail","Customer")]
		public string Email { get; set; }

		public string FullName => FirstName + " " + LastName;   // read-only property
	}
}