using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;

public class ActivitiesAttribute : ResultFilterAttribute  
    {  
        private string _description;  
  
        public ActivitiesAttribute(string description)  
        {  
            _description = description;  
        }  
  
        public override Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)  
        {  
            Debug.WriteLine($"===User : {context.HttpContext.User.Identity.Name}, description : {_description}, Date {DateTime.Now.ToString()}");  
            return base.OnResultExecutionAsync(context, next);  
        }  
    }