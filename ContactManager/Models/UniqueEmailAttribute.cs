using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using ContactManager.Data; // Adjust namespace as per your DbContext
using ContactManager.Models;
using Microsoft.EntityFrameworkCore;

public class UniqueContactAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        var contact = (Contact)validationContext.ObjectInstance;
        var dbContext = (ApplicationDbContext)validationContext.GetService(typeof(ApplicationDbContext));

        if (dbContext.Contacts.Any(c => c.Id != contact.Id && (c.Name == contact.Name || c.Email == contact.Email)))
        {
            return new ValidationResult("The name or email already exists.");
        }

        return ValidationResult.Success;
    }
}